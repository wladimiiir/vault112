package fonline.trader.gui

import swing._
import event.TableRowsSelected
import event.TableRowsSelected
import fonline.model._
import fonline.trader.entity.{BuyItem, Item, Trader}
import fonline.entity._
import swing.BorderPanel.Position
import javax.swing.table.{TableCellRenderer, DefaultTableCellRenderer, AbstractTableModel}
import swing.Table.ElementMode
import swing.event.TableRowsSelected
import swing.Swing._
import fonline.model.ItemRemoved
import fonline.model.ItemAdded
import fonline.model.ItemsChanged
import scala.Some
import javax.swing.{JTable, BorderFactory, SwingConstants, JLabel}
import java.awt.FlowLayout
import fonline.model.ItemsChanged
import fonline.model.ItemChanged
import scala.Some
import fonline.model.ItemRemoved
import fonline.model.ItemAdded
import fonline.model.ItemSelectionChanged
import fonline.gui.EntityDialog
import swing.Dialog.Result
import fonline.model.ItemsChanged
import fonline.model.ItemChanged
import scala.Some
import fonline.model.ItemRemoved
import fonline.model.ItemAdded
import fonline.model.ItemSelectionChanged

/**
 * User: wladimiiir
 * Date: 12/6/12
 * Time: 11:12 PM
 */
class TraderPanel(traderModel: ItemModel[Trader], npcModel: ItemModel[Npc], itemProtoModel: ItemModel[ItemProto], itemProtoGroupModel:ItemModel[ItemProtoGroup]) extends BorderPanel {
  private def currentTrader = traderModel.selectedItem

  private val tradersUI = new Table {
    model = new AbstractTableModel {
      def getRowCount = traderModel.size

      def getColumnCount = 2

      override def getColumnName(column: Int) = column match {
        case 0 => "Npc"
        case 1 => "Item count"
      }

      def getValueAt(rowIndex: Int, columnIndex: Int) = columnIndex match {
        case 0 => traderModel.items(rowIndex).npc.getOrElse(new Npc(0, "not defined")).name
        case 1 => traderModel.items(rowIndex).items.size.toString
      }
    }
    peer.getColumnModel.getColumn(1).setMaxWidth(73)
    peer.getColumnModel.getColumn(1).setMinWidth(73)
    selection.intervalMode = Table.IntervalMode.Single
    selection.elementMode = ElementMode.Row
    selection.reactions += {
      case TableRowsSelected(_, _, adjusting) if (!adjusting) => traderModel.selectedItem = selection.rows.headOption match {
        case Some(row) => Option(traderModel.items(row))
        case None => None
      }
    }

    traderModel.reactions += {
      case ItemsChanged(_, _) => model.asInstanceOf[AbstractTableModel].fireTableDataChanged()
      case ItemAdded(_) => model.asInstanceOf[AbstractTableModel].fireTableDataChanged()
      case ItemRemoved(_) => model.asInstanceOf[AbstractTableModel].fireTableDataChanged()
      case ItemChanged(_) => repaint()
    }

    override protected def rendererComponent(isSelected: Boolean, focused: Boolean, row: Int, column: Int) = {
      val c = super.rendererComponent(isSelected, focused, row, column)
      c.tooltip = c.peer.asInstanceOf[JLabel].getText
      c.peer.asInstanceOf[JLabel].setHorizontalAlignment(column match {
        case 0 => SwingConstants.LEFT
        case 1 => SwingConstants.RIGHT
      })
      c
    }
  }

  private val itemModel = new ItemModel[Item](Nil)
  private lazy val itemDialog = new EntityDialog[Item](new ItemEditor(itemProtoModel), "Refresh item") {
    centerOnScreen()
  }

  private def currentItem = itemModel.selectedItem

  traderModel.reactions += {
    case ItemSelectionChanged(trader: Option[Trader]) => itemModel.items = trader.getOrElse(new Trader).items
  }
  itemModel.reactions += {
    case ItemAdded(item: Item) => currentTrader match {
      case Some(trader) => trader.items :+= item
      case None =>
    }
    case ItemRemoved(item: Item) => currentTrader match {
      case Some(trader) => trader.items = trader.items.filterNot(_ eq item)
      case None =>
    }
  }

