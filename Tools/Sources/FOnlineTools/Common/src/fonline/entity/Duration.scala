package fonline.entity

import actors.threadpool.helpers.Utils.MillisProvider

/**
 *
 * @author Y12370
 * @since 7.12.2012, 14:24
 * @version 1.0
 */
class Duration(val value: Int = 0, val unit: Duration.Unit = Duration.Millisecond) {
  def getScriptStringReal: String = unit match {
    case Duration.Millisecond => "REAL_MS(" + value + ")"
    case Duration.Second => "REAL_SECOND(" + value + ")"
    case Duration.Minute => "REAL_MINUTE(" + value + ")"
    case Duration.Hour => "REAL_HOUR(" + value + ")"
    case Duration.Day => "REAL_DAY(" + value + ")"
    case Duration.Month => "REAL_MONTH(" + value + ")"
    case Duration.Year => "REAL_YEAR(" + value + ")"
  }

  override def toString = value.toString + " " + unit
}

object Duration extends Enumeration {
  type Unit = Value

  val Millisecond = Value("millisecond")
  val Second = Value("second")
  val Minute = Value("minute")
  val Hour = Value("hour")
  val Day = Value("day")
  val Month = Value("month")
  val Year = Value("year")
}