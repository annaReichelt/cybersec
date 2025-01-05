using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace Cybersec {
    class Dictionary {
        public Dictionary() { }


        public Dictionary<String, int> createDictionary(String path) {
            if (!File.Exists(path)) {
                throw new FileNotFoundException($"Could not find file '{path}'");
            }
            var lines = File.ReadAllLines(path);
            String[,] rawDict = new String[lines.Length, 2];
            Dictionary<string, int> dict = new Dictionary<string, int>();

            for (int i = 0; i < lines.Length; i++) {
                int count = 0;
                if (lines[i].Length > 4) {
                    String patternString = lines[i];
                    int matchable = (patternString.Length % 4) + 1;
                    for (int j = 1; j < lines.Length; j++) {
                        int start = 0;
                        String searchSpace = lines[j];
                        for (int k = 0; k < matchable; k++) {
                            String patternSubstring = patternString.Substring(start, 4);
                            Regex rgx1 = new Regex(patternSubstring);
                            if (rgx1.IsMatch(searchSpace)) {
                                count++;
                                //Console.WriteLine("Matched: " + patternSubstring + " in " + searchSpace);
                                start++;
                                if (dict.ContainsKey(patternSubstring)) {
                                    dict[patternSubstring] = dict[patternSubstring] + 1;
                                    break;
                                }
                                else {
                                    dict.Add(patternSubstring, count);
                                    break;
                                }

                            }
                        }
                    }
                }
                else {
                    for (int j = 0; j < lines.Length; j++) {
                        rawDict[i, 0] = lines[i];
                        Regex rgx = new Regex(lines[i]);
                        if (rgx.IsMatch(lines[j])) {
                            count++;
                            if (dict.ContainsKey(lines[i])) {
                                dict[lines[i]] = dict[lines[i]] + 1;
                                break;
                            }
                            else {
                                dict.Add(lines[i], count);
                                break;
                            }
                        }
                    }
                    rawDict[i, 1] = count.ToString();
                }
            }

            //get the top 100 matches
            return dict.OrderByDescending(x => x.Value)
                       .Take(50)
                       .ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}