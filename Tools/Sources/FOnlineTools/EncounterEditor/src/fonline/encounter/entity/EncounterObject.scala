package fonline.encounter.entity

import fonline.entity.Dialog

/**
 *
 * @author mikewall
 * @since 12.10.2012, 10:31
 * @version 1.0
 */
abstract class EncounterObject {
  var dialog: Option[Dialog] = None
  var script: Option[String] = None
  var ratio = 100
  var distance = 0
  var children = List[EncounterObject]()
  var checks = List[Check]()
}