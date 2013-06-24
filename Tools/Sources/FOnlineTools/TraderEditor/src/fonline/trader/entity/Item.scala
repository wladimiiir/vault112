package fonline.trader.entity

import fonline.entity.ItemProto

/**
 * User: wladimiiir
 * Date: 12/6/12
 * Time: 10:57 PM
 */
class Item (var proto: ItemProto) {
  var minCount = 0
  var maxCount = 0
  var spawnChance = 0.0
  var onlyCaps = false
}
