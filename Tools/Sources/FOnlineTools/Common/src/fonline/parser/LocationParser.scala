package fonline.parser

import util.matching.Regex
import fonline.logic.ScriptReader
import fonline.entity.Location

/**
 *
 * @author mikewall
 * @since 15.10.2012, 13:23
 * @version 1.0
 */
trait LocationParser {
  protected val locationDefineRegex = new Regex("#define\\s*LOCATION_([0-9a-zA-Z_]*Encounter[0-9a-zA-Z_]*)\\s*\\(\\s*(\\d*)\\s*\\)", "locationName", "locationID")

  protected def serverPath: String

  private var locationsOption: Option[List[Location]] = None

  def parseLocations = locationsOption match {
    case Some(locations) => locations
    case None => {
      val script = ScriptReader.getScript(serverPath, "_maps.fos")
      locationsOption = Option(locationDefineRegex.findAllIn(script).matchData.map(m => new Location(m.group("locationID").toInt, m.group("locationName"))).toList.sortBy(_.name))
      locationsOption.get
    }
  }

  def parseLocation(location: String) = {
    location.exists(ch => !ch.isDigit) match {
      case true => parseLocations.find(loc => loc.name == location || ("LOCATION_" + loc.name) == location)
      case false => parseLocations.find(loc => loc.id == location.toInt)
    }
  }
}