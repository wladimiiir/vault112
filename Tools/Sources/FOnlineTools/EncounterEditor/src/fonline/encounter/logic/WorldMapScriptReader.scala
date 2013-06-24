package fonline.encounter.logic

import collection.mutable
import io.Source
import java.io.File
import fonline.logic.ScriptReader.getScript

/**
 *
 * @author mikewall
 * @since 15.10.2012, 9:54
 * @version 1.0
 */
object WorldMapScriptReader {
  def getWorldMapScript(serverPath: String) = getScript(serverPath, "worldmap.fos")

  def getWorldMapInitScript(serverPath: String) = getScript(serverPath, "worldmap_init.fos")
}