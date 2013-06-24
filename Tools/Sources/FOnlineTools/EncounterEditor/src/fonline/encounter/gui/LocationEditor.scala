package fonline.encounter.gui

import javax.swing.BorderFactory
import swing.GridBagPanel.{Fill, Anchor}
import swing.ListView.Renderer
import swing.{Label, ComboBox, GridBagPanel}
import fonline.entity.{EntityHolder, Location}
import fonline.model.ItemModel
import fonline.gui.DialogEditor

/**
 * User: mikewall
 * Date: 10/28/12
 * Time: 6:02 PM
 */
class LocationEditor(locationModel: ItemModel[Location]) extends GridBagPanel with DialogEditor[EntityHolder[Location]] {
  private val locationsUI = new ComboBox[Location](locationModel.items) {
    renderer = Renderer(_.name)
  }

  border = BorderFactory.createEmptyBorder(10, 10, 10, 10)
  add(new Label("Location:"), new Constraints() {
    grid = (0, 0)
    anchor = Anchor.East
  })
  add(locationsUI, new Constraints() {
    grid = (1, 0)
    fill = Fill.Horizontal
  })

  def init(dialogEntity: EntityHolder[Location]) {
    locationsUI.selection.item = dialogEntity.entity
  }

  def commit(dialogEntity: EntityHolder[Location]) {
    dialogEntity.entity = locationsUI.selection.item
  }
}
