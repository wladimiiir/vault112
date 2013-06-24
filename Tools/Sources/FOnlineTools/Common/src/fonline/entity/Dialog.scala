package fonline.entity

/**
 *
 * @author mikewall
 * @since 15.10.2012, 14:50
 * @version 1.0
 */
class Dialog(val id: Int, val name: String) {
  override def toString = name + " (" + id + ")"
}