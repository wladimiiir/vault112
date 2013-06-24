package fonline.trader.builder

import fonline.model.ItemModel
import fonline.trader.entity.{BuyItem, Item, Trader}
import fonline.logic.ScriptReader
import util.matching.Regex
import fonline.entity.Npc

/**
 * User: wladimiiir
 * Date: 12/6/12
 * Time: 11:03 PM
 */
trait TraderInitBuilder {
  private val tradersRegex = "Trader\\@\\[\\]\\s+traders\\s*=\\s*\\{\\s*([^}]*)\\};"

  def serverPath: String

  def buildTraderInitScript(traderModel: ItemModel[Trader]) = {
    val script = ScriptReader.getScript(serverPath, "trader_init.fos")
    script.replaceFirst(tradersRegex, "Trader@[] traders = \n\t{" + buildTraders(traderModel.items) + "\n\t};")
  }

  def buildTraders(traders: List[Trader]): String = traders match {
    case trader :: Nil => "\n\t\t" + buildTrader(trader)
    case trader :: tail => "\n\t\t" + buildTrader(trader) + "," + buildTraders(tail)
    case _ => ""
  }

  def buildTrader(trader: Trader) = "Trader(NPC_PID_" + trader.npc.getOrElse(new Npc(0, "NotSpecified")).name +
          ", " + trader.minRefreshTime.getScriptStringReal +
          ", " + trader.maxRefreshTime.getScriptStringReal +
          ", " + trader.barterSkill +
          ", " + trader.clearInventory.toString.toLowerCase +
          ")" +
          buildItems(trader.items) +
          buildBuyItems(trader.buyItems)

  def buildItems(items: List[Item]): String = items match {
    case item :: tail => "\n\t\t\t" + buildItem(item) + buildItems(tail)
    case _ => ""
  }

  def buildItem(item: Item) = ".AddItem(PID_" + item.proto.name +
          ", " + item.spawnChance.toString +
          ", " + item.minCount +
          ", " + item.maxCount +
          ", " + item.onlyCaps.toString.toLowerCase +
          ")"

  def buildBuyItems(items: List[BuyItem]): String = items match {
    case item :: tail => "\n\t\t\t" + buildBuyItem(item) + buildBuyItems(tail)
    case _ => ""
  }

  def buildBuyItem(item: BuyItem) = ".AddBuyItem(PID_" + item.proto.name + ")"
}
