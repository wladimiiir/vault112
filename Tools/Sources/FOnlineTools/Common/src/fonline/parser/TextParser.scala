package fonline.parser

import util.matching.Regex
import fonline.entity.TextMessage

/**
 * User: mikewall
 * Date: 10/27/12
 * Time: 10:31 PM
 */
trait TextParser {
  private val textMessageRegex = new Regex(
    "\\{\\s*(\\d+)\\s*\\}\\{\\}\\{([^}]*)\\}",
    "textNumber", "text"
  )

  protected def serverPath: String

  private var texts: Option[List[TextMessage]] = None

  def parseTexts(textName: String) = texts match {
    case Some(messages) => messages
    case None => {
      val text = TextReader.getText(serverPath, textName, "engl")
      texts = Option(textMessageRegex.findAllIn(text).matchData.map(data => new TextMessage(data.group("textNumber").toInt, data.group("text"))).toList)
      texts.get
    }
  }
}
