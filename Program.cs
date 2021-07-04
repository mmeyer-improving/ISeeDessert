using System;
using System.Linq;

namespace ISeeDessert
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("A Console Calculator");
            Console.WriteLine("Enter what you would like to see added");

            string equation = Console.ReadLine();

            DoMath(equation);
        }

        static void DoMath (string equation)
        {
            var equationParts = equation.Split(' ').ToList();
            decimal x;
            decimal y;

            if (equationParts.Count() != 3)
            {
                Console.WriteLine("Invalid format.");
                return;
            }
            
            if (Decimal.TryParse(equationParts.ElementAt(0), out x))
            { }
            else
            {
                Console.WriteLine($"The first value, {equationParts.ElementAt(0)}, is not a number.");
                return;
            }

            if (Decimal.TryParse(equationParts.ElementAt(2), out y))
            { }
            else
            {
                Console.WriteLine($"The second value, {equationParts.ElementAt(2)}, is not a number.");
                return;
            }

            decimal answer = (decimal) x + y;
            Console.WriteLine($"Result: {answer}");
        }
    }
}
