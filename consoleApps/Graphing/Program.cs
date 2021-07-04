using System;
using System.Linq;

namespace GraphAppends
{
    class Program
    {
        public static void Main()
        {
            while (true)
            {
                Console.WriteLine("Deseja criar um novo grafico? ");

                if (Console.ReadLine().ToLower()?.FirstOrDefault() == 's')
                {
                    GraphMaking.DefaultSchedule();
                    
                    Console.WriteLine();
                    Console.WriteLine();

                    continue;
                }

                break;
            }
        }
    }
}