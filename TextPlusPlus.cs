using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TextPlusPlus
{
    public class Parser
    {
        private static bool LineContainsCharacters(string line)
        {
            string validChars = "abcdefghijklmnopqrstuvwxyz123456789$";

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

                for (int idx = line.IndexOf("$") + 1; idx < line.Length; idx++)
                {
                    // remember your raw C syntax folks!
                    // "" = string
                    // '' = char

                    if (line[idx] != ' ')
                        thisVarName += line[idx];
                    else
                        lastNextIndex = idx; break;
                }

                if (Variables.ContainsKey(thisVarName))
                    nl.Replace($"${thisVarName}", Variables[thisVarName]);
                else
                    // lastly, check if it's defining the thing
                    
                    int equPos = (line[lastNextIndex] == '=' | line[lastNextIndex - 1] == '=') ? lastNextIndex : 0;
                    string thisValue = "";

                    if (equPos != 0) {
                        for (int idx = equPos + line[idx + 1] == ' ' ? 2 : 1; idx < line.Length; idx++)
                        {
                            thisValue += line[idx];
                        }

                        Variables.Add(thisVarName, thisValue);
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

    // handler for math functions on TXT++
    internal class Math
    {
        internal static bool IsMathOperation(char c)
        {
            string MathCharacters = "+-*/^";

            foreach (char mc in MathCharacters) {
                if (c == mc) return true;
            }

            return false;
        }

        internal static float ProcessMath(string query2)
        {
            string query = query2.Replace(' ', '');
            string n1;
            string n2;
            bool _1 = false;
            string operation;

            for (int strIdx = 0; strIdx < query.Length; strIdx++)
            {
                if (!IsMathOperation(query[strIdx]))
                {
                    if (_1)
                        n2 += query[strIdx];
                    else
                        n1 += query[strIdx];
                } else
                {
                    switch (query[strIdx])
                    {
                        case "+":
                            operation = "add";
                            break;
                        case "-":
                            operation = "sub";
                            break;
                        case "*":
                            operation = "mul";
                            break;
                        case "/":
                            operation = "div";
                            break;
                        case "^":
                            operation = "pwr";
                            break;
                    }
                }
            }

            switch (operation)
            {
                case "add":
                    return float.TryParse(n1) + float.TryParse(n2);
                case "sub":
                    return float.TryParse(n1) - float.TryParse(n2);
                case "mul":
                    return float.TryParse(n1) * float.TryParse(n2);
                case "div":
                    return float.TryParse(n1) / float.TryParse(n2);
                case "pwr":
                    return float.TryParse(n1)^float.TryParse(n2);
            }

            return "";
        }
    }
}