package fonline.encounter.logic

import java.io.{FileWriter, FileReader, File}
import java.util.Properties
import javax.swing.filechooser.FileNameExtensionFilter
import swing.FileChooser
import swing.FileChooser.SelectionMode

/**
 * User: mikewall
 * Date: 10/31/12
 * Time: 7:03 PM
 */
object FileProvider {
  private val settingsFile = new File("settings.ini")
  private val properties = new Properties()
  if (!settingsFile.exists()) {
    settingsFile.createNewFile()
  }
  properties.load(new FileReader(settingsFile))

  def serverPath = {
    val serverPathOption = properties.get("serverDir") match {
      case serverPath: String => Option(serverPath)
      case _ => None
    }
    checkServerPath(serverPathOption) match {
      case Some(serverPath) => {
        properties.setProperty("serverDir", serverPath)
        val writer = new FileWriter(settingsFile)
        properties.store(writer, null)
        writer.close()
        Option(serverPath)
      }
      case None => None
    }
  }

  private def checkServerPath(serverPathOption: Option[String]): Option[String] = serverPathOption match {
    case Some(serverPath) if new File(serverPath, "scripts/_defines.fos").exists() => Option(serverPath)
    case _ => {
      val chooser = new FileChooser(new File(".")) {
        title = "Choose server path"
        fileSelectionMode = SelectionMode.DirectoriesOnly
      }
      chooser.showOpenDialog(null) match {
        case FileChooser.Result.Approve => checkServerPath(Option(chooser.selectedFile.getPath))
        case _ => None
      }
    }
  }

  def worldMapPath = {
    val worldMapPathOption = properties.get("worldMap") match {
      case worldMapFile: String => Option(worldMapFile)
      case _ => None
    }
    checkWorldMapPath(worldMapPathOption) match {
      case Some(worldMapPath) => {
        properties.setProperty("worldMap", worldMapPath)
        val writer = new FileWriter(settingsFile)
        properties.store(writer, null)
        writer.close()
        Option(worldMapPath)
      }
      case None => None
    }
  }

  private def checkWorldMapPath(worldMapPathOption: Option[String]): Option[String] = worldMapPathOption match {
    case Some(worldMapPath) if new File(worldMapPath).exists() => Option(worldMapPath)
    case _ => {
      val chooser = new FileChooser(new File(".")) {
        title = "Choose worldmap picture"
        fileSelectionMode = SelectionMode.FilesOnly
        fileFilter = new FileNameExtensionFilter("Pictures", "png", "jpg", "bmp")
      }
      chooser.showOpenDialog(null) match {
        case FileChooser.Result.Approve => checkWorldMapPath(Option(chooser.selectedFile.getPath))
        case _ => None
      }
    }
  }

  def generatedWorldMapInitPath = {
    val pathOption = properties.get("generatedWorldMapInit") match {
      case path: String => Option(path)
      case _ => None
    }
    val chooser = new FileChooser(new File(pathOption.getOrElse("."))) {
      title = "Generate to file..."
      fileSelectionMode = SelectionMode.FilesOnly
      selectedFile = new File(pathOption.getOrElse("worldmap_init.fos"))
    }
    chooser.showSaveDialog(null) match {
      case FileChooser.Result.Approve => {
        properties.setProperty("generatedWorldMapInit", chooser.selectedFile.getPath)
        val writer = new FileWriter(settingsFile)
        properties.store(writer, null)
        writer.close()
        Option(chooser.selectedFile.getPath)
      }
      case _ => None
    }
  }
}
