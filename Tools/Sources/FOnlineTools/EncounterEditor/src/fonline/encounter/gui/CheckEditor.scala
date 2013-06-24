package fonline.encounter.gui

import fonline.encounter.entity._
import fonline.entity.{Param, Var}
import java.awt.Dimension
import java.text.NumberFormat
import javax.swing.BorderFactory
import swing.GridBagPanel.{Fill, Anchor}
import swing.ListView.Renderer
import swing.{Label, FormattedTextField, ComboBox, GridBagPanel}
import fonline.model.ItemModel
import fonline.gui.DialogEditor

/**
 * User: mikewall
 * Date: 10/28/12
 * Time: 4:24 PM
 */
class CheckEditor(lvarModel: ItemModel[Var] = new ItemModel[Var](Nil), gvarModel: ItemModel[Var] = new ItemModel[Var](Nil), paramModel: ItemModel[Param] = new ItemModel[Param](Nil)) extends GridBagPanel with DialogEditor[Check] {
  private val lvarLabel = new Label("LVar:")
  private val lvarsUI = new ComboBox[Var](lvarModel.items) {
    renderer = Renderer(lvar => if(lvar eq null) "" else lvar.name)
  }
  private val gvarLabel = new Label("GVar:")
  private val gvarsUI = new ComboBox[Var](gvarModel.items) {
    renderer = Renderer(gvar => if(gvar eq null) "" else gvar.name)
  }
  private val paramLabel = new Label("Param:")
  private val paramsUI = new ComboBox[Param](paramModel.items) {
    renderer = Renderer(param => if(param eq null) "" else param.name)
  }
  private val operatorLabel = new Label("Operator:")
  private val operatorUI = new ComboBox[Operator.Operator](Operator.values.toList) {
    renderer = Renderer(operator => if(operator eq null) "" else operator.toString)
  }
  private val valueUI = new FormattedTextField(NumberFormat.getIntegerInstance) {
    preferredSize = new Dimension(150, 21)
  }

  border = BorderFactory.createEmptyBorder(10, 10, 10, 10)

  add(lvarLabel, new Constraints() {
    grid = (0, 0)
    anchor = Anchor.East
  })
  add(lvarsUI, new Constraints() {
    grid = (1, 0)
    fill = Fill.Horizontal
  })
  add(gvarLabel, new Constraints() {
    grid = (0, 1)
    anchor = Anchor.East
  })
  add(gvarsUI, new Constraints() {
    grid = (1, 1)
    fill = Fill.Horizontal
  })
  add(paramLabel, new Constraints() {
    grid = (0, 1)
    anchor = Anchor.East
  })
  add(paramsUI, new Constraints() {
    grid = (1, 1)
    fill = Fill.Horizontal
  })
  add(operatorLabel, new Constraints() {
    grid = (0, 2)
    anchor = Anchor.East
  })
  add(operatorUI, new Constraints() {
    grid = (1, 2)
    fill = Fill.Horizontal
  })
  add(new Label("Value:"), new Constraints() {
    grid = (0, 3)
    anchor = Anchor.East
  })
  add(valueUI, new Constraints() {
    grid = (1, 3)
    fill = Fill.Horizontal
  })

  def init(dialogEntity: Check) {
    dialogEntity match {
      case LVarCheck(name, operator, value) => {
        lvarLabel.visible = true
        lvarsUI.visible = true
        gvarLabel.visible = false
        gvarsUI.visible = false
        paramLabel.visible = false
        paramsUI.visible = false
        operatorLabel.visible = true
        operatorUI.visible = true
        lvarsUI.selection.item = lvarModel.find(_.name == name).getOrElse(null)
        operatorUI.selection.item = operator
        valueUI.text = value.toString
      }
      case GVarCheck(name, operator, value) => {
        gvarLabel.visible = true
        gvarsUI.visible = true
        lvarLabel.visible = false
        lvarsUI.visible = false
        paramLabel.visible = false
        paramsUI.visible = false
        operatorLabel.visible = true
        operatorUI.visible = true
        gvarsUI.selection.item = gvarModel.find(_.name == name).getOrElse(null)
        operatorUI.selection.item = operator
        valueUI.text = value.toString
      }
      case ParamCheck(name, operator, value) => {
        gvarLabel.visible = false
        gvarsUI.visible = false
        lvarLabel.visible = false
        lvarsUI.visible = false
        paramLabel.visible = true
        paramsUI.visible = true
        operatorLabel.visible = true
        operatorUI.visible = true
        paramsUI.selection.item = paramModel.find(_.name == name).getOrElse(null)
        operatorUI.selection.item = operator
        valueUI.text = value.toString
      }
      case HourCheck(operator, value) => {
        lvarLabel.visible = false
        lvarsUI.visible = false
        gvarLabel.visible = false
        gvarsUI.visible = false
        paramLabel.visible = false
        paramsUI.visible = false
        operatorLabel.visible = true
        operatorUI.visible = true
        operatorUI.selection.item = operator
        valueUI.text = value.toString
      }
      case RandomCheck(value) => {
        gvarLabel.visible = false
        gvarsUI.visible = false
        lvarLabel.visible = false
        lvarsUI.visible = false
        paramLabel.visible = false
        paramsUI.visible = false
        operatorLabel.visible = false
        operatorUI.visible = false
        valueUI.text = value.toString
      }
    }
  }

  def commit(dialogEntity: Check) {
    dialogEntity match {
      case lvar: LVarCheck => {
        lvar.name = lvarsUI.selection.item.name
        lvar.operator = operatorUI.selection.item
        lvar.value = valueUI.text.toInt
      }
      case gvar: GVarCheck => {
        gvar.name = gvarsUI.selection.item.name
        gvar.operator = operatorUI.selection.item
        gvar.value = valueUI.text.toInt
      }
      case param: ParamCheck => {
        param.name = paramsUI.selection.item.name
        param.operator = operatorUI.selection.item
        param.value = valueUI.text.toInt
      }
      case hour: HourCheck => {
        hour.operator = operatorUI.selection.item
        hour.value = valueUI.text.toInt
      }
      case random: RandomCheck => {
        random.value = valueUI.text.toInt
      }
    }
  }
}
