using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Text;

namespace TXP
{
    public class Parser
    {
        private static bool LineContainsCharacters(string line)
        {
            string validChars = "abcdefghijklmnopqrstuvwxyz123456789${}+-*/^";

            foreach (char c in line)
            {
                if (!validChars.ToLower().Contains(c))
                    return false; break;
            }

            return true;
        }

        private static Dictionary<string, string> Variables = new();

        private static string ParseLineForVariables(string line)
        {
            string nl = line;

            if (line.Contains("$"))
            {
                string thisVarName = "";
                int lastNextIndex = 0;

                if (line[line.IndexOf("$") + 1] == '{') {
                    for (int idx = line.IndexOf("$") + 2; idx < line.Length; idx++)
                    {
                        // remember your raw C syntax folks!
                        // "" = string
                        // '' = char
    
                        if (line[idx] != '}')
                            thisVarName += line[idx];
                        else
                            lastNextIndex = idx; break;
                    }
                }

                if (Variables.ContainsKey(thisVarName))
                    nl.Replace($"$\{{thisVarName}\}", Variables[thisVarName]);
                else
                    // lastly, check if it's defining the thing
                    
                    int equPos = (line[lastNextIndex] == '=' | line[lastNextIndex - 1] == '=') ? lastNextIndex : 0;
                    string thisValue = "";

                    if (equPos != 0) {
                        for (int idx = equPos + line[idx + 1] == ' ' ? 2 : 1; idx < line.Length; idx++)
                        {
                            thisValue += line[idx];
                        }

                        Variables.Add(thisVarName, TryProcessMath(thisValue));
                        nl = "";
                        // define variables: $coolVariable = value
                    }
            }

            return nl;
        }

        public static void DefineVariable(string name, string value)
        {
            Variables.Add(name, value);
        }

        public static string GetVariable(string name)
        {
            if (Variables.ContainsKey(name)) return Variables[name];
            return "";
            
        }

        public static List<string> ParseSourceFile(string path)
        {
            List<string> fileLiteral = new List<string>(File.ReadAllLines(path));
            List<string> literalContents = new();

            foreach (string line in fileLiteral)
            {
                if (LineContainsCharacters(line)) &&
                {
                    string thing = ParseLineForVariables(line);
                    if (thing != "")
                        literalContents.Add(thing);
                }
            }

            return literalContents;
        }
    }
}
