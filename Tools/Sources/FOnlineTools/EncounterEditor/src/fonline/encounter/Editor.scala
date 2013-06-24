package fonline.encounter

import builder.ScriptBuilder
import com.jgoodies.looks.plastic.Plastic3DLookAndFeel
import entity._
import entity.Table
import gui.{GroupPanel, TablePanel, ZonePanel}
import java.awt.Dimension
import java.io.{FileWriter, File}
import javax.swing.UIManager
import logic.FileProvider
import fonline.encounter.model.{ZoneModel}
import parser.ScriptParser
import swing.TabbedPane.Page
import swing._
import fonline.model.ItemModel
import fonline.entity._
import fonline.entity.Dialog
import scala.Some

/**
 * User: mikewall
 * Date: 10/24/12
 * Time: 8:22 PM
 */
object Editor extends SimpleSwingApplication {
  UIManager.setLookAndFeel(new Plastic3DLookAndFeel)

  private val serverDir = FileProvider.serverPath
  if (serverDir.isEmpty) {
    sys.exit()
  }
  private val worldMapPath = FileProvider.worldMapPath
  if (worldMapPath.isEmpty) {
    sys.exit()
  }

  private val scriptParser = new ScriptParser(serverDir.get)
  private val scriptBuilder = new ScriptBuilder(serverDir.get)
  private val groupModel = new ItemModel[Group](scriptParser.parseGroups)
  private val teamModel = new ItemModel[Team](scriptParser.parseTeams)
  private val tableModel = new ItemModel[Table](scriptParser.parseTables)
  private val zoneModel = new ZoneModel(scriptParser.parseZones)
  private val dialogModel = new ItemModel[Dialog](scriptParser.parseDialogs.sortBy(_.name))
  private val roleModel = new ItemModel[Role](scriptParser.parseRoles)
  private val npcModel = new ItemModel[Npc](scriptParser.parseNpcs.sortBy(_.name))
  private val aiModel = new ItemModel[AI](scriptParser.parseAIs)
  private val bagModel = new ItemModel[Bag](scriptParser.parseBags)
  private val itemProtoModel = new ItemModel[ItemProto](scriptParser.parseItemProtos.sortBy(_.name))
  private val lvarsModel = new ItemModel[Var](scriptParser.parseLVars.sortBy(_.name))
  private val gvarsModel = new ItemModel[Var](scriptParser.parseGVars.sortBy(_.name))
  private val paramModel = new ItemModel[Param](scriptParser.parseParams)
  private val locationModel = new ItemModel[Location](scriptParser.parseLocations)
  private val gmTextsModel = new ItemModel[TextMessage](scriptParser.parseTexts("FOGM.MSG").filter(_.id >= 10030000).sortBy(_.text))
  private val terrainModel = new ItemModel[Terrain](scriptParser.parseTerrains)
  private val chanceModel = new ItemModel[Chance](scriptParser.parseChances)
  private val globalMap = new GlobalMap(new File(worldMapPath.get)) {
    width = scriptParser.getGlobalMapWidth
    height = scriptParser.getGlobalMapHeight
  }

  //  println(gmTextsModel.items.filter(_.id >= 10030000).sortBy(_.text).map(_.text).distinct.mkString("\n"))

  def top = {
    val frame = new MainFrame {
      title = "Encounter editor " + Version
      contents = new BorderPanel {
        add(new TabbedPane {
          pages += new Page("Groups", new GroupPanel(groupModel, teamModel, npcModel, dialogModel, roleModel, aiModel, bagModel, itemProtoModel, lvarsModel, gvarsModel, paramModel))
          pages += new Page("Tables", new TablePanel(tableModel, gmTextsModel, locationModel, groupModel, lvarsModel, gvarsModel, paramModel))
          pages += new Page("Zones", new ZonePanel(scriptParser, zoneModel, globalMap, groupModel, gmTextsModel, tableModel, terrainModel, chanceModel))
        }, BorderPanel.Position.Center)
        add(new FlowPanel(FlowPanel.Alignment.Right)(
          Button("Generate worldmap_init") {
            FileProvider.generatedWorldMapInitPath match {
              case Some(path) => {
                val script = scriptBuilder.buildWorldMapInitScript(groupModel, tableModel, zoneModel)
                val writer = new FileWriter(path)
                writer.write(script)
                writer.close()
              }
              case None =>
            }
          },
          Button("Exit") {
            sys.exit()
          }
        ), BorderPanel.Position.South)
      }
    }
    frame.size = new Dimension(1024, 730)
    frame.centerOnScreen()
    frame
  }
}
