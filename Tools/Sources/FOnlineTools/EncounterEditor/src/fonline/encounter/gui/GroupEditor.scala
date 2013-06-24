package fonline.encounter.gui

import fonline.encounter.entity._
import fonline.encounter.model._
import java.text.NumberFormat
import javax.swing.BorderFactory
import scala.Some
import swing.GridBagPanel.{Fill, Anchor}
import swing.ListView.{IntervalMode, Renderer}
import swing._
import event.SelectionChanged
import event.SelectionChanged
import event.{ValueChanged, SelectionChanged, ListSelectionChanged}
import fonline.entity._
import fonline.entity.Dialog
import scala.Some
import fonline.model._
import scala.Some
import fonline.model.ItemRemoved
import fonline.model.ItemAdded
import fonline.model.ItemSelectionChanged

/**
 *
 * @author mikewall
 * @since 29.10.2012, 14:35
 * @version 1.0
 */
class GroupEditor(groupModel: ItemModel[Group],
                  teamModel: ItemModel[Team],
                  npcModel: ItemModel[Npc],
                  dialogModel: ItemModel[Dialog],
                  roleModel: ItemModel[Role],
                  aiModel: ItemModel[AI],
                  bagModel: ItemModel[Bag],
                  itemProtoModel: ItemModel[ItemProto],
                  lvarModel: ItemModel[Var],
                  gvarModel: ItemModel[Var],
                  paramModel: ItemModel[Param]) extends GridBagPanel {
  private val critterModel = new ItemModel[Critter](Nil) {
    reactions += {
      case ItemAdded(critter: Critter) => currentGroup match {
        case Some(group) => group.critters :+= critter
        case None =>
      }
      case ItemRemoved(critter: Critter) => currentGroup match {
        case Some(group) => group.critters = group.critters.filterNot(_ eq critter)
        case None =>
      }
    }
  }
  private val itemModel = new ItemModel[Item](Nil) {
    reactions += {
      case ItemAdded(item: Item) => currentGroup match {
        case Some(group) => group.items :+= item
        case None =>
      }
      case ItemRemoved(item: Item) => currentGroup match {
        case Some(group) => group.items = group.items.filterNot(_ eq item)
        case None =>
      }
    }
  }

  private def currentGroup = groupModel.selectedItem


  groupModel.reactions += {
    case ItemSelectionChanged(groupOption: Option[Group]) => refresh()
  }

  private def refresh() {
    val group = currentGroup.getOrElse(new Group(""))
    teamUI.selection.item = group.team.getOrElse(null)
    positionUI.selection.item = group.position
    spacingUI.text = group.spacing.toString
    distanceUI.text = group.distance.toString
    critterModel.items = group.critters
    itemModel.items = group.items
  }

  teamModel.reactions += {
    case ItemAdded(_) => //TODO: refresh teams
    case ItemRemoved(_) => //TODO: refresh teams
  }

  private val teamUI = new ComboBox[Team](teamModel.items) {
    renderer = Renderer(team => if (team eq null) "" else team.name)
    selection.reactions += {
      case SelectionChanged(_) => currentGroup match {
        case Some(group) => group.team = Option(selection.item)
        case None =>
      }
    }
  }
  private val positionUI = new ComboBox[Position.Position](Position.values.toList) {
    selection.reactions += {
      case SelectionChanged(_) => currentGroup match {
        case Some(group) => group.position = selection.item
        case None =>
      }
    }
  }
  private val spacingUI = new FormattedTextField(NumberFormat.getIntegerInstance) {
    reactions += {
      case ValueChanged(_) if !text.isEmpty && !text.exists(!_.isDigit) => currentGroup match {
        case Some(group) => group.spacing = text.toInt
        case None =>
      }
    }
  }
  private val distanceUI = new FormattedTextField(NumberFormat.getIntegerInstance) {
    reactions += {
      case ValueChanged(_) if !text.isEmpty && !text.exists(!_.isDigit) => currentGroup match {
        case Some(group) => group.distance = text.toInt
        case None =>
      }
    }
  }
  private val crittersUI = new ListView[Critter]() {
    renderer = Renderer(_.npc match {
      case Some(npc) => npc.name
      case None => " "
    })
    selection.intervalMode = IntervalMode.Single
    selection.reactions += {
      case ListSelectionChanged(_, _, adjusting) if (!adjusting) => critterModel.selectedItem = selection.items.headOption
    }
    critterModel.reactions += {
      case ItemAdded(critter: Critter) => listData = critterModel.items
      case ItemChanged(_) => repaint()
      case ItemsChanged(_, critters: List[Critter]) => listData = critters
      case ItemRemoved(critter: Critter) => listData = critterModel.items
    }
  }
  private val critterEditor = new CritterEditor(critterModel, npcModel, dialogModel, roleModel, aiModel, bagModel, itemProtoModel, lvarModel, gvarModel, paramModel)
  private val itemsUI = new ListView[Item]() {
    renderer = Renderer(_.proto match {
      case Some(proto) => proto.name
      case None => " "
    })
    selection.intervalMode = IntervalMode.Single
    selection.reactions += {
      case ListSelectionChanged(_, _, adjusting) if (!adjusting) => itemModel.selectedItem = selection.items.headOption
    }
    itemModel.reactions += {
      case ItemAdded(item: Item) => listData = itemModel.items
      case ItemChanged(_) => repaint()
      case ItemsChanged(_, items: List[Item]) => listData = items
      case ItemRemoved(item: Item) => listData = itemModel.items
    }
  }
  private val itemEditor = new ItemEditor(itemModel, itemProtoModel, dialogModel, lvarModel, gvarModel, paramModel)

  private implicit def toLabel(text: String): Label = new Label(text)

  import fonline.gui.ButtonFactory.Button

  refresh()
  add("Team:", new Constraints() {
    grid = (0, 0)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(teamUI, new Constraints() {
    grid = (1, 0)
    fill = Fill.Horizontal
  })
  add("Position:", new Constraints() {
    grid = (0, 1)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(positionUI, new Constraints() {
    grid = (1, 1)
    fill = Fill.Horizontal
  })
  add("Spacing:", new Constraints() {
    grid = (0, 2)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(spacingUI, new Constraints() {
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
  add(new BorderPanel {
    add(new BorderPanel {
      border = BorderFactory.createTitledBorder("Critters")
      add(new BorderPanel {
        add(new ScrollPane(crittersUI), BorderPanel.Position.Center)
        add(new FlowPanel(FlowPanel.Alignment.Right)(
          Button("Add") {
            currentGroup match {
              case Some(group) => {
                critterModel.addItem(new Critter(None))
                crittersUI.peer.clearSelection()
                crittersUI.selection.indices += critterModel.items.size - 1
              }
              case None =>
            }
          },
          Button("Remove") {
            critterModel.selectedItem match {
              case Some(critter) => critterModel.removeItem(critter)
              case None =>
            }
          }
        ) {
          vGap = 1
          hGap = 1
        }, BorderPanel.Position.South)
      }, BorderPanel.Position.Center)
      add(new BorderPanel {
        border = BorderFactory.createTitledBorder("Critter")
        add(critterEditor, BorderPanel.Position.Center)
      }, BorderPanel.Position.South)
    }, BorderPanel.Position.West)
    add(new BorderPanel {
      border = BorderFactory.createTitledBorder("Items")
      add(new BorderPanel {
        add(new ScrollPane(itemsUI), BorderPanel.Position.Center)
        add(new FlowPanel(FlowPanel.Alignment.Right)(
          Button("Add") {
            currentGroup match {
              case Some(group) => {
                itemModel.addItem(new Item(None))
                itemsUI.peer.clearSelection()
                itemsUI.selection.indices += itemModel.items.size - 1
              }
              case None =>
            }
          },
          Button("Remove") {
            itemModel.selectedItem match {
              case Some(item) => itemModel.removeItem(item)
              case None =>
            }
          }
        ) {
          vGap = 1
          hGap = 1
        }, BorderPanel.Position.South)
      }, BorderPanel.Position.Center)
      add(new BorderPanel {
        border = BorderFactory.createTitledBorder("Item")
        add(itemEditor, BorderPanel.Position.Center)
      }, BorderPanel.Position.South)
    }, BorderPanel.Position.East)
  }, new Constraints() {
    grid = (0, 4)
    gridwidth = 2
    weighty = 1
    fill = Fill.Both
  })
}