package fonline.encounter.parser

import fonline.encounter.entity.Zone
import util.matching.Regex
import fonline.encounter.logic.WorldMapScriptReader

/**
 * User: mikewall
 * Date: 10/25/12
 * Time: 6:43 PM
 */
trait ZoneParser extends TableParser with TerrainParser with ChanceParser {
  protected val zoneRegex = new Regex(
    "SetZone\\(" +
            "\\s*(\\d+)\\s*," +
            "\\s*(\\d+)\\s*," +
            "\\s*TABLE_([\\d\\w_]+)\\s*," +
            "\\s*([\\-\\d]+)\\s*," +
            "\\s*TERRAIN_([\\d\\w_]+)\\s*," +
            "\\s*CHANCE_([\\d\\w_]+)\\s*" +
            "(?:,\\s*CHANCE_([\\d\\w_]+)\\s*)?" +
            "(?:,\\s*CHANCE_([\\d\\w_]+)\\s*)?" +
            "\\);",

    "x", "y", "table", "difficulty", "terrain", "morningChance", "afternoonChance", "nightChance"
  )

  protected def serverPath: String

  def parseZones = {
    val script = WorldMapScriptReader.getWorldMapInitScript(serverPath)
    zoneRegex.findAllIn(script).matchData.map(data =>
      new Zone(data.group("x").toInt, data.group("y").toInt) {
        table = parseTable(data group "table")
        difficulty = data.group("difficulty").toInt
        terrain = parseTerrain(data group "terrain")
        morningChance = parseChance(data.group("morningChance"))
        afternoonChance = data.groupNames.contains("afternoonChance") match {
          case true => parseChance(data.group("afternoonChance"))
          case false => morningChance
        }
        nightChance = data.groupNames.contains("nightChance") match {
          case true => parseChance(data.group("nightChance"))
          case false => morningChance
        }
      }).toList
  }
}
