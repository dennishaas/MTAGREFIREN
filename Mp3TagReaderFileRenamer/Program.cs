using System;
using System.IO;
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace Mp3TagReaderFileRenamer
{
  
    public class Program
    {

        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void Main(string[] args)
        {
            
            string folderName = args[0];
            if (args[0] == null || args[0].Equals("-?"))
            {
                _log.Info("Mp3TagReaderFileRenamer  [folderName]");
            }
            string[] filePaths = Directory.GetFiles(folderName);
            foreach (string file in filePaths)
            {
                uint trackno = ReadMp3Tags(file);
                _log.InfoFormat("The old Filename is: {0}",
                    file);
                String newPath = AddTracknumberToFileName(trackno, file);
                _log.InfoFormat("The new Filename is : {0}",newPath);
                if (!File.Exists(newPath)){
                    File.Copy(file, newPath);
                    _log.Info("File was copied");
                } else{
                    _log.InfoFormat("File {0} already exists",newPath);

                }
            }




        }

        protected static String AddTracknumberToFileName(uint trackno, string file)
        {
            if (System.IO.File.Exists(file))
            {
                DirectoryInfo DI_Data = new DirectoryInfo(file);
                int numberOfParts = DI_Data.NumberOfParts();
                string filename = DI_Data.DirectoryPart(numberOfParts - 1);
                string tracknoprefix = GetTrackNoPrefixIncludingLeadingZero(trackno);
                if (!filename.StartsWith(tracknoprefix))
                {
                    filename = tracknoprefix + " - " + filename;
                }
                string currentNewPath = SpecifyPath(DI_Data);
                currentNewPath = AddFileName(filename, currentNewPath);               
                return currentNewPath;
            }
            return "";
        }

        protected static string SpecifyPath(DirectoryInfo DI_Data)
        {
            string currentNewPath = "";
            for (int i = DI_Data.NumberOfParts() - 1; i > 0; i--)
            {
                string currentSuffix = DI_Data.Parts()[i];
                _log.DebugFormat("CurrentSuffix: {0}", currentSuffix);
                if (DI_Data.NumberOfParts() - 1 == i)
                {
                    currentNewPath = currentNewPath + currentSuffix;
                }
                else
                {
                    currentNewPath = currentNewPath + @"\" + currentSuffix;
                }
                _log.DebugFormat("New Path: {0}", currentNewPath);
            }

            return currentNewPath;
        }

        protected static string AddFileName(string filename, string currentNewPath)
        {
            currentNewPath = currentNewPath + @"\" + filename;
            currentNewPath.Replace("\\\\", "\\");
            return currentNewPath;
        }

        public static string GetTrackNoPrefixIncludingLeadingZero(uint trackno)
        {
            return trackno / 10 == 0 ? "0" + trackno : "" + trackno;
        }

        protected static uint ReadMp3Tags(string filename)
        {
            TagLib.File file = TagLib.File.Create(filename);
            uint trackno = file.Tag.Track;
            String title = file.Tag.Title;
            String album = file.Tag.Album;
            String length = file.Properties.Duration.ToString();
            //Console.WriteLine(trackno);
            //Console.WriteLine(title);
            //Console.WriteLine(album);
            //Console.WriteLine(length);
            return trackno;
        }
    }
}
