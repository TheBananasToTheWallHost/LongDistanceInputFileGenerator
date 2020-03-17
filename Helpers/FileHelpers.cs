using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace Helpers
{
    public static class FileHelpers
    {
        /// <summary>
        /// Saves a file and its contents to the programs default location (Desktop: Trial Generator Files\Output_Trials)
        /// </summary>
        /// <param name="filename">the name of the file</param>
        /// <param name="contents">the contents of the file</param>
        public static void SaveContentsToFile(string filename, string[] contents) {


            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string directory = "Distance Inputs/";
            string directory_path = Path.Combine(desktop, directory);

            if (!Directory.Exists(directory_path)) {
                Directory.CreateDirectory(directory_path);
            }

            string filename_with_extension = filename + ".txt";
            string full_path = Path.Combine(directory_path, filename_with_extension);
            string new_contents = "";

            for (int i = 0; i < contents.Length; i++) {
                new_contents += contents[i];
            }

            FileStream file = File.Create(full_path);

            file.Write(Encoding.ASCII.GetBytes(new_contents), 0, Encoding.ASCII.GetByteCount(new_contents));
            file.Dispose();
        }
    }
}
