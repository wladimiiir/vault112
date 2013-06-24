package fonline.parser

import collection.mutable
import io.Source

/**
 *
 * @author mikewall
 * @since 15.10.2012, 9:54
 * @version 1.0
 */
object TextReader {
  private val textMap = new mutable.HashMap[String, String]()

  def getText(serverPath: String, textName: String, localization: String) = textMap.get(textName + "_" + localization) match {
    case Some(text) => text
    case None => {
      val textFile = Source.fromFile(serverPath + "/text/" + localization + "/" + textName, "ISO-8859-1")
      val text = textFile.getLines().mkString("\n")
      textFile.close()
      textMap += textName + "_" + localization -> text
      text
    }
  }
}