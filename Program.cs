﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //Day1 prog = new Day1();
            //Day2 prog = new Day2();
            //Day3 prog = new Day3();
            //Day4 prog = new Day4();
            //Day5 prog = new Day5();
            //Day6 prog = new Day6();
            //Day7 prog = new Day7();
            //Day8 prog = new Day8();
            //Day9 prog = new Day9();
            //Day10 prog = new Day10();
            //Day11 prog = new Day11();
            //Day12 prog = new Day12();
            //Day13 prog = new Day13();
            //Day14 prog = new Day14();
            //Day15 prog = new Day15(); //has a main2 for part2
            //Day16 prog = new Day16();
            Day17 prog = new Day17();
            prog.main();
            //prog.main2();
        }


        public static string[] readFile(int num)
        {
            /*
            int counter = 0;
            string line;
            Dictionary<int, string> lines = new Dictionary<int, string>();

            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\TroZ\Projects\AdventOfCode\AdventOfCode\AdventOfCode\day"+num+".txt");
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(counter, line);
                System.Console.WriteLine(line);
                counter++;
            }

            file.Close();
            System.Console.WriteLine("There were {0} lines.", counter);

            string[] ret = new string[lines.Count];
            for(int i = 0; i < ret.Length; i++)
            {
                ret[i] = lines[i];
            }
            /*/
            return System.IO.File.ReadAllLines(@"C:\Users\TroZ\Projects\AdventOfCode\AdventOfCode\AdventOfCode\day" + num + ".txt");
            //*/
        }

        internal static void saveImg(Bitmap pic, int num)
        {
            pic.Save(@"C:\Users\TroZ\Projects\AdventOfCode\AdventOfCode\AdventOfCode\day" + num + ".png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
