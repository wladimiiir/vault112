package fonline.encounter.gui

import fonline.encounter.entity._
import java.text.NumberFormat
import javax.swing.table.AbstractTableModel
import javax.swing.{SwingConstants, JLabel, BorderFactory}
import scala.Option
import scala.Some
import swing.BorderPanel.Position
import swing.GridBagPanel.{Fill, Anchor}
import swing.ListView.Renderer
import swing.Swing._
import swing.Table.{ElementMode, IntervalMode}
import swing._
import event.ButtonClicked
import event.SelectionChanged
import event.TableRowsSelected
import swing.Table
import event._
import fonline.entity._
import fonline.entity.Dialog
import fonline.model._
import scala.Some
import fonline.gui.EntityDialog

/**
 *
 * @author mikewall
 * @since 29.10.2012, 16:30
 * @version 1.0
 */
class CritterEditor(critterModel: ItemModel[Critter],
                    npcModel: ItemModel[Npc],
                    dialogModel: ItemModel[Dialog],
                    roleModel: ItemModel[Role],
                    aiModel: ItemModel[AI],
                    bagModel: ItemModel[Bag],
                    itemProtoModel: ItemModel[ItemProto],
                    protected val lvarModel: ItemModel[Var],
                    protected val gvarModel: ItemModel[Var],
                    protected val paramModel: ItemModel[Param]) extends GridBagPanel with ChecksComponentMixin {

  private var copiedItems: List[Item] = Nil

  private def currentCritter = critterModel.selectedItem

  protected def canAdd = currentCritter.isDefined

  private val itemModel = new ItemModel[Item](Nil) {
    reactions += {
      case ItemAdded(item: Item) => currentCritter match {
        case Some(critter) => critter.children :+= item
        case None =>
      }
      case ItemRemoved(item: Item) => currentCritter match {
        case Some(critter) => critter.children = critter.children.filterNot(_ eq item)
        case None =>
      }
      case ItemsChanged(_, items: List[Item]) => currentCritter match {
        case Some(critter) => critter.children = items
        case None =>
      }
    }
  }
  protected val checkModel = new ItemModel[Check](Nil) {
    reactions += {
      case ItemAdded(check: Check) => currentCritter match {
        case Some(critter) => critter.checks :+= check
        case None =>
      }
      case ItemRemoved(check: Check) => currentCritter match {
        case Some(critter) => critter.checks = critter.checks.filterNot(_ eq check)
        case None =>
      }
    }
  }

  critterModel.reactions += {
    case ItemSelectionChanged(selection: Option[Critter]) => selection match {
      case Some(critter) => init(critter)
      case None => init(new Critter(None))
    }
  }

  private def init(critter: Critter) {
    npcUI.selection.item = critter.npc.getOrElse(null)
    ratioUI.text = critter.ratio.toString
    dialogUI.selection.item = critter.dialog.getOrElse(null)
    roleUI.selection.item = critter.role.getOrElse(null)
    aiUI.selection.item = critter.ai.getOrElse(null)
    bagUI.selection.item = critter.bag.getOrElse(null)
    distanceUI.text = critter.distance.toString
    scriptUI.text = critter.script.getOrElse("")
    deadUI.selected = critter.dead
    itemModel.items = critter.children.filter(_.isInstanceOf[Item]).map(_.asInstanceOf[Item])
    checkModel.items = critter.checks
  }

  private val npcUI = new ComboBox[Npc](npcModel.items) {
    renderer = Renderer(npc => if (npc eq null) "" else npc.name)
    selection.reactions += {
      case SelectionChanged(_) => currentCritter match {
        case Some(critter) => critter.npc = Option(selection.item)
        case None =>
      }
    }
  }
  private val ratioUI = new FormattedTextField(NumberFormat.getIntegerInstance) {
    reactions += {
      case ValueChanged(_) if !text.isEmpty && text.forall(_.isDigit) => currentCritter match {
        case Some(critter) => critter.ratio = text.toInt
        case None =>
      }
    }
  }
  private val dialogUI = new ComboBox[Dialog](dialogModel.items) {
    renderer = Renderer(dialog => if (dialog eq null) "" else dialog.name)
    selection.reactions += {
      case SelectionChanged(_) => currentCritter match {
        case Some(critter) => critter.dialog = Option(selection.item)
        case None =>
      }
    }
  }
  private val roleUI = new ComboBox[Role](roleModel.items) {
    renderer = Renderer(role => if (role eq null) "" else role.name)
    selection.reactions += {
      case SelectionChanged(_) => currentCritter match {
        case Some(critter) => critter.role = Option(selection.item)
        case None =>
      }
    }
  }
  private val aiUI = new ComboBox[AI](aiModel.items) {
    renderer = Renderer(ai => if (ai eq null) "" else ai.name)
    selection.reactions += {
      case SelectionChanged(_) => currentCritter match {
        case Some(critter) => critter.ai = Option(selection.item)
        case None =>
      }
    }
  }
  private val bagUI = new ComboBox[Bag](bagModel.items) {
    renderer = Renderer(bag => if (bag eq null) "<not specified>" else bag.name)
    selection.reactions += {
      case SelectionChanged(_) => currentCritter match {
        case Some(critter) => critter.bag = Option(selection.item)
        case None =>
      }
    }
  }
  private val distanceUI = new FormattedTextField(NumberFormat.getIntegerInstance) {
    reactions += {
      case ValueChanged(_) if !text.isEmpty && text.forall(_.isDigit) => currentCritter match {
        case Some(critter) => critter.distance = text.toInt
        case None =>
      }
    }
  }
  private val scriptUI = new TextField() {
    reactions += {
      case ValueChanged(_) => currentCritter match {
        case Some(critter) => critter.script = if (text.isEmpty) None else Option(text)
        case None =>
      }
    }
  }
  private val deadUI = new CheckBox("Dead") {
    reactions += {
      case ButtonClicked(_) => currentCritter match {
        case Some(critter) => critter.dead = selected
        case None =>
      }
    }
  }
  private val itemsUI = new Table {
    model = new AbstractTableModel {
      def getRowCount = itemModel.size

      def getColumnCount = 5

      override def getColumnName(column: Int) = column match {
        case 0 => "Item"
        case 1 => "Slot"
        case 2 => "Chance"
        case 3 => "Min"
        case 4 => "Max"
      }

      def getValueAt(rowIndex: Int, columnIndex: Int) = {
        val item = itemModel.items(rowIndex)
        columnIndex match {
          case 0 => item.proto.getOrElse(new ItemProto(0, "")).name
          case 1 => item.slot
          case 2 => item.checks.find(_.isInstanceOf[RandomCheck]) match {
            case Some(RandomCheck(value)) => value + " %"
            case _ => "100 %"
          }
          case 3 => item.minCount.toString
          case 4 => item.maxCount.toString
        }
      }
    }
    peer.getColumnModel.getColumn(3).setMinWidth(30)
    peer.getColumnModel.getColumn(3).setMaxWidth(30)
    peer.getColumnModel.getColumn(4).setMinWidth(30)
    peer.getColumnModel.getColumn(4).setMaxWidth(30)

//    selection.intervalMode = IntervalMode.Single
    selection.elementMode = ElementMode.Row
    selection.reactions += {
      case TableRowsSelected(_, _, adjusting) if !adjusting => itemModel.selectedItem = selection.rows.headOption match {
        case Some(row) => Option(itemModel.items(row))
        case None => None
      }
    }
    itemModel.reactions += {
      case ItemAdded(_) => model.asInstanceOf[AbstractTableModel].fireTableDataChanged()
      case ItemRemoved(_) => model.asInstanceOf[AbstractTableModel].fireTableDataChanged()
      case ItemsChanged(_, _) => model.asInstanceOf[AbstractTableModel].fireTableDataChanged()
      case ItemChanged(_) => repaint()
    }

    override protected def rendererComponent(isSelected: Boolean, focused: Boolean, row: Int, column: Int) = {
      val c = super.rendererComponent(isSelected, focused, row, column)
      column match {
        case 2 => c.peer.asInstanceOf[JLabel].setHorizontalAlignment(SwingConstants.RIGHT)
        case 3 => c.peer.asInstanceOf[JLabel].setHorizontalAlignment(SwingConstants.RIGHT)
        case 4 => c.peer.asInstanceOf[JLabel].setHorizontalAlignment(SwingConstants.RIGHT)
        case _ => c.peer.asInstanceOf[JLabel].setHorizontalAlignment(SwingConstants.LEFT)
      }
      c
    }

    keys.reactions += {
      case KeyReleased(_, Key.C, Key.Modifier.Control, _) => copiedItems = selection.rows.map(row => itemModel.items(row)).toList
      case KeyReleased(_, Key.V, Key.Modifier.Control, _) => copiedItems.foreach(item => {
        val newItem = new Item(item.proto)
        newItem.minCount = item.minCount
        newItem.maxCount = item.maxCount
        newItem.slot = item.slot
        newItem.checks = List() ::: item.checks
        itemModel.addItem(newItem)
      })
    }
  }

  keys.reactions += {
    case KeyReleased(_, Key.V, Key.Modifier.Control, _) => println("pressed")
  }

  private val itemEditorDialog = new EntityDialog[Item](new CritterItemEditor(itemProtoModel), "Critter item") {
    centerOnScreen()
  }

  listenTo(npcUI.selection, ratioUI, dialogUI.selection, roleUI.selection, aiUI.selection, distanceUI, scriptUI, deadUI)

  reactions += {
    case event: ComponentEvent => currentCritter match {
      case Some(critter) => critterModel.fireItemChanged(critter)
      case None =>
    }
  }

  private implicit def toLabel(text: String): Label = new Label(text)

  import fonline.gui.ButtonFactory.Button

  border = BorderFactory.createEmptyBorder(3, 3, 3, 3)
  add("NPC:", new Constraints() {
    grid = (0, 0)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(npcUI, new Constraints() {
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
  add("Role:", new Constraints() {
    grid = (0, 3)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(new BorderPanel {
    add(roleUI, Position.Center)
    add(Button("X") {
      roleUI.selection.item = null
    }, Position.East)
  }, new Constraints() {
    grid = (1, 3)
    fill = Fill.Horizontal
  })
  add("AI:", new Constraints() {
    grid = (0, 4)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(new BorderPanel {
    add(aiUI, Position.Center)
    add(Button("X") {
      aiUI.selection.item = null
    }, Position.East)
  }, new Constraints() {
    grid = (1, 4)
    fill = Fill.Horizontal
  })
  add("Bag:", new Constraints() {
    grid = (0, 5)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(new BorderPanel {
    add(bagUI, Position.Center)
    add(Button("X") {
      bagUI.selection.item = null
    }, Position.East)
  }, new Constraints() {
    grid = (1, 5)
    fill = Fill.Horizontal
  })
  add("Distance:", new Constraints() {
    grid = (0, 6)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(distanceUI, new Constraints() {
    grid = (1, 6)
    fill = Fill.Horizontal
  })
  add("Script:", new Constraints() {
    grid = (0, 7)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(scriptUI, new Constraints() {
    grid = (1, 7)
    fill = Fill.Horizontal
  })
  add(deadUI, new Constraints() {
    grid = (0, 8)
    gridwidth = 2
    anchor = Anchor.East
  })
  add(new BorderPanel {
    add(new BorderPanel {
      border = BorderFactory.createTitledBorder("Items")
      add(new ScrollPane(itemsUI) {
        preferredSize = (100, 80)
        mouse.clicks.reactions += {
          case MouseClicked(_, _, _, 1, _) => itemsUI.requestFocus()
        }
      }, Position.Center)
      add(new FlowPanel(FlowPanel.Alignment.Right)(
        Button("Add") {
          currentCritter match {
            case Some(critter) => {
              val item = new Item(None)
              itemEditorDialog.showFor(item) match {
                case EntityDialog.OK => itemModel.addItem(item)
                case EntityDialog.Cancel =>
              }
            }
            case None =>
          }
        },
        Button("Edit") {
          itemsUI.selection.rows.headOption match {
            case Some(row) => {
              val item = itemModel.items(row)
              itemEditorDialog.showFor(item) match {
                case EntityDialog.OK => itemModel.fireItemChanged(item)
                case EntityDialog.Cancel =>
              }
            }
            case None =>
          }
        },
        Button("Remove") {
          itemModel.items = itemModel.items.filterNot(itemsUI.selection.rows.toList.sorted.map(row => itemModel.items(row)) contains)
        }
      ) {
        vGap = 1
        hGap = 1
      }, Position.South)
    }, Position.North)
    add(createChecksComponent, Position.South)
  }, new Constraints {
    grid = (0, 9)
    gridwidth = 2
    fill = Fill.Horizontal
  })
}