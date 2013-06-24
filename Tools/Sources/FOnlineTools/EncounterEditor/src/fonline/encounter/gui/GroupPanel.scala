package fonline.encounter.gui

import fonline.encounter.entity._
import javax.swing.BorderFactory
import swing.BorderPanel.Position
import swing.ListView.{IntervalMode, Renderer}
import swing.Swing._
import swing.event.ListSelectionChanged
import swing.{ScrollPane, BorderPanel, ListView}
import fonline.entity._
import fonline.entity.Dialog
import fonline.model.ItemModel

/**
 *
 * @author mikewall
 * @since 29.10.2012, 10:29
 * @version 1.0
 */
class GroupPanel(groupModel: ItemModel[Group],
                 teamModel: ItemModel[Team],
                 npcModel: ItemModel[Npc],
                 dialogModel: ItemModel[Dialog],
                 roleModel: ItemModel[Role],
                 aiModel: ItemModel[AI],
                 bagModel: ItemModel[Bag],
                 itemProtoModel: ItemModel[ItemProto],
                 lvarModel: ItemModel[Var],
                 gvarModel: ItemModel[Var],
                 paramModel: ItemModel[Param]) extends BorderPanel {
  private val groupsUI = new ListView[Group](groupModel.items) {
    renderer = Renderer(group => group.name)
    selection.intervalMode = IntervalMode.Single
    selection.reactions += {
      case ListSelectionChanged(_, _, adjusting) if (!adjusting) => groupModel.selectedItem = selection.items.headOption
    }
  }
  private val groupEditor = new GroupEditor(groupModel, teamModel, npcModel, dialogModel, roleModel, aiModel, bagModel, itemProtoModel, lvarModel, gvarModel, paramModel)

  add(new ScrollPane(groupsUI) {
    preferredSize = (200, 100)
  }, Position.West)
  add(new BorderPanel {
    border = BorderFactory.createTitledBorder("Group")
    add(groupEditor, Position.Center)
  }, Position.Center)
}