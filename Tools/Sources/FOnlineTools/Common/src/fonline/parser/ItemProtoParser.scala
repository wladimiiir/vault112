package fonline.parser

import util.matching.Regex
import fonline.logic.ScriptReader
import fonline.entity.ItemProto

/**
 *
 * @author mikewall
 * @since 15.10.2012, 13:23
 * @version 1.0
 */
trait ItemProtoParser {
  protected val itemProtoDefineRegex = new Regex("#define\\s*PID_([0-9a-zA-Z_]*)\\s*\\(\\s*(\\d*)\\s*\\)", "protoName", "protoID")
  protected lazy val itemProtos = {
    val script = ScriptReader.getScript(serverPath, "_itempid.fos")
    itemProtoDefineRegex.findAllIn(script).matchData
            .map(m => new ItemProto(m.group("protoID").toInt, m.group("protoName"))).toList
  }

  protected def serverPath: String

  def parseItemProtos = itemProtos

  def parseItemProto(itemProto: String) = {
    itemProto.exists(ch => !ch.isDigit) match {
      case true => parseItemProtos.find(ip => ip.name == itemProto || ("PID_" + ip.name) == itemProto)
      case false => parseItemProtos.find(ip => ip.id == itemProto.toInt)
    }
  }
}