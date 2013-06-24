package fonline.encounter.entity

/**
 * User: mikewall
 * Date: 10/27/12
 * Time: 10:16 PM
 */
object Operator extends Enumeration {
  type Operator = Value

  val Lower = Value("<")
  val Greater = Value(">")
  val Equals = Value("=")
}
