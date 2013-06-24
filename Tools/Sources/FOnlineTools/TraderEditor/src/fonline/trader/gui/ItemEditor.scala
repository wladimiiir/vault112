package fonline.trader.gui

import swing._
import fonline.gui.DialogEditor
import fonline.trader.entity.Item
import fonline.model.ItemModel
import fonline.entity.ItemProto
import java.text.NumberFormat
import javax.swing.BorderFactory
import swing.GridBagPanel.{Fill, Anchor}
import swing.ListView.Renderer

/**
 * User: wladimiiir
 * Date: 12/8/12
 * Time: 7:58 AM
 */
class ItemEditor(itemProtoModel: ItemModel[ItemProto]) extends GridBagPanel with DialogEditor[Item] {
  private val protoUI = new ComboBox[ItemProto](itemProtoModel.items) {
    renderer = Renderer(item => if (item eq null) "<select item>" else item.name)
  }
  private val minCountUI = new FormattedTextField(NumberFormat.getIntegerInstance)
  private val maxCountUI = new FormattedTextField(NumberFormat.getIntegerInstance)
  private val chanceUI = new FormattedTextField(NumberFormat.getNumberInstance)
  private val onlyCapsUI = new CheckBox("Only for caps")

  def init(dialogEntity: Item) {
    protoUI.selection.item = dialogEntity.proto
    minCountUI.text = dialogEntity.minCount.toString
    maxCountUI.text = dialogEntity.maxCount.toString
    chanceUI.text = dialogEntity.spawnChance.toString
    onlyCapsUI.selected = dialogEntity.onlyCaps
  }

  def commit(dialogEntity: Item) {
    dialogEntity.proto = protoUI.selection.item
    dialogEntity.minCount = minCountUI.text match {
      case text if !text.exists(_.isDigit) => dialogEntity.minCount
      case text => text.toInt
    }
    dialogEntity.maxCount = maxCountUI.text match {
      case text if !text.exists(_.isDigit) => dialogEntity.maxCount
      case text => text.toInt
    }
    try {
      dialogEntity.spawnChance = chanceUI.text.replaceAll(",", ".").filter(ch => ch.isDigit || ch == '.').toDouble
    } catch {
      case ex: NumberFormatException => //ignore
    }
    dialogEntity.onlyCaps = onlyCapsUI.selected
  }

  implicit def toLabel(text: String) = new Label(text)

  border = BorderFactory.createEmptyBorder(5, 5, 5, 5)

  add("Proto:", new Constraints() {
    grid = (0, 0)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(protoUI, new Constraints() {
    grid = (1, 0)
    fill = Fill.Horizontal
  })
  add("Min count:", new Constraints() {
    grid = (0, 1)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(minCountUI, new Constraints() {
    grid = (1, 1)
    fill = Fill.Horizontal
  })
  add("Max count:", new Constraints() {
    grid = (0, 2)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(maxCountUI, new Constraints() {
    grid = (1, 2)
    fill = Fill.Horizontal
  })
  add("Spawn chance:", new Constraints() {
    grid = (0, 3)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(chanceUI, new Constraints() {
    grid = (1, 3)
    fill = Fill.Horizontal
  })
  add(onlyCapsUI, new Constraints() {
    grid = (0, 4)
    gridwidth = 2
    anchor = Anchor.East
  })
}
