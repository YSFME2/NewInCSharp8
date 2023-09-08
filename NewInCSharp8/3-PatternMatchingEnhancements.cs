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

        #region Property pattern

        static bool IsConferenceDay(DateTime date) => date is { Year: 2020, Month: 5, Day: 19 or 20 or 21 };

        static string TakeFive(object input) => input switch
        {
            string { Length: >= 5 } s => s[..5],
            string s => s,

            ICollection<char> { Count: >= 5 } symbols => new string(symbols.Take(5).ToArray()),
            ICollection<char> symbols => new string(symbols.ToArray()),

            null => throw new ArgumentNullException(nameof(input)),
            _ => throw new ArgumentException("Not supported input type."),
        };

        public record Point(int X, int Y);
        public record Segment(Point Start, Point End);

        static bool IsAnyEndOnXAxis(Segment segment) =>
            segment is { Start.Y: 0 } or { End.Y: 0 };

        #endregion

        #region Positional pattern
        public readonly struct PointPP
        {
            public int X { get; }
            public int Y { get; }

            public PointPP(int x, int y) => (X, Y) = (x, y);

            public void Deconstruct(out int x, out int y) => (x, y) = (X, Y);
            public void Method()
            {
                var numbers = new List<int> { 1, 2, 3 };
                if (SumAndCount(numbers) is { Sum: >1 , Count: >= 1 } result)
                {
                    Console.WriteLine($"Sum of numbers [{string.Join(",", numbers)}] is {result.Sum}");
                }
            }
            static (double Sum, int Count) SumAndCount(IEnumerable<int> numbers)
            {
                int sum = 0;
                int count = 0;
                foreach (int number in numbers)
                {
                    sum += number;
                    count++;
                }
                return (sum, count);
            }
        }

        static string Classify(PointPP point) => point switch
        {
            (0, 0) => "Origin",
            (1, 0) => "positive X basis end",
            (0, 1) => "positive Y basis end",
            _ => "Just a point",
        };

        static double Discount(int groupCount, DateTime date) => (groupCount, date.DayOfWeek) switch
        {
            (0, _) => throw new ArgumentException("Group must  be more than zero"),
            (_, DayOfWeek.Saturday or DayOfWeek.Sunday) => 0,
            ( >= 5 and <= 10, DayOfWeek.Thursday) => 0.25,
            ( >= 10, DayOfWeek.Thursday) => 0.3,
            ( >= 5, DayOfWeek.Friday) => 0.1,
            (_, DayOfWeek.Thursday) => 0.10,
            _ => 0
        };

        public record WeightedPoint(int X, int Y)
        {
            public double Weight { get; set; }
        }

        static bool IsInDomain(WeightedPoint point) => point is ( >= 0, >= 0) { Weight: >= 0.0 };
        #endregion

        #region var pattern
        static bool IsAcceptable(int id, int absLimit) =>
    SimulateDataFetch(id) is var results
    && results.Min() >= -absLimit
    && results.Max() <= absLimit;

        static int[] SimulateDataFetch(int id)
        {
            var rand = new Random();
            return Enumerable
                       .Range(start: 0, count: 5)
                       .Select(s => rand.Next(minValue: -10, maxValue: 11))
                       .ToArray();
        }

        static Point Transform(Point point) => point switch
        {
            var (x, y) when x < y => new Point(-x, y),
            var (x, y) when x > y => new Point(x, -y),
            var (x, y) => new Point(x, y),
        };

        static void TestTransform()
        {
            Console.WriteLine(Transform(new Point(1, 2)));  // output: Point { X = -1, Y = 2 }
            Console.WriteLine(Transform(new Point(5, 2)));  // output: Point { X = 5, Y = -2 }
        }
        #endregion

        #region Parenthesized pattern
        public void Method ()
        {
            object input = 0;
            if (input is not (float or double))
            {
                return;
            }
        }
        #endregion
    }
}
