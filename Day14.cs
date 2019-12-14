using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day14
    {


        Dictionary<Chem, List<Chem>> reactions = new Dictionary<Chem, List<Chem>>();

        List<Chem> need = new List<Chem>();
        List<Chem> extra = new List<Chem>();

        long totalOre = 1000000000000L;

        public void main()
        {

            getData();


            need.Add(new Chem("FUEL", 1));

            long oreamount = 0;

            while (need.Count > 0)
            {
                Chem needthis = need[0];
                need.RemoveAt(0);

                //first check extra;
                for(int i = 0; i < extra.Count; i++)
                {

                    if(extra[i].name.Equals(needthis.name))
                    {
                        if(extra[i].amount > needthis.amount)
                        {
                            extra[i].amount -= needthis.amount;
                            needthis.amount = 0;
                            break;
                        }
                        else
                        {
                            // < or =
                            needthis.amount -= extra[i].amount;
                            extra.RemoveAt(i);
                            break;
                        }
                    }

                }


                //now make what we need
                foreach(Chem result in reactions.Keys)
                {
                    if (result.name.Equals(needthis.name)){
                        //we found reaction!

                        //how many times do we need to run it?
                        long times = (needthis.amount / result.amount);
                        if(needthis.amount % result.amount != 0)
                        {
                            times++;
                        }

                        //deal with extra
                        long extraCh = (result.amount * times) - needthis.amount;
                        if(extraCh > 0)
                        {
                            Chem extr = new Chem(needthis.name, extraCh);

                            bool found = false;
                            for(int i = 0; i < extra.Count; i++)
                            {
                                if (extra[i].name.Equals(extr.name))
                                {
                                    found = true;
                                    extra[i].amount += extr.amount;
                                }
                            }
                            if (!found)
                            {
                                extra.Add(extr);
                            }
                        }

                        //add inputs we now need
                        List<Chem> willneed = reactions[result];
                        foreach(Chem input in willneed)
                        {
                            //add to need
                            bool found = false;
                            for(int i = 0; i < need.Count; i++)
                            {
                                if (need[i].name.Equals(input.name))
                                {
                                    found = true;
                                    need[i].amount += (input.amount * times);
                                }
                            }
                            if (!found)
                            {
                                if (!input.name.Equals("ORE"))
                                {
                                    Chem newneed = new Chem(input.name, input.amount * times);
                                    need.Add(newneed);
                                }
                                else
                                {
                                    oreamount += input.amount * times;
                                }
                            }

                        }


                        break;
                    }
                }
            }


            Console.WriteLine("ORE needed is  " + oreamount);

            //3061522


            need.Clear();
            extra.Clear();
            int totalfuel = 0;
            totalOre = 1000000000000L;

            int place = 1000000000;
            while(place > 0) {
                totalfuel += place;
                need.Clear();
                extra.Clear();
                totalOre = 1000000000000L;
                if (!MakeFuel(totalfuel))
                {
                    totalfuel -= place;
                    place /= 10;

                }
                else
                {
                    Console.WriteLine(" fuel: " + totalfuel + "   ore left: " + totalOre);
                }
            }
            Console.WriteLine("Total fuel is  " + totalfuel);





            need.Clear();
            extra.Clear();
            totalfuel = 0; 
            totalOre = 1000000000000L;
            while (totalfuel < 3060000)
            {
                MakeFuel(1000);
                totalfuel += 1000;

                if (totalfuel % 10000 == 0)
                {
                    Console.WriteLine(" fuel: " + totalfuel + "   ore left: " + totalOre);
                }
            }
            while (MakeFuel())
            {
                totalfuel++;

                if (totalfuel % 10000 == 0)
                {
                    Console.WriteLine(" fuel: " + totalfuel + "   ore left: " + totalOre);
                }
            }
            Console.WriteLine("Total fuel is  " + totalfuel);


/*
            need.Clear();
            extra.Clear();
            totalfuel = 0;
            totalOre = 1000000000000L;
           
            long onetenth = totalOre / 10;
            int estfuel = (int)(totalOre / oreamount);
            int makefuel = estfuel / 10;
            while(totalOre > onetenth)
            {
                MakeFuel(makefuel);
                totalfuel += makefuel;
                Console.WriteLine(" fuel: " + totalfuel + "   ore left: " + totalOre);
            }
            while (MakeFuel())
            {
                totalfuel++;

                if (totalfuel % 10000 == 0)
                {
                    Console.WriteLine(" fuel: " + totalfuel + "   ore left: " + totalOre);
                }
            }
            Console.WriteLine("Total fuel is  " + totalfuel);
*/



            need.Clear();
            extra.Clear();
            totalfuel = 0;
            totalOre = 1000000000000L;
            while (MakeFuel())
            {
                totalfuel++;

                if (totalfuel % 10000 == 0)
                {
                    Console.WriteLine(" fuel: " + totalfuel + "   ore left: " + totalOre);
                }
            }
            Console.WriteLine("Total fuel is  " + totalfuel);


        }



        bool MakeFuel(int amnt = 1)
        {
            need.Add(new Chem("FUEL", amnt));

            while (need.Count > 0)
            {
                Chem needthis = need[0];
                need.RemoveAt(0);

                //first check extra;
                for (int i = 0; i < extra.Count; i++)
                {

                    if (extra[i].name.Equals(needthis.name))
                    {
                        if (extra[i].amount > needthis.amount)
                        {
                            extra[i].amount -= needthis.amount;
                            needthis.amount = 0;
                            break;
                        }
                        else
                        {
                            // < or =
                            needthis.amount -= extra[i].amount;
                            extra.RemoveAt(i);
                            break;
                        }
                    }

                }


                //now make what we need
                foreach (Chem result in reactions.Keys)
                {
                    if (result.name.Equals(needthis.name))
                    {
                        //we found reaction!

                        //how many times do we need to run it?
                        long times = (needthis.amount / result.amount);
                        if (needthis.amount % result.amount != 0)
                        {
                            times++;
                        }

                        //deal with extra
                        long extraCh = (result.amount * times) - needthis.amount;
                        if (extraCh > 0)
                        {
                            Chem extr = new Chem(needthis.name, extraCh);

                            bool found = false;
                            for (int i = 0; i < extra.Count; i++)
                            {
                                if (extra[i].name.Equals(extr.name))
                                {
                                    found = true;
                                    extra[i].amount += extr.amount;
                                }
                            }
                            if (!found)
                            {
                                extra.Add(extr);
                            }
                        }

                        //add inputs we now need
                        List<Chem> willneed = reactions[result];
                        foreach (Chem input in willneed)
                        {
                            //add to need
                            bool found = false;
                            for (int i = 0; i < need.Count; i++)
                            {
                                if (need[i].name.Equals(input.name))
                                {
                                    found = true;
                                    need[i].amount += (input.amount * times);
                                }
                            }
                            if (!found)
                            {
                                if (!input.name.Equals("ORE"))
                                {
                                    Chem newneed = new Chem(input.name, input.amount * times);
                                    need.Add(newneed);
                                }
                                else
                                {
                                    if(totalOre > input.amount * times)
                                    {
                                        totalOre -= input.amount * times;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                            }

                        }


                        break;
                    }
                }
            }
            return true;
        }



        void getData()
        {
            string[] lines = Program.readFile(14);

/*
            string[] lines = @"10 ORE => 10 A
1 ORE => 1 B
7 A, 1 B => 1 C
7 A, 1 C => 1 D
7 A, 1 D => 1 E
7 A, 1 E => 1 FUEL".Split('\n');
*/
/*
            string[] lines = @"9 ORE => 2 A
8 ORE => 3 B
7 ORE => 5 C
3 A, 4 B => 1 AB
5 B, 7 C => 1 BC
4 C, 1 A => 1 CA
2 AB, 3 BC, 4 CA => 1 FUEL".Split('\n');
*/
/*
            string[] lines = @"157 ORE => 5 NZVS
165 ORE => 6 DCFZ
44 XJWVT, 5 KHKGT, 1 QDVJ, 29 NZVS, 9 GPVTF, 48 HKGWZ => 1 FUEL
12 HKGWZ, 1 GPVTF, 8 PSHF => 9 QDVJ
179 ORE => 7 PSHF
177 ORE => 5 HKGWZ
7 DCFZ, 7 PSHF => 2 XJWVT
165 ORE => 2 GPVTF
3 DCFZ, 7 NZVS, 5 HKGWZ, 10 PSHF => 8 KHKGT".Split('\n');
*/
/*
            string[] lines = @"2 VPVL, 7 FWMGM, 2 CXFTF, 11 MNCFX => 1 STKFG
17 NVRVD, 3 JNWZP => 8 VPVL
53 STKFG, 6 MNCFX, 46 VJHF, 81 HVMC, 68 CXFTF, 25 GNMV => 1 FUEL
22 VJHF, 37 MNCFX => 5 FWMGM
139 ORE => 4 NVRVD
144 ORE => 7 JNWZP
5 MNCFX, 7 RFSQX, 2 FWMGM, 2 VPVL, 19 CXFTF => 3 HVMC
5 VJHF, 7 MNCFX, 9 VPVL, 37 CXFTF => 6 GNMV
145 ORE => 6 MNCFX
1 NVRVD => 8 CXFTF
1 VJHF, 6 MNCFX => 4 RFSQX
176 ORE => 6 VJHF".Split('\n');
*/
/*
            string[] lines = @"171 ORE => 8 CNZTR
7 ZLQW, 3 BMBT, 9 XCVML, 26 XMNCP, 1 WPTQ, 2 MZWV, 1 RJRHP => 4 PLWSL
114 ORE => 4 BHXH
14 VRPVC => 6 BMBT
6 BHXH, 18 KTJDG, 12 WPTQ, 7 PLWSL, 31 FHTLT, 37 ZDVW => 1 FUEL
6 WPTQ, 2 BMBT, 8 ZLQW, 18 KTJDG, 1 XMNCP, 6 MZWV, 1 RJRHP => 6 FHTLT
15 XDBXC, 2 LTCX, 1 VRPVC => 6 ZLQW
13 WPTQ, 10 LTCX, 3 RJRHP, 14 XMNCP, 2 MZWV, 1 ZLQW => 1 ZDVW
5 BMBT => 4 WPTQ
189 ORE => 9 KTJDG
1 MZWV, 17 XDBXC, 3 XCVML => 2 XMNCP
12 VRPVC, 27 CNZTR => 2 XDBXC
15 KTJDG, 12 BHXH => 5 XCVML
3 BHXH, 2 VRPVC => 7 MZWV
121 ORE => 7 VRPVC
7 XCVML => 6 RJRHP
5 BHXH, 4 VRPVC => 5 LTCX".Split('\n');
*/

            for (int i = 0; i < lines.Length; i++)
            {
                string[] react = lines[i].Trim().Split(" => ");

                string[] result = react[1].Split(' ');
                Chem res = new Chem(result[1].Trim(), int.Parse(result[0].Trim()));


                string[] needed = react[0].Split(',');
                List<Chem> input = new List<Chem>();

                for (int j = 0; j < needed.Length; j++)
                {
                    string[] chem = needed[j].Trim().Split(' ');
                    Chem ch = new Chem(chem[1].Trim(), int.Parse(chem[0].Trim()));
                    input.Add(ch);
                }

                reactions.Add(res, input);
            }
        }

        class Chem
        {
            public string name;
            public long amount;

            public Chem(string n, long c)
            {
                name = n;
                amount = c;
            }
        }
    }
}
