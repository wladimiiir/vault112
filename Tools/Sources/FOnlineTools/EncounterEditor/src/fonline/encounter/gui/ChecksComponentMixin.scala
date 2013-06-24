package fonline.encounter.gui

import fonline.encounter.entity._
import fonline.gui.ButtonFactory.Button
import fonline.entity.{Param, Var}
import fonline.model._
import javax.swing.BorderFactory
import scala.Some
import swing.BorderPanel.Position
import swing.ListView.Renderer
import swing.Swing._
import swing._
import event.ListSelectionChanged
import fonline.gui.EntityDialog

/**
 * User: mikewall
 * Date: 10/30/12
 * Time: 7:47 PM
 */
trait ChecksComponentMixin {
  protected def canAdd: Boolean

  protected def lvarModel: ItemModel[Var]

  protected def gvarModel: ItemModel[Var]

  protected def paramModel: ItemModel[Param]

  protected def checkModel: ItemModel[Check]

  private def createChecksUI = new ListView[Check](Nil) {
    renderer = Renderer(check => check match {
      case LVarCheck(name, operator, value) => "LVar: " + name + " " + operator.toString + " " + value
      case GVarCheck(name, operator, value) => "GVar: " + name + " " + operator.toString + " " + value
      case ParamCheck(name, operator, value) => "Param: " + name + " " + operator.toString + " " + value
      case RandomCheck(value) => "Random: " + value
      case _ => ""
    })
    checkModel.reactions += {
      case ItemsChanged(_, items: List[Check]) => listData = items
      case ItemChanged(_) => repaint()
      case ItemAdded(_) => listData = checkModel.items
      case ItemRemoved(_) => listData = checkModel.items
    }
    selection.reactions += {
      case ListSelectionChanged(_, _, adjusting) if !adjusting => checkModel.selectedItem = selection.items.headOption
    }
  }

  protected def createChecksComponent: Component = new BorderPanel {
    val checkDialog = new EntityDialog[Check](new CheckEditor(lvarModel, gvarModel, paramModel), "Check") {
      centerOnScreen()
    }

    border = BorderFactory.createTitledBorder("Checks")
    add(new ScrollPane(createChecksUI) {
      preferredSize = (100, 40)
    }, Position.Center)
    add(new FlowPanel(FlowPanel.Alignment.Right)(
      Button("Add LVar") {
        canAdd match {
          case true => {
            val check = new LVarCheck()
            checkDialog.showFor(check) match {
              case EntityDialog.OK => checkModel.addItem(check)
              case EntityDialog.Cancel =>
            }
          }
          case false =>
        }
      },
      Button("Add GVar") {
        canAdd match {
          case true => {
            val check = new GVarCheck()
            checkDialog.showFor(check) match {
              case EntityDialog.OK => checkModel.addItem(check)
              case EntityDialog.Cancel =>
            }
          }
          case false =>
        }
      },
      Button("Add Param") {
        canAdd match {
          case true => {
            val check = new ParamCheck()
            checkDialog.showFor(check) match {
              case EntityDialog.OK => checkModel.addItem(check)
              case EntityDialog.Cancel =>
            }
          }
          case false =>
        }
      },
      Button("Add Random") {
        canAdd match {
          case true => {
            val check = new RandomCheck()
            checkDialog.showFor(check) match {
              case EntityDialog.OK => checkModel.addItem(check)
              case EntityDialog.Cancel =>
            }
          }
          case false =>
        }
      },
      Button("Edit") {
        checkModel.selectedItem match {
          case Some(check) => {
            checkDialog.showFor(check) match {
              case EntityDialog.OK => checkModel.fireItemChanged(check)
              case EntityDialog.Cancel =>
            }
          }
          case None =>
        }
      },
      Button("Remove") {
        createChecksUI.selection.items.headOption match {
          case Some(check) => checkModel.removeItem(check)
          case None =>
        }
      }
    ) {
      vGap = 1
      hGap = 1
    }, Position.South)
  }
}
