package fonline.encounter.gui

import fonline.encounter.entity._
import fonline.entity._
import fonline.model.ItemModel
import fonline.encounter.model.ZoneModel
import javax.swing.BorderFactory
import swing.BorderPanel.Position
import swing.{ListView, ScrollPane, BorderPanel}
import swing.ListView.Renderer
import fonline.encounter.parser.ScriptParser

/**
 * User: mikewall
 * Date: 10/25/12
 * Time: 6:34 PM
 */
class ZonePanel(scriptParser: ScriptParser,
                zoneModel: ZoneModel,
                globalMap: GlobalMap,
                groupModel: ItemModel[Group],
                encounterTextModel: ItemModel[TextMessage],
                tableModel: ItemModel[Table],
                terrainModel: ItemModel[Terrain],
                chanceModel: ItemModel[Chance]) extends BorderPanel {
  private val selectionGroupModel = new ItemModel[Group](groupModel.items)
  private val selectionTableModel = new ItemModel[Table](tableModel.items)
  private val selectionEncounterTextModel = new ItemModel[TextMessage](encounterTextModel.items)

  add(new ScrollPane(new WorldMapPanel(scriptParser, globalMap, zoneModel, tableModel, selectionGroupModel, selectionTableModel, selectionEncounterTextModel)), Position.Center)
  add(new BorderPanel {
    add(new BorderPanel {
      border = BorderFactory.createTitledBorder("Zone")
      add(new ZoneEditor(zoneModel, tableModel, terrainModel, chanceModel), Position.Center)
    }, Position.Center)
    add(new BorderPanel {
      border = BorderFactory.createTitledBorder("Show")
      add(new WorldMapSelectionPanel(selectionGroupModel, selectionTableModel, selectionEncounterTextModel), Position.Center)
    }, Position.South)
  }, Position.East)
}
