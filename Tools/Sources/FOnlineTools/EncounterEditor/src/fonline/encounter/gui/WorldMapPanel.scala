package fonline.encounter.gui

import fonline.encounter.entity._
import fonline.model.{ItemSelectionChanged, ItemModel}
import java.awt.{Toolkit, Point, Color}
import javax.imageio.ImageIO
import scala.swing.Graphics2D
import swing.Panel
import swing.Swing._
import swing.event._
import scala.Some
import fonline.entity.TextMessage
import fonline.encounter.model.ZoneModel
import swing.event.MouseMoved
import scala.Some
import fonline.model.ItemSelectionChanged
import swing.event.MouseClicked
import java.awt.datatransfer.{DataFlavor, Clipboard}
import fonline.encounter.parser.ScriptParser

/**
 * User: mikewall
 * Date: 10/24/12
 * Time: 8:14 PM
 */
class WorldMapPanel(scriptParser: ScriptParser,
                    globalMap: GlobalMap,
                    zoneModel: ZoneModel,
                    tableModel: ItemModel[Table],
                    selectionGroupModel: ItemModel[Group],
                    selectionTableModel: ItemModel[Table],
                    selectionEncounterTextModel: ItemModel[TextMessage]) extends Panel {

  private object zoneString {
    var x = 0
    var y = 0
    var info = ""
    var encounters = ""

    def draw(g: Graphics2D) {
      g.setColor(Color.GREEN)
      drawLines(info.split("\n").toList ::: encounters.split("\n").toList, y)

      def drawLines(lines: List[String], y: Int) {
        lines match {
          case line :: tail => {
            g.drawString(lines.head, x, y)
            drawLines(tail, y + g.getFontMetrics.getHeight)
          }
          case Nil =>
        }
      }
    }
  }

  private def selectedGroup = selectionGroupModel.selectedItem

  selectionGroupModel.reactions += {
    case ItemSelectionChanged(_) => repaint()
  }

  private def selectedTable = selectionTableModel.selectedItem

  selectionTableModel.reactions += {
    case ItemSelectionChanged(_) => repaint()
  }

  private def selectedEncounter = selectionEncounterTextModel.selectedItem

  selectionEncounterTextModel.reactions += {
    case ItemSelectionChanged(_) => repaint()
  }

  tableModel.reactions += {
    case ItemSelectionChanged(table: Option[Table]) => table match {
      case Some(table) => zoneModel.find(_.table.getOrElse(new Table("")) eq table) match {
        case Some(zone) => zoneModel.selectedItem = Option(zone)
        case None =>
      }
      case None =>
    }
  }

  private val image = ImageIO.read(globalMap.worldMapFile)

  private def globalMapWidth = globalMap.width

  private def globalMapHeight = globalMap.height

  private def imageHeight = image.getHeight / 2

  private def imageWidth: Int = image.getWidth / 2

  private def zoneWidth = imageWidth / globalMapWidth

  private def zoneHeight = imageHeight / globalMapHeight

  private def getZoneX(p: Point) = p.getX.toInt / zoneWidth

  private def getZoneY(p: Point) = p.getY.toInt / zoneHeight

  private def getZoneX(x: Int) = x * zoneWidth

  private def getZoneY(y: Int) = y * zoneHeight

  private def getZone(p: Point) = {
    val x = getZoneX(p)
    val y = getZoneY(p)
    zoneModel.getZone(x, y) match {
      case Some(zone) => Option(zone)
      case None if (x < globalMapWidth && y < globalMapHeight) => {
        val zone = new Zone(x, y)
        zoneModel.addItem(zone)
        Option(zone)
      }
      case None => None
    }
  }

  preferredSize = (imageWidth, imageHeight)
  mouse.clicks.reactions += {
    case MouseClicked(_, p, _, 1, _) => {
      requestFocus()
      zoneModel.selectedItem = getZone(p)
      zoneModel.selectedItem match {
        case Some(zone) => zone.table match {
          case Some(table) => tableModel.selectedItem = Option(table)
          case None =>
        }
        case None =>
      }
      repaint()
    }
  }
  mouse.moves.reactions += {
    case MouseMoved(_, p, _) => {
      getZone(p) match {
        case Some(zone) => {
          zoneString.info = createZoneText
          zoneString.encounters = zone.table match {
            case Some(table) => createEncountersString(table.encounters.filter(_.text.isDefined))
            case None => ""
          }
          zoneString.x = p.getX.toInt + 20
          zoneString.y = p.getY.toInt + 20

          def createZoneText = {
            zone.x + ", " + zone.y + (zone.table match {
              case Some(table) => "\n" + table.name
              case None => ""
            })
          }
        }
        case None => {
          zoneString.info = ""
          zoneString.x = 0
          zoneString.y = 0
        }
      }
      repaint()
    }
  }

  private def createEncountersString(encounters: List[Encounter]): String = encounters match {
    case encounter :: tail => encounter.text.get.text + " (" + encounter.chance + " %)" + "\n" + createEncountersString(tail)
    case Nil => ""
  }

  keys.reactions += {
    case KeyReleased(_, Key.V, Key.Modifier.Control, _) => paste()
  }

  private def paste() {
    val clipboard = Toolkit.getDefaultToolkit.getSystemClipboard
    clipboard.getData(DataFlavor.stringFlavor) match {
      case data: String => zoneModel.selectedItem match {
        case Some(zone) => zone.table match {
          case Some(table) => {
            table.encounters ++= scriptParser.parseEncountersFromString(data)
            tableModel.fireItemChanged(table)
            zoneString.encounters = createEncountersString(table.encounters.filter(_.text.isDefined))
            repaint()
          }
          case None =>
        }
        case None =>
      }
      case _ => println("Nothing usefull in clipboard")
    }
  }


  override protected def paintComponent(g: Graphics2D) {
    g.setColor(Color.DARK_GRAY)
    g.fillRect(0, 0, size.width, size.height)
    g.drawImage(image, 0, 0, imageWidth, imageHeight, null)
    paintGrid(g)
    paintSelectedZone(g)
    paintSelectedGroup(g)
    paintSelectedTable(g)
    paintSelectedEncounter(g)
    zoneString.draw(g)
  }

  private def paintGrid(g: Graphics2D) {
    paintVerticalLines(g, globalMapWidth, 0)
    paintHorizontalLines(g, globalMapHeight, 0)
  }

  private def paintSelectedZone(g: Graphics2D) {
    zoneModel.selectedItem match {
      case Some(zone) => {
        g.setColor(Color.RED)
        g.drawRect(getZoneX(zone.x), getZoneY(zone.y), zoneWidth, zoneHeight)
      }
      case None =>
    }
  }

  private def paintSelectedGroup(g: Graphics2D) {
    selectedGroup match {
      case Some(group) => {
        g.setColor(Color.GREEN)
        zoneModel.items
                .filter(_.table.isDefined)
                .filter(_.table.get.encounters.exists(_.groups.exists(_.group.getOrElse(null) == group)))
                .foreach(zone => {
          g.fillRect(getZoneX(zone.x) + 2, getZoneY(zone.y) + 2, 5, 5)
        })
      }
      case None =>
    }
  }

  private def paintSelectedTable(g: Graphics2D) {
    selectedTable match {
      case Some(table) => {
        g.setColor(Color.YELLOW)
        zoneModel.items
                .filter(_.table.getOrElse(null) == table)
                .foreach(zone => {
          g.fillRect(getZoneX(zone.x) + 2, getZoneY(zone.y) + zoneHeight - 6, 5, 5)
        })
      }
      case None =>
    }
  }

  private def paintSelectedEncounter(g: Graphics2D) {
    selectedEncounter match {
      case Some(encounter) => {
        g.setColor(Color.BLUE)
        zoneModel.items
                .filter(_.table.isDefined)
                .filter(_.table.get.encounters.exists(_.text.getOrElse(null) == encounter))
                .foreach(zone => {
          g.fillRect(getZoneX(zone.x) + zoneWidth - 6, getZoneY(zone.y) + zoneHeight - 6, 5, 5)
        })
      }
      case None =>
    }
  }

  private def paintVerticalLines(g: Graphics2D, count: Int, x: Int) {
    g.setColor(Color.BLACK)
    g.drawLine(x, 0, x, imageHeight)

    if (count > 0) {
      paintVerticalLines(g, count - 1, x + zoneWidth)
    }
  }

  private def paintHorizontalLines(g: Graphics2D, count: Int, y: Int) {
    g.setColor(Color.BLACK)
    g.drawLine(0, y, imageWidth, y)

    if (count > 0) {
      paintHorizontalLines(g, count - 1, y + zoneHeight)
    }
  }
}
