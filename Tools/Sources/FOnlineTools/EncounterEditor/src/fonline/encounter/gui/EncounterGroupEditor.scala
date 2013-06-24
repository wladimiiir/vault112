package fonline.encounter.gui

import fonline.encounter.entity.{EncounterGroup, Group}
import fonline.model.ItemModel
import java.text.NumberFormat
import javax.swing.BorderFactory
import swing.GridBagPanel.{Fill, Anchor}
import swing.ListView.Renderer
import swing._
import fonline.gui.DialogEditor

/**
 * User: mikewall
 * Date: 10/28/12
 * Time: 1:08 PM
 */
class EncounterGroupEditor(groupModel: ItemModel[Group]) extends GridBagPanel with DialogEditor[EncounterGroup] {
  private val groupUI = new ComboBox[Group](groupModel.items) {
    renderer = Renderer(group => if (group eq null) "" else group.name)
  }
  private val minRatioUI = new FormattedTextField(NumberFormat.getIntegerInstance)
  private val maxRatioUI = new FormattedTextField(NumberFormat.getIntegerInstance)

  border = BorderFactory.createEmptyBorder(10, 10, 10, 10)

  add(new Label("Group:"), new Constraints() {
    grid = (0, 0)
    anchor = Anchor.East
  })
  add(groupUI, new Constraints() {
    grid = (1, 0)
    fill = Fill.Horizontal
  })
  add(new Label("Min ratio:"), new Constraints() {
    grid = (0, 1)
    anchor = Anchor.East
  })
  add(minRatioUI, new Constraints() {
    grid = (1, 1)
    fill = Fill.Horizontal
  })
  add(new Label("Max ratio:"), new Constraints() {
    grid = (0, 2)
    anchor = Anchor.East
  })
  add(maxRatioUI, new Constraints() {
    grid = (1, 2)
    fill = Fill.Horizontal
  })

  def init(encounterGroup: EncounterGroup) {
    groupUI.selection.item = encounterGroup.group.getOrElse(null)
    minRatioUI.text = encounterGroup.minRatio.toString
    maxRatioUI.text = encounterGroup.maxRatio.toString
  }

  def commit(dialogEntity: EncounterGroup) {
    dialogEntity.group = Option(groupUI.selection.item)
    dialogEntity.minRatio = minRatioUI.text.toInt
    dialogEntity.maxRatio = maxRatioUI.text.toInt
  }
}
