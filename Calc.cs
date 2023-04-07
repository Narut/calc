using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace nCalculator
{
    public class Calc
    {
        private static String[] Operand = { "+", "-", "*", "/" };
        public static StringBuilder All_Equation = new StringBuilder();
        public static void AddData(string inputData)
        {
            All_Equation.Append(inputData);
        }
        public static string Calculate()
        {
            All_Equation = new StringBuilder(ConvertAllToDec(All_Equation.ToString()).ToString());
            string allEq = All_Equation.ToString();
            //string pattern = @"([\d]{1,})([-*+/]){1,}([\d]{1,})";
            string pattern1 = @"([box.\d]{1,})([*/]){1,}([box.\d]{1,})";
            string pattern2 = @"([box.\d]{1,})([-+]){1,}([box.\d]{1,})";
            allEq = RegExProcess(allEq,pattern1);
            allEq = RegExProcess(allEq,pattern2);

            return allEq;
        }
        private static string RegExProcess(string allEq, string pattern)
        {
            string val = "";
            Regex rx = new Regex(pattern, RegexOptions.IgnoreCase);
            // Find matches.
            MatchCollection matches = rx.Matches(allEq);
            while (matches.Count > 0)
            {
                val = "";
                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    string matchText = groups[0].Value;
                    double num1 = ConvertAllToDec(groups[1].Value);
                    string operand = groups[2].Value;
                    double num2 = ConvertAllToDec(groups[3].Value);

                    if (operand.Equals("*")) val = (num1 * num2).ToString();
                    else if (operand.Equals("/")) val = (num1 / num2).ToString();
                    else if (operand.Equals("+")) val = (num1 + num2).ToString();
                    else if (operand.Equals("-")) val = (num1 - num2).ToString();

                    allEq = allEq.Replace(matchText, val);
                }
                // Find matches.
                matches = rx.Matches(allEq);

            }
            return allEq;
        }
        private static double ConvertAllToDec(string input)
        {
            if (input.IndexOf("0x") == 0) return Convert.ToInt64(input.Replace("0x",""), 16);
            else if (input.IndexOf("0b") == 0) return Convert.ToInt64(input.Replace("0b",""), 2);
            else if (input.IndexOf("0o") == 0) return Convert.ToInt64(input.Replace("0o", ""), 8);
            else return Convert.ToDouble(input);
        }
    }
}
