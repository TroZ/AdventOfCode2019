using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day5
    {
        int size = 10000;

        public void main()
        {



            int[] data = getData();

            runPrg(data, 4, 3);


            
        }


        int runPrg(int[] mem, int noun, int verb)
        {
            //mem[1] = noun;
            //mem[2] = verb;

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
    
                switch (op%10)
                {
                    case 1:
                        result = getVal(mem,pos+1,param1) + getVal(mem,pos+2,param2);
                        setVal(mem, pos + 3, param3, result);
                        pos += 4;
                        break;
                    case 2:
                        result = getVal(mem, pos + 1, param1) * getVal(mem, pos + 2, param2);
                        setVal(mem, pos + 3, param3, result);
                        pos += 4;
                        break;
                    case 3:
                        string str = Console.ReadLine();
                        setVal(mem,pos + 1,param1, Int32.Parse(str.Trim()));
                        pos += 2;
                        break;
                    case 4:
                        Console.WriteLine(getVal(mem,pos+1,param1));
                        pos += 2;
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
                        if(val1 < val2)
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
                        System.Console.WriteLine("ERROR!");
                        ok = false;
                        break;
                }
               


            }

            return mem[0];
        }

        int getVal(int[] mem, int pos, int mode)
        {
            if(mode == 0)
            {
                return mem[mem[pos]];
            }
            else if(mode == 1)
            {
                return mem[pos];
            }
            return 0;
        }

        void setVal(int[] mem,int pos, int mode, int val)
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
            string[] lines = Program.readFile(5);
            //string[] lines = { "1002,4,3,4,33" };
            //string[] lines = { "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99" };
            string[] data = lines[0].Split(',');
            int[] val = new int[data.Length];
            for (int i = 0; i < val.Length; i++)
            {
                val[i] = int.Parse(data[i]);
            }
            return val;
        }
    }
}
