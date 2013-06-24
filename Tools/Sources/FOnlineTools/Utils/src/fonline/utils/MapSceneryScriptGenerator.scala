package fonline.utils

import io.Source
import util.matching.Regex
import java.io.{FileWriter, File}

/**
 * User: wladimiiir
 * Date: 12/20/12
 * Time: 6:56 PM
 */
object MapSceneryScriptGenerator {
  def main(args: Array[String]) {

    val sceneryPids = Array(2220, 2221, 2222, 2223, 2252, 2253, 2257, 2265, 2266, 2267, 2268, 2269, 2270, 2275, 2276, 2311, 2315, 2005, 2006)
    val serverPath = "d:\\Development\\vault112\\Server\\"

    for (cityNum <- Range(1, 9)) {
      val file = new File(serverPath + "maps/e_city" + cityNum + ".fomap")
      if (file.exists()) {
        println("Processing \"" + file.getAbsolutePath + "\"...")
        var map = Source.fromFile(file).getLines().mkString("\n")
        for (sceneryPid <- sceneryPids) {
          map = map.replaceAll("ProtoId\\s*" + sceneryPid + "\\s*MapX\\s*\\d*\\s*MapY\\s*\\d*", "$0\n" +
            "ScriptName           resources\nFuncName             _ScavengeSourceInit")
        }

        val writer = new FileWriter(file)
        writer.write(map)
        writer.close()
      }
    }
  }
}
