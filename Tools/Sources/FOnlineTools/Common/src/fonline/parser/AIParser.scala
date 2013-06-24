package fonline.parser

import util.matching.Regex
import fonline.entity.AI
import fonline.logic.ScriptReader

/**
 *
 * @author mikewall
 * @since 29.10.2012, 16:47
 * @version 1.0
 */
trait AIParser {
  private val aiDefineRegex = new Regex("#define\\s*AIPACKET_([0-9a-zA-Z_]*)\\s*\\(\\s*(\\d*)\\s*\\)", "name", "id")

  protected def serverPath: String

  private var aisOption: Option[List[AI]] = None

  def parseAIs = aisOption match {
    case Some(ais) => ais
    case None => {
      val script = ScriptReader.getScript(serverPath, "_ai.fos")
      aisOption = Option(aiDefineRegex.findAllIn(script).matchData.map(data => new AI(data.group("id").toInt, data.group("name"))).toList)
      aisOption.get
    }
  }

  def parseAI(aiString: String) = aiString.isEmpty || aiString.exists(!_.isDigit) match {
    case true => parseAIs.find(ai => ai.name == aiString || ("AIPACKET_" + ai.name) == aiString)
    case false => parseAIs.find(_.id == aiString.toInt)
  }
}