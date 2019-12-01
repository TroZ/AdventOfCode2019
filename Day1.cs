using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day1
    {

        public void main()
        {
            string[] lines = Program.readFile(1);


            int total = 0;
            foreach(string line in lines)
            {
                int num = int.Parse(line);
                total += num / 3 - 2;
            }
            System.Console.WriteLine("total = " + total);



            total = 0;
            foreach (string line in lines)
            {
                int num = int.Parse(line);

                int fuel = num / 3 - 2;

                int add = fuel;
                while (add > 0)
                {
                    total += add;
                    add = add / 3 - 2;
                }

            }
            

            
            System.Console.WriteLine("total2 = " + total);
        }
    }
}
