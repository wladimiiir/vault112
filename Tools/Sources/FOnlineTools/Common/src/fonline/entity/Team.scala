package fonline.entity

/**
 *
 * @author mikewall
 * @since 15.10.2012, 13:16
 * @version 1.0
 */
class Team(val id: Int, val name: String) {
  override def toString = name + " (" + id + ")"
}