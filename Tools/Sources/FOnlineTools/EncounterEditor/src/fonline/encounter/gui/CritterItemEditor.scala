package fonline.encounter.gui

import java.text.NumberFormat
import javax.swing.BorderFactory
import swing.GridBagPanel.{Fill, Anchor}
import swing.ListView.Renderer
import swing._
import fonline.entity.{Slot, ItemProto}
import fonline.model.ItemModel
import fonline.encounter.entity.{RandomCheck, Item}
import fonline.gui.DialogEditor

/**
 * User: mikewall
 * Date: 10/29/12
 * Time: 11:08 PM
 */
class CritterItemEditor(itemProtoModel: ItemModel[ItemProto]) extends GridBagPanel with DialogEditor[Item] {
  private val itemProtoUI = new ComboBox[ItemProto](itemProtoModel.items) {
    renderer = Renderer(proto => if (proto eq null) "" else proto.name)
  }
  private val slotUI = new ComboBox[Slot.Slot](Slot.values.toList)
  private val chanceUI = new FormattedTextField(NumberFormat.getIntegerInstance)
  private val minCountUI = new FormattedTextField(NumberFormat.getIntegerInstance)
  private val maxCountUI = new FormattedTextField(NumberFormat.getIntegerInstance)

  private implicit def toLabel(text: String): Label = new Label(text)

  border = BorderFactory.createEmptyBorder(3, 3, 3, 3)
  add("Proto:", new Constraints() {
    grid = (0, 0)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(itemProtoUI, new Constraints() {
    grid = (1, 0)
    fill = Fill.Horizontal
  })
  add("Slot:", new Constraints() {
    grid = (0, 1)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(slotUI, new Constraints() {
    grid = (1, 1)
    fill = Fill.Horizontal
  })
  add("Chance:", new Constraints() {
    grid = (0, 2)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(chanceUI, new Constraints() {
    grid = (1, 2)
    fill = Fill.Horizontal
  })
  add("Min count:", new Constraints() {
    grid = (0, 3)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(minCountUI, new Constraints() {
    grid = (1, 3)
    fill = Fill.Horizontal
  })
  add("Max count:", new Constraints() {
    grid = (0, 4)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(maxCountUI, new Constraints() {
    grid = (1, 4)
    fill = Fill.Horizontal
  })


  def init(dialogEntity: Item) {
    itemProtoUI.selection.item = dialogEntity.proto.getOrElse(null)
    slotUI.selection.item = dialogEntity.slot
    chanceUI.text = dialogEntity.checks.find(_.isInstanceOf[RandomCheck]) match {
      case Some(RandomCheck(value)) => value.toString
      case _ => "100"
    }
    minCountUI.text = dialogEntity.minCount.toString
    maxCountUI.text = dialogEntity.maxCount.toString
  }

  def commit(dialogEntity: Item) {
    dialogEntity.proto = Option(itemProtoUI.selection.item)
    dialogEntity.slot = slotUI.selection.item
    dialogEntity.checks = chanceUI.text match {
      case "100" => Nil
      case number => try {
        List(new RandomCheck(chanceUI.text.filter(ch => ch.isDigit).toInt))
      } catch {
        case ex: NumberFormatException => Nil
      }
    }
    dialogEntity.minCount = minCountUI.text.toInt
    dialogEntity.maxCount = maxCountUI.text.toInt
  }
}
