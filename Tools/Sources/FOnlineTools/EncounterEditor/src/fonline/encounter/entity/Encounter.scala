package fonline.encounter.entity

import fonline.entity.{TextMessage, Location}

/**
 *
 * @author mikewall
 * @since 12.10.2012, 10:07
 * @version 1.0
 */
class Encounter {
  var chance = 0
  var text: Option[TextMessage] = None
  var location: Option[Location] = None
  var groups = List[EncounterGroup]()
  var fights = List[Fight]()
  var checks = List[Check]()
}