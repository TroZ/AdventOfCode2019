using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AdventOfCode
{
    class Day11
    {

        public void main()
        {

            long[] data = getData();


            Intercal11 comp = new Intercal11(data);

            comp.runToHalt();


            Console.WriteLine("Painted  " + comp.getPaintCount().Count + "  unique panels");
            render(comp.getPanels(), 500);


            data = getData();
            comp = new Intercal11(data);
            comp.setPanel(250, 250, 1);
            comp.runToHalt();

            render(comp.getPanels(), 500);

        }


        long[] getData()
        {
            string[] lines = Program.readFile(11);
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

        void render(int[,] grid, int size)
        {
            Bitmap pic = new Bitmap(size, size, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Color c;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    
                    if(grid[x,y] != 0)
                    {
                        c = Color.White;
                    }
                    else
                    {
                        c = Color.Black;
                    }
                    pic.SetPixel(x, y, c);

                }
            }

            Program.saveImg(pic, 11);
        }
    }

    class Intercal11
    {
        //long[] mem;
        Dictionary<long, long> mem = new Dictionary<long, long>();
        int pos = 0;
        long result = 0;
        bool ok = true;
        int relBase = 0;

        //Queue<long> inp = new Queue<long>();
        long outp = 0;

        long lastOp = 0;


        int posx = 250;
        int posy = 250;
        int direction = 0; //0 = up, 1= right, 2 = down, 3 = left;
        bool paint = true;

        int[,] panels = new int[500,500];

        Dictionary<Tuple<int, int>, int> paintCount = new Dictionary<Tuple<int, int>, int>();


        public Intercal11(long[] program)
        {
            /*
            int size = program.Length * 10;
            if(size < 10000)
            {
                size = 10000;
            }
            mem = new long[size];
            for(int i = 0; i < mem.Length; i++)
            {
                mem[i] = 0;
            }
            for(int i = 0; i < program.Length; i++)
            {
                mem[i] = program[i];
            }
            */
            for (int i = 0; i < program.Length; i++)
            {
                mem[i] = program[i];
            }
        }

        public Dictionary<Tuple<int, int>, int> getPaintCount()
        {
            return paintCount;
        }

        public int[,] getPanels()
        {
            return panels;
        }

        public void setInput(int input)
        {
            //inp.Enqueue(input);
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
                    //string str = Console.ReadLine();
                    //setVal(pos + 1, param1, Int32.Parse(str.Trim()));
                    //if (inp.Count == 0)
                    //{
                    //    return;
                    //}
                    int val = panels[posx, posy];
                    setVal(pos + 1, param1, val);
                    pos += 2;
                    break;
                case 4:
                    //Console.WriteLine(getVal(mem, pos + 1, param1));
                    outp = getVal(pos + 1, param1);
                    pos += 2;
                    //Console.WriteLine(outp);

                    if (paint)
                    {
                        //paint
                        panels[posx, posy] = (int)outp;
                        Tuple<int, int> here = new Tuple<int, int>(posx, posy);
                        if (paintCount.ContainsKey(here))
                        {
                            paintCount[here] = paintCount[here]++;
                        }
                        else
                        {
                            paintCount[here] = 1;
                        }
                    }
                    else
                    {
                        //move
                        if (outp == 0)
                        {
                            direction--;
                        }
                        else
                        {
                            direction++;
                        }
                        if (direction < 0) direction += 4;
                        if (direction > 3) direction -= 4;

                        switch (direction)
                        {
                            case 0:
                                posy--;
                                break;
                            case 1:
                                posx++;
                                break;
                            case 2:
                                posy++;
                                break;
                            case 3:
                                posx--;
                                break;
                        }
                    }

                    paint = !paint;//alternate paint and move

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

        internal void setPanel(int x, int y, int value)
        {
            panels[x,y] = value;
        }
    }
}
