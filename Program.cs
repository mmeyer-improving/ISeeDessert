using System;
using System.Collections.Generic;
using System.Linq;

namespace ISeeDessert
{
    public class History
    {
        public decimal LastResult { get; set; }
    }

    class Program
    {
        

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
                ValidateFormat(equation, ref history);
            } while (equation != "exit");
        }

        //Checks to see if both the first and last parts of the equation are numbers, then passes all parts on to validate the operand.
        //This feels kinda bad, but I can't figure out a nice way to refactor the number checks into another method.
        //Don't think I can return null from a Decimal method, which would still result in this method being a mess of logic.
        static void ValidateFormat (string equation, ref History history)
        {
            var equationParts = equation.Split(' ').ToList();
            decimal x;
            decimal y;
            string operand;




            //TODO: Check to see if operand has 3 parts (simple count check in this function on size of equationParts
            // If it has 3 pieces, proceed as listed below
            // else, if it has two pieces, check if first piece is valid operand
            // if that passes, move onto next stage with history.LastResult as the decimal x


            if (equationParts.Count == 3) 
            { 

            }





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

            ChooseOperation(x, y, operand, ref history);
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

        static void ChooseOperation(decimal x, decimal y, string operand, ref History history)
        {
            if (operand == "+")
            {
                Add(x, y, ref history);
            }
            else if (operand == "-")
            {
                Subtract(x, y, ref history);
            } else if (operand == "*")
            {
                Multiply(x, y, ref history);
            } else if (operand == "/")
            {
                Divide(x, y, ref history); ;
            }
        }

        static void Add(decimal x, decimal y, ref History history)
        {
            decimal additionResult = (decimal)x + y;
            history.LastResult = additionResult;
            Console.WriteLine($"Result: {additionResult}");
        }

        static void Subtract(decimal x, decimal y, ref History history)
        {
            decimal subtractionResult = (decimal)x - y;
            history.LastResult = subtractionResult;
            Console.WriteLine($"Result: {subtractionResult}");
        }

        static void Multiply (decimal x, decimal y, ref History history)
        {
            decimal multiplicationResult = (decimal)x * y;
            history.LastResult = multiplicationResult;
            Console.WriteLine($"Result: {multiplicationResult}");
        }

        static void Divide (decimal x, decimal y, ref History history)
        {
            if (y == 0)
            {
                Console.WriteLine("Cannot Divide by 0");
            } 
            else
            {
                decimal divisionResult = (decimal)x / y;
                history.LastResult = divisionResult;
                Console.WriteLine($"Result: {divisionResult}");
            }
        }
    }
}