  private val itemsUI = new Table {
    model = new AbstractTableModel {
      def getRowCount = itemModel.size

      def getColumnCount = 5

      override def getColumnName(column: Int) = column match {
        case 0 => "Name"
        case 1 => "Min"
        case 2 => "Max"
        case 3 => "%"
        case 4 => "Caps"
      }

      def getValueAt(rowIndex: Int, columnIndex: Int) = columnIndex match {
        case 0 => itemModel.items(rowIndex).proto.name
        case 1 => itemModel.items(rowIndex).minCount.toString
        case 2 => itemModel.items(rowIndex).maxCount.toString
        case 3 => itemModel.items(rowIndex).spawnChance.toString
        case 4 => itemModel.items(rowIndex).onlyCaps.asInstanceOf[AnyRef]
      }
    }

    peer.setDefaultRenderer(classOf[Boolean], new TableCellRenderer {
      private val checkBox = new CheckBox

      def getTableCellRendererComponent(table: JTable, value: Any, isSelected: Boolean, hasFocus: Boolean, row: Int, column: Int) = value match {
        case booleanValue: Boolean => {
          checkBox.selected = booleanValue
          checkBox.peer
        }
        case _ => new Label("").peer
      }
    })
    peer.getColumnModel.getColumn(0).setMinWidth(150)
    selection.intervalMode = Table.IntervalMode.Single
    selection.elementMode = ElementMode.Row
    selection.reactions += {
      case TableRowsSelected(_, _, adjusting) if (!adjusting) => itemModel.selectedItem = selection.rows.headOption match {
        case Some(row) => Option(itemModel.items(row))
        case None => None
      }
    }

    itemModel.reactions += {
      case ItemsChanged(_, _) => model.asInstanceOf[AbstractTableModel].fireTableDataChanged()
      case ItemAdded(_) => model.asInstanceOf[AbstractTableModel].fireTableDataChanged()
      case ItemRemoved(_) => model.asInstanceOf[AbstractTableModel].fireTableDataChanged()
      case ItemChanged(_) => repaint()
    }

    override protected def rendererComponent(isSelected: Boolean, focused: Boolean, row: Int, column: Int) = {
      val c = super.rendererComponent(isSelected, focused, row, column)
      c.peer match {
        case label: JLabel => {
          c.tooltip = label.getText
          label.setHorizontalAlignment(column match {
            case 0 => SwingConstants.LEFT
            case 1 => SwingConstants.RIGHT
            case 2 => SwingConstants.RIGHT
            case 3 => SwingConstants.RIGHT
            case 4 => SwingConstants.CENTER
          })
        }
        case _ =>
      }
      c
    }
  }

  private val buyItemModel = new ItemModel[BuyItem](Nil)
  private lazy val buyItemDialog = new EntityDialog[BuyItem](new BuyItemEditor(itemProtoModel), "Buy item") {
    centerOnScreen()
  }

  private def currentBuyItem = buyItemModel.selectedItem

  traderModel.reactions += {
    case ItemSelectionChanged(trader: Option[Trader]) => buyItemModel.items = trader.getOrElse(new Trader).buyItems
  }
  buyItemModel.reactions += {
    case ItemAdded(buyItem: BuyItem) => currentTrader match {
      case Some(trader) => trader.buyItems :+= buyItem
      case None =>
    }
    case ItemRemoved(buyItem: BuyItem) => currentTrader match {
      case Some(trader) => trader.buyItems = trader.buyItems.filterNot(_ eq buyItem)
      case None =>
    }
  }

  private val buyItemsUI = new Table {
    model = new AbstractTableModel {
      def getRowCount = buyItemModel.size

      def getColumnCount = 1

      override def getColumnName(column: Int) = column match {
        case 0 => "Proto"
      }

      def getValueAt(rowIndex: Int, columnIndex: Int) = columnIndex match {
        case 0 => buyItemModel.items(rowIndex).proto.name
      }
    }
    selection.intervalMode = Table.IntervalMode.Single
    selection.elementMode = ElementMode.Row
    selection.reactions += {
      case TableRowsSelected(_, _, adjusting) if (!adjusting) => buyItemModel.selectedItem = selection.rows.headOption match {
        case Some(row) => Option(buyItemModel.items(row))
        case None => None
      }
    }

    buyItemModel.reactions += {
      case ItemsChanged(_, _) => model.asInstanceOf[AbstractTableModel].fireTableDataChanged()
      case ItemAdded(_) => model.asInstanceOf[AbstractTableModel].fireTableDataChanged()
      case ItemRemoved(_) => model.asInstanceOf[AbstractTableModel].fireTableDataChanged()
      case ItemChanged(_) => repaint()
    }

    override protected def rendererComponent(isSelected: Boolean, focused: Boolean, row: Int, column: Int) = {
      val c = super.rendererComponent(isSelected, focused, row, column)
      c.tooltip = c.peer.asInstanceOf[JLabel].getText
      c.peer.asInstanceOf[JLabel].setHorizontalAlignment(column match {
        case 0 => SwingConstants.LEFT
      })
      c
    }
  }

