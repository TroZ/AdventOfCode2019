using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day7
    {

        public void main()
        {

            int[] data = getData();
            int[] prg = new int[data.Length];

            int max = 0;
            string setting = "";

            for (int a = 0; a < 5; a++)
            {
                for (int b = 0; b < 5; b++)
                {
                    if (b == a)
                        continue;
                    for (int c = 0; c < 5; c++)
                    {
                        if (c == a || c == b)
                            continue;
                        for (int d = 0; d < 5; d++)
                        {
                            if (d == a || d == b || d == c)
                                continue;
                            for (int e = 0; e < 5; e++)
                            {

                                if (e == a || e == b || e == c || e == d)
                                    continue;

                                int[] inp = { a, 0 };
                                int[] outp = new int[20];
                                outp[0] = 0;

                                Array.Copy(data, prg, data.Length);
                                runPrg(prg, inp, outp);//a

                                inp[0] = b;
                                inp[1] = outp[0];
                                outp[0] = 0;
                                Array.Copy(data, prg, data.Length);
                                runPrg(prg, inp, outp);//b

                                inp[0] = c;
                                inp[1] = outp[0];
                                outp[0] = 0;
                                Array.Copy(data, prg, data.Length);
                                runPrg(prg, inp, outp);//c

                                inp[0] = d;
                                inp[1] = outp[0];
                                outp[0] = 0;
                                Array.Copy(data, prg, data.Length);
                                runPrg(prg, inp, outp);//d

                                inp[0] = e;
                                inp[1] = outp[0];
                                outp[0] = 0;
                                Array.Copy(data, prg, data.Length);
                                runPrg(prg, inp, outp);//e


                                if (outp[0] > max)
                                {
                                    max = outp[0];
                                    setting = "" + a + "," + b + "," + c + "," + d + "," + e;
                                }
                            }

                        }

                    }

                }

            }

            Console.WriteLine("max = " + max + "     for " + setting);



            Intercal[] comp = new Intercal[5];
            int[][] prgs = new int[5][];          

            max = 0;

            for (int a = 5; a < 10; a++)
            {
                for (int b = 5; b < 10; b++)
                {
                    if (b == a)
                        continue;
                    for (int c = 5; c < 10; c++)
                    {
                        if (c == a || c == b)
                            continue;
                        for (int d = 5; d < 10; d++)
                        {
                            if (d == a || d == b || d == c)
                                continue;
                            for (int e = 5; e < 10; e++)
                            {

                                if (e == a || e == b || e == c || e == d)
                                    continue;

                                for (int i = 0; i < 5; i++)
                                {
                                    prgs[i] = new int[data.Length];
                                    Array.Copy(data, prgs[i], data.Length);
                                    comp[i] = new Intercal(prgs[i]);
                                }

                                comp[0].setInput(a);
                                comp[1].setInput(b);
                                comp[2].setInput(c);
                                comp[3].setInput(d);
                                comp[4].setInput(e);
                                comp[0].setInput(0);

                                while(!(comp[0].isHalted() || comp[1].isHalted() || comp[2].isHalted() || comp[3].isHalted() || comp[4].isHalted()))
                                {
                                    comp[0].runToOutOrHalt();
                                    comp[1].setInput(comp[0].getOutput());
                                    comp[1].runToOutOrHalt();
                                    comp[2].setInput(comp[1].getOutput());
                                    comp[2].runToOutOrHalt();
                                    comp[3].setInput(comp[2].getOutput());
                                    comp[3].runToOutOrHalt();
                                    comp[4].setInput(comp[3].getOutput());
                                    comp[4].runToOutOrHalt();
                                    comp[0].setInput(comp[4].getOutput());
                                }

                                if(comp[4].getOutput() > max)
                                {
                                    max = comp[4].getOutput();
                                    setting = "" + a + "," + b + "," + c + "," + d + "," + e;
                                }

                            }
                        }
                    }
                }
            }


            Console.WriteLine("max = " + max + "     for " + setting);
        }



        int runPrg(int[] mem, int[] input, int[] output)
        {
            //mem[1] = noun;
            //mem[2] = verb;
            int inPos = 0;
            int outPos = 0;
            int pos = 0;
            int result = 0;
            bool ok = true;
            while (mem[pos] != 99 && ok)
            {
                int op = mem[pos];
                int param1, param2, param3;
                param1 = (op / 100) % 10;
                param2 = (op / 1000) % 10;
                param3 = (op / 10000) % 10;

                switch (op % 10)
                {
                    case 1:
                        result = getVal(mem, pos + 1, param1) + getVal(mem, pos + 2, param2);
                        setVal(mem, pos + 3, param3, result);
                        pos += 4;
                        break;
                    case 2:
                        result = getVal(mem, pos + 1, param1) * getVal(mem, pos + 2, param2);
                        setVal(mem, pos + 3, param3, result);
                        pos += 4;
                        break;
                    case 3:
                        //string str = Console.ReadLine();
                        //setVal(mem, pos + 1, param1, Int32.Parse(str.Trim()));
                        setVal(mem, pos + 1, param1, input[inPos]);
                        pos += 2;
                        inPos++;
                        break;
                    case 4:
                        //Console.WriteLine(getVal(mem, pos + 1, param1));
                        output[outPos] = getVal(mem, pos + 1, param1);
                        pos += 2;
                        outPos++;
                        ok = false;
                        break;
                    case 5:
                        if (getVal(mem, pos + 1, param1) != 0)
                        {
                            pos = getVal(mem, pos + 2, param2);
                        }
                        else
                        {
                            pos += 3;
                        }
                        break;
                    case 6:
                        if (getVal(mem, pos + 1, param1) == 0)
                        {
                            pos = getVal(mem, pos + 2, param2);
                        }
                        else
                        {
                            pos += 3;
                        }
                        break;
                    case 7:
                        int val1 = getVal(mem, pos + 1, param1);
                        int val2 = getVal(mem, pos + 2, param2);
                        if (val1 < val2)
                        {
                            setVal(mem, pos + 3, param3, 1);
                        }
                        else
                        {
                            setVal(mem, pos + 3, param3, 0);
                        }
                        pos += 4;
                        break;
                    case 8:
                        val1 = getVal(mem, pos + 1, param1);
                        val2 = getVal(mem, pos + 2, param2);
                        if (val1 == val2)
                        {
                            setVal(mem, pos + 3, param3, 1);
                        }
                        else
                        {
                            setVal(mem, pos + 3, param3, 0);
                        }
                        pos += 4;
                        break;
                    default:
                        //System.Console.WriteLine("ERROR!");
                        ok = false;
                        break;
                }



            }

            return mem[0];
        }

        int getVal(int[] mem, int pos, int mode)
        {
            if (mode == 0)
            {
                return mem[mem[pos]];
            }
            else if (mode == 1)
            {
                return mem[pos];
            }
            return 0;
        }

        void setVal(int[] mem, int pos, int mode, int val)
        {
            if (mode == 0)
            {
                mem[mem[pos]] = val; ;
            }
            else if (mode == 1)
            {
                System.Console.WriteLine("ERROR!");
            }
        }

        int[] getData()
        {
            string[] lines = Program.readFile(7);
            //string[] lines = { "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0" };
            //string[] lines = { "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0" };
            //string[] lines = {"3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0"};
            //string[] lines = { "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5" };
            string[] data = lines[0].Split(',');
            int[] val = new int[data.Length];
            for (int i = 0; i < val.Length; i++)
            {
                val[i] = int.Parse(data[i]);
            }
            return val;
        }



    }

    class Intercal
    {
        int[] mem;
        int pos = 0;
        int result = 0;
        bool ok = true;

        Queue<int> inp = new Queue<int>();
        int outp = 0;

        int lastOp = 0;

        public Intercal(int[] program)
        {
            mem = program;
        }

        public void setInput(int input)
        {
            inp.Enqueue(input);
        }

        public int getOutput()
        {
            return outp;
        }

        public void runToHalt()
        {
            while(ok)
            {
                Step();
            }
        }

        public void runToOutOrHalt()
        {
            Step();
            while (lastOp != 4 &&  ok)
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
            int op = mem[pos];
            int param1, param2, param3;
            param1 = (op / 100) % 10;
            param2 = (op / 1000) % 10;
            param3 = (op / 10000) % 10;


            lastOp = op % 10;
            switch (lastOp)
            {
                case 1:
                    result = getVal( pos + 1, param1) + getVal( pos + 2, param2);
                    setVal( pos + 3, param3, result);
                    pos += 4;
                    break;
                case 2:
                    result = getVal( pos + 1, param1) * getVal( pos + 2, param2);
                    setVal( pos + 3, param3, result);
                    pos += 4;
                    break;
                case 3:
                    //string str = Console.ReadLine();
                    //setVal(mem, pos + 1, param1, Int32.Parse(str.Trim()));
                    if (inp.Count == 0)
                    {
                        return;
                    }
                    setVal( pos + 1, param1, inp.Dequeue());
                    pos += 2;
                    break;
                case 4:
                    //Console.WriteLine(getVal(mem, pos + 1, param1));
                    outp = getVal( pos + 1, param1);
                    pos += 2;
                    break;
                case 5:
                    if (getVal( pos + 1, param1) != 0)
                    {
                        pos = getVal( pos + 2, param2);
                    }
                    else
                    {
                        pos += 3;
                    }
                    break;
                case 6:
                    if (getVal( pos + 1, param1) == 0)
                    {
                        pos = getVal( pos + 2, param2);
                    }
                    else
                    {
                        pos += 3;
                    }
                    break;
                case 7:
                    int val1 = getVal( pos + 1, param1);
                    int val2 = getVal( pos + 2, param2);
                    if (val1 < val2)
                    {
                        setVal( pos + 3, param3, 1);
                    }
                    else
                    {
                        setVal( pos + 3, param3, 0);
                    }
                    pos += 4;
                    break;
                case 8:
                    val1 = getVal( pos + 1, param1);
                    val2 = getVal( pos + 2, param2);
                    if (val1 == val2)
                    {
                        setVal( pos + 3, param3, 1);
                    }
                    else
                    {
                        setVal( pos + 3, param3, 0);
                    }
                    pos += 4;
                    break;
                default:
                    //System.Console.WriteLine("ERROR!");
                    ok = false;
                    break;
            }



        }

        int getVal(int pos, int mode)
        {
            if (mode == 0)
            {
                return mem[mem[pos]];
            }
            else if (mode == 1)
            {
                return mem[pos];
            }
            return 0;
        }

        void setVal(int pos, int mode, int val)
        {
            if (mode == 0)
            {
                mem[mem[pos]] = val; ;
            }
            else if (mode == 1)
            {
                System.Console.WriteLine("ERROR!");
            }
        }
    }
}
