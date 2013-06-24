package fonline.encounter.entity

/**
 *
 * @author mikewall
 * @since 12.10.2012, 10:07
 * @version 1.0
 */
trait Check {
}

case class RandomCheck(var value: Int = 0) extends Check

case class LVarCheck(var name: String = "", var operator: Operator.Operator = Operator.Equals, var value: Int = 0) extends Check

case class GVarCheck(var name: String = "", var operator: Operator.Operator = Operator.Equals, var value: Int = 0) extends Check

case class ParamCheck(var name: String = "", var operator: Operator.Operator = Operator.Equals, var value: Int = 0) extends Check

case class HourCheck(var operator: Operator.Operator = Operator.Equals, var value: Int = 0) extends Check
