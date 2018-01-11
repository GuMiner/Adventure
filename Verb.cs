using System;
namespace Adventure
{
    internal class Verb
    {
        /*
        Routine to process a transitive verb
    */
        public static string trverb(State globals)
        {
            switch (globals.verb)
            {
                case Definitions.CALM:
                case Definitions.WALK:
                case Definitions.QUIT:
                case Definitions.SCORE:
                case Definitions.FOO:
                case Definitions.BRIEF:
                case Definitions.SUSPEND:
                case Definitions.HOURS:
                case Definitions.LOG:
                    return actspk(globals.verb);
                case Definitions.TAKE:
                    return vtake(globals);
                case Definitions.DROP:
                    return vdrop(globals);
                case Definitions.OPEN:
                case Definitions.LOCK:
                    return vopen(globals);
                case Definitions.SAY:
                    return vsay(globals);
                case Definitions.NOTHING:
                    return Database.rspeak(54);
                case Definitions.ON:
                    return von(globals);
                case Definitions.OFF:
                    return voff(globals);
                case Definitions.WAVE:
                    return vwave(globals);
                case Definitions.KILL:
                    return vkill(globals);
                case Definitions.POUR:
                    return vpour(globals);
                case Definitions.EAT:
                    return veat(globals);
                case Definitions.DRINK:
                    return vdrink(globals);
                case Definitions.RUB:
                    if (globals.@object != Definitions.LAMP)
                        return Database.rspeak(76);
                    else
                        return actspk(Definitions.RUB);
                case Definitions.THROW:
                    return vthrow(globals);
                case Definitions.FEED:
                    return vfeed(globals);
                case Definitions.FIND:
                case Definitions.INVENTORY:
                    return vfind(globals);
                case Definitions.FILL:
                    return vfill(globals);
                case Definitions.READ:
                    return vread(globals);
                case Definitions.BLAST:
                    return vblast(globals);
                case Definitions.BREAK:
                    return vbreak(globals);
                case Definitions.WAKE:
                    return vwake(globals);
                default:
                    return "This verb is not implemented yet.\n";
            }
        }

        /*
            CARRY TAKE etc.
        */
        public static string vtake(State globals)
        {
            int msg;
            int i;

            if (Database.toting(globals, globals.@object))
            {
                return actspk(globals.@verb);
            }
            /*
               special case objects and fixed objects
            */
            msg = 25;
            if (globals.@object == Definitions.PLANT && globals.prop[Definitions.PLANT] <= 0)
                msg = 115;
            if (globals.@object == Definitions.BEAR && globals.prop[Definitions.BEAR] == 1)
                msg = 169;
            if (globals.@object == Definitions.CHAIN && globals.prop[Definitions.BEAR] != 0)
                msg = 170;
            if (globals.@fixed[globals.@object] != 0)
            {
                return Database.rspeak(msg);
            }
            /*
               special case for liquids
            */
            if (globals.@object == Definitions.WATER || globals.@object == Definitions.OIL)
            {
                if (!Database.here(globals, Definitions.BOTTLE) || Database.liq(globals) != globals.@object)
                {
                    globals.@object = Definitions.BOTTLE;
                    if (Database.toting(globals, Definitions.BOTTLE) && globals.prop[Definitions.BOTTLE] == 1)
                    {
                        return vfill(globals);
                    }
                    if (globals.prop[Definitions.BOTTLE] != 1)
                        msg = 105;
                    if (!Database.toting(globals, Definitions.BOTTLE))
                        msg = 104;
                    return Database.rspeak(msg);
                }
                globals.@object = Definitions.BOTTLE;
            }
            if (globals.holding >= 7)
            {
                return Database.rspeak(92);
            }
            /*
               special case for bird.
            */
            if (globals.@object == Definitions.BIRD && globals.prop[Definitions.BIRD] == 0)
            {
                if (Database.toting(globals, Definitions.ROD))
                {
                    return Database.rspeak(26);
                }
                if (!Database.toting(globals, Definitions.CAGE))
                {
                    return Database.rspeak(27);
                }
                globals.prop[Definitions.BIRD] = 1;
            }
            if ((globals.@object == Definitions.BIRD || globals.@object == Definitions.CAGE) &&
                globals.prop[Definitions.BIRD] != 0)
                Database.carry(globals, (Definitions.BIRD + Definitions.CAGE) - globals.@object, globals.loc);
            Database.carry(globals, globals.@object, globals.loc);
            /*
               handle liquid in bottle
            */
            i = Database.liq(globals);
            if (globals.@object == Definitions.BOTTLE && i != 0)
                globals.place[i] = -1;
            return Database.rspeak(54);
        }

