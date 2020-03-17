using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Helpers;

namespace LongDistanceInputFileGenerator {
    /// <summary>
    /// inputs: 
    /// 1. directory - directory containing files that can be used to generate input files for the Long Distance AR Program
    /// 2. file name/identifier - expression used to identify the files that will be utilized to generate inputs
    /// 3. base name for generated files - the base save name for generated files
    /// 
    /// Program used to generate input files for the Long Distance AR application. It is meant to be used in conjunction 
    /// with another program (Trial Generator)
    /// </summary>
    class Program {
        static void Main(string[] args) {

            while (true) {
                Console.WriteLine("Press Q to quit or any other key to continue");

                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Q) {
                    break;
                }

                Console.Write(Environment.NewLine);

                Console.Write("Enter your inputs (directory) (file name/identifier) (base name for generated files): ");
                string input = Console.ReadLine();
                string[] input_args = new string[3];

                // Extract arguments from the string. An argument is anything enclosed by quotes 
                // or separated by spaces but not enclosed by quotes
                try {
                    int i = 0;
                    while (!string.IsNullOrEmpty(input)) {

                        input = input.Trim();
                        // argument enclosed by quotes
                        if (input[0] == '\"') {
                            int arg_start = input.IndexOf('\"');
                            int arg_end = input.NextIndexOf('\"', arg_start + 1);
                            string arg = input.IndexSubstring(arg_start, arg_end);
                            arg = arg.Trim('\"');
                            input = input.IndexRemove(arg_start, arg_end);
                            input_args[i] = arg;
                            i++;
                        }
                        else {
                            int arg_end = input.IndexOf(' ');
                            string arg = "";

                            if (arg_end == -1) {
                                arg = input;
                                input = input.Remove(0);
                            }
                            else {
                                arg = input.IndexSubstring(0, arg_end);
                                input = input.IndexRemove(0, arg_end);
                            }
                            arg = arg.Trim();
                            input_args[i] = arg;
                            i++;
                        }
                    }
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                    continue;
                }

                // check for the right number of arguments
                foreach (var arg in input_args) {
                    if (arg == null) {
                        Console.WriteLine("An error occured: incorrect number of inputs");
                        continue;
                    }
                }

                // regex with expression that'll be used to filter files         
                Regex name_checker = new Regex(string.Format("({0})", input_args[1]));

                List<string> file_names;

                // get all files in the input directory
                try {
                    file_names = Directory.EnumerateFiles(input_args[0]).ToList();
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                    continue;
                }

                // get the files that match the input expression
                GeneralHelpers.FindMatchingStrings(file_names, name_checker);

                // read the files and generate the long distance files
                for (int i = 0; i < file_names.Count; i++) {
                    string contents = File.ReadAllText(Path.Combine(input_args[0], file_names[i]));
                    string[] contents_array = contents.Trim().Split(' ');
                    GenerateLongDistanceFile(input_args[2], i + 1, contents_array);

                }

                Console.WriteLine("Success!");
                Console.Write(Environment.NewLine);
            }
        }

        /// <summary>
        /// Generates a file in the format required by the Long Distance AR Program
        /// </summary>
        /// <param name="base_name">name of the file</param>
        /// <param name="file_number">number to be appended to the file name</param>
        /// <param name="dynamic_inputs">array with the distance values that will be used by this file</param>
        private static void GenerateLongDistanceFile(string base_name, int file_number, string[] dynamic_inputs) {
            string target_name = base_name + file_number;
            string line1 = string.Format("{0}", target_name) + Environment.NewLine;
            string line2 = "casualMale13" + Environment.NewLine;
            string line3 = "(17)";

            for (int i = 0; i < dynamic_inputs.Length; i++) {
                line3 += string.Format(" ({0}) ", dynamic_inputs[i]);
            }

            line3 = line3.Trim();
            line3 += Environment.NewLine;

            string line4 = "1" + Environment.NewLine;
            string line5 = "0";

            string[] contents = new string[] { line1, line2, line3, line4, line5 };

            FileHelpers.SaveContentsToFile(target_name, contents);
        }
    }
}
