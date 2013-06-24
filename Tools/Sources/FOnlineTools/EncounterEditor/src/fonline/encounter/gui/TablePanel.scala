package fonline.encounter.gui

import fonline.encounter.entity._
import fonline.encounter.model._
import fonline.entity._
import java.awt.Color
import javax.swing.table.AbstractTableModel
import javax.swing.{SwingConstants, JLabel, BorderFactory}
import scala.Some
import scala.swing
import swing.BorderPanel.Position
import swing.ListView.{IntervalMode, Renderer}
import swing.Swing._
import swing.Table.ElementMode
import swing.{Table => TableView, _}
import event.TableRowsSelected
import event.{ListSelectionChanged, TableRowsSelected}
import fonline.model._
import scala.Some
import fonline.model.ItemRemoved
import fonline.model.ItemAdded
import fonline.model.ItemSelectionChanged
import fonline.gui.EntityDialog


/**
 * User: mikewall
 * Date: 10/24/12
 * Time: 8:50 PM
 */
class TablePanel(tableModel: ItemModel[Table],
                 gmTextMessageModel: ItemModel[TextMessage],
                 locationModel: ItemModel[Location],
                 groupModel: ItemModel[Group],
                 lvarModel: ItemModel[Var],
                 gvarModel: ItemModel[Var],
                 paramModel: ItemModel[Param]) extends BorderPanel {
  private def currentTable = tableModel.selectedItem

  tableModel.reactions += {
    case ItemSelectionChanged(_) => refresh()
    case ItemChanged(_) => refresh()
  }

  private def refresh() {
    val table = currentTable.getOrElse(new Table(""))
    tablesUI.peer.setSelectedIndex(tablesUI.listData.indexWhere(_ eq table))
    tablesUI.peer.scrollRectToVisible(tablesUI.peer.getCellBounds(tablesUI.peer.getSelectedIndex, tablesUI.peer.getSelectedIndex))
    locationsUI.listData = table.locations
    encounterModel.items = table.encounters
  }

  private val tablesUI = new ListView[Table](tableModel.items) {
    renderer = Renderer(table => table.name)
    selection.intervalMode = IntervalMode.Single
    selection.reactions += {
      case ListSelectionChanged(_, _, adjusting) if !adjusting =>
        tableModel.selectedItem = selection.items.headOption
    }
  }
  private val encounterModel = new ItemModel[Encounter](Nil)
  encounterModel.reactions += {
    case ItemAdded(encounter: Encounter) => currentTable match {
      case Some(table) => table.encounters :+= encounter
      case None =>
    }
    case ItemRemoved(encounter: Encounter) => currentTable match {
      case Some(table) => table.encounters = table.encounters.filterNot(_ eq encounter)
      case None =>
    }
  }
  private val encountersUI = new TableView {
    model = new AbstractTableModel {
      def getRowCount = encounterModel.size

      def getColumnCount = 2

      override def getColumnName(column: Int) = column match {
        case 0 => "Name"
        case 1 => "%"
      }

      def getValueAt(rowIndex: Int, columnIndex: Int) = columnIndex match {
        case 0 => encounterModel.items(rowIndex).text.getOrElse(new TextMessage(0, "")).text
        case 1 => encounterModel.items(rowIndex).chance.toString
      }
    }
    peer.getColumnModel.getColumn(1).setMaxWidth(30)
    peer.getColumnModel.getColumn(1).setMinWidth(30)
    selection.intervalMode = TableView.IntervalMode.Single
    selection.elementMode = ElementMode.Row
    selection.reactions += {
      case TableRowsSelected(_, _, adjusting) if (!adjusting) => encounterModel.selectedItem = selection.rows.headOption match {
        case Some(row) => Option(encounterModel.items(row))
        case None => None
      }
    }

    encounterModel.reactions += {
      case ItemsChanged(_, _) => model.asInstanceOf[AbstractTableModel].fireTableDataChanged()
      case ItemAdded(_) => model.asInstanceOf[AbstractTableModel].fireTableDataChanged()
      case ItemRemoved(_) => model.asInstanceOf[AbstractTableModel].fireTableDataChanged()
      case ItemChanged(_) => repaint()
    }

    override protected def rendererComponent(isSelected: Boolean, focused: Boolean, row: Int, column: Int) = {
      val c = super.rendererComponent(isSelected, focused, row, column)
      c.peer.asInstanceOf[JLabel].setHorizontalAlignment(column match {
        case 0 => SwingConstants.LEFT
        case 1 => SwingConstants.RIGHT
      })
      c
    }
  }
  private val locationsUI = new ListView[Location]() {
    renderer = Renderer(location => location.name)
  }
  private lazy val locationDialog = new EntityDialog[EntityHolder[Location]](new LocationEditor(locationModel), "Location") {
    centerOnScreen()
  }
  private val encounterEditor = new EncounterEditor(encounterModel, gmTextMessageModel, locationModel, groupModel, lvarModel, gvarModel, paramModel)

  private var copiedLocations = List[Location]()

  import fonline.gui.ButtonFactory.Button

  add(new BorderPanel {
    add(new ScrollPane(tablesUI) {
      preferredSize = (200, 100)
    }, Position.Center)
  }, Position.West)
  add(new BorderPanel {
    border = BorderFactory.createTitledBorder("Table")
    add(new BorderPanel {
      add(new BorderPanel() {
        border = BorderFactory.createTitledBorder("Encounters")
        add(new ScrollPane(encountersUI) {
          preferredSize = (200, 100)
          peer.getViewport.setBackground(Color.WHITE)
        }, Position.Center)
        add(new FlowPanel(FlowPanel.Alignment.Right)(
          Button("Add") {
            currentTable match {
              case Some(table) => {
                encounterModel.addItem(new Encounter)
                encountersUI.selection.rows += encounterModel.items.size - 1
              }
              case None =>
            }
          },
          Button("Remove") {
            currentTable match {
              case Some(table) => encountersUI.selection.rows.headOption match {
                case Some(rowIndex) => encounterModel.removeItem(encounterModel.items(rowIndex))
                case None =>
              }
              case None =>
            }
          }
        ) {
          vGap = 1
          hGap = 1
        }, Position.South)
      }, Position.Center)
      add(new BorderPanel {
        border = BorderFactory.createTitledBorder("Locations")
        add(new ScrollPane(locationsUI) {
          preferredSize = (100, 100)
        }, Position.Center)
        add(new FlowPanel(FlowPanel.Alignment.Right)(
          Button("Copy") {
            copiedLocations = locationsUI.listData.toList
          },
          Button("Paste") {
            currentTable match {
              case Some(table) => {
                table.locations = (table.locations ::: copiedLocations).distinct
                locationsUI.listData = table.locations
              }
              case None =>
            }
          },
          Button("Add") {
            currentTable match {
              case Some(table) => {
                val location = new EntityHolder[Location](if (table.locations.isEmpty) locationModel.items.head else table.locations.last)
                locationDialog.showFor(location) match {
                  case EntityDialog.OK => {
                    table.locations = table.locations ::: List(location.entity)
                    locationsUI.listData = table.locations
                  }
                  case EntityDialog.Cancel =>
                }
              }
              case None =>
            }
          },
          Button("Remove") {
            currentTable match {
              case Some(table) => {
                table.locations = table.locations.filterNot(location => locationsUI.selection.items.contains(location))
                locationsUI.listData = table.locations
              }
              case None =>
            }
          }
        ) {
          vGap = 1
          hGap = 1
        }, Position.South)
      }, Position.South)
    }, Position.West)
    add(new BorderPanel {
      border = BorderFactory.createTitledBorder("Encounter")

      add(encounterEditor, Position.Center)
    }, Position.Center)
  }, Position.Center)
}