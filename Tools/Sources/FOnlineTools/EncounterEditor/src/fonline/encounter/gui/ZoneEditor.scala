package fonline.encounter.gui

import fonline.encounter.entity.{Chance, Terrain, Table, Zone}
import fonline.model.ItemModel
import fonline.model.ItemSelectionChanged
import java.text.NumberFormat
import scala.Some
import swing.GridBagPanel.{Fill, Anchor}
import swing.ListView.Renderer
import swing._
import event.{ValueChanged, SelectionChanged}
import javax.swing.BorderFactory
import fonline.encounter.model.ZoneModel

/**
 * User: mikewall
 * Date: 10/24/12
 * Time: 8:50 PM
 */
class ZoneEditor(zoneModel: ZoneModel, tableModel: ItemModel[Table], terrainModel: ItemModel[Terrain], chanceModel: ItemModel[Chance]) extends GridBagPanel {
  private def currentZone = zoneModel.selectedItem

  zoneModel.reactions += {
    case ItemSelectionChanged(zoneOption: Option[Zone]) => refresh()
  }

  private def refresh() {
    val zone = currentZone.getOrElse(new Zone(-1, -1))
    coordinatesUI.text = "(" + zone.x + ", " + zone.y + ")"
    tableUI.selection.item = zone.table.getOrElse(null)
    difficultyUI.text = zone.difficulty.toString
    terrainUI.selection.item = zone.terrain.getOrElse(null)
    morningChanceUI.selection.item = zone.morningChance.getOrElse(null)
    afternoonChanceUI.selection.item = zone.afternoonChance.getOrElse(null)
    nightChanceUI.selection.item = zone.nightChance.getOrElse(null)
  }

  private val coordinatesUI = new Label
  private val tableUI = new ComboBox[Table](tableModel.items) {
    renderer = Renderer(table => if (table eq null) "" else table.name)
    selection.reactions += {
      case SelectionChanged(c: ComboBox[Table]) => currentZone match {
        case Some(zone) => zone.table = Option(c.selection.item)
        case None =>
      }
    }
  }
  private val difficultyUI = new FormattedTextField(NumberFormat.getIntegerInstance) {
    reactions += {
      case ValueChanged(_) if !text.isEmpty && !text.exists(!_.isDigit) =>
        currentZone match {
          case Some(zone) => zone.difficulty = text.toInt
          case None =>
        }
    }
  }
  private val terrainUI = new ComboBox[Terrain](terrainModel.items) {
    renderer = Renderer(terrain => if (terrain eq null) "" else terrain.name)
    selection.reactions += {
      case SelectionChanged(c: ComboBox[Terrain]) => currentZone match {
        case Some(zone) => zone.terrain = Option(c.selection.item)
        case None =>
      }
    }
  }
  private val morningChanceUI = new ComboBox[Chance](chanceModel.items) {
    renderer = Renderer(chance => if (chance eq null) "" else chance.name)
    selection.reactions += {
      case SelectionChanged(c: ComboBox[Chance]) => currentZone match {
        case Some(zone) => zone.morningChance = Option(c.selection.item)
        case None =>
      }
    }
  }
  private val afternoonChanceUI = new ComboBox[Chance](chanceModel.items) {
    renderer = Renderer(chance => if (chance eq null) "" else chance.name)
    selection.reactions += {
      case SelectionChanged(c: ComboBox[Chance]) => currentZone match {
        case Some(zone) => zone.afternoonChance = Option(c.selection.item)
        case None =>
      }
    }
  }
  private val nightChanceUI = new ComboBox[Chance](chanceModel.items) {
    renderer = Renderer(chance => if (chance eq null) "" else chance.name)
    selection.reactions += {
      case SelectionChanged(c: ComboBox[Chance]) => currentZone match {
        case Some(zone) => zone.afternoonChance = Option(c.selection.item)
        case None =>
      }
    }
  }
  refresh()

  private implicit def toLabel(text: String) = new Label(text)

  border = BorderFactory.createEmptyBorder(5, 5, 5, 5)
  add("Zone:", new Constraints {
    grid = (0, 0)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(coordinatesUI, new Constraints {
    grid = (1, 0)
    fill = Fill.Horizontal
  })
  add("Table:", new Constraints {
    grid = (0, 1)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(tableUI, new Constraints {
    grid = (1, 1)
    fill = Fill.Horizontal
  })
  add("Difficulty:", new Constraints {
    grid = (0, 2)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(difficultyUI, new Constraints {
    grid = (1, 2)
    fill = Fill.Horizontal
  })
  add("Terrain:", new Constraints {
    grid = (0, 3)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(terrainUI, new Constraints {
    grid = (1, 3)
    fill = Fill.Horizontal
  })
  add("Morning chance:", new Constraints {
    grid = (0, 4)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(morningChanceUI, new Constraints {
    grid = (1, 4)
    fill = Fill.Horizontal
  })
  add("Afternoon chance:", new Constraints {
    grid = (0, 5)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(afternoonChanceUI, new Constraints {
    grid = (1, 5)
    fill = Fill.Horizontal
  })
  add("Night chance:", new Constraints {
    grid = (0, 6)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(nightChanceUI, new Constraints {
    grid = (1, 6)
    fill = Fill.Horizontal
  })
  add(new BorderPanel, new Constraints{
    grid = (0, 7)
    gridwidth = 2
    weighty = 1.0
  })
}