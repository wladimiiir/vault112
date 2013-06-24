package fonline.trader.gui

import fonline.entity.{Duration, Npc}
import fonline.model.{ItemSelectionChanged, ItemModel}
import fonline.trader.entity.Trader
import java.text.NumberFormat
import javax.swing.{BorderFactory, SpinnerNumberModel, JSpinner}
import swing.ListView.Renderer
import swing._
import event.{ComponentEvent, SelectionChanged, ValueChanged, ButtonClicked}
import scala.Some
import swing.GridBagPanel.{Fill, Anchor}
import swing.BorderPanel.Position

/**
 *
 * @author Y12370
 * @since 7.12.2012, 8:48
 * @version 1.0
 */
class TraderEditor(traderModel: ItemModel[Trader], npcModel: ItemModel[Npc]) extends GridBagPanel {
  private def currentTrader = traderModel.selectedItem

  traderModel.reactions += {
    case ItemSelectionChanged(_) => refresh()
  }

  private val npcUI = new ComboBox[Npc](npcModel.items) {
    renderer = Renderer(npc => if (npc eq null) "<select Npc>" else npc.name)
    selection.reactions += {
      case SelectionChanged(_) => currentTrader match {
        case Some(trader) => trader.npc = Option(selection.item)
        case None =>
      }
    }
  }
  private val minRefreshTimeValueUI = new FormattedTextField(NumberFormat.getIntegerInstance)
  private val minRefreshTimeUnitUI = new ComboBox[Duration.Unit](Duration.values.toList)
  private val minRefreshTimeListener = new BorderPanel {
    reactions += {
      case ValueChanged(_) => currentTrader match {
        case Some(trader) => saveMinRefreshTime(trader)
        case None =>
      }
      case SelectionChanged(_) => currentTrader match {
        case Some(trader) => saveMinRefreshTime(trader)
        case _ =>
      }
    }
  }

  private def saveMinRefreshTime(trader: Trader) {
    val text = minRefreshTimeValueUI.text

    if (!text.isEmpty && !text.exists(!_.isDigit)) {
      trader.minRefreshTime = new Duration(text.toInt, minRefreshTimeUnitUI.selection.item)
    }
  }

  private val maxRefreshTimeValueUI = new FormattedTextField(NumberFormat.getIntegerInstance)
  private val maxRefreshTimeUnitUI = new ComboBox[Duration.Unit](Duration.values.toList)
  private val maxRefreshTimeListener = new BorderPanel {
    reactions += {
      case ValueChanged(_) => currentTrader match {
        case Some(trader) => saveMaxRefreshTime(trader)
        case None =>
      }
      case SelectionChanged(_) => currentTrader match {
        case Some(trader) => saveMaxRefreshTime(trader)
        case None =>
      }
    }
  }

  private def saveMaxRefreshTime(trader: Trader) {
    val text = maxRefreshTimeValueUI.text

    if (!text.isEmpty && !text.exists(!_.isDigit)) {
      trader.maxRefreshTime = new Duration(text.toInt, maxRefreshTimeUnitUI.selection.item)
    }
  }

  private val barterSkillUI = Component.wrap(new JSpinner(new SpinnerNumberModel(100, 1, 300, 1)) {
    addChangeListener(Swing.ChangeListener(_ => currentTrader match {
      case Some(trader) => trader.barterSkill = getValue.asInstanceOf[Int]
      case None =>
    }))
  })
  private val clearInventoryUI = new CheckBox("Clear inventory on refresh") {
    reactions += {
      case ButtonClicked(_) => currentTrader match {
        case Some(trader) => trader.clearInventory = selected
        case None =>
      }
    }
  }

  refresh()
  listenTo(npcUI.selection, minRefreshTimeValueUI, minRefreshTimeUnitUI.selection, maxRefreshTimeValueUI, maxRefreshTimeUnitUI.selection, barterSkillUI, clearInventoryUI)

  reactions += {
    case event: ComponentEvent => currentTrader match {
      case Some(trader) => traderModel.fireItemChanged(trader)
      case None =>
    }
  }

  private def refresh() {
    val trader = currentTrader.getOrElse(new Trader)
    npcUI.selection.item = trader.npc.getOrElse(null)
    minRefreshTimeListener.deafTo(minRefreshTimeValueUI, minRefreshTimeUnitUI.selection)
    minRefreshTimeValueUI.text = trader.minRefreshTime.value.toString
    minRefreshTimeUnitUI.selection.item = trader.minRefreshTime.unit
    minRefreshTimeListener.listenTo(minRefreshTimeValueUI, minRefreshTimeUnitUI.selection)
    maxRefreshTimeListener.deafTo(maxRefreshTimeValueUI, maxRefreshTimeUnitUI.selection)
    maxRefreshTimeValueUI.text = trader.maxRefreshTime.value.toString
    maxRefreshTimeUnitUI.selection.item = trader.maxRefreshTime.unit
    maxRefreshTimeListener.listenTo(maxRefreshTimeValueUI, maxRefreshTimeUnitUI.selection)
    barterSkillUI.peer.asInstanceOf[JSpinner].setValue(trader.barterSkill)
    clearInventoryUI.selected = trader.clearInventory
  }

  private implicit def toLabel(text: String) = new Label(text)

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
  add("Min refresh:", new Constraints() {
    grid = (0, 1)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(new BorderPanel {
    add(minRefreshTimeValueUI, Position.Center)
    add(minRefreshTimeUnitUI, Position.East)
  }, new Constraints() {
    grid = (1, 1)
    fill = Fill.Horizontal
  })
  add("Max refresh:", new Constraints() {
    grid = (0, 2)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(new BorderPanel {
    add(maxRefreshTimeValueUI, Position.Center)
    add(maxRefreshTimeUnitUI, Position.East)
  }, new Constraints() {
    grid = (1, 2)
    fill = Fill.Horizontal
  })
  add("Barter skill:", new Constraints() {
    grid = (0, 3)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(barterSkillUI, new Constraints() {
    grid = (1, 3)
    fill = Fill.Horizontal
  })
  add(clearInventoryUI, new Constraints() {
    grid = (0, 4)
    gridwidth = 2
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 0)
  })
}