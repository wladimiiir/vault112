package fonline.gui

import swing.Component

/**
 * User: mikewall
 * Date: 10/28/12
 * Time: 1:33 PM
 */
trait DialogEditor[A] extends Component {
  def init(dialogEntity: A)

  def commit(dialogEntity: A)
}
