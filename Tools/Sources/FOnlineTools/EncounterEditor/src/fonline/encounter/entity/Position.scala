package fonline.encounter.entity

/**
 *
 * @author mikewall
 * @since 12.10.2012, 10:48
 * @version 1.0
 */
object Position extends Enumeration {
  type Position = Value

  val None = Value("POSITION_NONE")
  val Surrounding = Value("POSITION_SURROUNDING")
  val Huddle = Value("POSITION_HUDDLE")
  val Wedge = Value("POSITION_WEDGE")
  val Cone = Value("POSITION_CONE")
  val DoubleLine = Value("POSITION_DOUBLE_LINE")
  val StraightLine = Value("POSITION_STRAIGHT_LINE")
}