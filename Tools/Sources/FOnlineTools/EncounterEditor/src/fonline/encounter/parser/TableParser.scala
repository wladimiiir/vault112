package fonline.encounter.parser

import fonline.encounter.entity._
import util.matching.Regex
import fonline.encounter.logic.WorldMapScriptReader
import collection.mutable.ListBuffer
import fonline.parser.{TextParser, LocationParser}

/**
 * User: mikewall
 * Date: 10/24/12
 * Time: 8:59 PM
 */
trait TableParser extends LocationParser with TextParser with GroupParser {
  private val tableDefineRegex = new Regex("#define\\s*TABLE_([0-9a-zA-Z_]*)", "tableName")
  private val tableRegex = new Regex(
    "@table\\s*=\\s*@EncounterTables\\[\\s*TABLE_([\\w_\\d]*)\\s*\\];\\s*" +
            "((?:table.AddLocationPid.*;\\s*)+)" +
            "((?:table.AddEncounter.*;\\s*)*)",

    "tableName", "locations", "encounters"
  )
  private val locationRegex = new Regex(
    "table.AddLocationPid\\(\\s*LOCATION_([\\w\\d_]+)\\s*\\);",

    "locationName"
  )
  private val encounterRegex = new Regex(
    "table.AddEncounter\\(\\s*(\\d+)\\s*,\\s*(\\d+)\\s*\\)" +
            "([^;]+)",

    "chance", "textNum", "properties"
  )
  private val groupRegex = new Regex(
    ".AddGroup\\(\\s*GROUP_([\\w\\d_]+)\\s*,\\s*(\\d+)\\s*,\\s*(\\d+)\\s*\\)",

    "groupName", "minRatio", "maxRatio"
  )
  private val locationPidRegex = new Regex(
    ".LocationPid\\(\\s*LOCATION_([\\w\\d_]+)\\s*\\)",

    "locationName"
  )
  private val fightingRegex = new Regex(
    ".Fighting\\(\\s*(\\d+)\\s*,\\s*(\\d+)\\s*\\)",

    "groupFrom", "groupTo"
  )
  private val checkLVarRegex = new Regex(
    ".CheckLVar\\(\\s*LVAR_([\\w\\d_]+)\\s*,\\s*\\'(.)\\'\\s*,\\s*(\\d+)\\s*\\)",

    "name", "operator", "value"
  )
  private val checkGVarRegex = new Regex(
    ".CheckGVar\\(\\s*GVAR_([\\w\\d_]+)\\s*,\\s*\\'(.)\\'\\s*,\\s*(\\d+)\\s*\\)",

    "name", "operator", "value"
  )

  private val checkHour = new Regex(
    ".CheckHour\\(\\s*\\'(.)\\'\\s*,\\s*(\\d+)\\s*\\)",

    "operator", "value"
  )

  protected def serverPath: String

  private var initializedTablesOption: Option[List[Table]] = None
  private var tablesOption: Option[List[Table]] = None

  def parseTables = tablesOption match {
    case Some(tables) => tables
    case None => {
      val tables = new ListBuffer[Table]
      val script = WorldMapScriptReader.getWorldMapScript(serverPath)

      for (tableMatch <- tableDefineRegex.findAllIn(script).matchData) {
        val name = tableMatch.group("tableName")
        parseInitializedTables.find(table => table.name == name) match {
          case Some(table) => tables += table
          case None => tables += new Table(name)
        }
      }
      tablesOption = Option(tables.toList)
      tables.toList
    }
  }

  private def parseInitializedTables = initializedTablesOption match {
    case Some(tables) => tables
    case None => {
      val script = WorldMapScriptReader.getWorldMapInitScript(serverPath)
      initializedTablesOption = Option(tableRegex.findAllIn(script).matchData.map(data => parseTableMatch(data)).toList)
      initializedTablesOption.get
    }
  }

  private def parseTableMatch(data: Regex.Match) = {
    val table = new Table(data group "tableName")
    table.locations = locationRegex.findAllIn(data.group("locations")).matchData.map(data => parseLocationMatch(data)).toList
    table.encounters = parseEncountersFromString(data.group("encounters"))
    table
  }

  private def parseLocationMatch(data: Regex.Match) = parseLocation(data.group("locationName")).get

  def parseEncountersFromString(string: String) = encounterRegex.findAllIn(string).matchData.map(data => parseEncounterMatch(data)).toList

  private def parseEncounterMatch(data: Regex.Match) = {
    val encounter = new Encounter
    encounter.chance = data.group("chance").toInt
    encounter.text = parseTexts("FOGM.MSG").find(_.id == data.group("textNum").toInt)
    encounter.location = parseLocationPid(locationPidRegex.findFirstIn(data.group("properties")))
    encounter.groups = groupRegex.findAllIn(data.group("properties")).matchData.map(parseGroupMatch(_)).toList
    encounter.fights = fightingRegex.findAllIn(data.group("properties")).matchData.map(data => parseFight(encounter.groups, data)).filterNot(_ eq null).toList
    encounter.checks ++= checkLVarRegex.findAllIn(data.group("properties")).matchData.map(parseLVarCheck(_))
    encounter.checks ++= checkGVarRegex.findAllIn(data.group("properties")).matchData.map(parseGVarCheck(_))
    encounter.checks ++= checkHour.findAllIn(data.group("properties")).matchData.map(parseHourCheck(_))
    encounter
  }

  private def parseLocationPid(locationOption: Option[String]) = locationOption match {
    case Some(location) => parseLocation(location)
    case None => None
  }

  private def parseGroupMatch(data: Regex.Match) =
    new EncounterGroup(parseGroup(data.group("groupName")), data.group("minRatio").toInt, data.group("maxRatio").toInt)

  private def parseFight(groups: List[EncounterGroup], data: Regex.Match) = {
    val fromIndex = data.group("groupFrom").toInt
    val toIndex = data.group("groupTo").toInt
    if (fromIndex < groups.size && toIndex < groups.size) {
      new Fight(groups(fromIndex), groups(toIndex))
    } else {
      null
    }
  }

  private def parseLVarCheck(data: Regex.Match) = new LVarCheck(
    data.group("name"),
    Operator.values.find(_.toString == data.group("operator")).get,
    data.group("value").toInt
  )

  private def parseGVarCheck(data: Regex.Match) = new GVarCheck(
    data.group("name"),
    Operator.values.find(_.toString == data.group("operator")).get,
    data.group("value").toInt
  )

  private def parseHourCheck(data: Regex.Match) = new HourCheck(
    Operator.values.find(_.toString == data.group("operator")).get,
    data.group("value").toInt
  )

  def parseTable(name: String) = parseTables.find(table => table.name == name || ("TABLE_" + table.name) == name)
}
