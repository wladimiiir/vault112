package fonline.parser

import fonline.entity.Dialog
import util.matching.Regex
import fonline.logic.ScriptReader

/**
 *
 * @author mikewall
 * @since 15.10.2012, 13:23
 * @version 1.0
 */
trait DialogParser {
  protected val dialogDefineRegex = new Regex("#define\\s*DIALOG_([0-9a-zA-Z_]*)\\s*\\(\\s*(\\d*)\\s*\\)", "dialogName", "dialogID")

  protected def serverPath: String

  def parseDialog(dialog: String) = {
    dialog.isEmpty || dialog.exists(!_.isDigit) match {
      case true => parseDialogs.find(d => d.name == dialog)
      case false => parseDialogs.find(d => d.id == dialog.toInt)
    }
  }

  def parseDialogs = {
    val script = ScriptReader.getScript(serverPath, "_dialogs.fos")
    dialogDefineRegex.findAllIn(script).matchData.map(m => new Dialog(m.group("dialogID").toInt, m.group("dialogName"))).toList
  }
}