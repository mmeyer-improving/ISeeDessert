using System;

namespace ISeeDessert
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal x = 0;
            decimal y = 0;

            //First variable
            Console.WriteLine("Enter a Number.");
            var firstInput = Console.ReadLine();
            if (Decimal.TryParse(firstInput, out x))
            { } 
            else 
            { 
                Console.WriteLine($"The first value, '{firstInput}', was not a number.");
                return;
            }

            //Second variable
            Console.WriteLine("Enter a second number, and I will add it to the first.");
            string secondInput = Console.ReadLine();
            if (Decimal.TryParse(secondInput, out y))
            { }
            else
            {
                Console.WriteLine($"The second value, '{secondInput}', was not a number.");
                return;
            }

            //Write answer
            Add(x, y);
        }

        static void Add (decimal x, decimal y)
        {
            var result = (decimal)x + y;
            Console.WriteLine($"result: {result}");
        }
    }
}