        /*
            DROP etc.
        */
        public static string vdrop(State globals)
        {
            int i;
            string output = string.Empty;

            /*
               check for dynamite
            */
            if (Database.toting(globals, Definitions.ROD2) && globals.@object == Definitions.ROD && !Database.toting(globals, Definitions.ROD))
                globals.@object = Definitions.ROD2;
            if (!Database.toting(globals, globals.@object))
            {
                return actspk(globals.verb);
            }
            /*
               snake and bird
            */
            if (globals.@object == Definitions.BIRD && Database.here(globals, Definitions.SNAKE))
            {
                output += Database.rspeak(30);
                if (globals.closed)
                    output += Turn.dwarfend(globals);
                Database.dstroy(globals, Definitions.SNAKE);
                globals.prop[Definitions.SNAKE] = -1;
            }
            /*
               coins and vending machine
            */
            else if (globals.@object == Definitions.COINS && Database.here(globals, Definitions.VEND))
            {
                Database.dstroy(globals, Definitions.COINS);
                Database.drop(globals, Definitions.BATTERIES, globals.loc);
                output += Database.pspeak(Definitions.BATTERIES, 0);
                return output;
            }
            /*
               bird and dragon (ouch!!)
            */
            else if (globals.@object == Definitions.BIRD && Database.at(globals, Definitions.DRAGON) && globals.prop[Definitions.DRAGON] == 0)
            {
                output += Database.rspeak(154);
                Database.dstroy(globals, Definitions.BIRD);
                globals.prop[Definitions.BIRD] = 0;
                if (globals.place[Definitions.SNAKE] != 0)
                    ++globals.tally2;
                return output;
            }
            /*
               Bear and troll
            */
            if (globals.@object == Definitions.BEAR && Database.at(globals, Definitions.TROLL))
            {
                output += Database.rspeak(163);
                Database.move(globals, Definitions.TROLL, 0);
                Database.move(globals, (Definitions.TROLL + Definitions.MAXOBJ), 0);
                Database.move(globals, Definitions.TROLL2, 117);
                Database.move(globals, (Definitions.TROLL2 + Definitions.MAXOBJ), 122);
                Database.juggle(Definitions.CHASM);
                globals.prop[Definitions.TROLL] = 2;
            }
            /*
               vase
            */
            else if (globals.@object == Definitions.VASE)
            {
                if (globals.loc == 96)
                    output += Database.rspeak(54);
                else
                {
                    globals.prop[Definitions.VASE] = Database.at(globals, Definitions.PILLOW) ? 0 : 2;
                    Database.pspeak(Definitions.VASE, globals.prop[Definitions.VASE] + 1);
                    if (globals.prop[Definitions.VASE] != 0)
                        globals.@fixed[Definitions.VASE] = -1;
                }
            }
            /*
               handle liquid and bottle
            */
            i = Database.liq(globals);
            if (i == globals.@object)
                globals.@object = Definitions.BOTTLE;
            if (globals.@object == Definitions.BOTTLE && i != 0)
                globals.place[i] = 0;
            /*
               handle bird and cage
            */
            if (globals.@object == Definitions.CAGE && globals.prop[Definitions.BIRD] != 0)
                Database.drop(globals, Definitions.BIRD, globals.loc);
            if (globals.@object == Definitions.BIRD)
                globals.prop[Definitions.BIRD] = 0;
            Database.drop(globals, globals.@object, globals.loc);

            return output;
        }

