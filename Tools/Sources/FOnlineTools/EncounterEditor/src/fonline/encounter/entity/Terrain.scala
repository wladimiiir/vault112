package fonline.encounter.entity

/**
 * User: mikewall
 * Date: 10/24/12
 * Time: 8:53 PM
 */
class Terrain(val name: String) {
  override def hashCode() = 29 + name.hashCode

  override def equals(obj: Any) = obj.isInstanceOf[Terrain] && obj.asInstanceOf[Terrain].name == name
}
