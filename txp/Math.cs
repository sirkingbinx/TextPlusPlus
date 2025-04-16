using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Text;

namespace TXP {
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

        internal static string TryProcessMath(string query2)
        {
            string query = query2.Replace(' ', '');
            string finishedQuery = "";
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
                    return float.TryParse(n1) + float.TryParse(n2).ToString();
                case "sub":
                    return float.TryParse(n1) - float.TryParse(n2).ToString();
                case "mul":
                    return float.TryParse(n1) * float.TryParse(n2).ToString();
                case "div":
                    return float.TryParse(n1) / float.TryParse(n2).ToString();
                case "pwr":
                    return float.TryParse(n1)^float.TryParse(n2).ToString();
            }

            return query2;
        }
    }
}