        /*
            LOCK, UNLOCK, OPEN, CLOSE etc.
        */
        public static string vopen(State globals)
        {
            int msg, oyclam;

            switch (globals.@object)
            {
                case Definitions.CLAM:
                case Definitions.OYSTER:
                    oyclam = (globals.@object == Definitions.OYSTER ? 1 : 0);
                    if (globals.verb == Definitions.LOCK)
                        msg = 61;
                    else if (!Database.toting(globals, Definitions.TRIDENT))
                        msg = 122 + oyclam;
                    else if (Database.toting(globals, globals.@object))
                        msg = 120 + oyclam;
                    else
                    {
                        msg = 124 + oyclam;
                        Database.dstroy(globals, Definitions.CLAM);
                        Database.drop(globals, Definitions.OYSTER, globals.loc);
                        Database.drop(globals, Definitions.PEARL, 105);
                    }
                    break;
                case Definitions.DOOR:
                    msg = (globals.prop[Definitions.DOOR] == 1 ? 54 : 111);
                    break;
                case Definitions.CAGE:
                    msg = 32;
                    break;
                case Definitions.KEYS:
                    msg = 55;
                    break;
                case Definitions.CHAIN:
                    if (!Database.here(globals, Definitions.KEYS))
                        msg = 31;
                    else if (globals.verb == Definitions.LOCK)
                    {
                        if (globals.prop[Definitions.CHAIN] != 0)
                            msg = 34;
                        else if (globals.loc != 130)
                            msg = 173;
                        else
                        {
                            globals.prop[Definitions.CHAIN] = 2;
                            if (Database.toting(globals, Definitions.CHAIN))
                                Database.drop(globals, Definitions.CHAIN, globals.loc);
                            globals.@fixed[Definitions.CHAIN] = -1;
                            msg = 172;
                        }
                    }
                    else
                    {
                        if (globals.prop[Definitions.BEAR] == 0)
                            msg = 41;
                        else if (globals.prop[Definitions.CHAIN] == 0)
                            msg = 37;
                        else
                        {
                            globals.prop[Definitions.CHAIN] = 0;
                            globals.@fixed[Definitions.CHAIN] = 0;
                            if (globals.prop[Definitions.BEAR] != 3)
                                globals.prop[Definitions.BEAR] = 2;
                            globals.@fixed[Definitions.BEAR] = 2 - globals.prop[Definitions.BEAR];
                            msg = 171;
                        }
                    }
                    break;
                case Definitions.GRATE:
                    if (!Database.here(globals, Definitions.KEYS))
                        msg = 31;
                    else if (globals.closing)
                    {
                        if (globals.panic != 0)
                        {
                            globals.clock2 = 15;
                            ++globals.panic;
                        }
                        msg = 130;
                    }
                    else
                    {
                        msg = 34 + globals.prop[Definitions.GRATE];
                        globals.prop[Definitions.GRATE] = (globals.verb == Definitions.LOCK ? 0 : 1);
                        msg += 2 * globals.prop[Definitions.GRATE];
                    }
                    break;
                default:
                    msg = 33;
                    break;
            }

            return Database.rspeak(msg);
        }

        /*
            SAY etc.
        */
        public static string vsay(State globals)
        {
            int wtype, wval;
            wtype = wval = 0;
            Tuple<bool, string> analysisResults = English.analyze(globals.word1, ref wtype, ref wval);
            return (analysisResults.Item2 + $"Okay.\n{(wval == Definitions.SAY ? globals.word2 : globals.word1)}\n");
        }

        /*
            ON etc.
        */
        public static string von(State globals)
        {
            string output = string.Empty;
            if (!Database.here(globals, Definitions.LAMP))
                return actspk(globals.verb);
            else if (globals.limit < 0)
                return Database.rspeak(184);
            else
            {
                globals.prop[Definitions.LAMP] = 1;
                output += Database.rspeak(39);
                if (globals.wzdark)
                {
                    globals.wzdark = false;
                    output += Environment.NewLine + Turn.describe(globals);
                }
            }

            return output;
        }

        /*
            OFF etc.
        */
        public static string voff(State globals)
        {
            if (!Database.here(globals, Definitions.LAMP))
                return actspk(globals.verb);
            else
            {
                globals.prop[Definitions.LAMP] = 0;
                return Database.rspeak(40);
            }
        }

        /*
            WAVE etc.
        */
        public static string vwave(State globals)
        {
            if (!Database.toting(globals, globals.@object) && (globals.@object != Definitions.ROD || !Database.toting(globals, Definitions.ROD2)))
                return Database.rspeak(29);
            else if (globals.@object != Definitions.ROD || !Database.at(globals, Definitions.FISSURE) || !Database.toting(globals, globals.@object) || globals.closing)
                return actspk(globals.verb);
            else
            {
                globals.prop[Definitions.FISSURE] = 1 - globals.prop[Definitions.FISSURE];
                return Database.pspeak(Definitions.FISSURE, 2 - globals.prop[Definitions.FISSURE]);
            }
        }

