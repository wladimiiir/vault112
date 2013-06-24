package fonline.encounter.entity

/**
 * User: mikewall
 * Date: 10/25/12
 * Time: 11:14 PM
 */
class Chance(val id: Int, val name: String) {

  override def hashCode() = 29 + id.hashCode()

  override def equals(obj: Any) = obj.isInstanceOf[Chance] && obj.asInstanceOf[Chance].id == id

  override def toString = name + " (" + id + ")"
}
