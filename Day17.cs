using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day17
    {

        public void main()
        {

            long[] data = getData();


            Intcode17 comp = new Intcode17(data);

            string outp = "";
            while (!comp.isHalted() && !comp.isNeedInput())
            {
                comp.runToOutOrHalt();
                if (!comp.isHalted() && !comp.isNeedInput())
                {
                    outp += (char)comp.getOutput();
                }
            }



            Console.WriteLine(outp);


            string[] lines = outp.Split("\n");
            int xmax = lines[0].Length - 1;
            int ymax = lines.Length - 2;
            int sum = 0;
            for (int y = 1; y < ymax; y++)
            {
                char[] chars = lines[y].ToCharArray();
                for (int x = 1; x < xmax; x++)
                {
                    if(chars[x] == '#')
                    {
                        if(chars[x-1] == '#' && chars[x + 1] == '#')
                        {
                            if(lines[y-1].ToCharArray()[x] == '#' && lines[y + 1].ToCharArray()[x] == '#')
                            {
                                sum += x * y;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Sum of alignment is  " + sum);



            data = getData();
            data[0] = 2;
            comp = new Intcode17(data);

            string prg = "L,12,R,4,R,4,L,6, L,12,R,4,R,4,R,12, L,12,R,4,R,4,L,6, L,10,L,6,R,4, L,12,R,4,R,4,L,6, L,12,R,4,R,4R,12, L,10,L,6,R,4, L,12,R,4,R,4,R,12, L,10,L,6,R,4, L,12,R,4,R,4,L,6";
            string prg2 = @"A,B,A,C,A,B,C,B,C,A
L,12,R,4,R,4,L,6
L,12,R,4,R,4,R,12
L,10,L,6,R,4
n
";

            for(int i = 0; i < prg2.Length; i++)
            {
                char c = prg2.ToCharArray()[i];
                if(c>32 || c == 10)
                {
                    comp.setInput(c);
                }
            }

            comp.runToHalt();

            Console.WriteLine("Dust =  " + comp.getOutput());


        }


        long[] getData()
        {
            string[] lines = Program.readFile(17);
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

    class Intcode17
    {
        //long[] mem;
        Dictionary<long, long> mem = new Dictionary<long, long>();
        int pos = 0;
        long result = 0;
        bool ok = true;
        int relBase = 0;
        bool needInp;

        Queue<long> inp = new Queue<long>();
        long outp = 0;

        long lastOp = 0;

        public Intcode17(long[] program)
        {

            for (int i = 0; i < program.Length; i++)
            {
                mem[i] = program[i];
            }
        }


        public void setInput(int input)
        {
            inp.Enqueue(input);
            needInp = false;
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
            while (lastOp != 4 && ok && !needInp)
            {
                Step();
            }

        }

        public Boolean isHalted()
        {
            return !ok;
        }

        public Boolean isNeedInput()
        {
            return needInp;
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

                    if(inp.Count == 0)
                    {
                        needInp = true;
                        return;
                    }

                    setVal(pos + 1, param1, inp.Dequeue());

                    pos += 2;
                    break;
                case 4:

                    outp = getVal(pos + 1, param1);
                    pos += 2;
                    if (outp < 128)
                    {
                        Console.Write((char)outp);
                    }
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