        /*
            ATTACK, KILL etc.
        */
        public static string vkill(State globals)
        {
            string output = string.Empty;
            int msg = 0;
            int i;

            switch (globals.@object)
            {
                case Definitions.BIRD:
                    if (globals.closed)
                        msg = 137;
                    else
                    {
                        Database.dstroy(globals, Definitions.BIRD);
                        globals.prop[Definitions.BIRD] = 0;
                        if (globals.place[Definitions.SNAKE] == 19)
                            ++globals.tally2;
                        msg = 45;
                    }
                    break;
                case 0:
                    msg = 44;
                    break;
                case Definitions.CLAM:
                case Definitions.OYSTER:
                    msg = 150;
                    break;
                case Definitions.SNAKE:
                    msg = 46;
                    break;
                case Definitions.DWARF:
                    if (globals.closed)
                        output += Turn.dwarfend(globals);
                    msg = 49;
                    break;
                case Definitions.TROLL:
                    msg = 157;
                    break;
                case Definitions.BEAR:
                    msg = 165 + (globals.prop[Definitions.BEAR] + 1) / 2;
                    break;
                case Definitions.DRAGON:
                    if (globals.prop[Definitions.DRAGON] != 0)
                    {
                        msg = 167;
                        break;
                    }
                    output += Database.rspeak(49);
                    output += Environment.NewLine + Database.pspeak(Definitions.DRAGON, 1);
                    globals.prop[Definitions.DRAGON] = 2;
                    globals.prop[Definitions.RUG] = 0;
                    Database.move(globals, (Definitions.DRAGON + Definitions.MAXOBJ), -1);
                    Database.move(globals, (Definitions.RUG + Definitions.MAXOBJ), 0);
                    Database.move(globals, Definitions.DRAGON, 120);
                    Database.move(globals, Definitions.RUG, 120);
                    for (i = 1; i < Definitions.MAXOBJ; ++i)
                        if (globals.place[i] == 119 || globals.place[i] == 121)
                            Database.move(globals, i, 120);
                    globals.newloc = 120;
                    return output;
                default:
                    output += actspk(globals.verb);
                    return output;
            }

            output += Database.rspeak(msg);
            return output;
        }

        /*
            POUR
        */
        public static string vpour(State globals)
        {
            if (globals.@object == Definitions.BOTTLE || globals.@object == 0)
                globals.@object = Database.liq(globals);
            if (globals.@object == 0)
            {
                return needobj(globals);
            }
            if (!Database.toting(globals, globals.@object))
            {
                return actspk(globals.verb);
            }
            if (globals.@object != Definitions.OIL && globals.@object != Definitions.WATER)
            {
                return Database.rspeak(78);
            }

            string output = string.Empty;
            globals.prop[Definitions.BOTTLE] = 1;
            globals.place[globals.@object] = 0;
            if (Database.at(globals, Definitions.PLANT))
            {
                if (globals.@object != Definitions.WATER)
                    output += Database.rspeak(112);
                else
                {
                    output += Database.pspeak(Definitions.PLANT, globals.prop[Definitions.PLANT] + 1);
                    globals.prop[Definitions.PLANT] = (globals.prop[Definitions.PLANT] + 2) % 6;
                    globals.prop[Definitions.PLANT2] = globals.prop[Definitions.PLANT] / 2;
                    output += Environment.NewLine + Turn.describe(globals);
                }
            }
            else if (Database.at(globals, Definitions.DOOR))
            {
                globals.prop[Definitions.DOOR] = (globals.@object == Definitions.OIL ? 1 : 0);
                output += Database.rspeak(113 + globals.prop[Definitions.DOOR]);
            }
            else
                output += Database.rspeak(77);

            return output;
        }

        /*
            EAT
        */
        public static string veat(State globals)
        {
            int msg;

            switch (globals.@object)
            {
                case Definitions.FOOD:
                    Database.dstroy(globals, Definitions.FOOD);
                    msg = 72;
                    break;
                case Definitions.BIRD:
                case Definitions.SNAKE:
                case Definitions.CLAM:
                case Definitions.OYSTER:
                case Definitions.DWARF:
                case Definitions.DRAGON:
                case Definitions.TROLL:
                case Definitions.BEAR:
                    msg = 71;
                    break;
                default:
                    return actspk(globals.verb);
            }

            return Database.rspeak(msg);
        }

