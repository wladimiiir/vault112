package fonline.encounter.parser

import fonline.encounter.entity.Chance
import util.matching.Regex
import fonline.encounter.logic.WorldMapScriptReader

/**
 * User: mikewall
 * Date: 10/24/12
 * Time: 8:59 PM
 */
trait ChanceParser {
  protected val chanceDefineRegex = new Regex("#define\\s*CHANCE_([0-9a-zA-Z_]*)\\s*\\(\\s*(\\d*)\\s*\\)", "chanceName", "chanceID")

  protected def serverPath: String

  def parseChances = {
    val script = WorldMapScriptReader.getWorldMapScript(serverPath)
    chanceDefineRegex.findAllIn(script).matchData.map(data => new Chance((data group "chanceID").toInt, data group "chanceName")).toList
  }

  def parseChance(name: String) = parseChances.find(chance => chance.name == name || ("CHANCE_" + chance.name) == name)
}
