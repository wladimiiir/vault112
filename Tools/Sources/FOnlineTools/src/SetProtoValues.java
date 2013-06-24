import java.io.*;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * @author Y12370
 * @version 1.0
 * @since 9.10.2012, 15:28
 */
public class SetProtoValues {

    public static void main(String[] args) throws IOException {
        if (args.length != 3) {
            System.out.println("Usage: SetProtoValues [proto_filename] [stat_names] [new_stat_value]\n" +
                    "Example: SetProtoValues tla.fopro SK_SMALL_GUNS;SK_UNARMED;SK_BIG_GUNS 200");
            return;
        }

        final File file = new File(args[0]);
        final String[] stats = args[1].split("[;,]");
        final String newValue = args[2];

        if (!file.exists()) {
            System.out.println("Specified file does not exists.");
            return;
        }

        final StringBuilder fileString = new StringBuilder(getString(file));
        for (String stat : stats) {
            final Pattern pattern = Pattern.compile(stat + "=(\\d*)");
            int start = 0;
            Matcher matcher = pattern.matcher(fileString);
            while (matcher.find(start)) {
                fileString.replace(matcher.start(), matcher.end(), stat + "=" + newValue);
                start = matcher.end();
                matcher = pattern.matcher(fileString);
            }
        }
        saveString(file, fileString.toString());
    }

    private static void saveString(File p_file, String p_fileString) throws IOException {
        final FileOutputStream outputStream = new FileOutputStream(p_file);
        outputStream.write(p_fileString.getBytes());
        outputStream.close();
    }

    private static String getString(File p_file) throws IOException {
        final ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
        final FileInputStream fileInputStream = new FileInputStream(p_file);
        byte[] bytes = new byte[1024];
        int read;
        while ((read = fileInputStream.read(bytes)) > 0) {
            outputStream.write(bytes, 0, read);
        }
        try {
            return outputStream.toString();
        } finally {
            outputStream.close();
        }
    }
}