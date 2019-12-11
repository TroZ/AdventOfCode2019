using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day9
    {
        public void main()
        {

            long[] data = getData();


            Intercal9 comp = new Intercal9(data);
            comp.setInput(1);
            comp.runToHalt();

            Console.WriteLine("Part1 = " + comp.getOutput());


            comp = new Intercal9(data);
            comp.setInput(2);
            comp.runToHalt();

            Console.WriteLine("Part1 = " + comp.getOutput());
        }


        long[] getData()
        {
            string[] lines = Program.readFile(9);
            //string[] lines = { "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99" };
            //string[] lines = { "1102,34915192,34915192,7,4,7,99,0" };
            //string[] lines = { "104,1125899906842624,99" };
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




    class Intercal9
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

        public Intercal9(long[] program)
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
            long op = getVal(pos,1);
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
                    if (inp.Count == 0)
                    {
                        return;
                    }
                    setVal(pos + 1, param1, inp.Dequeue());
                    pos += 2;
                    break;
                case 4:
                    //Console.WriteLine(getVal(mem, pos + 1, param1));
                    outp = getVal(pos + 1, param1);
                    pos += 2;
                    Console.WriteLine(outp);
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
                if (mem.ContainsKey(realPos)) {
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
                if (mem.ContainsKey(relBase+realPos))
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
                mem[getVal(pos,1)] = val; 
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
