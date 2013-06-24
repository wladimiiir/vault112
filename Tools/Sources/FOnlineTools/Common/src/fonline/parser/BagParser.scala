package fonline.parser

import fonline.entity.{AI, Bag, Dialog}
import fonline.logic.ScriptReader
import util.matching.Regex

/**
 *
 * @author mikewall
 * @since 15.10.2012, 13:23
 * @version 1.0
 */
trait BagParser {
  protected val bagDefineRegex = new Regex("#define\\s*BAG_([\\d\\w_]*)\\s*\\(\\s*(\\d*)\\s*\\)", "bagName", "bagID")

  protected def serverPath: String

  private var bagsOption: Option[List[Bag]] = None

  def parseBag(bag: String) = {
    bag.isEmpty || bag.exists(!_.isDigit) match {
      case true => parseBags.find(b => b.name == bag || ("BAG_" + b.name) == bag)
      case false => parseBags.find(b => b.id == bag.toInt)
    }
  }

  def parseBags = bagsOption match {
    case Some(bags) => bags
    case None => {
      val script = ScriptReader.getScript(serverPath, "_bags.fos")
      bagsOption = Option(bagDefineRegex.findAllIn(script).matchData.map(m => new Bag(m.group("bagID").toInt, m.group("bagName"))).toList)
      bagsOption.get
    }
  }
}