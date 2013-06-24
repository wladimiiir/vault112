package fonline.trader.gui

import fonline.entity.{ItemProtoGroup, EntityHolder, ItemProto}
import fonline.gui.DialogEditor
import fonline.model.ItemModel
import fonline.trader.entity.BuyItem
import java.awt.Insets
import javax.swing.BorderFactory
import swing.GridBagPanel.{Fill, Anchor}
import swing.ListView.Renderer
import swing.{Label, ComboBox, GridBagPanel}

/**
 * User: wladimiiir
 * Date: 12/8/12
 * Time: 8:18 AM
 */
class ItemProtoGroupEditor(itemProtoGroupModel: ItemModel[ItemProtoGroup]) extends GridBagPanel with DialogEditor[EntityHolder[ItemProtoGroup]] {
  private val protoGroupUI = new ComboBox[ItemProtoGroup](itemProtoGroupModel.items) {
    renderer = Renderer(group => if (group eq null) "<select item>" else group.name)
  }

  def init(dialogEntity: EntityHolder[ItemProtoGroup]) {
    protoGroupUI.selection.item = dialogEntity.entity
  }

  def commit(dialogEntity: EntityHolder[ItemProtoGroup]) {
    dialogEntity.entity = protoGroupUI.selection.item
  }

  implicit def toLabel(text: String) = new Label(text)

  border = BorderFactory.createEmptyBorder(5, 5, 5, 5)
  add("Proto group:", new Constraints() {
    grid = (0, 0)
    anchor = Anchor.East
    insets = new Insets(0, 0, 0, 3)
  })
  add(protoGroupUI, new Constraints() {
    grid = (1, 0)
    fill = Fill.Horizontal
  })
}
