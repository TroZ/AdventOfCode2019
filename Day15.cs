using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day15
    {
        public static int size = 60;

        int[,] area = new int[size, size];
        int xpos = size / 2;
        int ypos = size / 2;

        public void main()
        {

            long[] data = getData();


            Intcode15 comp = new Intcode15(data);
            long outp = 0;
            printMap();
            while (outp != 2 && !comp.isHalted())
            {

                string str = "";
                while (str.Length < 1)
                {
                    str = Console.ReadLine();
                }
                int inp = 0;
                switch (str[0])
                {
                    case 'e': inp = 1; break;
                    case 'd': inp = 2; break;
                    case 's': inp = 3; break;
                    case 'f': inp = 4; break;
                }

                comp.setInput(inp);
                comp.runToOutOrHalt();

                outp = comp.getOutput();
                if(outp == 0)
                {
                    switch (str[0])
                    {
                        case 'e': area[xpos,ypos-1] = 3; break;
                        case 'd': area[xpos, ypos + 1] = 3; break;
                        case 's': area[xpos-1, ypos] = 3; break;
                        case 'f': area[xpos+1, ypos] = 3; break;
                    }
                }
                else
                {
                    switch (str[0])
                    {
                        case 'e': ypos--; break;
                        case 'd': ypos++; break;
                        case 's': xpos--; break;
                        case 'f': xpos++; break;
                    }
                    area[xpos, ypos] = (int)outp;
                }


                printMap();

            }

            printMap(true);


            
        }


        public void main2()
        {

            string[] mapdata = @" #### ### ############# # ########### ###
 #...#...#.............#.#...........#...#
 #.#.#.#.###.#########.#.#.###.#####.###.#
 #.#...#...#...#.....#.#.#.#...#.........#
 #.#######.###.#.#####.#.#.#.###########.#
 #.#...........#.......#.#.#.......#...#.#
 #.###.#########.#######.#.#######.#.#.#.#
 #...#.#.......#.#...............#.#.#...#
 ###.###.#####.#.#.###########.###.#.####
 #...#...#...#.#.#...#...#.....#...#.#...#
 #.###.###.#.#.#.#####.#.#.#####.###.###.#
 #...#...#.#.#.#...#...#.#...#...#.......#
 #.#.###.###.#.###.#.###.#####.##########
 #.#...#.#...#.#...#.#.#.......#...#.....#
 #####.#.#.###.#.###.#.###.#####.#.#.###.#
 #.....#.#...#.#.....#...#.#...#.#.....#.#
 #.#####.#.#.#.#######.#.#.#.#.#.#######.#
 #.#.....#.#.#...#.....#...#.#...#.......#
 #.#.#######.###.###########.#####.######
 #.#.#.....#...#.#...........#   #.#
 #.#.#.#.#.#.#.#.#.#####.####    #.#
 #.#.#.#.#...#.#.#.#...#         #.#
 #.#.###.#####.#.#.#.##          #.#
 #.#...#.....#.#...#.#           #.#
 #.###.#.###.#####.#.#          ##.#
 #.....#.#...#...#.#.#         #...#
 #.#####.#.###.#.###.###########.###.####
 #...#...#...#.#.....#...........#.......
 ###.###.###.#.#######.###########.#####.#
 #.#...#.#...#.........#   #.....#.#.....#
 #.###.###.#.##########   ##.#.###.#.###.#
 #.#...#...#.#...#.....# #...#.....#.#...#
 #.#.###.###.#.#.#.###.# #.#########.#.##
 #...#...#.#...#.#.#...# #.#......@#.#...#
 #.###.###.#####.#.#.##  #.###.   ##.###.#
 #.#...#.......#...#.#    .....# #...#.#.#
 #.###.#.###.#######.## ######.###.###.#.#
 #.#...#.#.#.......#...#.....#.#...#.....#
 #.#.###.#.#######.###.#.###.###.###.####
 #...#...........#.......#.......#.......#
 #### ########### ####### ####### #######".Split("\n");


            int[,] area = new int[50, 50];

            //init
            for (int y = 0; y < 50; y++)
            {
                for (int x = 0; x < 50; x++)
                {

                    area[x, y] = -1;
                }
            }


            //fill map
            for (int y = 0; y < mapdata.Length; y++)
            {
                string line = mapdata[y].Trim();
                for (int x = 0; x < line.Length; x++)
                {
                    char c = line[x];
                    switch (c)
                    {
                        case '#': area[x, y] = -1; break;
                        case '.': area[x, y] = 0; break;
                        case '@': area[x, y] = 1; break;
                    }
                }
            }


            //spread O2
            bool added = true;
            int max = 1;
            while (added)
            {
                added = false;

                for (int y = 0; y < mapdata.Length; y++)
                {
                    string line = mapdata[y].Trim();
                    for (int x = 0; x < 50; x++)
                    {
                        if (x < 49 && area[x + 1, y] == 1)
                        {
                            Console.WriteLine("here");
                        }
                        if (area[x, y] == 0) {
                            int val1 = (x > 0) ? area[x - 1, y] : 0;
                            int val2 = (x < 49) ? area[x + 1, y] : 0;
                            int val3 = (y > 0) ? area[x, y-1] : 0;
                            int val4 = (y < 49) ? area[x, y+1] : 0;

                            int val = (val1 < 0) ? 0: val1 ;
                            val = (val <= 0) ? val2 : (val2 <= 0) ? val : Math.Min(val2, val);
                            val = (val <= 0) ? val3 : (val3 <= 0) ? val : Math.Min(val3, val);
                            val = (val <= 0) ? val4 : (val4 <= 0) ? val : Math.Min(val4, val);
                            if (val > 0)
                            {
                                added = true;
                                area[x, y] = val+1;
                                if (val + 1 > max)
                                {
                                    max = val + 1;
                                }
                            }

                        }
                    }

                }

            }


            Console.WriteLine("Max time = " + (max - 1));  //minus one sine we started the initial location with 1 instead of 0;
        }

        void printMap(bool end = false)
        {
            for (int y = 0; y < size; y++)
            {
                string str = "";
                for (int x = 0; x < size; x++)
                {

                    if (!end && x == xpos && y == ypos)
                    {
                        str += 'D';
                    }
                    else if (x == 50 && y == 50)
                    {
                        str += "o";
                    }
                    else
                    {
                        switch (area[x, y])
                        {
                            default: str += " "; break;
                            case 1: str += "."; break;
                            case 2: str += "@"; break;
                            case 3: str += "#"; break;
                        }
                    }
                }
                Console.WriteLine(str);
            }
        }


        long[] getData()
        {
            string[] lines = Program.readFile(15);
            //string[] lines = { "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0" };
            //string[] lines = { "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0" };
            //string[] lines = {"3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0"};
            //string[] lines = { "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5" };
            string[] data = lines[0].Split(',');
            long[] val = new long[data.Length];
            for (int i = 0; i < val.Length; i++)
            {
                val[i] = long.Parse(data[i]);
            }
            return val;
        }
    }

    class Intcode15
    {
        //long[] mem;
        Dictionary<long, long> mem = new Dictionary<long, long>();
        int pos = 0;
        long result = 0;
        bool ok = true;
        int relBase = 0;

        Queue<long> inp = new Queue<long>();
        long outp = 0;

        long lastOp = 0;

        public Intcode15(long[] program)
        {

            for (int i = 0; i < program.Length; i++)
            {
                mem[i] = program[i];
            }
        }


        public void setInput(int input)
        {
            inp.Enqueue(input);
        }

        public long getOutput()
        {
            return outp;
        }

        public void runToHalt()
        {
            while (ok)
            {
                Step();
            }
        }

        public void runToOutOrHalt()
        {
            Step();
            while (lastOp != 4 && ok)
            {
                Step();
            }

        }

        public Boolean isHalted()
        {
            return !ok;
        }

        void Step()
        {
            long op = getVal(pos, 1);
            int param1, param2, param3;
            param1 = (int)(op / 100) % 10;
            param2 = (int)(op / 1000) % 10;
            param3 = (int)(op / 10000) % 10;


            lastOp = op % 10;
            switch (lastOp)
            {
                case 1:
                    result = getVal(pos + 1, param1) + getVal(pos + 2, param2);
                    setVal(pos + 3, param3, result);
                    pos += 4;
                    break;
                case 2:
                    result = getVal(pos + 1, param1) * getVal(pos + 2, param2);
                    setVal(pos + 3, param3, result);
                    pos += 4;
                    break;
                case 3:

                   

                    setVal(pos + 1, param1, inp.Dequeue());

                    pos += 2;
                    break;
                case 4:
                    //Console.WriteLine(getVal(mem, pos + 1, param1));
                    outp = getVal(pos + 1, param1);
                    pos += 2;
                    //Console.WriteLine(outp);



                    break;
                case 5:
                    if (getVal(pos + 1, param1) != 0)
                    {
                        pos = (int)getVal(pos + 2, param2);
                    }
                    else
                    {
                        pos += 3;
                    }
                    break;
                case 6:
                    if (getVal(pos + 1, param1) == 0)
                    {
                        pos = (int)getVal(pos + 2, param2);
                    }
                    else
                    {
                        pos += 3;
                    }
                    break;
                case 7:
                    long val1 = getVal(pos + 1, param1);
                    long val2 = getVal(pos + 2, param2);
                    if (val1 < val2)
                    {
                        setVal(pos + 3, param3, 1);
                    }
                    else
                    {
                        setVal(pos + 3, param3, 0);
                    }
                    pos += 4;
                    break;
                case 8:
                    val1 = getVal(pos + 1, param1);
                    val2 = getVal(pos + 2, param2);
                    if (val1 == val2)
                    {
                        setVal(pos + 3, param3, 1);
                    }
                    else
                    {
                        setVal(pos + 3, param3, 0);
                    }
                    pos += 4;
                    break;
                case 9:
                    relBase += (int)getVal(pos + 1, param1);
                    pos += 2;
                    break;
                default:
                    //System.Console.WriteLine("ERROR!");
                    ok = false;
                    break;
            }



        }

        long getVal(int pos, int mode)
        {
            if (mode == 0)
            {
                long realPos = getVal(pos, 1);
                if (mem.ContainsKey(realPos))
                {
                    return mem[realPos];
                }
                return 0;
            }
            else if (mode == 1)
            {
                if (mem.ContainsKey(pos))
                {
                    return mem[pos];
                }
                return 0;
            }
            else if (mode == 2)
            {
                long realPos = getVal(pos, 1);
                if (mem.ContainsKey(relBase + realPos))
                {
                    return mem[relBase + realPos];
                }
                return 0;
            }
            return 0;
        }

        void setVal(int pos, int mode, long val)
        {
            if (mode == 0)
            {
                mem[getVal(pos, 1)] = val;
            }
            else if (mode == 1)
            {
                System.Console.WriteLine("ERROR!");
            }
            else if (mode == 2)
            {
                mem[relBase + mem[pos]] = val;
            }
        }


    }
}
