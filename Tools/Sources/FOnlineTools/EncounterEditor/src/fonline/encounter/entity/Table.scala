package fonline.encounter.entity

import fonline.entity.Location

/**
 * User: mikewall
 * Date: 10/24/12
 * Time: 8:53 PM
 */
class Table(val name: String) {
  var encounters = List[Encounter]()
  var locations = List[Location]()

  override def hashCode() = 29 + name.hashCode

  override def equals(obj: Any)= obj.isInstanceOf[Table] && obj.asInstanceOf[Table].name == name
}
