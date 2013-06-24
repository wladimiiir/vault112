package fonline.entity

/**
 *
 * @author mikewall
 * @since 12.10.2012, 10:48
 * @version 1.0
 */
object Slot extends Enumeration {
  type Slot = Value

  val Hand1 = Value("SLOT_HAND1")
  val Hand2 = Value("SLOT_HAND2")
  val Inventory = Value("SLOT_INV")
  val Armor = Value("SLOT_ARMOR")
}