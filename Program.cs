using System;
using System.Collections.Generic;
using System.Linq;

namespace ISeeDessert
{
    public class History
    {
        public decimal LastResult { get; set; }
        public List<string> EquationHistory { get; set; }

        public History()
        {
            EquationHistory = new List<string>();
        }
    }

    class Program
    {
        

        //TODO: Currently only the equation typed in is saved, need to save the equation **and** the result.
        //This is only a problem in a case like '+ 8' where just trying to save it at the end doesn't work
        //The way I have it set up, the equation is currently complete at that point. It doesn't know that it was ever was relying on history.LastResult
        //So first fix that, and in the process maybe save the equation as a list of parts, i.e. ["8", "+", "7", "=", "15"]. List<List<String>>
        //Might make the formatting history thing you do AFTER fixing this more sane.
        //That said, MAKE IT WORK FIRST AND FOREMOST!! DO NOT get stuck trying to make it perfect. Working and crappy is the Jr. Dev's signature
        //Will have a chance to do it better next week.


        static void Main(string[] args)
        {
            History history = new History();
            string equation;

            Console.WriteLine("A Console Calculator");
            do
            {
                
                Console.WriteLine("Enter what you would like to see calculated.");

                equation = Console.ReadLine();


                if (equation == "exit")
                {
                    return;
                }
                else if (equation == "history")
                {
                    DisplayHistory(history);
                }
                else
                {
                    ValidateFormat(equation, ref history);
                }
            } while (equation != "exit");
        }

        //Writes out the history
        static void DisplayHistory(History history)
        {
            var maxEquationWidth = -1;

            foreach (string equation in history.EquationHistory)
            {
                var equationParts = equation.Split(' ').ToList();

                string firstPart = equationParts.ElementAt(0) + " _ " + equationParts.ElementAt(2);

                if (firstPart.Length > maxEquationWidth)
                {
                    maxEquationWidth = firstPart.Length;
                }
            }


            foreach (string equation in history.EquationHistory)
            {
                var equationParts = equation.Split(' ').ToList();

                if (equationParts.Count() == 4)
                {
                    var firstPart = $"{equationParts.ElementAt(0)} {equationParts.ElementAt(1)}";
                    var firstPartWithSpacing = firstPart.PadRight(maxEquationWidth, ' ');

                    Console.WriteLine(String.Format(" {0}  {1} {2}", firstPartWithSpacing, "=", equationParts.ElementAt(3)));
                } 
                else 
                {
                    var firstPart = $"{equationParts.ElementAt(0)} {equationParts.ElementAt(1)} {equationParts.ElementAt(2)}";
                    var firstPartWithSpacing = firstPart.PadRight(maxEquationWidth, ' ');

                    Console.WriteLine(String.Format(" {0}  {1} {2}", firstPartWithSpacing, "=", equationParts.ElementAt(4)));
                }
            }
        }

        //Checks to see if equation has 1 or 2 parts, then validates parts based on that. 
        //This feels kinda bad, but I can't figure out a nice way to refactor the number checks into another method.
        static void ValidateFormat (string equation, ref History history)
        {
            var equationParts = equation.Split(' ').ToList();
            decimal x;
            decimal y;
            string operand;

            if (!ValidateParts(equationParts)) 
            {
                return;
            }

            if(equationParts.Count() == 3) //normal equation, validate first number, operand, last number, break equation if any do not fit
            {
                if (!(Decimal.TryParse(equationParts.ElementAt(0), out x)))
                {
                    Console.WriteLine($"The first value, {equationParts.ElementAt(0)}, is not a number.");
                    return;
                }

                if (!isValidOperand(equationParts.ElementAt(1)))
                {
                    return;
                }
                else
                {
                    operand = equationParts.ElementAt(1);
                }

                if (!(Decimal.TryParse(equationParts.ElementAt(2), out y)))
                {
                    Console.WriteLine($"The second value, {equationParts.ElementAt(2)}, is not a number.");
                    return;
                }
            }
            else //equation definitely has two pieces, now need to validate operand to pass on, then grab the x value from history to pass on, then validate y
            {
                
                if (!isValidOperand(equationParts.ElementAt(0)))
                {
                    return;
                } 
                else
                {
                    operand = equationParts.ElementAt(0);
                    x = history.LastResult;
                }

                if (!(Decimal.TryParse(equationParts.ElementAt(1), out y)))
                {
                    Console.WriteLine($"The second value, {equationParts.ElementAt(1)}, is not a number.");
                    return;
                }
            }

            ChooseOperation(x, y, operand, ref history, equation);
        }


        static bool ValidateParts(List<string> equationParts)
        {
            string test = "test";
            if (!(equationParts.Count() == 3 || equationParts.Count() == 2))
            {
                Console.WriteLine("An operation must be in the following format: '5 + 8' or '+ 8' if countinuing from a previous value. Please try again.");
                return false;
            }
            else
            {
                return true;
            }
        }

        static bool isValidOperand(string potentialOperand)
        {
            List<string> operands = new List<string>()
            {
                "+",
                "-",
                "*",
                "/"
            };

            if (!operands.Contains(potentialOperand))
            {
                Console.WriteLine($"The Operation {potentialOperand} is invalid. Please use one of the following: + - * /");
                return false;
            } 
            else
            {
                return true;
            }
        }

        static void ChooseOperation(decimal x, decimal y, string operand, ref History history, string equation)
        {
            if (operand == "+")
            {
                Add(x, y, ref history, equation);
            }
            else if (operand == "-")
            {
                Subtract(x, y, ref history, equation);
            } else if (operand == "*")
            {
                Multiply(x, y, ref history, equation);
            } else if (operand == "/")
            {
                Divide(x, y, ref history, equation); ;
            }
        }

        static void Add(decimal x, decimal y, ref History history, string equation)
        {
            decimal additionResult = (decimal)x + y;
            history.LastResult = additionResult;
            history.EquationHistory.Add($"{equation} = {additionResult}");
            Console.WriteLine($"Result: {additionResult}");
        }

        static void Subtract(decimal x, decimal y, ref History history, string equation)
        {
            decimal subtractionResult = (decimal)x - y;
            history.LastResult = subtractionResult;
            history.EquationHistory.Add($"{equation} = {subtractionResult}");
            Console.WriteLine($"Result: {subtractionResult}");
        }

        static void Multiply (decimal x, decimal y, ref History history, string equation)
        {
            decimal multiplicationResult = (decimal)x * y;
            history.LastResult = multiplicationResult;
            history.EquationHistory.Add($"{equation} = {multiplicationResult}");
            Console.WriteLine($"Result: {multiplicationResult}");
        }

        static void Divide (decimal x, decimal y, ref History history, string equation)
        {
            if (y == 0)
            {
                Console.WriteLine("Cannot Divide by 0");
            } 
            else
            {
                decimal divisionResult = (decimal)x / y;
                history.LastResult = divisionResult;
                history.EquationHistory.Add($"{equation} = {divisionResult}");
                Console.WriteLine($"Result: {divisionResult}");
            }
        }
    }
}
