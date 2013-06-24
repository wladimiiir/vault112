package fonline.encounter.parser

import collection.mutable.ListBuffer
import fonline.encounter.entity._
import util.matching.Regex
import fonline.encounter.logic.WorldMapScriptReader
import fonline.parser._
import fonline.encounter.entity.LVarCheck
import fonline.encounter.entity.GVarCheck
import scala.Some
import fonline.encounter.entity.ParamCheck
import fonline.encounter.entity.RandomCheck
import fonline.entity.Slot

/**
 *
 * @author mikewall
 * @since 15.10.2012, 13:20
 * @version 1.0
 */
trait GroupParser extends TeamParser with DialogParser with ItemProtoParser with NpcParser with AIParser with RoleParser with BagParser {
  private val groupDefineRegex = new Regex("#define\\s*GROUP_([0-9a-zA-Z_]*)", "groupName")

  private val groupRegex = new Regex(
    "@group\\s*=\\s*@EncounterGroups\\[\\s*GROUP_([\\w_\\d]*)\\s*\\];\\s*" +
            "(?:group.TeamNum\\s*=\\s*([\\w\\d_]+);\\s*)?" +
            "(?:group.Position\\s*=\\s*([A-Z_]*);\\s*)?" +
            "(?:group.Distance\\s*=\\s*(\\d*);\\s*)?" +
            "(?:group.Spacing\\s*=\\s*(\\d*);\\s*)?" +
            "((?:group.Add[^;]+;\\s*)+)",

    "groupName", "team", "position", "distance", "spacing", "objects"
  )
  private val critterRegex = new Regex(
    "group.AddCritter\\(\\s*([\\w\\d_]+)\\s*\\)([^;]*);",

    "npc", "properties"
  )
  private val itemRegex = new Regex(
    "group.AddItem\\(\\s*([\\w\\d_]+)\\s*\\)([^;]*);",

    "itemPID", "properties"
  )
  /*private val critterItemRegex = new Regex(
    ".AddItem\\(\\s*([\\d\\w_]+)\\s*,\\s*([0-9]+)\\s*,\\s*([0-9]+)\\s*,\\s*([\\d\\w_]+)\\s*\\)",

    "itemPID", "minCount", "maxCount", "slot"
  )*/
  private val critterItemRegex = new Regex(
    ".AddItem\\(\\s*([\\d\\w_]+)\\s*,(?:\\s*([\\d\\.]+)\\s*,)?\\s*([0-9]+)\\s*,\\s*([0-9]+)\\s*,\\s*([\\d\\w_]+)\\s*\\)",

    "itemPID", "chance", "minCount", "maxCount", "slot"
  )
  private val checkRandomRegex = new Regex(
    ".CheckRandom\\(\\s*(\\d+)\\s*\\)",

    "value"
  )
  private val checkLVarRegex = new Regex(
    ".CheckLVar\\(\\s*LVAR_([\\w\\d_]+)\\s*,\\s*\\'(.)\\'\\s*,\\s*(\\d+)\\s*\\)",

    "name", "operator", "value"
  )
  private val checkGVarRegex = new Regex(
    ".CheckGVar\\(\\s*GVAR_([\\w\\d_]+)\\s*,\\s*\\'(.)\\'\\s*,\\s*(\\d+)\\s*\\)",

    "name", "operator", "value"
  )

  private val checkParamRegex = new Regex(
    ".CheckParam\\(\\s*([\\w\\d_]+)\\s*,\\s*\\'(.)\\'\\s*,\\s*(\\d+)\\s*\\)",

    "name", "operator", "value"
  )

  private var groupsOption: Option[List[Group]] = None
  private var initializedGroupsOption: Option[List[Group]] = None

  protected def serverPath: String

  private def propertyRegex(propertyName: String) = new Regex("." + propertyName + "\\(\\s*\"?([@\\w\\d\\_]+)\"?\\s*\\)", "property")

  private def findProperty(propertyName: String, properties: String) = propertyRegex(propertyName).findFirstMatchIn(properties) match {
    case Some(data) => Option(data.group("property"))
    case None => None
  }

  def parseGroups = groupsOption match {
    case Some(groups) => groups
    case None => {
      val groups = new ListBuffer[Group]
      val script = WorldMapScriptReader.getWorldMapScript(serverPath)

      for (groupMatch <- groupDefineRegex.findAllIn(script).matchData) {
        val name = groupMatch.group("groupName")
        parseInitializedGroups.find(group => group.name == name) match {
          case Some(group) => groups += group
          case None => groups += new Group(name)
        }
      }
      groupsOption = Option(groups.toList)
      groups.toList
    }
  }

  private def parseInitializedGroups = initializedGroupsOption match {
    case Some(groups) => groups
    case None => {
      val script = WorldMapScriptReader.getWorldMapInitScript(serverPath)
      val groups = groupRegex.findAllIn(script).matchData.map(parseGroupMatch(_)).toList
      initializedGroupsOption = Option(groups)
      groups
    }
  }

