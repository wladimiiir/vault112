package fonline.encounter.gui

import fonline.encounter.entity._
import fonline.encounter.entity.Table
import fonline.entity._
import java.awt.Color
import javax.swing.BorderFactory
import swing.GridBagPanel.{Fill, Anchor}
import swing.ListView.Renderer
import swing._
import swing.event.SelectionChanged
import fonline.model.ItemModel
import Swing._

/**
 * User: wladimiiir
 * Date: 11/10/12
 * Time: 2:14 PM
 */
class WorldMapSelectionPanel(groupModel: ItemModel[Group],
                             tableModel: ItemModel[Table],
                             encounterTextModel: ItemModel[TextMessage]) extends GridBagPanel {
  private val groupUI = new ComboBox[Group](groupModel.items) {
    renderer = Renderer(group => if (group eq null) "" else group.name)
    selection.item = null
    selection.reactions += {
      case SelectionChanged(_) => groupModel.selectedItem = Option(selection.item)
    }
  }
  private val tableUI = new ComboBox[Table](tableModel.items) {
    renderer = Renderer(table => if (table eq null) "" else table.name)
    selection.item = null
    selection.reactions += {
      case SelectionChanged(_) => tableModel.selectedItem = Option(selection.item)
    }
  }
  private val encounterUI = new ComboBox[TextMessage](encounterTextModel.items) {
    preferredSize = (150, 21)
    renderer = Renderer(message => if (message eq null) "" else message.text)
    selection.item = null
    selection.reactions += {
      case SelectionChanged(_) => encounterTextModel.selectedItem = Option(selection.item)
    }
  }

  private implicit def toLabel(text: String) = new Label(text)

  border = BorderFactory.createEmptyBorder(5, 5, 5, 5)

  add("Group:", new Constraints {
    grid = (0, 0)
    anchor = Anchor.East
  })
  add(groupUI, new Constraints {
    grid = (1, 0)
    fill = Fill.Horizontal
    weightx = 1.0
  })
  add(Button("X") {
    groupUI.selection.item = null
  }, new Constraints {
    grid = (2, 0)
  })
  add(colorPanel(Color.GREEN), new Constraints {
    grid = (3, 0)
  })
  add("Table:", new Constraints {
    grid = (0, 1)
    anchor = Anchor.East
  })
  add(tableUI, new Constraints {
    grid = (1, 1)
    fill = Fill.Horizontal
    weightx = 1.0
  })
  add(Button("X") {
    tableUI.selection.item = null
  }, new Constraints {
    grid = (2, 1)
  })
  add(colorPanel(Color.YELLOW), new Constraints {
    grid = (3, 1)
  })
  add("Encounter:", new Constraints {
    grid = (0, 2)
    anchor = Anchor.East
  })
  add(encounterUI, new Constraints {
    grid = (1, 2)
    fill = Fill.Horizontal
    weightx = 1.0
  })
  add(Button("X") {
    encounterUI.selection.item = null
  }, new Constraints {
    grid = (2, 2)
  })
  add(colorPanel(Color.BLUE), new Constraints {
    grid = (3, 2)
  })

  private def colorPanel(color: Color) = new BorderPanel {
    preferredSize = (21, 21)

    override protected def paintComponent(g: _root_.scala.swing.Graphics2D) {
      g.setColor(color)
      g.fillRect(0, 0, size.width, size.height)
    }
  }
}
