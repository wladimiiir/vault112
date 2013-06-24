package fonline.trader.gui

import swing.{Label, ComboBox, GridBagPanel}
import fonline.gui.DialogEditor
import fonline.trader.entity.BuyItem
import fonline.model.ItemModel
import fonline.entity.ItemProto
import swing.GridBagPanel.{Fill, Anchor}
import java.awt.Insets
import javax.swing.BorderFactory
import swing.ListView.Renderer

/**
 * User: wladimiiir
 * Date: 12/8/12
 * Time: 8:18 AM
 */
class BuyItemEditor(itemProtoModel: ItemModel[ItemProto]) extends GridBagPanel with DialogEditor[BuyItem] {
  private val protoUI = new ComboBox[ItemProto](itemProtoModel.items) {
    renderer = Renderer(item => if (item eq null) "<select item>" else item.name)
  }

  def init(dialogEntity: BuyItem) {
    protoUI.selection.item = dialogEntity.proto
  }

  def commit(dialogEntity: BuyItem) {
    dialogEntity.proto = protoUI.selection.item
  }

  border = BorderFactory.createEmptyBorder(5, 5, 5, 5)

  implicit def toLabel(text: String) = new Label(text)

  add("Proto:", new Constraints() {
    grid = (0, 0)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(protoUI, new Constraints() {
    grid = (1, 0)
    fill = Fill.Horizontal
  })
}
