package fonline.utils

import swing._
import swing.Swing._
import swing.BorderPanel.Position
import javax.swing.BorderFactory
import java.awt.Toolkit
import java.awt.datatransfer.StringSelection

/**
 * User: wladimiiir
 * Date: 11/1/12
 * Time: 1:26 PM
 */
object GroupDefineRenderer extends SimpleSwingApplication {
  def top = {
    val textArea = new TextArea()
    val frame = new Frame {
      title = "Renderer"
      contents = new BorderPanel {
        border = BorderFactory.createTitledBorder("Enter list of group (1 group per line)")
        add(new ScrollPane(textArea), Position.Center)
        add(new FlowPanel(FlowPanel.Alignment.Right)(
          Button("Render group defines") {
            Toolkit.getDefaultToolkit.getSystemClipboard.setContents(new StringSelection(
              textArea.text.lines
                      .toList.sortBy(_.toString)
                      .zipWithIndex
                      .map(pair => "#define GROUP_" + pair._1.replace(" ", "_") + "\t\t" + "( " + pair._2 + " )")
                      .mkString("\n")
            ), null)
            Dialog.showMessage(message = "Groups have been copied to clipboard.")
          },
          Button("Render Encounter messages") {
            Toolkit.getDefaultToolkit.getSystemClipboard.setContents(new StringSelection(
              textArea.text.lines
                      .zipWithIndex
                      .map(pair => "{" + (1003000 + pair._2) + "0}{}{" + pair._1 + "}")
                      .mkString("\n")
            ), null)
            Dialog.showMessage(message = "Encounters have been copied to clipboard.")
          }
        ), Position.South)
      }
    }
    frame.size = (640, 480)
    frame.centerOnScreen()
    frame
  }
}
