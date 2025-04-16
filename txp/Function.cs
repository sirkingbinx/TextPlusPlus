namespace TXP
{
    public class Function
    {
        // handles stuff like printing to the console

        internal static Dictionary<string, Action> functions = new Dictionary<string, Action>();

        internal static void InitializeCorelibs()
        {
            functions.Add("print", TXP_ProgramFN.str_print);
            functions.Add("upper", TXP_ProgramFN.str_upper);
            functions.Add("lower", TXP_ProgramFN.str_lower);
        }

        internal static void TryParseForFunctions()
        {
            // wip
        }
    }

    public class TXP_ProgramFN
    {
        public static string str_print(List<string> args)
        {
            Console.WriteLine(args[0] != null ? args[0] : '');
            return "OK";
        }

        string lower = "abcdefghijklmnopqrstuvwxyz";
        string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string str_upper(List<string> args)
        {
            if (args[0] != null)
            {
                string toUpperThing = args[0].ToString();
                string reconstructed = "";

                foreach (char c in toUpperThing) {
                    if (lower.IndexOf(c) != null)
                        reconstructed += upper[lower.IndexOf(c)];
                }

                return reconstructed;
            }

            return '';
        }

        public static string str_lower(List<string> args)
        {
            if (args[0] != null)
            {
                string toLowerThing = args[0].ToString();
                string reconstructed = "";

                foreach (char c in toLowerThing) {
                    if (upper.IndexOf(c) != null)
                        reconstructed += lower[upper.IndexOf(c)];
                }

                return reconstructed;
            }

            return '';
        }
    }
}