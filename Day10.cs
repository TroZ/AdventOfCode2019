using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day10
    {


        public void main()
        {
            Console.WriteLine("" + Math.Atan2(5, 0));
            Console.WriteLine("" + Math.Atan2(5, 1));
            Console.WriteLine("" + Math.Atan2(5, -1));

            Console.WriteLine("" + Math.Atan2(-1, 5));
            Console.WriteLine("" + Math.Atan2(0, 5));
            Console.WriteLine("" + Math.Atan2(1, 5));

            string[] data = getData();

            Dictionary<Tuple<int, int>, int> area = new Dictionary<Tuple<int, int>, int>();

            //make data structure of asteroids.
            int count = 0;
            for (int y = 0; y < data.Length; y++)
            {
                for (int x = 0; x < data[y].Length; x++)
                {
                    if (data[y].ToCharArray()[x] == '#')
                    {
                        area.Add(new Tuple<int, int>(x, y), count);
                        count++;
                    }
                }
            }


            //iterate structure, for each asteroid, find angle (minimized) to other asteroids, and look for others on same angle
            int bestsee = 0;
            Tuple<int, int> bestloc = new Tuple<int, int>(0, 0);
            foreach (Tuple<int, int> loc in area.Keys)
            {
                int cansee = 0;
                int locx = loc.Item1;
                int locy = loc.Item2;

                if (locx == 4 && locy == 0)
                {
                    Console.WriteLine("here");
                }

                foreach (Tuple<int, int> check in area.Keys)
                {
                    if (check.Item1 == loc.Item1 && check.Item2 == loc.Item2)
                    {
                        continue;//no need to check itself
                    }

                    int xdif = check.Item1 - locx;
                    int ydif = check.Item2 - locy;

                    //minimize
                    for (int i = 2; i < 26; i++)
                    {
                        int abx = Math.Abs(xdif);
                        int aby = Math.Abs(ydif);
                        while (abx % i == 0 && aby % i == 0)
                        {
                            xdif = xdif / i;
                            ydif = ydif / i;
                            abx = Math.Abs(xdif);
                            aby = Math.Abs(ydif);
                        }
                    }

                    //now see if there are others in the way
                    int chkx = locx + xdif;
                    int chky = locy + ydif;
                    int tochk = 1;
                    bool ok = true;
                    while (chkx > -1 && chkx < 50 && chky > -1 && chky < 50)
                    {
                        if (chkx == check.Item1 && chky == check.Item2)
                        {
                            break;
                        }

                        if (area.ContainsKey(new Tuple<int, int>(chkx, chky)))
                        {
                            ok = false;
                            break;
                        }

                        tochk++;
                        chkx = locx + xdif * tochk;
                        chky = locy + ydif * tochk;
                    }

                    if (ok)
                    {
                        cansee++;
                    }

                }
                if (cansee > bestsee)
                {
                    bestsee = cansee;
                    bestloc = loc;
                }
            }

            Console.WriteLine(" Can see  " + bestsee + "  at " + bestloc.Item1 + " , " + bestloc.Item2);



            //part2.
            area.Remove(bestloc);//don't want to accidentally hit radar asteroid

            //this will be lists sorted by angle, with each list sorted by distance
            SortedList<double, SortedList<int, Tuple<int, int>>> hitlist = new SortedList<double, SortedList<int, Tuple<int, int>>>();

            //create list of asteroids sorted by angle and distance
            int cx = bestloc.Item1;
            int cy = bestloc.Item2;
            foreach (Tuple<int, int> loc in area.Keys)
            {
                int dx = loc.Item1 - cx;
                int dy = cy - loc.Item2;
                double angle = Math.Atan2(dx,dy );
                if(angle < 0)
                {
                    angle += Math.PI * 2;
                }

                SortedList<int, Tuple<int, int>> distlist;
                if (hitlist.ContainsKey(angle))
                {
                    distlist = hitlist[angle];
                }
                else
                {
                    distlist = new SortedList<int, Tuple<int, int>>();
                    hitlist[angle] = distlist;
                }

                int dist = Math.Abs(loc.Item1 - cx) + Math.Abs(loc.Item2 - cy);
                distlist.Add(dist, loc);

            }

            //ok, now iterate the list, removing one item from each list per rotation
            int hitcount = 0;
            while(hitlist.Count > 0 && hitcount < 200)
            {
                foreach(double key in hitlist.Keys)
                {
                    SortedList<int, Tuple<int, int>> distlist = hitlist[key];
                    if(distlist.Count == 0)
                    {
                        continue;
                    }

                    Tuple<int, int> loc = distlist.Values[0];
                    hitcount++;

                    distlist.Remove(distlist.Keys[0]);
                    //if(distlist.Count == 0)
                    //{
                    //    hitlist.Remove(key);
                    //}

                    Console.WriteLine("hit " + hitcount + "  is at  " + loc.Item1 + " , " + loc.Item2);

                    if (hitcount == 200)
                    {
                        Console.WriteLine(" VALUE = " + ((loc.Item1 * 100) + loc.Item2));
                    }
                }


            }

        }
        

        string[] getData()
        {
            string[] lines = Program.readFile(10);
            /*
             * string[] lines = @".#..#
.....
#####
....#
...##".Split("\n");
*/
/*
            string[] lines = @".#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##".Split("\n");
*/
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Trim();
            }
            return lines;
        }
    }
}
