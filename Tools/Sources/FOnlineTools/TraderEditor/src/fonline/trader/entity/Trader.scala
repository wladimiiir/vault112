package fonline.trader.entity

import fonline.entity.{Duration, Npc}

/**
 * User: wladimiiir
 * Date: 12/6/12
 * Time: 10:55 PM
 */
class Trader {
  var npc: Option[Npc] = None
  var minRefreshTime = new Duration(1, Duration.Hour)
  var maxRefreshTime = new Duration(1, Duration.Hour)
  var barterSkill = 100
  var clearInventory = true
  var items = List[Item]()
  var buyItems = List[BuyItem]()
}