        /*
            DRINK
        */
        public static string vdrink(State globals)
        {
            if (globals.@object != Definitions.WATER)
                return Database.rspeak(110);
            else if (Database.liq(globals) != Definitions.WATER || !Database.here(globals, Definitions.BOTTLE))
                return actspk(globals.verb);
            else
            {
                globals.prop[Definitions.BOTTLE] = 1;
                globals.place[Definitions.WATER] = 0;
                return Database.rspeak(74);
            }
        }

        /*
            THROW etc.
        */
        public static string vthrow(State globals)
        {
            int msg;
            int i;

            if (Database.toting(globals, Definitions.ROD2) && globals.@object == Definitions.ROD && !Database.toting(globals, Definitions.ROD))
                globals.@object = Definitions.ROD2;
            if (!Database.toting(globals, globals.@object))
            {
                return actspk(globals.verb);
            }
            /*
               treasure to troll
            */
            if (Database.at(globals, Definitions.TROLL) && globals.@object >= 50 && globals.@object < Definitions.MAXOBJ)
            {
                Database.drop(globals, globals.@object, 0);
                Database.move(globals, Definitions.TROLL, 0);
                Database.move(globals, (Definitions.TROLL + Definitions.MAXOBJ), 0);
                Database.drop(globals, Definitions.TROLL2, 117);
                Database.drop(globals, (Definitions.TROLL2 + Definitions.MAXOBJ), 122);
                Database.juggle(Definitions.CHASM);
                return Database.rspeak(159);
            }
            /*
               feed the bears...
            */
            if (globals.@object == Definitions.FOOD && Database.here(globals, Definitions.BEAR))
            {
                globals.@object = Definitions.BEAR;
                return vfeed(globals);
            }
            /*
               if not axe, same as drop...
            */
            if (globals.@object != Definitions.AXE)
            {
                return vdrop(globals);
            }
            /*
               AXE is THROWN
            */
            /*
               at a dwarf...
            */
            if ((i = Database.dcheck(globals)) > 0)
            {
                msg = 48;
                if (Database.pct(33))
                {
                    globals.dseen[i] = false;
                    globals.dloc[i] = 0;
                    msg = 47;
                    ++globals.dkill;
                    if (globals.dkill == 1)
                        msg = 149;
                }
            }
            /*
               at a dragon...
            */
            else if (Database.at(globals, Definitions.DRAGON) && globals.prop[Definitions.DRAGON] == 0)
                msg = 152;
            /*
               at the troll...
            */
            else if (Database.at(globals, Definitions.TROLL))
                msg = 158;
            /*
               at the bear...
            */
            else if (Database.here(globals, Definitions.BEAR) && globals.prop[Definitions.BEAR] == 0)
            {
                Database.drop(globals, Definitions.AXE, globals.loc);
                globals.@fixed[Definitions.AXE] = -1;
                globals.prop[Definitions.AXE] = 1;
                Database.juggle(Definitions.BEAR);
                return Database.rspeak(164);
            }
            /*
               otherwise it is an attack
            */
            else
            {
                globals.verb = Definitions.KILL;
                globals.@object = 0;
                return itverb(globals);
            }
            /*
               handle the left over axe...
            */
            string output = Database.rspeak(msg);
            Database.drop(globals, Definitions.AXE, globals.loc);
            return Environment.NewLine + Turn.describe(globals);
        }

        /*
            INVENTORY, FIND etc.
        */
        public static string vfind(State globals)
        {
            int msg;
            if (Database.toting(globals, globals.@object))
                msg = 24;
            else if (globals.closed)
                msg = 138;
            else if (Database.dcheck(globals) > 0 && globals.dflag >= 2 && globals.@object == Definitions.DWARF)
                msg = 94;
            else if (Database.at(globals, globals.@object) ||
                (Database.liq(globals) == globals.@object && Database.here(globals, Definitions.BOTTLE)) ||
                globals.@object == Database.liqloc(globals, globals.loc))
                msg = 94;
            else
            {
                return actspk(globals.verb);
            }
            return Database.rspeak(msg);
        }

