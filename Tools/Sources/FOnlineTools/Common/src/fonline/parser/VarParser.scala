package fonline.parser

import util.matching.Regex
import fonline.entity.Var
import fonline.logic.ScriptReader

/**
 * User: mikewall
 * Date: 10/28/12
 * Time: 4:25 PM
 */
trait VarParser {
  private val lvarDefineRegex = new Regex("#define\\s*LVAR_([0-9a-zA-Z_]*)\\s*\\(\\s*(\\d*)\\s*\\)", "name", "id")
  private val gvarDefineRegex = new Regex("#define\\s*GVAR_([0-9a-zA-Z_]*)\\s*\\(\\s*(\\d*)\\s*\\)", "name", "id")

  protected def serverPath: String

  private var lvarsOption: Option[List[Var]] = None

  def parseLVars = lvarsOption match {
    case Some(lvars) => lvars
    case None => {
      val script = ScriptReader.getScript(serverPath, "_vars.fos")
      lvarsOption = Option(lvarDefineRegex.findAllIn(script).matchData.map(data => new Var(data.group("name"))).toList.sortBy(_.name))
      lvarsOption.get
    }
  }

  def parseLVar(name: String) = parseLVars.find(lvar => lvar.name == name || ("LVAR_" + lvar.name) == name)

  private var gvarsOption: Option[List[Var]] = None

  def parseGVars = gvarsOption match {
    case Some(gvars) => gvars
    case None => {
      val script = ScriptReader.getScript(serverPath, "_vars.fos")
      gvarsOption = Option(gvarDefineRegex.findAllIn(script).matchData.map(data => new Var(data.group("name"))).toList.sortBy(_.name))
      gvarsOption.get
    }
  }

  def parseGVar(name: String) = parseGVars.find(gvar => gvar.name == name || ("GVAR_" + gvar.name) == name)
}
