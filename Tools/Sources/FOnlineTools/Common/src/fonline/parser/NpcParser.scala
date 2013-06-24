package fonline.parser

import util.matching.Regex
import fonline.entity.Npc
import fonline.logic.ScriptReader

/**
 *
 * @author mikewall
 * @since 15.10.2012, 13:23
 * @version 1.0
 */
trait NpcParser {
  protected val npcDefineRegex = new Regex("#define\\s*NPC_PID_([0-9a-zA-Z_]*)\\s*\\(\\s*(\\d*)\\s*\\)", "npcName", "npcID")

  protected def serverPath: String

  private var npcsOption: Option[List[Npc]] = None

  def parseNpcs = npcsOption match {
    case Some(npcs) => npcs
    case None => {
      val script = ScriptReader.getScript(serverPath, "_npc_pids.fos")
      npcsOption = Option(npcDefineRegex.findAllIn(script).matchData.map(m => new Npc(m.group("npcID").toInt, m.group("npcName"))).toList)
      npcsOption.get
    }
  }

  def parseNpc(npcString: String) = {
    npcString.exists(!_.isDigit) match {
      case true => parseNpcs.find(npc => npc.name == npcString || ("NPC_PID_" + npc.name) == npcString)
      case false => parseNpcs.find(npc => npc.id == npcString.toInt)
    }
  }
}