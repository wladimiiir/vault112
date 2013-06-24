package fonline.encounter.model

import fonline.encounter.entity.Zone
import fonline.model.ItemModel

/**
 * User: mikewall
 * Date: 10/25/12
 * Time: 5:54 PM
 */
class ZoneModel(zones: List[Zone]) extends ItemModel[Zone](zones) {
  def getZone(x: Int, y: Int) = find(zone => zone.x == x && zone.y == y)
}