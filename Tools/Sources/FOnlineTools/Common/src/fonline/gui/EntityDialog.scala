package fonline.gui

import scala.Some
import swing.BorderPanel.Position
import swing._

/**
 * User: mikewall
 * Date: 10/28/12
 * Time: 1:38 PM
 */
class EntityDialog[A](editor: DialogEditor[A], dialogTitle: String) extends Dialog {
  private var currentEntity: Option[A] = None
  private var result = EntityDialog.Cancel

  title = dialogTitle
  modal = true
  contents = new BorderPanel {
    add(editor, Position.Center)
    add(new FlowPanel(FlowPanel.Alignment.Right)(Button("OK")({
      currentEntity match {
        case Some(entity) => editor.commit(entity)
        case None =>
      }
      result = EntityDialog.OK
      EntityDialog.this.visible = false
    }), Button("Cancel")({
      result = EntityDialog.Cancel
      EntityDialog.this.visible = false
    })), Position.South)
  }
  pack()

  def showFor(entity: A) = {
    result = EntityDialog.Cancel
    currentEntity = Option(entity)
    editor.init(entity)
    visible = true
    result
  }
}

object EntityDialog extends Enumeration {
  type Result = Value

  val OK, Cancel = Value
}
