package fonline.parser

import collection.mutable.ListBuffer
import util.matching.Regex
import fonline.logic.ScriptReader
import fonline.entity.Param

/**
 * User: mikewall
 * Date: 10/30/12
 * Time: 7:25 PM
 */
trait ParamParser {
  private val statDefineRegex = new Regex("#define\\s*(ST_[0-9a-zA-Z_]*)\\s*\\(\\s*(\\d*)\\s*\\)", "name", "id")
  private val skillDefineRegex = new Regex("#define\\s*(SK_[0-9a-zA-Z_]*)\\s*\\(\\s*(\\d*)\\s*\\)", "name", "id")
  private val perkDefineRegex = new Regex("#define\\s*(PE_[0-9a-zA-Z_]*)\\s*\\(\\s*(\\d*)\\s*\\)", "name", "id")
  private val karmaDefineRegex = new Regex("#define\\s*(KARMA_[0-9a-zA-Z_]*)\\s*\\(\\s*(\\d*)\\s*\\)", "name", "id")
  private val traitDefineRegex = new Regex("#define\\s*(TRAIT_[0-9a-zA-Z_]*)\\s*\\(\\s*(\\d*)\\s*\\)", "name", "id")
  private val reputationDefineRegex = new Regex("#define\\s*(REPUTATION_[0-9a-zA-Z_]*)\\s*\\(\\s*(\\d*)\\s*\\)", "name", "id")

  protected def serverPath: String

  def parseParams = {
    val script = ScriptReader.getScript(serverPath, "_defines.fos")
    val params = new ListBuffer[Param]
    params ++= statDefineRegex.findAllIn(script).matchData.map(data => new Param(data.group("id").toInt, data.group("name")))
    params ++= skillDefineRegex.findAllIn(script).matchData.map(data => new Param(data.group("id").toInt, data.group("name")))
    params ++= perkDefineRegex.findAllIn(script).matchData.map(data => new Param(data.group("id").toInt, data.group("name")))
    params ++= karmaDefineRegex.findAllIn(script).matchData.map(data => new Param(data.group("id").toInt, data.group("name")))
    params ++= traitDefineRegex.findAllIn(script).matchData.map(data => new Param(data.group("id").toInt, data.group("name")))
    params ++= reputationDefineRegex.findAllIn(script).matchData.map(data => new Param(data.group("id").toInt, data.group("name")))
    params.toList
  }
}