  private lazy val itemProtoGroupDialog = new EntityDialog[EntityHolder[ItemProtoGroup]](new ItemProtoGroupEditor(itemProtoGroupModel), "Choose proto group") {
    centerOnScreen()
  }

  import fonline.gui.ButtonFactory.Button

  add(new BorderPanel {
    border = BorderFactory.createTitledBorder("Traders")
    add(new ScrollPane(tradersUI) {
      preferredSize = (250, 100)
    }, Position.Center)
    add(new FlowPanel(FlowPanel.Alignment.Right)(
      Button("Add") {
        traderModel.addItem(new Trader)
        tradersUI.selection.rows.clear()
        tradersUI.selection.rows += traderModel.items.size - 1
      },
      Button("Remove") {
        currentTrader match {
          case Some(trader) => traderModel.removeItem(trader)
          case None =>
        }
      }
    ){
      vGap = 0
      hGap = 0
    }, Position.South)
  }, Position.West)
  add(new BorderPanel {
    border = BorderFactory.createTitledBorder("Trader")
    add(new TraderEditor(traderModel, npcModel), Position.North)
    add(new BorderPanel {
      add(new BorderPanel {
        border = BorderFactory.createTitledBorder("Refresh items")
        add(new ScrollPane(itemsUI), Position.Center)
        add(new FlowPanel(FlowPanel.Alignment.Right)(
          Button("Add") {
            currentTrader match {
              case Some(trader) => {
                val item = new Item(itemProtoModel.items.head)
                itemDialog.showFor(item) match {
                  case EntityDialog.OK => itemModel.addItem(item)
                  case _ =>
                }
              }
              case None =>
            }
          },
          Button("Edit") {
            currentItem match {
              case Some(item) => itemDialog.showFor(item) match {
                case EntityDialog.OK => itemModel.fireItemChanged(item)
                case _ =>
              }
              case None =>
            }
          },
          Button("Remove") {
            currentItem match {
              case Some(item) => itemModel.removeItem(item)
              case None =>
            }
          }
        ){
          vGap = 0
          hGap = 0
        }, Position.South)
      }, Position.Center)
      add(new BorderPanel {
        preferredSize = (200, 100)
        border = BorderFactory.createTitledBorder("Buy items")
        add(new ScrollPane(buyItemsUI), Position.Center)
        add(new FlowPanel(FlowPanel.Alignment.Right)(
          Button("Add group") {
            currentTrader match {
              case Some(trader) => {
                val holder = new EntityHolder[ItemProtoGroup](itemProtoGroupModel.items.head)
                itemProtoGroupDialog.showFor(holder) match {
                  case EntityDialog.OK => {
                    for (proto <- holder.entity.protos if !buyItemModel.items.exists(_.proto eq proto)) {
                      buyItemModel.addItem(new BuyItem(proto))
                    }
                  }
                  case _ =>
                }
              }
              case None =>
            }
          },
          new Separator(Orientation.Vertical),
          Button("Add") {
            currentTrader match {
              case Some(trader) => {
                val buyItem = new BuyItem(itemProtoModel.items.head)
                buyItemDialog.showFor(buyItem) match {
                  case EntityDialog.OK => buyItemModel.addItem(buyItem)
                  case _ =>
                }
              }
              case None =>
            }
          },
          Button("Edit") {
            currentBuyItem match {
              case Some(buyItem) => buyItemDialog.showFor(buyItem) match {
                case EntityDialog.OK => buyItemModel.fireItemChanged(buyItem)
                case _ =>
              }
              case None =>
            }
          },
          Button("Remove") {
            currentBuyItem match {
              case Some(buyItem) => buyItemModel.removeItem(buyItem)
              case None =>
            }
          }
        ){
          vGap = 0
          hGap = 0
        }, Position.South)
      }, Position.East)
    }, Position.Center)
  }, Position.Center)
}
