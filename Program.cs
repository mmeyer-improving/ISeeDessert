using System;
using System.Collections.Generic;
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

            ValidateFormat(equation);
        }

        //Checks to see if both the first and last parts of the equation are numbers, then passes all parts on to validate the operand.
        //This feels kinda bad, but I can't figure out a nice way to refactor the number checks into another method. Don't think I can return null from a Decimal method, which would still result in this method being a mess of logic.
        static void ValidateFormat (string equation)
        {
            var equationParts = equation.Split(' ').ToList();
            decimal x;
            decimal y;

            if (!CheckForThree(equationParts)) {
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

            CheckOperand(x, y, equationParts.ElementAt(1));
        }

        static bool CheckForThree(List<string> equationParts)
        {
            if (equationParts.Count() != 3)
            {
                Console.WriteLine("Invalid format.");
                return false;
            }
            else
            {
                return true;
            }
        }

        static void CheckOperand(decimal x, decimal y, string operand)
        {
            List<string> operands = new List<string>()
            {
                "+",
                "-",
                "*",
                "/"
            };

            if (!operands.Contains(operand))
            {
                Console.WriteLine("Invalid Operand");
                return;
            }
            else if (operand == "+")
            {
                Add(x, y);
            }
            else if (operand == "-")
            {
                Subtract(x, y);
            } else if (operand == "*")
            {
                Multiply(x, y);
            } else if (operand == "/")
            {
                Divide(x, y);
            }
        }

        static void Add(decimal x, decimal y)
        {
            decimal result = (decimal)x + y;
            Console.WriteLine($"Result: {result}");
        }

        static void Subtract(decimal x, decimal y)
        {
            decimal result = (decimal)x - y;
            Console.WriteLine($"Result: {result}");
        }

        static void Multiply (decimal x, decimal y)
        {
            decimal result = (decimal)x * y;
            Console.WriteLine($"Result: {result}");
        }

        static void Divide (decimal x, decimal y)
        {
            if (y == 0)
            {
                Console.WriteLine("Cannot Divide by 0");
            } 
            else
            {
                decimal result = (decimal)x / y;
                Console.WriteLine($"Result: {result}");
            }
        }
    }
}
