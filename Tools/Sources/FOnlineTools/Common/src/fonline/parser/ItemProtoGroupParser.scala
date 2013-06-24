package fonline.parser

import fonline.logic.ScriptReader
import util.matching.Regex
import util.matching.Regex.Match
import fonline.entity.ItemProtoGroup

/**
 * User: wladimiiir
 * Date: 12/8/12
 * Time: 9:10 AM
 */
trait ItemProtoGroupParser extends ItemProtoParser {
  private val itemProtoGroupRegex = new Regex("const uint16\\[\\] Pids_([\\w\\d\\_]+)\\s*=\\s*\\{([^\\}]+)",
    "name", "protos")

  def serverPath: String

  def parseItemProtoGroups = {
    val script = ScriptReader.getScript(serverPath, "pids_groups.fos")

    itemProtoGroupRegex.findAllIn(script).matchData.map(parseItemProtoGroup(_)).toList
  }

  private def parseItemProtoGroup(matchData: Match) = {
    val itemProtoGroup = new ItemProtoGroup(matchData.group("name"))
    itemProtoGroup.protos = matchData.group("protos").split(",")
            .map(itemProto => parseItemProto(itemProto.trim))
            .filter(_.isDefined)
            .map(_.get)
            .toList
    itemProtoGroup
  }
}
