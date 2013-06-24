package fonline.encounter.gui

import fonline.encounter.entity._
import fonline.entity._
import fonline.entity.Dialog
import fonline.model.{ItemRemoved, ItemAdded, ItemSelectionChanged, ItemModel}
import java.text.NumberFormat
import javax.swing.BorderFactory
import swing.BorderPanel.Position
import swing.GridBagPanel.{Fill, Anchor}
import swing.ListView.Renderer
import swing._
import event.{Event, SelectionChanged, ValueChanged}

/**
 *
 * @author mikewall
 * @since 29.10.2012, 16:30
 * @version 1.0
 */
class ItemEditor(itemModel: ItemModel[Item],
                 itemProtoModel: ItemModel[ItemProto],
                 dialogModel: ItemModel[Dialog],
                 protected val lvarModel: ItemModel[Var],
                 protected val gvarModel: ItemModel[Var],
                 protected val paramModel: ItemModel[Param]) extends GridBagPanel with ChecksComponentMixin {
  private def currentItem = itemModel.selectedItem

  protected def canAdd = currentItem.isDefined

  protected val checkModel = new ItemModel[Check](Nil) {
    reactions += {
      case ItemAdded(check: Check) => currentItem match {
        case Some(item) => item.checks :+= check
        case None =>
      }
      case ItemRemoved(check: Check) => currentItem match {
        case Some(item) => item.checks = item.checks.filterNot(_ eq check)
        case None =>
      }
    }
  }

  itemModel.reactions += {
    case ItemSelectionChanged(selection: Option[Item]) => selection match {
      case Some(item) => init(item)
      case None => init(new Item(None))
    }
  }

  private def init(item: Item) {
    protoUI.selection.item = item.proto.getOrElse(null)
    ratioUI.text = item.ratio.toString
    dialogUI.selection.item = item.dialog.getOrElse(null)
    distanceUI.text = item.distance.toString
    scriptUI.text = item.script.getOrElse("")
    checkModel.items = item.checks
  }

  private val protoUI = new ComboBox[ItemProto](itemProtoModel.items) {
    renderer = Renderer(proto => if (proto eq null) "" else proto.name)
    selection.reactions += {
      case SelectionChanged(_) => currentItem match {
        case Some(item) => item.proto = Option(selection.item)
        case None =>
      }
    }
  }
  private val ratioUI = new FormattedTextField(NumberFormat.getIntegerInstance) {
    reactions += {
      case ValueChanged(_) if !text.isEmpty && text.forall(_.isDigit) => currentItem match {
        case Some(item) => item.ratio = text.toInt
        case None =>
      }
    }
  }
  private val dialogUI = new ComboBox[Dialog](dialogModel.items) {
    renderer = Renderer(dialog => if (dialog eq null) "" else dialog.name)
    selection.reactions += {
      case SelectionChanged(_) => currentItem match {
        case Some(item) => item.dialog = Option(selection.item)
        case None =>
      }
    }
  }
  private val distanceUI = new FormattedTextField(NumberFormat.getIntegerInstance) {
    reactions += {
      case ValueChanged(_) if !text.isEmpty && text.forall(_.isDigit) => currentItem match {
        case Some(item) => item.distance = text.toInt
        case None =>
      }
    }
  }
  private val scriptUI = new TextField() {
    reactions += {
      case ValueChanged(_) => currentItem match {
        case Some(item) => item.script = if (text.isEmpty) None else Option(text)
        case None =>
      }
    }
  }
  listenTo(protoUI.selection, ratioUI, dialogUI.selection, distanceUI, scriptUI)
  reactions += {
    case event: Event => currentItem match {
      case Some(item) => itemModel.fireItemChanged(item)
      case None =>
    }
  }

  private implicit def toLabel(text: String): Label = new Label(text)

  import fonline.gui.ButtonFactory.Button

  border = BorderFactory.createEmptyBorder(3, 3, 3, 3)
  add("ProtoID:", new Constraints() {
    grid = (0, 0)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(protoUI, new Constraints() {
    grid = (1, 0)
    fill = Fill.Horizontal
  })
  add("Ratio:", new Constraints() {
    grid = (0, 1)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(ratioUI, new Constraints() {
    grid = (1, 1)
    fill = Fill.Horizontal
  })
  add("Dialog:", new Constraints() {
    grid = (0, 2)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(new BorderPanel {
    add(dialogUI, Position.Center)
    add(Button("X") {
      dialogUI.selection.item = null
    }, Position.East)
  }, new Constraints() {
    grid = (1, 2)
    fill = Fill.Horizontal
  })
  add("Distance:", new Constraints() {
    grid = (0, 3)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(distanceUI, new Constraints() {
    grid = (1, 3)
    fill = Fill.Horizontal
  })
  add("Script:", new Constraints() {
    grid = (0, 4)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(scriptUI, new Constraints() {
    grid = (1, 4)
    fill = Fill.Horizontal
  })
  add(createChecksComponent, new Constraints() {
    grid = (0, 5)
    gridwidth = 2
    fill = Fill.Horizontal
  })
}