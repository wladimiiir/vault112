package fonline.logic

import java.io.File
import io.Source
import collection.mutable

/**
 * User: wladimiiir
 * Date: 12/6/12
 * Time: 10:01 PM
 */
object ScriptReader {
  private val scriptMap = new mutable.HashMap[String, String]()

  def getScript(serverPath: String, scriptName: String) = scriptMap.get(scriptName) match {
    case Some(script) => script
    case None => {
      val fileName = serverPath + "/scripts/" + scriptName
      val script = new File(fileName).exists() match {
        case true => {
          val scriptFile = Source.fromFile(fileName, "ISO-8859-1")
          val result = scriptFile.getLines().mkString("\n")
          scriptFile.close()
          result
        }
        case false => ""
      }
      scriptMap += scriptName -> script
      script
    }
  }
}
