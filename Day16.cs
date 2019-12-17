using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day16
    {
        int phases = 100;//4;
        int[] pattern = { 0, 1, 0, -1 };

        public void main()
        {

            int[] data = getData();


            int[] inData, outData;
            outData = data;

            int phase = 0;
            while (phase < phases)
            {
                phase++;
                inData = outData;
                outData = new int[inData.Length];

                for(int pos = 0; pos < inData.Length; pos++)
                {
                    int val = 0;
                    for(int i = pos; i < inData.Length; i++)
                    {

                        int j = i + 1;
                        j = (j / (pos+1));
                        j = j % pattern.Length;

                        int v = inData[i] * pattern[j];
                        val += v;

                    }

                    outData[pos] = Math.Abs(val) % 10;
                }

            }


            Console.Write("First 8 are:  ");
            for(int i = 0; i < 8; i++)
            {
                Console.Write(outData[i]);
            }
            Console.WriteLine();



            //part2

            data = getData2();
            outData = data;
            int dataLen = data.Length / 10000;

            int msgOffset = int.Parse("" + data[0] + data[1] + data[2] + data[3] + data[4] + data[5] + data[6]);
            Console.WriteLine("msgOffset " + msgOffset);

            int startpos = data.Length - (2 * (data.Length - msgOffset));
            Console.WriteLine("Startpos " + startpos);

            phase = 0;
            while (phase < phases)
            {
                Console.WriteLine("Phase " + phase);
                phase++;
                inData = outData;
                outData = new int[inData.Length];


                /*
                Parallel.For(startpos, inData.Length, pos =>
                //for (int pos = startpos ; pos < inData.Length; pos++)
                {
                    int val = 0;
                    for (int i = pos; i < inData.Length; i++)
                    {

                        int j = i + 1;
                        j = (j / (pos + 1));
                        j = j % pattern.Length;

                        int v = inData[i] * pattern[j];
                        val += v;

                    }

                    outData[pos] = Math.Abs(val) % 10;
                }
                );
                */

                int val = 0;
                for(int pos = inData.Length - 1; pos > startpos; pos--)
                {
                    val += inData[pos];
                    val = val % 10;
                    outData[pos] = val;
                }
            }


            Console.Write("First 8 are:  ");
            for (int i = 0; i < 8; i++)
            {
                Console.Write(outData[i + msgOffset]);

            }
            Console.WriteLine();
        }

        int[] getData()
        {
            string[] lines = Program.readFile(16);
            //string[] lines = { "12345678" };
            //string[] lines = { "80871224585914546619083218645595" };
            //string[] lines = {"3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0"};
            //string[] lines = { "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5" };

            char[] data = lines[0].ToCharArray();
            int[] val = new int[data.Length];
            for (int i = 0; i < val.Length; i++)
            {
                val[i] = int.Parse("" + data[i]);
            }
            return val;
        }


        int[] getData2()
        {
            string[] lines = Program.readFile(16);
            //string[] lines = { "03036732577212944063491565474664" };
            //string[] lines = { "80871224585914546619083218645595" };
            //string[] lines = {"3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0"};
            //string[] lines = { "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5" };

            //part2
            {
                string realdata = "";
                for (int i = 0; i < 10000; i++)
                {
                    realdata += lines[0];
                }
                lines[0] = realdata;
            }

            Console.WriteLine("Lenght: " + lines[0].Length);

            char[] data = lines[0].ToCharArray();
            int[] val = new int[data.Length];
            for (int i = 0; i < val.Length; i++)
            {
                val[i] = int.Parse("" + data[i]);
            }
            return val;
        }
    }
}
