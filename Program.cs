using System;

namespace ISeeDessert
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a Number.");
            string xString = Console.ReadLine();
            var x = Decimal.Parse(xString);
            Console.WriteLine("Enter a second number, and I will add it to the first.");
            string yString = Console.ReadLine();
            var y = Decimal.Parse(yString);
            var answer = Add(x, y);
            Console.WriteLine($"Result: {answer}");
        }

        static decimal Add (decimal x, decimal y)
        {
            return x + y;
        }
    }
}
