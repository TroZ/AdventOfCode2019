using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day4
    {


        int size = 10000;

        public void main()
        {

            //int[] data = getData();

            int min = 178416;
            int max = 676461;
            int count = 0;



            for (int i = min; i < max + 1; i++)
            {

                bool ok = true;
                String pass = "" + i;
                char c = ' ';
                for (int pos = 0; pos < pass.Length; pos++)
                {
                    if (pass[pos] < c)
                    {
                        ok = false;
                    }
                    c = pass[pos];
                }
                if (ok)
                {
                    ok = false;
                    for (int pos = 0; pos < pass.Length - 1; pos++)
                    {
                        if (pass[pos] == pass[pos + 1])
                        {
                            ok = true;
                        }
                    }
                }
                if (ok)
                {
                    //System.Console.WriteLine(pass);
                    count++;
                }
            }


            System.Console.WriteLine("count = " + count);


            count = 0;

            for (int i = min; i < max+1; i++)
            {

                bool ok = true;
                String pass = "" + i;
                char c = ' ';
                for(int pos = 0; pos < pass.Length; pos++) { 
                    if(pass[pos] < c)
                    {
                        ok = false;
                    }
                    c = pass[pos];
                }
                if (ok)
                {
                    ok = false;
                    for (int pos = 0; pos < pass.Length - 1; pos++)
                    {
                        if (pass[pos] == pass[pos + 1] && (pos==0 || (pos > 0 && pass[pos-1] != pass[pos])))
                        {
                            if (pos + 2 == pass.Length || (pos + 2 < pass.Length && pass[pos] != pass[pos + 2]))
                            {
                                ok = true;
                            }
                        }
                    }
                }
                if (ok)
                {
                   //System.Console.WriteLine(pass);
                   count++;
                }
            }


            System.Console.WriteLine("count = " + count);

        }

        int[] getData()
        {
            string[] lines = Program.readFile(4);
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
