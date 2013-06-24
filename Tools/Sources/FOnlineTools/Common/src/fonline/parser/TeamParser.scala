package fonline.parser

import util.matching.Regex
import fonline.entity.Team
import fonline.logic.ScriptReader

/**
 *
 * @author mikewall
 * @since 15.10.2012, 13:23
 * @version 1.0
 */
trait TeamParser {
  private val teamDefineRegex = new Regex("#define\\s*TEAM_([0-9a-zA-Z_]*)\\s*\\(\\s*(\\d*)\\s*\\)", "teamName", "teamID")

  protected def serverPath: String

  private var teamsOption: Option[List[Team]] = None

  def parseTeams = teamsOption match {
    case Some(teams) => teams
    case None => {
      val script = ScriptReader.getScript(serverPath, "_teams.fos")
      teamsOption = Option(teamDefineRegex.findAllIn(script).matchData.map(m => new Team(m.group("teamID").toInt, m.group("teamName"))).toList)
      teamsOption.get
    }
  }

  def parseTeam(team: String) = {
    team.exists(ch => !ch.isDigit) match {
      case true => parseTeams.find(t => t.name == team || ("TEAM_" + t.name) == team)
      case false => parseTeams.find(t => t.id == team.toInt)
    }
  }
}