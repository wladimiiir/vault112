package fonline.trader.parser

import fonline.trader.entity.{BuyItem, Item, Trader}
import util.matching.Regex
import fonline.logic.ScriptReader
import fonline.parser.{ItemProtoParser, NpcParser}
import collection.mutable.ListBuffer
import collection.mutable
import fonline.entity.{ItemProto, Duration}
import swing.Dialog.Message
import swing.Dialog

/**
 * User: wladimiiir
 * Date: 12/6/12
 * Time: 11:03 PM
 */
trait TraderParser extends NpcParser with ItemProtoParser {
  private val tradersRegex = new Regex("Trader\\@\\[\\]\\s+traders\\s*=\\s*\\{\\s*([^}]*)", "traders")

  def serverPath: String

  def parseTraders: List[Trader] = {
    val script = ScriptReader.getScript(serverPath, "trader_init.fos")
    tradersRegex.findFirstMatchIn(script) match {
      case Some(traders) => parseTraders(traders.group("traders").lines.map(_.trim).filterNot(_.isEmpty).toBuffer)
      case None => Nil
    }
  }

  private def parseTraders(lines: mutable.Buffer[String]): List[Trader] = parseTrader(lines) match {
    case Some(trader) => trader :: parseTraders(lines)
    case None if lines.isEmpty => Nil
    case None => parseTraders(lines.drop(1))
  }

  def parseDuration(string: String): Duration = string match {
    case duration if duration.startsWith("REAL_MS(") => new Duration(duration.substring(8, duration.indexOf(')')).trim.toInt, Duration.Millisecond)
    case duration if duration.startsWith("REAL_SECOND(") => new Duration(duration.substring(12, duration.indexOf(')')).trim.toInt, Duration.Second)
    case duration if duration.startsWith("REAL_MINUTE(") => new Duration(duration.substring(12, duration.indexOf(')')).trim.toInt, Duration.Minute)
    case duration if duration.startsWith("REAL_HOUR(") => new Duration(duration.substring(10, duration.indexOf(')')).trim.toInt, Duration.Hour)
    case duration if duration.startsWith("REAL_DAY(") => new Duration(duration.substring(9, duration.indexOf(')')).trim.toInt, Duration.Day)
    case duration if duration.startsWith("REAL_MONTH(") => new Duration(duration.substring(11, duration.indexOf(')')).trim.toInt, Duration.Month)
    case duration if duration.startsWith("REAL_YEAR(") => new Duration(duration.substring(10, duration.indexOf(')')).trim.toInt, Duration.Year)
    case _ => new Duration(0, Duration.Hour)
  }

  private def parseTrader(lines: mutable.Buffer[String]) = !lines.isEmpty && lines(0).startsWith("Trader(") match {
    case true => {
      val trader = new Trader
      var traderLine = lines(0)
      trader.npc = parseNpc(traderLine.substring(7, traderLine.indexOf(',')).trim)
      traderLine = traderLine.drop(traderLine.indexOf(',') + 1)
      trader.minRefreshTime = parseDuration(traderLine.substring(0, traderLine.indexOf(',')).trim)
      traderLine = traderLine.drop(traderLine.indexOf(',') + 1)
      trader.maxRefreshTime = parseDuration(traderLine.substring(0, traderLine.indexOf(',')).trim)
      traderLine = traderLine.drop(traderLine.indexOf(',') + 1)
      trader.barterSkill = traderLine.substring(0, traderLine.indexOf(',')).trim.toInt
      traderLine = traderLine.drop(traderLine.indexOf(',') + 1)
      trader.clearInventory = traderLine.trim.startsWith("true")
      lines.remove(0)
      while (!lines.isEmpty && !lines(0).startsWith("Trader(")) {
        lines(0).trim match {
          case item if item.startsWith(".AddItem(") => trader.items :+= parseItem(item)
          case buyItem if buyItem.startsWith(".AddBuyItem(") => trader.buyItems :+= parseBuyItem(buyItem)
        }
        lines.remove(0)
      }
      Option(trader)
    }
    case false => None
  }

  def parseItem(itemLine: String): Item = {
    val itemRegex = new Regex(".AddItem\\(\\s*PID_([\\w\\d\\_]+),\\s*([\\d\\.]+),\\s*(\\d+),\\s*(\\d+)\\,\\s*(\\w+)\\s*\\)",
      "itemProto", "chance", "minCount", "maxCount", "capsOnly")
    println(itemLine)
    itemRegex.findFirstMatchIn(itemLine) match {
      case Some(matchData) => parseItemProto(matchData.group("itemProto")) match {
        case Some(itemProto) => {
          val item = new Item(itemProto)
          item.spawnChance = matchData.group("chance").toDouble
          item.minCount = matchData.group("minCount").toInt
          item.maxCount = matchData.group("maxCount").toInt
          item.onlyCaps = matchData.group("capsOnly") == "true"
          item
        }
        case None => throw new IllegalArgumentException("Error parsing item.")
      }
      case None => throw new IllegalArgumentException("Error parsing item.")
    }
  }

  def parseBuyItem(buyItemString: String): BuyItem = {
    val buyItemRegex = new Regex(".AddBuyItem\\(\\s*PID_([\\w\\d\\_]+)\\s*\\)", "itemProto")
    buyItemRegex.findFirstMatchIn(buyItemString) match {
      case Some(matchData) => parseItemProto(matchData.group("itemProto")) match {
        case Some(itemProto) => new BuyItem(itemProto)
        case None => throw new IllegalArgumentException("Error parsing buy item.")
      }
      case None => throw new IllegalArgumentException("Error parsing buy item.")
    }
  }
}
