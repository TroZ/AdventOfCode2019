using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day13
    {
        public static int size = 50;

        public void main()
        {

            long[] data = getData();


            Intcode13 comp = new Intcode13(data);
            comp.runToHalt();

            int[,] screen = comp.getPanels();

            int count = 0;
            for(int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    if(screen[x,y] == 2)
                    {
                        count++;
                    }
                }
            }

            Console.WriteLine("Num Blocks = " + count);



            data = getData();

            data[0] = 2;

            comp = new Intcode13(data);
            comp.runToHalt();

            comp.drawScreen();
        }


        long[] getData()
        {
            string[] lines = Program.readFile(13);
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

    class Intcode13
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


        
        short outmode = 0;
        int posx = 0;
        int posy = 0;
        int[,] panels = new int[Day13.size, Day13.size];

        long score = 0;

        int ballx = 0;
        int paddlex = 0;

        bool autoplay = true;

        public Intcode13(long[] program)
        {
           
            for (int i = 0; i < program.Length; i++)
            {
                mem[i] = program[i];
            }
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

                    drawScreen();

                    string str = "0";
                    if (autoplay)
                    {
                        if (ballx < paddlex)
                        {
                            str = "-1";
                        }
                        if (ballx > paddlex)
                        {
                            str = "1";
                        }
                    }
                    else
                    {
                        str = Console.ReadLine();
                    }

                    
                    setVal(pos + 1, param1, Int32.Parse(str.Trim()));
                    
                    pos += 2;
                    break;
                case 4:
                    //Console.WriteLine(getVal(mem, pos + 1, param1));
                    outp = getVal(pos + 1, param1);
                    pos += 2;
                    //Console.WriteLine(outp);


                    switch (outmode)
                    {
                        case 0:
                            posx = (int)outp;
                            break;
                        case 1:
                            posy = (int)outp;
                            break;
                        case 2:
                            if(posx==-1 && posy == 0)
                            {
                                Console.WriteLine(" Score increased by " + (outp - score));
                                score = outp;
                            }
                            else
                            {
                                panels[posx, posy] = (int)outp;

                                if(outp ==4)
                                {
                                    ballx = posx;
                                }
                                if(outp == 3)
                                {
                                    paddlex = posx;
                                }
                            }
                            
                            break;
                    }

                    outmode++;
                    outmode = (short)(outmode % 3);

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

        void setPanel(int x, int y, int value)
        {
            panels[x, y] = value;
        }

        public void drawScreen()
        {
            for (int y = 0; y < 20; y++)
            {
                string str = "";
                for (int x = 0; x < 50; x++)
                {
                    
                    switch(panels[x, y])
                    {
                        case 0:
                            str += " "; break;
                        case 1:
                            str += "#"; break;
                        case 2:
                            str += "B"; break;
                        case 3:
                            str += "-"; break;
                        case 4:
                            str += "*"; break;
                    }
                }
                Console.WriteLine(str);
            }
            Console.WriteLine(" SCORE: " + score);
        }
    }
}
