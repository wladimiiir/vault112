package fonline.encounter.entity

import fonline.entity.{Slot, ItemProto}

/**
 *
 * @author mikewall
 * @since 12.10.2012, 10:07
 * @version 1.0
 */
class Item(var proto: Option[ItemProto]) extends EncounterObject {
  var minCount = 1
  var maxCount = 1
  var slot = Slot.Inventory
}
