package fonline.utils

import java.util.Properties
import util.matching.Regex
import io.Source
import java.io.{FileWriter, StringReader}
import collection.JavaConversions
import util.Random

/**
 * User: wladimiiir
 * Date: 1/3/13
 * Time: 10:41 PM
 */
object TileMapperPresetGenerator {

  def main(args: Array[String]) {
    if (args.length != 3) {
      println("Usage: \"java -jar TileMapperPresetGenerator.jar [input_preset] [preset_transform] [output_preset]\"")
      return
    }

    val preset = Source.fromFile(args(0)).getLines().mkString("\n")
    val tileTransforms = getTileTransforms(args(1))
    val sceneryTransforms = getSceneryTransforms(args(1))

    val outputPreset = tileTransforms.foldLeft(preset)((input, transform) => transform.process(input))

    val writer = new FileWriter(args(2))
    writer.write(outputPreset)
    writer.close()
  }

  private def getTileTransforms(transformFile: String) = {
    val properties = new Properties()
    properties.load(new StringReader(getSection("Tiles", transformFile)))

    JavaConversions.asScalaSet(properties.entrySet()).map(entry => new TileTransform(entry.getKey.asInstanceOf[String], entry.getValue.asInstanceOf[String]))
  }

  private def getSceneryTransforms(transformFile: String) = {
    val properties = new Properties()
    properties.load(new StringReader(getSection("Tiles", transformFile)))

    JavaConversions.asScalaSet(properties.entrySet()).map(entry => new SceneryTransform(entry.getKey.asInstanceOf[String], entry.getValue.asInstanceOf[String]))
  }

  private def getSection(sectionName: String, transformFile: String) = {
    val transformLines = Source.fromFile(transformFile).getLines()
    val startIndex = transformLines.indexWhere(_ == "[" + sectionName + "]")

    transformLines.drop(startIndex).takeWhile(line => !line.startsWith("[") && !line.endsWith("]")).mkString("\n")
  }
}

class TileTransform(val fromTile: String, to: String) {

  private def getToTile = {
    val toTiles = to.split(";")(0).split(",")
    toTiles(Random.nextInt(toTiles.length))
  }

  def process(input: String) = {
    input.replaceAll(fromTile, getToTile)

  }
}

class SceneryTransform(val fromScenery: String, to: String) {

}