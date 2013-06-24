package fonline.trader.logic

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

  def generatedTraderInitPath = {
    val pathOption = properties.get("generatedTraderInit") match {
      case path: String => Option(path)
      case _ => None
    }
    val chooser = new FileChooser(new File(pathOption.getOrElse("."))) {
      title = "Generate to file..."
      fileSelectionMode = SelectionMode.FilesOnly
      selectedFile = new File(pathOption.getOrElse("trader_init.fos"))
    }
    chooser.showSaveDialog(null) match {
      case FileChooser.Result.Approve => {
        properties.setProperty("generatedTraderInit", chooser.selectedFile.getPath)
        val writer = new FileWriter(settingsFile)
        properties.store(writer, null)
        writer.close()
        Option(chooser.selectedFile.getPath)
      }
      case _ => None
    }
  }
}
