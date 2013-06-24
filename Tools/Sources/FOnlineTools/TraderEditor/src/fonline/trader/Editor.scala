package fonline.trader

import builder.ScriptBuilder
import com.jgoodies.looks.plastic.Plastic3DLookAndFeel
import entity.Trader
import fonline.entity._
import fonline.model.ItemModel
import gui.TraderPanel
import java.awt.Dimension
import java.io.{FileWriter, File}
import javax.swing.UIManager
import logic.FileProvider
import parser.ScriptParser
import scala.Some
import swing.TabbedPane.Page
import swing._

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

  private val scriptParser = new ScriptParser(serverDir.get)
  private val scriptBuilder = new ScriptBuilder(serverDir.get)
  private val traderModel = new ItemModel[Trader](scriptParser.parseTraders)
  private val npcModel = new ItemModel[Npc](scriptParser.parseNpcs.sortBy(_.name))
  private val itemProtoModel = new ItemModel[ItemProto](scriptParser.parseItemProtos.sortBy(_.name))
  private val itemProtoGroupModel = new ItemModel[ItemProtoGroup](scriptParser.parseItemProtoGroups)

  def top = {
    val frame = new MainFrame {
      title = "Trader editor " + Version
      contents = new BorderPanel {
        add(new TraderPanel(traderModel, npcModel, itemProtoModel, itemProtoGroupModel), BorderPanel.Position.Center)
        add(new FlowPanel(FlowPanel.Alignment.Right)(
          Button("Generate trader_init") {
            FileProvider.generatedTraderInitPath match {
              case Some(path) => {
                val script = scriptBuilder.buildTraderInitScript(traderModel)
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
    frame.size = new Dimension(800, 600)
    frame.centerOnScreen()
    frame
  }
}
