package fonline.gui

import java.awt.Dimension
import javax.swing.ImageIcon
import swing.{Action, Button}

/**
 * User: mikewall
 * Date: 10/30/12
 * Time: 6:27 PM
 */
object ButtonFactory {
  def Button(text: String)(action: => Unit) = new Button(Action(text)(action)) {
    tooltip = text
    icon = findIcon(text) match {
      case Some(icon) => {
        text = null
        focusPainted = false
        preferredSize = new Dimension(icon.getIconWidth + 1, icon.getIconHeight + 1)
        icon
      }
      case None => null
    }
  }

  private def findIcon(text: String) = text match {
    case "Add" => Option(new ImageIcon(getClass.getResource("icons/plus.png")))
    case "Add group" => Option(new ImageIcon(getClass.getResource("icons/pack.png")))
    case "Remove" => Option(new ImageIcon(getClass.getResource("icons/minus.png")))
    case "Edit" => Option(new ImageIcon(getClass.getResource("icons/edit.png")))
    case "Add LVar" => Option(new ImageIcon(getClass.getResource("icons/l.png")))
    case "Add GVar" => Option(new ImageIcon(getClass.getResource("icons/g.png")))
    case "Add Param" => Option(new ImageIcon(getClass.getResource("icons/p.png")))
    case "Add Random" => Option(new ImageIcon(getClass.getResource("icons/random.png")))
    case "Add Hour" => Option(new ImageIcon(getClass.getResource("icons/hour.png")))
    case "Copy" => Option(new ImageIcon(getClass.getResource("icons/copy.png")))
    case "Paste" => Option(new ImageIcon(getClass.getResource("icons/paste.png")))
    case _ => None
  }
}
