package fonline.entity

/**
 * User: mikewall
 * Date: 10/27/12
 * Time: 6:21 PM
 */
class TextMessage(val id:Int, val text: String) {
  override def hashCode() = 29 + id.hashCode()

  override def equals(obj: Any) = obj match {
    case textMessage: TextMessage => textMessage.id == id
    case _ => false
  }
}
