package fonline.encounter.entity

/**
 *
 * @author mikewall
 * @since 12.10.2012, 10:07
 * @version 1.0
 */
class Zone(val x: Int, val y: Int) {
  var table: Option[Table] = None
  var difficulty = 0
  var terrain: Option[Terrain] = None
  var morningChance: Option[Chance] = None
  var afternoonChance: Option[Chance] = None
  var nightChance: Option[Chance] = None
}