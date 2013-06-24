package fonline.encounter.gui

import fonline.encounter.entity._
import fonline.encounter.model._
import java.awt.Color
import java.text.NumberFormat
import javax.swing._
import javax.swing.table.AbstractTableModel
import scala.Some
import swing.BorderPanel.Position
import swing.GridBagPanel.{Fill, Anchor}
import swing.ListView.{IntervalMode, Renderer}
import swing.Swing._
import swing._
import swing.Table
import swing.event._
import fonline.entity.{Param, Var, Location, TextMessage}
import fonline.model._
import fonline.gui.EntityDialog

/**
 * User: mikewall
 * Date: 10/28/12
 * Time: 9:51 AM
 */
class EncounterEditor(encounterModel: ItemModel[Encounter],
                      gmTextMessageModel: ItemModel[TextMessage],
                      locationModel: ItemModel[Location],
                      groupModel: ItemModel[Group],
                      protected val lvarModel: ItemModel[Var],
                      protected val gvarModel: ItemModel[Var],
                      protected val paramModel: ItemModel[Param]) extends GridBagPanel with ChecksComponentMixin {
  private def currentEncounter = encounterModel.selectedItem

  protected def canAdd = currentEncounter.isDefined

  protected val checkModel = new ItemModel[Check](Nil) {
    reactions += {
      case ItemAdded(check: Check) => currentEncounter match {
        case Some(encounter) => encounter.checks :+= check
        case None =>
      }
      case ItemRemoved(check: Check) => currentEncounter match {
        case Some(encounter) => encounter.checks = encounter.checks.filterNot(_ eq check)
        case None =>
      }
    }
  }

  encounterModel.reactions += {
    case ItemSelectionChanged(_) => refresh()
  }

  private def refresh() {
    val encounter = currentEncounter.getOrElse(new Encounter)
    encounter.text match {
      case Some(text) => textUI.selection.index = texts.zipWithIndex.find(tuple => tuple._1.id == text.id) match {
        case Some((tm, index)) => index
        case None => -1
      }
      case None => textUI.selection.item = null
    }
    chanceUI.text = encounter.chance.toString
    locationUI.selection.item = encounter.location.getOrElse(null)
    encounterGroupModel.items = encounter.groups
    fightsUI.listData = encounter.fights
    checkModel.items = encounter.checks
  }

  private val texts = gmTextMessageModel.items
  private val textUI = new ComboBox[TextMessage](texts) {
    renderer = Renderer(tm => if (tm == null) "" else tm.text + " (" + tm.id + ")")
    preferredSize = (300, 21)
    selection.reactions += {
      case SelectionChanged(_) => currentEncounter match {
        case Some(encounter) => encounter.text = Option(selection.item)
        case None =>
      }
    }
  }
  private val chanceUI = new FormattedTextField(NumberFormat.getIntegerInstance) {
    reactions += {
      case ValueChanged(_) if !text.isEmpty && !text.exists(!_.isDigit) =>
        currentEncounter match {
          case Some(encounter) => encounter.chance = text.toInt
          case None =>
        }
    }
  }
  private val locationUI = new ComboBox[Location](locationModel.items) {
    renderer = Renderer(location => if (location eq null) "" else location.name)
    selection.reactions += {
      case SelectionChanged(_) => currentEncounter match {
        case Some(encounter) => encounter.location = Option(selection.item)
        case None =>
      }
    }
  }
  private val encounterGroupModel = new ItemModel[EncounterGroup](Nil)
  encounterGroupModel.reactions += {
    case ItemAdded(group: EncounterGroup) => currentEncounter match {
      case Some(encounter) => encounter.groups :+= group
      case None =>
    }
    case ItemRemoved(group: EncounterGroup) => currentEncounter match {
      case Some(encounter) => encounter.groups = encounter.groups.filterNot(_ eq group)
      case None =>
    }
  }
  private val groupsUI = new Table {
    model = new AbstractTableModel {
      def getRowCount = encounterGroupModel.size

      def getColumnCount = 3

      override def getColumnName(column: Int) = column match {
        case 0 => "Group"
        case 1 => "Min"
        case 2 => "Max"
      }

      def getValueAt(rowIndex: Int, columnIndex: Int) = columnIndex match {
        case 0 => encounterGroupModel.items(rowIndex).group.getOrElse(new Group("")).name
        case 1 => encounterGroupModel.items(rowIndex).minRatio.toString
        case 2 => encounterGroupModel.items(rowIndex).maxRatio.toString
      }
    }
    peer.getColumnModel.getColumn(1).setMinWidth(30)
    peer.getColumnModel.getColumn(1).setMaxWidth(30)
    peer.getColumnModel.getColumn(2).setMinWidth(30)
    peer.getColumnModel.getColumn(2).setMaxWidth(30)

    selection.reactions += {
      case TableRowsSelected(_, _, adjusting) if !adjusting => encounterGroupModel.selectedItem = selection.rows.headOption match {
        case Some(row) => Option(encounterGroupModel.items(row))
        case None => None
      }
    }

    encounterGroupModel.reactions += {
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
        case 2 => SwingConstants.RIGHT
      })
      c
    }
  }
  private lazy val encounterGroupDialog = new EntityDialog[EncounterGroup](new EncounterGroupEditor(groupModel), "Encounter group") {
    centerOnScreen()
  }
  private val fightsUI = new ListView[Fight]() {
    renderer = Renderer(fight => groupString(fight.groupFrom) + " \u2192 " + groupString(fight.groupTo))

    def groupString(group: EncounterGroup) = group.group.getOrElse(new Group("")).name + " (" + (currentEncounter.get.groups.indexOf(group) + 1) + ")"

    selection.intervalMode = IntervalMode.Single
  }

  listenTo(textUI.selection, chanceUI, locationUI.selection, fightsUI)
  reactions += {
    case event: ComponentEvent => encounterModel.selectedItem match {
      case Some(encounter) => encounterModel.fireItemChanged(encounter)
      case None =>
    }
  }
  refresh()

  import fonline.gui.ButtonFactory.Button

  border = BorderFactory.createEmptyBorder(3, 3, 3, 3)
  add(new Label("Text:"), new Constraints() {
    grid = (0, 0)
    anchor = Anchor.East
  })
  add(textUI, new Constraints() {
    grid = (1, 0)
    fill = Fill.Horizontal
  })
  add(new Label("Chance:"), new Constraints() {
    grid = (0, 1)
    anchor = Anchor.East
  })
  add(chanceUI, new Constraints() {
    grid = (1, 1)
    fill = Fill.Horizontal
  })
  add(new Label("Location:"), new Constraints() {
    grid = (0, 2)
    anchor = Anchor.East
  })
  add(new BorderPanel() {
    add(locationUI, Position.Center)
    add(Button("X")({
      locationUI.selection.item = null
    }), Position.East)
  }, new Constraints() {
    grid = (1, 2)
    fill = Fill.Horizontal
  })
  add(new BorderPanel() {
    border = BorderFactory.createTitledBorder("Groups")
    add(new ScrollPane(groupsUI) {
      preferredSize = (150, 100)
      peer.getViewport.setBackground(Color.WHITE)
    }, Position.Center)
    add(new FlowPanel(FlowPanel.Alignment.Right)(
      Button("Add") {
        currentEncounter match {
          case Some(encounter) => {
            val encounterGroup = new EncounterGroup(None, 0, 0)
            encounterGroupDialog.showFor(encounterGroup) match {
              case EntityDialog.OK => encounterGroupModel.addItem(encounterGroup)
              case EntityDialog.Cancel =>
            }
          }
          case None =>
        }
      },
      Button("Edit") {
        encounterGroupModel.selectedItem match {
          case Some(encounterGroup) => {
            encounterGroupDialog.showFor(encounterGroup) match {
              case EntityDialog.OK => encounterGroupModel.fireItemChanged(encounterGroup)
              case EntityDialog.Cancel =>
            }
          }
          case None =>
        }
      },
      Button("Remove") {
        encounterGroupModel.selectedItem match {
          case Some(encounterGroup) => currentEncounter match {
            case Some(encounter) => {
              encounterGroupModel.removeItem(encounterGroup)
            }
            case None =>
          }
          case None =>
        }
      }) {
      vGap = 1
      hGap = 1
    }, Position.South)
  }, new Constraints() {
    grid = (0, 3)
    gridwidth = 2
    weightx = 1.0
    weighty = 1.0
    fill = Fill.Both
  })
  add(new BorderPanel {
    border = BorderFactory.createTitledBorder("Fights")
    add(new ScrollPane(fightsUI), Position.Center)
    add(new FlowPanel(FlowPanel.Alignment.Right)(
      Button("Add") {
        currentEncounter match {
          case Some(encounter) => groupsUI.selection.rows.size match {
            case 2 => {
              encounter.fights = encounter.fights ::: List(new Fight(encounter.groups(groupsUI.selection.rows.toList(0)), encounter.groups(groupsUI.selection.rows.toList(1))))
              fightsUI.listData = encounter.fights
            }
            case _ => Dialog.showMessage(message = "Select 2 groups in Groups table.")
          }
          case None =>
        }
      },
      Button("Remove") {
        currentEncounter match {
          case Some(encounter) => fightsUI.selection.items.headOption match {
            case Some(fight) => {
              encounter.fights = encounter.fights.filterNot(f => f == fight)
              fightsUI.listData = encounter.fights
            }
            case None =>
          }
          case None =>
        }
      }
    ) {
      vGap = 1
      hGap = 1
    }, Position.South)
  }, new Constraints() {
    grid = (0, 4)
    gridwidth = 2
    weightx = 1.0
    weighty = 1.0
    fill = Fill.Both
  })
  add(createChecksComponent, new Constraints() {
    grid = (0, 5)
    gridwidth = 2
    weightx = 1.0
    weighty = 1.0
    fill = Fill.Both
  })
}
