package fonline.encounter.builder

import fonline.encounter.entity._
import fonline.encounter.model.ZoneModel
import fonline.model.ItemModel
import scala.Some

/**
 * User: mikewall
 * Date: 10/31/12
 * Time: 8:43 PM
 */
trait WorldMapInitBuilder {
  protected def serverPath: String

  def buildWorldMapInitScript(groupModel: ItemModel[Group], tableModel: ItemModel[Table], zoneModel: ZoneModel) = {
    val script = new StringBuilder
    if (groupModel.items.exists(_.critters.exists(_.role.isDefined))) {
      script.append("#include \"_npc_roles.fos\"\n")
    }
    if (groupModel.items.exists(_.critters.size > 0)) {
      script.append("#include \"_npc_pids.fos\"\n")
    }
    if (groupModel.items.exists(_.critters.exists(_.ai.isDefined))) {
      script.append("#include \"_ai.fos\"\n")
    }
    if (groupModel.items.exists(_.team.isDefined)) {
      script.append("#include \"_teams.fos\"\n")
    }
    if (groupModel.items.exists(_.critters.exists(_.dialog.isDefined))) {
      script.append("#include \"_dialogs.fos\"\n")
    }
    if (groupModel.items.exists(_.critters.exists(_.bag.isDefined))) {
      script.append("#include \"_bags.fos\"\n")
    }
    //    if (groupModel.items.exists(_.critters.exists(_.children.exists(child => child.isInstanceOf[Item] && child.asInstanceOf[Item].proto.isDefined)))) {
    //      script.append("#include \"_itempid.fos\"\n")
    //    }
    script.append("\n")
    script.append(buildGroups(groupModel)).append("\n")
    script.append(buildTables(tableModel)).append("\n")
    script.append(buildZones(zoneModel)).append("\n")
    script.toString()
  }

  def buildGroups(groupModel: ItemModel[Group]) = {
    val groupsBuild = new StringBuilder
    groupsBuild.append("CEncounterGroup@ group;\n\n")
    groupsBuild.append(groupModel.items
            .filterNot(group => group.critters.isEmpty && group.items.isEmpty)
            .map(buildGroup(_)).mkString("\n"))
    groupsBuild
  }

  private def buildGroup(group: Group) = {
    val groupBuild = new StringBuilder
    groupBuild.append("@group = @EncounterGroups[ GROUP_" + group.name + " ];\n")
    group.team match {
      case Some(team) => groupBuild.append("group.TeamNum = TEAM_").append(team.name).append(";\n")
      case None =>
    }
    groupBuild.append("group.Position = ").append(group.position).append(";\n")
    if (group.distance > 0) {
      groupBuild.append("group.Distance = ").append(group.distance).append(";\n")
    }
    if (group.spacing > 0) {
      groupBuild.append("group.Spacing = ").append(group.spacing).append(";\n")
    }
    groupBuild.append(group.critters.filterNot(_.npc.isEmpty).map(buildCritter(_)).mkString)
    groupBuild.append(group.items.filterNot(_.proto.isEmpty).map(buildItem(_)).mkString)
    groupBuild
  }

  private def buildCritter(critter: Critter) = {
    val critterBuild = new StringBuilder
    critterBuild.append("group.AddCritter( NPC_PID_").append(critter.npc match {
      case Some(npc) if !npc.name.trim.isEmpty => npc.name
      case Some(npc) => npc.id
      case None => ""
    }).append(" )")
    critter.ai match {
      case Some(ai) => critterBuild.append(".AI( AIPACKET_").append(ai.name).append(" )")
      case None =>
    }
    critter.bag match {
      case Some(bag) => critterBuild.append(".Bag( BAG_").append(bag.name).append(" )")
      case None =>
    }
    critter.role match {
      case Some(role) => critterBuild.append(".Role( ROLE_").append(role.name).append(" )")
      case None =>
    }
    critterBuild.append(".Ratio( ").append(critter.ratio).append(" )")
    if (critter.distance > 0) {
      critterBuild.append(".Distance( ").append(critter.distance).append(" )")
    }
    critter.dialog match {
      case Some(dialog) => critterBuild.append(".Dialog( DIALOG_").append(dialog.name).append(" )")
      case None =>
    }
    critter.script match {
      case Some(script) => critterBuild.append(".Script( \"").append(script).append("\" )")
      case None =>
    }
    if (critter.dead) {
      critterBuild.append(".Dead( true )")
    }
    if (!critter.checks.isEmpty) {
      critterBuild.append("\n\t").append(critter.checks.map(buildCheck(_)).mkString("\n\t"))
    }
    val items = critter.children
            .filter(_.isInstanceOf[Item])
            .map(_.asInstanceOf[Item])
            .filter(item => item.proto.isDefined && item.minCount > 0 && item.maxCount > 0)
    if (!items.isEmpty) {
      critterBuild.append("\n\t").append(items.map(buildCritterItem(_)).mkString("\n\t"))
    }
    critterBuild.append(";\n")
    critterBuild
  }

