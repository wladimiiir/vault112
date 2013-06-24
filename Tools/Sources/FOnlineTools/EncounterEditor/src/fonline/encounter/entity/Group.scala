package fonline.encounter.entity

import fonline.entity.Team

/**
 *
 * @author mikewall
 * @since 12.10.2012, 10:07
 * @version 1.0
 */
class Group(val name: String) {
  var team: Option[Team] = None
  var position = Position.None
  var spacing = 0
  var distance = 0
  var critters = List[Critter]()
  var items = List[Item]()


  override def hashCode() = 29 + name.hashCode

  override def equals(obj: Any) = obj match {
    case group: Group => group.name == name
    case _ => false
  }

  override def toString = "team = " + team
}