  private def parseGroupMatch(groupMatch: Regex.Match) = {
    val group = new Group(groupMatch.group("groupName"))
    group.team = groupMatch.group("team") match {
      case team: String => parseTeam(team)
      case _ => None
    }
    group.position = groupMatch.group("position") match {
      case position: String => Position.withName(position)
      case _ => Position.None
    }
    group.distance = groupMatch.group("distance") match {
      case distance: String => distance.toInt
      case _ => 0
    }
    group.spacing = groupMatch.group("spacing") match {
      case spacing: String => spacing.toInt
      case _ => 0
    }
    group.critters = critterRegex.findAllIn(groupMatch.group("objects")).matchData.map(data => parseCritter(data)).toList
    group.items = itemRegex.findAllIn(groupMatch.group("objects")).matchData.map(data => parseItem(data)).toList
    group
  }

  private def parseCritter(critterMatch: Regex.Match) = {
    val critter = new Critter(parseNpc(critterMatch.group("npc")))
    val properties = critterMatch.group("properties")

    critter.dialog = findProperty("Dialog", properties) match {
      case Some(dialog) => parseDialog(dialog)
      case None => None
    }
    critter.script = findProperty("Script", properties) match {
      case Some(script) if !script.isEmpty => Option(script)
      case _ => None
    }
    critter.ratio = findProperty("Ratio", properties) match {
      case Some(ratio) if ratio.forall(_.isDigit) => ratio.toInt
      case _ => 100
    }
    critter.distance = findProperty("Distance", properties) match {
      case Some(distance) if distance.forall(_.isDigit) => distance.toInt
      case _ => 0
    }
    critter.ai = findProperty("AI", properties) match {
      case Some(ai) => parseAI(ai)
      case None => None
    }
    critter.bag = findProperty("Bag", properties) match {
      case Some(bag) => parseBag(bag)
      case None => None
    }
    critter.role = findProperty("Role", properties) match {
      case Some(role) => parseRole(role)
      case None => None
    }
    critter.dead = findProperty("Dead", properties) match {
      case Some("true") => true
      case _ => false
    }
    critter.children = critterItemRegex.findAllIn(properties).matchData.map(data => parseCritterItem(data)).toList/* :::
            critterItemWithChanceRegex.findAllIn(properties).matchData.map(data => parseCritterItem(data)).toList*/
    critter.checks ++= checkRandomRegex.findAllIn(properties).matchData.map(data => new RandomCheck(data.group("value").toInt))
    critter.checks ++= checkLVarRegex.findAllIn(properties).matchData.map(data => new LVarCheck(
      data.group("name"),
      Operator.values.find(value => value.toString == data.group("operator")).get,
      data.group("value").toInt)
    )
    critter.checks ++= checkGVarRegex.findAllIn(properties).matchData.map(data => new GVarCheck(
      data.group("name"),
      Operator.values.find(value => value.toString == data.group("operator")).get,
      data.group("value").toInt)
    )
    critter.checks ++= checkParamRegex.findAllIn(properties).matchData.map(data => new ParamCheck(
      data.group("name"),
      Operator.values.find(value => value.toString == data.group("operator")).get,
      data.group("value").toInt)
    )

    critter
  }

  private def parseCritterItem(itemMatch: Regex.Match): Item = {
    val critterItem = new Item(parseItemProto(itemMatch.group("itemPID")))
    critterItem.minCount = itemMatch.group("minCount") match {
      case minCount: String => minCount.toInt
      case _ => 0
    }
    critterItem.checks = itemMatch.group("chance") match {
      case chance: String => List(new RandomCheck(chance.toDouble.toInt))
      case _ => Nil
    }
    critterItem.maxCount = itemMatch.group("maxCount") match {
      case maxCount: String => maxCount.toInt
      case _ => 0
    }
    critterItem.slot = itemMatch.group("slot") match {
      case slot: String => Slot.withName(slot)
      case _ => Slot.Inventory
    }
    critterItem
  }

  private def parseItem(data: Regex.Match) = {
    val item = new Item(parseItemProto(data.group("itemPID")))
    val properties = data.group("properties")

    item.dialog = findProperty("Dialog", properties) match {
      case Some(dialog) => parseDialog(dialog)
      case None => None
    }
    item.script = findProperty("Script", properties) match {
      case Some(script) if !script.isEmpty => Option(script)
      case _ => None
    }
    item.ratio = findProperty("Ratio", properties) match {
      case Some(ratio) if ratio.forall(_.isDigit) => ratio.toInt
      case _ => 100
    }
    item.distance = findProperty("Distance", properties) match {
      case Some(distance) if distance.forall(_.isDigit) => distance.toInt
      case _ => 0
    }
    item.checks ++= checkRandomRegex.findAllIn(properties).matchData.map(data => new RandomCheck(data.group("value").toInt))
    item.checks ++= checkLVarRegex.findAllIn(properties).matchData.map(data => new LVarCheck(
      data.group("name"),
      Operator.values.find(value => value.toString == data.group("operator")).get,
      data.group("value").toInt)
    )
    item.checks ++= checkGVarRegex.findAllIn(properties).matchData.map(data => new GVarCheck(
      data.group("name"),
      Operator.values.find(value => value.toString == data.group("operator")).get,
      data.group("value").toInt)
    )
    item.checks ++= checkParamRegex.findAllIn(properties).matchData.map(data => new ParamCheck(
      data.group("name"),
      Operator.values.find(value => value.toString == data.group("operator")).get,
      data.group("value").toInt)
    )

    item
  }

  def parseGroup(name: String) = parseInitializedGroups.find(group => group.name == name || ("GROUP_" + group.name == name)) match {
    case Some(group) => Option(group)
    case None => parseGroups.find(group => group.name == name || ("GROUP_" + group.name == name))
  }
}