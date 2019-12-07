using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day6
    {

        Dictionary<String, Obj> all = new Dictionary<string, Obj>();

        public void main()
        {



            string[] data = getData();

            foreach(string str in data)
            {
                string line = str.Trim();
                string[] vals = line.Split(')');

                Obj center;
                if (all.ContainsKey(vals[0]))
                {
                    center = all[vals[0]];
                }
                else
                {
                    center = new Obj();
                    center.name = vals[0];
                    all.Add(center.name, center);
                }

                Obj orbit;
                if (all.ContainsKey(vals[1]))
                {
                    orbit = all[vals[1]];
                }
                else
                {
                    orbit = new Obj();
                    orbit.name = vals[1];
                    all.Add(orbit.name, orbit);
                }

                if(orbit.parent != null)
                {
                    Console.WriteLine("ERROR!");
                }
                else
                {
                    orbit.parent = center;
                }
            }



            //calc check
            int count = 0;
            foreach(Obj o in all.Values)
            {
                Obj ob = o;
                while(ob.parent != null)
                {
                    count++;
                    ob = ob.parent;
                }
            }

            Console.WriteLine("Checksum = " + count);


            //get transfers
            string[] ListYou = getList("YOU");
            string[] ListSAN = getList("SAN");

            //remove the nodes in common
            int pos = 0;
            while (ListYou[pos].Equals(ListSAN[pos]))
            {
                pos++;
            }
            //sum the remaining path length
            int trans = (ListYou.Length - pos) + (ListSAN.Length - pos);
            //the above is technically wrong, as it removes the last node in common (and hence 2 transfers), 
            //however, we are already at the nodes at the end of the list, and don't need to transfer to them,
            //so the end nodes add on 2 transfers, balancing things out.

            Console.WriteLine("Transfers = " + trans);
        }

        string[] getList(string name)
        {
            //returns the array of nodes from root up to (but not including) the specified node
            Dictionary<int,string> list = new Dictionary<int, string>();

            Obj o = all[name];
            if(o == null)
            {
                Console.WriteLine("ERROR!");
                return new string[0];
            }
            o = o.parent;
            int count = 0;
            while(o.parent != null)
            {
                list.Add(count, o.name);
                o = o.parent;
                count++;
            }
            list.Add(count, o.name);
            count++;

            string[] ret = new string[count];
            for (int i=0; count-i > 0; i++)
            {
                ret[count - i - 1] = list[i];
            }

            return ret;
        }

        string[] getData()
        {
            string[] lines = Program.readFile(6);
            /*
            string[] lines = @"COM)B
            B)C
            C)D
            D)E
            E)F
            B)G
            G)H
            D)I
            E)J
            J)K
            K)L
            K)YOU
            I)SAN".Split("\n");
            //*/
            return lines;
        }


        class Obj
        {
            public string name;
            public Obj parent;


        }
    }

   
}
