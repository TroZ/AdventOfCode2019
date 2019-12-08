using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day8
    {

        int w = 25;
        int h = 6;

        public void main()
        {

            int[] data = getData();

            int layerlen = w * h;

            int layers = data.Length / layerlen;

            int mincnt = layerlen;
            int ret = 0;
            for (int i = 0; i < layers; i++)
            {
                int zcount = 0;
                int ocount = 0;
                int tcount = 0;

                for (int j = 0; j < layerlen; j++)
                {
                    int val = data[i * layerlen + j];
                    if (val == 0)
                    {
                        zcount++;
                    }
                    if (val == 1)
                    {
                        ocount++;
                    }
                    if (val == 2)
                    {
                        tcount++;
                    }

                }

                if (zcount < mincnt)
                {
                    mincnt = zcount;
                    ret = ocount * tcount;
                }

            }


            Console.WriteLine("checksum = " + ret);




            int[] image = new int[layerlen];

            for (int j = 0; j < layerlen; j++)
            {
                image[j] = 2;
            }

            for (int i = 0; i < layers; i++)
            {
                
                for (int j = 0; j < layerlen; j++)
                {
                    int val = image[ j]; 
                    if(val == 2)
                    {
                        image[j] = data[i * layerlen + j];
                    }
                }
            }


            //output
            for (int j = 0; j < layerlen; j++)
            {
                if(j % w == 0)
                {
                    Console.WriteLine();
                }
                int val = image[j];
                if (val == 0)
                {
                    Console.Write(" ");
                }
                if (val == 1)
                {
                    Console.Write("#");
                }
                if (val == 2)
                {
                    Console.Write("!");
                }

            }
            Console.WriteLine();

        }

        int[] getData()
        {
            string[] lines = Program.readFile(8);
            //string[] lines = { "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0" };
            //string[] lines = { "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0" };
            //string[] lines = {"3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0"};
            //string[] lines = { "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5" };
            char[] data = lines[0].ToCharArray();
            int[] val = new int[data.Length];
            for (int i = 0; i < val.Length; i++)
            {
                val[i] = int.Parse(""+data[i]);
            }
            return val;
        }
    }
}