        /*
            FILL
        */
        public static string vfill(State globals)
        {
            int msg;
            int i;

            switch (globals.@object)
            {
                case Definitions.BOTTLE:
                    if (Database.liq(globals) != 0)
                        msg = 105;
                    else if (Database.liqloc(globals, globals.loc) == 0)
                        msg = 106;
                    else
                    {
                        globals.prop[Definitions.BOTTLE] = globals.cond[globals.loc] & Definitions.WATOIL;
                        i = Database.liq(globals);
                        if (Database.toting(globals, Definitions.BOTTLE))
                            globals.place[i] = -1;
                        msg = (i == Definitions.OIL ? 108 : 107);
                    }
                    break;
                case Definitions.VASE:
                    if (Database.liqloc(globals, globals.loc) == 0)
                    {
                        msg = 144;
                        break;
                    }
                    if (!Database.toting(globals, Definitions.VASE))
                    {
                        msg = 29;
                        break;
                    }
                    string output = Database.rspeak(145);
                    output += Environment.NewLine + vdrop(globals);
                    return output;
                default:
                    msg = 29;
                    break;
            }
            return Database.rspeak(msg);
        }

        /*
            FEED
        */
        public static string vfeed(State globals)
        {
            int msg;

            switch (globals.@object)
            {
                case Definitions.BIRD:
                    msg = 100;
                    break;
                case Definitions.DWARF:
                    if (!Database.here(globals, Definitions.FOOD))
                    {
                        return actspk(globals.verb);
                    }
                    ++globals.dflag;
                    msg = 103;
                    break;
                case Definitions.BEAR:
                    if (!Database.here(globals, Definitions.FOOD))
                    {
                        if (globals.prop[Definitions.BEAR] == 0)
                            msg = 102;
                        else if (globals.prop[Definitions.BEAR] == 3)
                            msg = 110;
                        else
                        {
                            return actspk(globals.verb);
                        }
                        break;
                    }
                    Database.dstroy(globals, Definitions.FOOD);
                    globals.prop[Definitions.BEAR] = 1;
                    globals.@fixed[Definitions.AXE] = 0;
                    globals.prop[Definitions.AXE] = 0;
                    msg = 168;
                    break;
                case Definitions.DRAGON:
                    msg = (globals.prop[Definitions.DRAGON] != 0 ? 110 : 102);
                    break;
                case Definitions.TROLL:
                    msg = 182;
                    break;
                case Definitions.SNAKE:
                    if (globals.closed || !Database.here(globals, Definitions.BIRD))
                    {
                        msg = 102;
                        break;
                    }
                    msg = 101;
                    Database.dstroy(globals, Definitions.BIRD);
                    globals.prop[Definitions.BIRD] = 0;
                    ++globals.tally2;
                    break;
                default:
                    msg = 14;
                    break;
            }

            return Database.rspeak(msg);
        }

        /*
            READ etc.
        */
        public static string vread(State globals)
        {
            int msg;

            msg = 0;
            if (Database.dark(globals))
            {
                return $"I see no {Turn.probj(globals)} here.\n";
            }

            switch (globals.@object)
            {
                case Definitions.MAGAZINE:
                    msg = 190;
                    break;
                case Definitions.TABLET:
                    msg = 196;
                    break;
                case Definitions.MESSAGE:
                    msg = 191;
                    break;
                case Definitions.OYSTER:
                    if (!Database.toting(globals, Definitions.OYSTER) || !globals.closed)
                        break;
                    return Database.rspeak(192);
                default:
                    break;
            }
            if (msg != 0)
                return Database.rspeak(msg);
            else
                return actspk(globals.verb);
        }

        /*
            BLAST etc.
        */
        public static string vblast(State globals)
        {
            if (globals.prop[Definitions.ROD2] < 0 || !globals.closed)
                return actspk(globals.verb);
            else
            {
                globals.bonus = 133;
                if (globals.loc == 115)
                    globals.bonus = 134;
                if (Database.here(globals, Definitions.ROD2))
                    globals.bonus = 135;
                string output = Database.rspeak(globals.bonus);
                return output + Environment.NewLine + Turn.normend(globals);
            }
        }

        /*
            BREAK etc.
        */
        public static string vbreak(State globals)
        {
            int msg;
            if (globals.@object == Definitions.MIRROR)
            {
                msg = 148;
                if (globals.closed)
                {
                    string output = Database.rspeak(197);
                    return $"{output}{Environment.NewLine}{Turn.dwarfend(globals)}";
                }
            }
            else if (globals.@object == Definitions.VASE && globals.prop[Definitions.VASE] == 0)
            {
                msg = 198;
                if (Database.toting(globals, Definitions.VASE))
                    Database.drop(globals, Definitions.VASE, globals.loc);
                globals.prop[Definitions.VASE] = 2;
                globals.@fixed[Definitions.VASE] = -1;
            }
            else
            {
                return actspk(globals.verb);
            }

            return Database.rspeak(msg);
        }

