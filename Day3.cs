using System;
using System.Collections.Generic;
using System.Text;


namespace AdventOfCode
{
    class Day3
    {

        int size = 10000;

        public void main()
        {



            string[][] data = getData();


            int[,] grid = new int[size*2,size*2];

            clear(grid);

            plotLine(grid,data[0], 1);
            plotLine(grid,data[1], 2);
                       
            findPos(grid, 3);


            clear(grid);

            plotLineLen(grid, data[0], 0);
            plotLineLen(grid, data[1], 1);


            //a cleaned up part 2 implementation
            //clear(grid);
            //plotLineLenBetter(grid, data[0], 0);
            //plotLineLenBetter(grid, data[1], 1);
        }

        void clear(int[,] grid)
        {
            for (int x = 0; x < size * 2; x++)
            {
                for (int y = 0; y < size * 2; y++)
                {
                    grid[x, y] = 0;
                }
            }
        }

        void findPos(int[,] grid,int val)
        {
            int bestx = 0;
            int besty = 0;
            int bestval = size * 2;

            for (int x = 0; x < size * 2; x++)
            {
                for (int y = 0; y < size * 2; y++)
                {
                    if (grid[x, y] == 3)
                    {
                        int dist = Math.Abs(size - x) + Math.Abs(size - y);
                        if (dist < bestval && x != size && y != size)
                        {
                            bestval = dist;
                            bestx = x;
                            besty = y;
                        }


                    }
                }
            }
             
            System.Console.WriteLine("distance = "+bestval);

        }

        void plotLine(int[,] grid, string[] cmds,int val)
        {
            int x = size;
            int y = size;

            foreach(string cmd in cmds)
            {
                char dir = cmd.ToLower().ToCharArray()[0];
                int len = Int32.Parse(cmd.Substring(1));

                switch (dir)
                {
                    case 'u':
                        for(int cnt = 0; cnt < len; cnt++)
                        {
                            grid[x,y+cnt] += val;
                        }
                        y = y + len;
                        break;
                    case 'd':
                        for (int cnt = 0; cnt < len; cnt++)
                        {
                            grid[x, y - cnt] += val;
                        }
                        y = y - len;
                        break;
                    case 'r':
                        for (int cnt = 0; cnt < len; cnt++)
                        {
                            grid[x + cnt, y] += val;
                        }
                        x = x + len;
                        break;
                    case 'l':
                        for (int cnt = 0; cnt < len; cnt++)
                        {
                            grid[x - cnt, y] += val;
                        }
                        x = x - len;
                        break;

                }
            }
        }

        string[][] getData()
        {
            //string[] lines = Program.readFile(3);
            string[] lines = { "R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83" };
            string[][] data = new string[2][];
            data[0] = lines[0].Split(',');
            data[1] = lines[1].Split(',');
            return data;
        }



        void plotLineLen(int[,] grid, string[] cmds, int val)
        {
            int x = size;
            int y = size;
            int count = 0;
            int minval = Int32.MaxValue;

            foreach (string cmd in cmds)
            {
                char dir = cmd.ToLower().ToCharArray()[0];
                int len = Int32.Parse(cmd.Substring(1));

                switch (dir)
                {
                    case 'u':
                        for (int cnt = 0; cnt < len; cnt++)
                        {
                            if (val == 1)
                            {
                                if(grid[x, y + cnt] != 0)
                                {
                                    if(grid[x, y + cnt] + count < minval){
                                        minval = grid[x, y + cnt] + count;
                                    }
                                }
                            }
                            else
                            {
                                grid[x, y + cnt] += count;
                            }
                            count++;
                        }
                        y = y + len;
                        break;
                    case 'd':
                        for (int cnt = 0; cnt < len; cnt++)
                        {
                            if (val == 1)
                            {
                                if (grid[x, y - cnt] != 0)
                                {
                                    if (grid[x, y - cnt] + count < minval)
                                    {
                                        minval = grid[x, y - cnt] + count;
                                    }
                                }
                            }
                            else
                            {
                                grid[x, y - cnt] += count;
                            }
                            count++;

                        }
                        y = y - len;
                        break;
                    case 'r':
                        for (int cnt = 0; cnt < len; cnt++)
                        {
                            if (val == 1)
                            {
                                if (grid[x + cnt, y] != 0)
                                {
                                    if (grid[x + cnt, y] + count < minval)
                                    {
                                        minval = grid[x + cnt, y] + count;
                                    }
                                }
                            }
                            else
                            {
                                grid[x + cnt, y] += count;
                            }
                            count++;

                        }
                        x = x + len;
                        break;
                    case 'l':
                        for (int cnt = 0; cnt < len; cnt++)
                        {
                            if (val == 1)
                            {
                                if (grid[x - cnt, y] != 0)
                                {
                                    if (grid[x - cnt, y] + count < minval)
                                    {
                                        minval = grid[x - cnt, y] + count;
                                    }
                                }
                            }
                            else
                            {
                                grid[x - cnt, y] += count;
                            }
                            count++;

                        }
                        x = x - len;
                        break;

                }
            }

            if (val == 1)
            {
                System.Console.WriteLine("closest inter = " + minval);
            }
        }



        //I refactored my part 2 after getting the correct solution. My initial version was too complex, causing me time in verifying if it was correct.
        //This version is much easier to see is correct.  My part 1 probably should have been similiar; it would have made part 2 easier.
        void plotLineLenBetter(int[,] grid, string[] cmds, int val)
        {
            int x = size;
            int y = size;
            int count = 0;
            int minval = Int32.MaxValue;

            foreach (string cmd in cmds)
            {
                char dir = cmd.ToLower().ToCharArray()[0];
                int len = Int32.Parse(cmd.Substring(1));

                for (int cnt = 0; cnt < len; cnt++)
                {

                    if (val == 1)
                    {
                        if (grid[x, y] != 0)
                        {
                            if (grid[x, y] + count < minval)
                            {
                                minval = grid[x, y] + count;
                            }
                        }
                    }
                    else
                    {
                        grid[x, y] += count;
                    }
                    count++;

                    switch (dir)
                    {
                        case 'u':
                            y = y - 1;
                            break;
                        case 'd':
                            y = y + 1;
                            break;
                        case 'r':
                            x = x + 1;
                            break;
                        case 'l':
                            x = x - 1;
                            break;
                    }
                }
            }

            if (val == 1)
            {
                System.Console.WriteLine("closest inter = " + minval);
            }
        }
    }



    
}
