using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day12
    {

        int steps = 1000;

        readonly static Regex rxMoon = new Regex(@"=([-0-9]*)[,>]", RegexOptions.Compiled | RegexOptions.IgnoreCase);


        Dictionary<int, State> history = new Dictionary<int, State>();


        public void main()
        {


            Moon[] data = getData();


            print(data, 0);
            for (int i = 0; i < steps; i++)
            {
                foreach (Moon m in data)
                {
                    m.gravity(data);
                }
                foreach (Moon m in data)
                {
                    m.velocity();
                }
                print(data, i + 1);
            }


            int energy = 0;
            foreach (Moon m in data)
            {
                int pe = m.getPE();
                int ke = m.getKE();
                energy += (pe * ke);
            }

            Console.WriteLine(" Total energy = " + energy);


            Console.WriteLine();



            data = getData();

            history.Add(0, new State(data));
            bool fx = false;
            bool fy = false;
            bool fz = false;
            int xl = 0;
            int yl = 0;
            int zl = 0;

            for (int i = 0; i < 1000000; i++)
            {
                if (i % 1000 == 0)
                {
                    Console.WriteLine(" Step " + i);
                }

                foreach (Moon m in data)
                {
                    m.gravity(data);
                }
                foreach (Moon m in data)
                {
                    m.velocity();
                }

                //make current state
                State s = new State(data);
                //check history
                foreach(State past in history.Values)
                {
                    /*
                    if (past.Equals(s))
                    {
                        Console.WriteLine("FOUND REPEAT after step " + (i + 1));
                        i = int.MaxValue;
                        break;
                    }
                    */
                    if (!fx)
                    {
                        bool ok = true;
                        for (int j = 0; j < 4 && ok; j++)
                        {
                            if(s.moons[j].x!=past.moons[j].x || s.moons[j].vx != past.moons[j].vx)
                            {
                                ok = false;
                            }
                        }
                        if (ok)
                        {
                            fx = true;
                            xl = i + 1;
                            Console.WriteLine("XL!");
                        }
                    }

                    if (!fy)
                    {
                        bool ok = true;
                        for (int j = 0; j < 4 && ok; j++)
                        {
                            if (s.moons[j].y != past.moons[j].y || s.moons[j].vy != past.moons[j].vy)
                            {
                                ok = false;
                            }
                        }
                        if (ok)
                        {
                            fy = true;
                            yl = i + 1;
                            Console.WriteLine("YL!");
                        }
                    }

                    if (!fz)
                    {
                        bool ok = true;
                        for (int j = 0; j < 4 && ok; j++)
                        {
                            if (s.moons[j].z != past.moons[j].z || s.moons[j].vz != past.moons[j].vz)
                            {
                                ok = false;
                            }
                        }
                        if (ok)
                        {
                            fz = true;
                            zl = i + 1;
                            Console.WriteLine("ZL!");
                        }
                    }
                }

                //add to history
                //history.Add(i + 1, s);

                if (fx && fy && fz)
                {
                    break;
                }
            }

            long loop = ((long)xl) * ((long)yl) * ((long)zl);
            Console.WriteLine(" Loops after " + loop + "   xl = " + xl + "  yl = " + yl + "  zl = " + zl);


            Console.WriteLine(" min loops " + lcm(xl, lcm(yl, zl)));

        }



        long lcm(long a, long b)
        {
            long num1, num2;
            if (a > b)
            {
                num1 = a; num2 = b;
            }
            else
            {
                num1 = b; num2 = a;
            }

            for (int i = 1; i < num2; i++)
            {
                if ((num1 * i) % num2 == 0)
                {
                    return i * num1;
                }
            }
            return num1 * num2;
        }


        void print(Moon[] moons, int step)
        {
            Console.WriteLine("After " + step + " steps:");
            foreach(Moon m in moons)
            {
                m.print();
            }
            Console.WriteLine();
        }

        Moon[] getData()
        {
            string[] lines = Program.readFile(12);
            /*
            string[] lines =  @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>".Split("\n") ;
*/
/*
            string[] lines = @"<x=-8, y=-10, z=0>
<x=5, y=5, z=10>
<x=2, y=-7, z=3>
<x=9, y=-8, z=-3>".Split("\n");
*/
            Moon[] val = new Moon[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                MatchCollection mc = rxMoon.Matches(lines[i]);
                if(mc.Count == 3)
                {
                    int x = int.Parse(mc[0].Groups[1].Value);
                    int y = int.Parse(mc[1].Groups[1].Value);
                    int z = int.Parse(mc[2].Groups[1].Value);
                    Moon m = new Moon(x, y, z);
                    val[i] = m;
                }
                
            }
            return val;
        }
    }

    class State
    {
        public Moon[] moons;
        public State(Moon[] m)
        {
            moons = new Moon[m.Length];
            for(int i = 0; i < m.Length; i++)
            {
                moons[i] = new Moon(m[i]);
            }
        }

        public bool Equals(State s)
        {
            bool ret = true;
            for(int i = 0; i < 4; i++)
            {
                ret = ret && moons[i].Equals(s.moons[i]);
            }
            return ret;
        }
    }


    class Moon
    {
        public int x = 0;
        public int y = 0;
        public int z = 0;

        public int vx = 0;
        public int vy = 0;
        public int vz = 0;

        public Moon(int xx, int yy, int zz)
        {
            x = xx;
            y = yy;
            z = zz;
        }

        public Moon(Moon m)
        {
            x = m.x;
            y = m.y;
            z = m.z;
            vx = m.vx;
            vy = m.vy;
            vz = m.vz;
        }

        public bool Equals(Moon m)
        {
            bool ret = true;

            ret = ret && x == m.x && y == m.y && z == m.z;
            ret = ret && vx == m.vx && vy == m.vy && vz == m.vz;

            return ret;
        }

        public void gravity(Moon[] moons)
        {

            foreach(Moon m in moons)
            {
                if (m == this)
                {
                    continue;
                }
                if (m.x > this.x)
                {
                    vx++;
                }
                if(m.x < this.x)
                {
                    vx--;
                }
                if (m.y > this.y)
                {
                    vy++;
                }
                if (m.y < this.y)
                {
                    vy--;
                }
                if (m.z > this.z)
                {
                    vz++;
                }
                if (m.z < this.z)
                {
                    vz--;
                }
            }
        }

        public void velocity()
        {
            x += vx;
            y += vy;
            z += vz;
        }

        public int getPE()
        {
            return Math.Abs(x) + Math.Abs(y) + Math.Abs(z);
        }

        public int getKE()
        {
            return Math.Abs(vx) + Math.Abs(vy) + Math.Abs(vz);
        }

        public void print()
        {
            Console.WriteLine("Moon: pos=<x= " + x + ", y= " + y + ", z= " + z + ">,  vel=<x= " + vx + ", y= " + vy + ", z= " + vz + ">");
        }
        
    }
}