        /*
            WAKE etc.
        */
        public static string vwake(State globals)
        {
            if (globals.@object != Definitions.DWARF || !globals.closed)
                return actspk(globals.verb);
            else
            {
                string output = Database.rspeak(199);
                return output + Environment.NewLine + Turn.dwarfend(globals);
            }
        }

        /*
            Routine to speak default verb message
        */
        public static string actspk(int verb)
        {
            int i;

            if (verb < 1 || verb > 31)
                return Database.bug(39);
            i = State.actmsg[verb];
            if (i != 0)
                return Database.rspeak(i);
            return string.Empty;
        }

        /*
            Routine to indicate no reasonable
            object for verb found.  Used mostly by
            intransitive verbs.
        */
        public static string needobj(State globals)
        {
            int wtype, wval;
            wtype = wval = 0;

            Tuple<bool, string> analysisResult = English.analyze(globals.word1, ref wtype, ref wval);
            return analysisResult.Item2 + string.Format($"{(wtype == 2 ? globals.word1 : globals.word2)} what?\n");
        }

        /*
            Routines to process intransitive verbs
        */
        public static string itverb(State globals)
        {
            switch (globals.verb)
            {
                case Definitions.DROP:
                case Definitions.SAY:
                case Definitions.WAVE:
                case Definitions.CALM:
                case Definitions.RUB:
                case Definitions.THROW:
                case Definitions.FIND:
                case Definitions.FEED:
                case Definitions.BREAK:
                case Definitions.WAKE:
                    return needobj(globals);
                case Definitions.TAKE:
                    return ivtake(globals);
                case Definitions.OPEN:
                case Definitions.LOCK:
                    return ivopen(globals);
                case Definitions.NOTHING:
                    return Database.rspeak(54);
                case Definitions.ON:
                case Definitions.OFF:
                case Definitions.POUR:
                    return trverb(globals);
                case Definitions.WALK:
                    return actspk(globals.verb);
                case Definitions.KILL:
                    return ivkill(globals);
                case Definitions.EAT:
                    return iveat(globals);
                case Definitions.DRINK:
                    return ivdrink(globals);
                case Definitions.QUIT:
                    return Turn.normend(globals);
                case Definitions.FILL:
                    return ivfill(globals);
                case Definitions.BLAST:
                    return vblast(globals);
                case Definitions.SCORE:
                    return Turn.score(globals);
                case Definitions.FOO:
                    return ivfoo(globals);
                case Definitions.INVENTORY:
                    return inventory(globals);
                default:
                    return "This intransitive not implemented yet\n";
            }
        }

        /*
            CARRY, TAKE etc.
        */
        public static string ivtake(State globals)
        {
            int anobj, item;

            anobj = 0;
            for (item = 1; item < Definitions.MAXOBJ; ++item)
            {
                if (globals.place[item] == globals.loc)
                {
                    if (anobj != 0)
                    {
                        return needobj(globals);
                    }
                    anobj = item;
                }
            }
            if (anobj == 0 || (Database.dcheck(globals) > 0 && globals.dflag >= 2))
            {
                return needobj(globals);
            }
            globals.@object = anobj;
            return vtake(globals);
        }

        /*
            OPEN, LOCK, UNLOCK
        */
        public static string ivopen(State globals)
        {
            if (Database.here(globals, Definitions.CLAM))
                globals.@object = Definitions.CLAM;
            if (Database.here(globals, Definitions.OYSTER))
                globals.@object = Definitions.OYSTER;
            if (Database.at(globals, Definitions.DOOR))
                globals.@object = Definitions.DOOR;
            if (Database.at(globals, Definitions.GRATE))
                globals.@object = Definitions.GRATE;
            if (Database.here(globals, Definitions.CHAIN))
            {
                if (globals.@object != 0)
                {
                    return needobj(globals);
                }
                globals.@object = Definitions.CHAIN;
            }
            if (globals.@object == 0)
            {
                return Database.rspeak(28);
            }

            return vopen(globals);
        }

