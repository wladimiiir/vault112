package fonline.encounter.parser

import util.matching.Regex
import fonline.encounter.logic.WorldMapScriptReader
import fonline.logic.ScriptReader

/**
 * User: mikewall
 * Date: 10/25/12
 * Time: 9:25 PM
 */
trait ConfigParser {
  def serverPath: String

  def getGlobalMapWidth = getValue("GlobalMapWidth") match {
    case Some(value) => value.toInt
    case None => 0
  }

  def getGlobalMapHeight = getValue("GlobalMapHeight") match {
    case Some(value) => value.toInt
    case None => 0
  }

  private def getValue(valueName: String) = {
    val script = ScriptReader.getScript(serverPath, "config.fos")
    new Regex("__" + valueName + "\\s*=\\s*(\\d+)", "value").findFirstMatchIn(script) match {
      case Some(data) => Option(data group "value")
      case None => None
    }
  }
}
