using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Helpers {
    public static class GeneralHelpers {

        /// <summary>
        /// Checks a list of strings for a match to a regular expression. Any string that doesn't
        /// match the regular expression is removed from the list.
        /// </summary>
        /// <param name="strings">a list of strings to compare against</param>
        /// <param name="string_checker">a regex object with the regular expression being searched for</param>
        public static void FindMatchingStrings(List<string> strings, Regex string_checker) {
            for (int i = 0; i < strings.Count(); i++) {
                if (!string_checker.IsMatch(strings[i])) {
                    strings.RemoveAt(i);
                    i--;
                }
            }
        }
        
    }
}