        /*
            ATTACK, KILL etc
        */
        public static string ivkill(State globals)
        {
            globals.object1 = 0;
            if (Database.dcheck(globals) > 0 && globals.dflag >= 2)
                globals.@object = Definitions.DWARF;
            if (Database.here(globals, Definitions.SNAKE))
                addobj(globals, Definitions.SNAKE);
            if (Database.at(globals, Definitions.DRAGON) && globals.prop[Definitions.DRAGON] == 0)
                addobj(globals, Definitions.DRAGON);
            if (Database.at(globals, Definitions.TROLL))
                addobj(globals, Definitions.TROLL);
            if (Database.here(globals, Definitions.BEAR) && globals.prop[Definitions.BEAR] == 0)
                addobj(globals, Definitions.BEAR);
            if (globals.object1 != 0)
            {
                return needobj(globals);
            }
            if (globals.@object != 0)
            {
                return vkill(globals);
            }
            if (Database.here(globals, Definitions.BIRD) && globals.verb != Definitions.THROW)
                globals.@object = Definitions.BIRD;
            if (Database.here(globals, Definitions.CLAM) || Database.here(globals, Definitions.OYSTER))
                addobj(globals, Definitions.CLAM);
            if (globals.object1 != 0)
            {
                return needobj(globals);
            }
            return vkill(globals);
        }

        /*
            EAT
        */
        public static string iveat(State globals)
        {
            if (!Database.here(globals, Definitions.FOOD))
                return needobj(globals);
            else
            {
                globals.@object = Definitions.FOOD;
                return veat(globals);
            }
        }

        /*
            DRINK
        */
        public static string ivdrink(State globals)
        {
            if (Database.liqloc(globals, globals.loc) != Definitions.WATER && (Database.liq(globals) != Definitions.WATER || !Database.here(globals, Definitions.BOTTLE)))
                return needobj(globals);
            else
            {
                globals.@object = Definitions.WATER;
                return vdrink(globals);
            }
        }

        /*
            FILL
        */
        public static string ivfill(State globals)
        {
            if (!Database.here(globals, Definitions.BOTTLE))
                return needobj(globals);
            else
            {
                globals.@object = Definitions.BOTTLE;
                return vfill(globals);
            }
        }

        /*
            Handle fee fie foe foo...
        */
        public static string ivfoo(State globals)
        {
            int k, msg;
            k = Database.vocab(globals.word1, 3000);
            msg = 42;
            if (globals.foobar != 1 - k)
            {
                if (globals.foobar != 0)
                    msg = 151;
                return Database.rspeak(msg);
            }
            globals.foobar = k;
            if (k != 4)
                return string.Empty;
            globals.foobar = 0;
            if (globals.place[Definitions.EGGS] == 92 ||
               (Database.toting(globals, Definitions.EGGS) && globals.loc == 92))
            {
                return Database.rspeak(msg);
            }
            if (globals.place[Definitions.EGGS] == 0 && globals.place[Definitions.TROLL] == 0 &&
                globals.prop[Definitions.TROLL] == 0)
                globals.prop[Definitions.TROLL] = 1;
            if (Database.here(globals, Definitions.EGGS))
                k = 1;
            else if (globals.loc == 92)
                k = 0;
            else
                k = 2;
            Database.move(globals, Definitions.EGGS, 92);
            return Database.pspeak(Definitions.EGGS, k);
        }

        /*
            read etc...
        */
        /*  no room for this...
        ivread()
        {
            if (Database.here(globals, MAGAZINE))
                Globals.@object = MAGAZINE;
            if (Database.here(globals, TABLET))
                Globals.@object = object*100 + TABLET;
            if (Database.here(globals, MESSAGE))
                Globals.@object = object*100 + MESSAGE;
            if (Globals.@object> 100 || Globals.@object == 0 || dark()) {
                needobj(globals);
                return;
            }
            vread();
        }
        */

        /*
            INVENTORY 
        */
        public static string inventory(State globals)
        {
            string output = string.Empty;
            int msg;
            int i;

            msg = 98;
            for (i = 1; i <= Definitions.MAXOBJ; ++i)
            {
                if (i == Definitions.BEAR || !Database.toting(globals, i))
                    continue;
                if (msg != 0)
                    output += Environment.NewLine + Database.rspeak(99);
                msg = 0;
                output += Environment.NewLine + Database.pspeak(i, -1);
            }
            if (Database.toting(globals, Definitions.BEAR))
                msg = 141;
            if (msg != 0)
                output += Environment.NewLine + Database.rspeak(msg);

            return output;
        }

        /*
            ensure uniqueness as objects are searched
            out for an intransitive verb
        */
        public static void addobj(State globals, int obj)
        {
            if (globals.object1 != 0)
                return;
            if (globals.@object != 0)
            {
                globals.object1 = -1;
                return;
            }

            globals.@object = obj;
        }
    }
}