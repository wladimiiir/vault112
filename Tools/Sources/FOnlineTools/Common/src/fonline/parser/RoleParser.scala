package fonline.parser

import util.matching.Regex
import fonline.entity.Role
import fonline.logic.ScriptReader

/**
 *
 * @author mikewall
 * @since 29.10.2012, 16:47
 * @version 1.0
 */
trait RoleParser {
  private val roleDefineRegex = new Regex("#define\\s*ROLE_([0-9a-zA-Z_]*)\\s*\\(\\s*(\\d*)\\s*\\)", "name", "id")

  protected def serverPath: String

  private var rolesOption: Option[List[Role]] = None

  def parseRoles = rolesOption match {
    case Some(roles) => roles
    case None => {
      val script = ScriptReader.getScript(serverPath, "_npc_roles.fos")
      rolesOption = Option(roleDefineRegex.findAllIn(script).matchData.map(data => new Role(data.group("id").toInt, data.group("name"))).toList)
      rolesOption.get
    }
  }

  def parseRole(roleString: String) = roleString.isEmpty || roleString.exists(!_.isDigit) match {
    case true => parseRoles.find(role => role.name == roleString || ("ROLE_" + role.name) == roleString)
    case false => parseRoles.find(_.id == roleString.toInt)
  }
}