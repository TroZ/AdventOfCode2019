using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day2
    {
        public void main()
        {
            


            int[] val = getData();

            

            /*
            val[0] = 2;
            val[1] = 4;
            val[2] = 4;
            val[3] = 5;
            val[4] = 99;
            val[5] = 0;
            */

            

            System.Console.WriteLine("position 0 has " + runPrg(val,12,2));

            for(int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    val = getData();
                    int ret = runPrg(val, noun, verb);
                    System.Console.WriteLine("noun = " + noun + " verb = " + verb + "  value = " + ret);
                    if (ret == 19690720)
                    {
                        System.Console.WriteLine("noun = "+noun+ " verb = "+ verb+"  answer = "+((100*noun)+verb));
                        noun = verb = 100;
                        break;
                    }
                }
            }
        }

        int runPrg(int[] mem, int noun,int verb)
        {
            mem[1] = noun;
            mem[2] = verb;

            int pos = 0;
            int result = 0;
            bool ok = true;
            while (mem[pos] != 99 && ok)
            {
                int op = mem[pos];
                switch (op)
                {
                    case 1:
                        result = mem[mem[pos + 1]] + mem[mem[pos + 2]];
                        mem[mem[pos + 3]] = result;
                        pos += 4;
                        break;
                    case 2:
                        result = mem[mem[pos + 1]] * mem[mem[pos + 2]];
                        mem[mem[pos + 3]] = result;
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

        int[] getData()
        {
            string[] lines = Program.readFile(2);
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
