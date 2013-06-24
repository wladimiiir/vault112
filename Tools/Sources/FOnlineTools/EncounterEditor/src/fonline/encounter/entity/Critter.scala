package fonline.encounter.entity

import fonline.entity.{Bag, Role, AI, Npc}

/**
 *
 * @author mikewall
 * @since 12.10.2012, 10:07
 * @version 1.0
 */
class Critter(var npc: Option[Npc]) extends EncounterObject {
  var ai: Option[AI] = None
  var bag: Option[Bag] = None
  var role: Option[Role] = None
  var dead = false
}