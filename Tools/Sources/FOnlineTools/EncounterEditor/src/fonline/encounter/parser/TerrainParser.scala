package fonline.encounter.parser

import fonline.encounter.entity.Terrain
import util.matching.Regex
import fonline.encounter.logic.WorldMapScriptReader

/**
 * User: mikewall
 * Date: 10/24/12
 * Time: 8:59 PM
 */
trait TerrainParser {
  private val terrainDefineRegex = new Regex("#define\\s*TERRAIN_([0-9a-zA-Z_]*)", "terrainName")

  protected def serverPath: String

  private var terrainsOption: Option[List[Terrain]] = None

  def parseTerrains = terrainsOption match {
    case Some(terrains) => terrains
    case None => {
      val script = WorldMapScriptReader.getWorldMapScript(serverPath)
      terrainsOption = Option(terrainDefineRegex.findAllIn(script).matchData.map(data => new Terrain(data group "terrainName")).toList)
      terrainsOption.get
    }
  }

  def parseTerrain(name: String) = parseTerrains.find(terrain => terrain.name == name || ("TERRAIN_" + terrain.name) == name)
}
