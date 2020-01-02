using System;
using System.IO;

namespace Mp3TagReaderFileRenamer
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderName = args[0];
            if (args[0] == null || args[0].Equals("-?"))
            {
                Console.WriteLine("Mp3TagReaderFileRenamer  [folderName]");
            }
            string[] filePaths = Directory.GetFiles(folderName);
            foreach (string file in filePaths)
            {
                uint trackno = ReadMp3Tags(file);
                String newPath = AddTracknumber(trackno, file);
                Console.WriteLine(newPath);
                File.Copy(file, newPath);
            }




        }

        private static String AddTracknumber(uint trackno, string file)
        {
            if (System.IO.File.Exists(file))
            {
                DirectoryInfo DI_Data = new DirectoryInfo(file);
                int numberOfParts = DI_Data.NumberOfParts();
                string filename = DI_Data.DirectoryPart(numberOfParts - 1);
                string tracknoprefix = trackno / 10 == 0 ? "0" + trackno : "" + trackno;
                filename = tracknoprefix + " - " + filename;
                Console.WriteLine("???" + filename);
                string newPath = "";
                for (int i = DI_Data.NumberOfParts()-1 ; i > 0; i--)
                {
                    string suffix = DI_Data.Parts()[i];
                    Console.WriteLine(suffix);
                    if (DI_Data.NumberOfParts()- 1 == i)
                    {
                        newPath = newPath + suffix;
                    }
                    else
                    {
                        newPath = newPath + @"\" + suffix;
                    }                
                    Console.WriteLine("New Path" +newPath);
                }
                newPath = newPath + @"\" +filename;
                newPath.Replace("\\\\", "\\");
                Console.WriteLine("---" + file);
                Console.WriteLine("***" + newPath);
                return newPath;
            }
            return "";
        }



        private static uint ReadMp3Tags(string filename)
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
