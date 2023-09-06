using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace NewInCSharp8
{
    internal class PatternMatchingEnhancements
    {
        public void DeclarationAndTypePatterns()
        {
            object str = "Hello World!";
            if (str is string greeting)
            {
                Console.WriteLine(greeting);
            }

        }
        public static string GetType(object obj) => obj switch
        {
            string => "string",
            int => "integer",

            null => throw new ArgumentNullException(nameof(obj)),
            _ => throw new ArgumentException("Unknown type of a object", nameof(obj)),
        };

        public string GetSign(double number) => number switch
        {
            < 0 => "Negative",
            > 0 => "Positive",
            0 => "Zero",
            double.NaN => "Not a number"
        };
    }
}
