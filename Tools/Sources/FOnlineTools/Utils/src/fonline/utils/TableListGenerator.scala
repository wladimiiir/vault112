package fonline.utils

/**
 * User: wladimiiir
 * Date: 1/2/13
 * Time: 3:15 PM
 */
object TableListGenerator {
  def main(args: Array[String]) {
    for (first <- Range(0, 28)) {
      for (second <- Range(0, 16)) {
        //        printDefine(first, second)
        printZone(first, second)
      }
      //            println()
    }
  }

  def printDefine(first: Int, second: Int) {
    println("#define\tTABLE_" + (first match {
      case number: Int if number < 10 => "0" + number
      case _ => first.toString
    }) + "_" + (second match {
      case number: Int if number < 10 => "0" + number
      case _ => second.toString
    }) + "\t\t\t( " + (first * 16 + second) + " )")
  }

  def printZone(first: Int, second: Int) {
    println("SetZone( " + first + ", " + second + ", TABLE_" + (first match {
      case number: Int if number < 10 => "0" + number
      case _ => first.toString
    }) + "_" + (second match {
      case number: Int if number < 10 => "0" + number
      case _ => second.toString
    }) + ", 0, TERRAIN_Mountain, CHANCE_Common, CHANCE_Common, CHANCE_Common );")
  }
}