  private def buildCritterItem(item: Item) = {
    val itemBuild = new StringBuilder
    itemBuild.append(".AddItem( ").append(item.proto match {
      case Some(proto) if !proto.name.trim.isEmpty => "PID_" + proto.name
      case Some(proto) => proto.id
      case None => ""
    }).append(", ").append(item.checks.find(_.isInstanceOf[RandomCheck]) match {
      case Some(RandomCheck(value)) => value + ", "
      case _ => ""
    }).append(item.minCount).append(", ").append(item.maxCount).append(", ").append(item.slot).append(" )")
    itemBuild
  }

  private def buildItem(item: Item) = {
    val itemBuild = new StringBuilder
    itemBuild.append("group.AddItem( ").append(item.proto match {
      case Some(proto) if !proto.name.trim.isEmpty => "PID_" + proto.name
      case Some(proto) => proto.id
      case None => ""
    }).append(" )")
    itemBuild.append(".Ratio( ").append(item.ratio).append(" )")
    if (item.distance > 0) {
      itemBuild.append(".Distance( ").append(item.distance).append(" )")
    }
    item.dialog match {
      case Some(dialog) => itemBuild.append(".Dialog( DIALOG_").append(dialog.name).append(" )")
      case None =>
    }
    item.script match {
      case Some(script) => itemBuild.append(".Script( ").append(script).append(" )")
      case None =>
    }
    if (!item.checks.isEmpty) {
      itemBuild.append("\n\t").append(item.checks.map(buildCheck(_)).mkString("\n\t"))
    }
    itemBuild.append(";\n")
    itemBuild
  }

  def buildTables(tableModel: ItemModel[Table]) = {
    val tablesBuild = new StringBuilder
    tablesBuild.append("CEncounterTable@ table;\n\n")
    tablesBuild.append(tableModel.items
            .filterNot(table => table.locations.isEmpty)
            .map(buildTable(_)).mkString("\n\n"))
    tablesBuild
  }

  private def buildTable(table: Table) = {
    val tableBuild = new StringBuilder
    tableBuild.append("@table = @EncounterTables[ TABLE_" + table.name + " ];\n")
    tableBuild.append(table.locations.map(location => "table.AddLocationPid( LOCATION_" + location.name + " );\n").mkString)
    tableBuild.append(table.encounters.filter(_.text.isDefined).filterNot(_.groups.filter(_.group.isDefined).isEmpty).map(buildEncounter(_)).mkString)
    tableBuild
  }

  private def buildEncounter(encounter: Encounter) = {
    val encounterBuild = new StringBuilder
    encounterBuild.append("table.AddEncounter( ").append(encounter.chance).append(", ").append(encounter.text match {
      case Some(text) => text.id
      case None => ""
    }).append(" )")
    encounterBuild.append(encounter.groups
            .filter(_.group.isDefined)
            .map(group => ".AddGroup( GROUP_" + group.group.get.name + ", " + group.minRatio + ", " + group.maxRatio + " )").mkString)
    encounterBuild.append(encounter.fights
            .filter(_.groupFrom.group.isDefined)
            .map(fight => ".Fighting( " + encounter.groups.indexOf(fight.groupFrom) + ", " + encounter.groups.indexOf(fight.groupTo) + " )").mkString)
    encounterBuild.append(encounter.checks
            .map(buildCheck(_)).mkString)
    encounterBuild.append(";\n")
    encounterBuild
  }

  private def buildCheck(check: Check) = check match {
    case LVarCheck(name, operator, value) => ".CheckLVar( LVAR_" + name + ", \'" + operator + "\', " + value + " )"
    case GVarCheck(name, operator, value) => ".CheckGVar( GVAR_" + name + ", \'" + operator + "\', " + value + " )"
    case ParamCheck(name, operator, value) => ".CheckParam( " + name + ", \'" + operator + "\', " + value + " )"
    case RandomCheck(value) => ".CheckRandom( " + value + " )"
    case HourCheck(operator, value) => ".CheckHour( \'" + operator + "\', " + value + " )"
  }

  private def buildZones(zoneModel: ZoneModel) = {
    val zonesBuild = new StringBuilder
    zonesBuild.append(zoneModel.items
            .filter(_.table.isDefined)
            .filter(_.terrain.isDefined)
            .filterNot(zone => zone.morningChance.isEmpty && zone.afternoonChance.isEmpty && zone.nightChance.isEmpty)
            .sortBy(zone => zone.y * 100 + zone.x)
            .map(parseZone(_)).mkString)
    zonesBuild
  }

  private def parseZone(zone: Zone) = {
    val zoneBuild = new StringBuilder
    zoneBuild.append("SetZone( " + zone.x + ", " + zone.y + ", TABLE_" + zone.table.get.name + ", " + zone.difficulty + ", TERRAIN_" + zone.terrain.get.name + ", ")
    zoneBuild.append(zone.morningChance match {
      case Some(chance) => "CHANCE_" + chance.name
      case None => "CHANCE_None"
    }).append(", ")
    zoneBuild.append(zone.afternoonChance match {
      case Some(chance) => "CHANCE_" + chance.name
      case None => zone.morningChance match {
        case Some(morningChance) => "CHANCE_" + morningChance.name
        case None => "CHANCE_None"
      }
    }).append(", ")
    zoneBuild.append(zone.nightChance match {
      case Some(chance) => "CHANCE_" + chance.name
      case None => zone.morningChance match {
        case Some(morningChance) => "CHANCE_" + morningChance.name
        case None => "CHANCE_None"
      }
    }).append(" );\n")
    zoneBuild
  }
}
