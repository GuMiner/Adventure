using System;
using System.Collections.Generic;

namespace Adventure
{
    internal class Turn
    {
        /*
        Routine to take 1 turn
    */
        public static string turn(State globals, string nextLine)
        {
            string output = string.Empty;
            if (globals.IfFirstRun)
            {
                /*
                        if globals.closing, then he can't leave except via
                        the main office.
                    */
                if (globals.newloc < 9 && globals.newloc != 0 && globals.closing)
                {
                    output += Database.rspeak(130);
                    globals.newloc = globals.loc;
                    if (globals.panic != 0)
                        globals.clock2 = 15;
                    globals.panic = 1;
                }
                /*
                    see if a Definitions.DWARF has seen him and has come
                    from where he wants to go.
                */
                if (globals.newloc != globals.loc && !Database.forced(globals, globals.loc) && (globals.cond[globals.loc] & Definitions.NOPIRAT) == 0)
                    for (int i = 1; i < (Definitions.DWARFMAX - 1); ++i)
                        if (globals.odloc[i] == globals.newloc && globals.dseen[i])
                        {
                            globals.newloc = globals.loc;
                            output += Database.rspeak(2);
                            break;
                        }

                output += dwarves(globals);  /* & special Definitions.DWARF(pirate who steals)	*/

                /* added by BDS C conversion */
                if (globals.loc != globals.newloc)
                {
                    ++globals.turns;
                    globals.loc = globals.newloc;
                    /*	causes occasional "move" with two describe & descitem	*/
                    /*	}	*/            /* if (loc != globals.newloc)	*/

                    /* check for death */
                    if (globals.loc == 0)
                    {
                        output += death(globals);
                        return output;
                    }

                    /* check for forced move */
                    if (Database.forced(globals, globals.loc))
                    {
                        output += describe(globals);
                        output += domove(globals);
                        return output;
                    }

                    /* check for wandering in dark */
                    if (globals.wzdark && Database.dark(globals) && Database.pct(35))
                    {
                        output += Database.rspeak(23);
                        globals.oldloc2 = globals.loc;
                        output += death(globals);
                        return output;
                    }

                    /* describe his situation */
                    output += describe(globals);
                    if (!Database.dark(globals))
                    {
                        ++globals.visited[globals.loc];
                        output += descitem(globals);
                    }
                    /*	causes occasional "move" with no describe & descitem	*/
                }               /* if (loc != globals.newloc)	*/

                if (globals.closed)
                {
                    if (globals.prop[Definitions.OYSTER] < 0 && Database.toting(globals, Definitions.OYSTER))
                        output += Database.pspeak(Definitions.OYSTER, 1);
                    for (int i = 1; i <= Definitions.MAXOBJ; ++i)
                        if (Database.toting(globals, i) && globals.prop[i] < 0)
                            globals.prop[i] = -1 - globals.prop[i];
                }

                globals.wzdark = Database.dark(globals);
                if (globals.knfloc > 0 && globals.knfloc != globals.loc)
                    globals.knfloc = 0;

                Tuple<bool, string> timerResult = stimer(globals);
                output += timerResult.Item2;
                if (timerResult.Item1)   /* as the grains of sand slip by	*/
                    return output;


                globals.IfFirstRun = false;
            }
            
            if (nextLine.Contains("exit") || nextLine.Contains("quit"))
            {
                globals.IfFirstRun = true;
                globals.Initialized = false;
                return "Restarting. Please enter a message to continue.";
            }

            Tuple<bool, string> lastParsedInstruction = English.english(globals, nextLine);  /* retrieve player instructions	*/
            globals.LastLineSuccessful = lastParsedInstruction.Item1;

            if (globals.LastLineSuccessful)
            {
                if ((globals.motion != 0))     /* execute player instructions	*/
                {
                    output += domove(globals);
                }
                else if (globals.@object != 0)
                    output += doobj(globals);
                else
                    output += Verb.itverb(globals);

                /*
                    if globals.closing, then he can't leave except via
                    the main office.
                */
                if (globals.newloc < 9 && globals.newloc != 0 && globals.closing)
                {
                    output += Database.rspeak(130);
                    globals.newloc = globals.loc;
                    if (globals.panic != 0)
                        globals.clock2 = 15;
                    globals.panic = 1;
                }
                /*
                    see if a Definitions.DWARF has seen him and has come
                    from where he wants to go.
                */
                if (globals.newloc != globals.loc && !Database.forced(globals, globals.loc) && (globals.cond[globals.loc] & Definitions.NOPIRAT) == 0)
                    for (int i = 1; i < (Definitions.DWARFMAX - 1); ++i)
                        if (globals.odloc[i] == globals.newloc && globals.dseen[i])
                        {
                            globals.newloc = globals.loc;
                            output += Database.rspeak(2);
                            break;
                        }

                output += dwarves(globals);  /* & special Definitions.DWARF(pirate who steals)	*/

                /* added by BDS C conversion */
                if (globals.loc != globals.newloc)
                {
                    ++globals.turns;
                    globals.loc = globals.newloc;
                    /*	causes occasional "move" with two describe & descitem	*/
                    /*	}	*/            /* if (loc != globals.newloc)	*/

                    /* check for death */
                    if (globals.loc == 0)
                    {
                        output += death(globals);
                        return output;
                    }

                    /* check for forced move */
                    if (Database.forced(globals, globals.loc))
                    {
                        output += describe(globals);
                        output += domove(globals);
                        return output;
                    }

                    /* check for wandering in dark */
                    if (globals.wzdark && Database.dark(globals) && Database.pct(35))
                    {
                        output += Database.rspeak(23);
                        globals.oldloc2 = globals.loc;
                        output += death(globals);
                        return output;
                    }

                    /* describe his situation */
                    output += describe(globals);
                    if (!Database.dark(globals))
                    {
                        ++globals.visited[globals.loc];
                        output += descitem(globals);
                    }
                    /*	causes occasional "move" with no describe & descitem	*/
                }               /* if (loc != globals.newloc)	*/

                if (globals.closed)
                {
                    if (globals.prop[Definitions.OYSTER] < 0 && Database.toting(globals, Definitions.OYSTER))
                        output += Database.pspeak(Definitions.OYSTER, 1);
                    for (int i = 1; i <= Definitions.MAXOBJ; ++i)
                        if (Database.toting(globals, i) && globals.prop[i] < 0)
                            globals.prop[i] = -1 - globals.prop[i];
                }

                globals.wzdark = Database.dark(globals);
                if (globals.knfloc > 0 && globals.knfloc != globals.loc)
                    globals.knfloc = 0;

                Tuple<bool, string> timerResult = stimer(globals);
                output += timerResult.Item2;
                if (timerResult.Item1)   /* as the grains of sand slip by	*/
                    return output;
            }
            
            return output;
        }

        /*
            Routine to describe current Globals.location
        */
        public static string describe(State globals)
        {
            string result = string.Empty;
            if (Database.toting(globals, Definitions.BEAR))
                result += Database.rspeak(141);

            if (Database.dark(globals))
                result += Database.rspeak(16);
            else if (globals.visited[globals.loc] > 0)
                result += Database.descsh(globals.loc);
            else
                result += Database.desclg(globals.loc);
            if (globals.loc == 33 && Database.pct(25) && !globals.closing)
                result += Database.rspeak(8);

            return result;
        }

        /*
            Routine to describe visible items
        */
        static string descitem(State globals)
        {
            int i, state;
            List<string> items = new List<string>();
            for (i = 1; i < Definitions.MAXOBJ; ++i)
            {
                if (Database.at(globals, i))
                {
                    if (i == Definitions.STEPS && Database.toting(globals, Definitions.NUGGET))
                        continue;
                    if (globals.prop[i] < 0)
                    {
                        if (globals.closed)
                            continue;
                        else
                        {
                            globals.prop[i] = 0;
                            if (i == Definitions.RUG || i == Definitions.CHAIN)
                                ++globals.prop[i];
                            --globals.tally;
                        }
                    }
                    if (i == Definitions.STEPS && globals.loc == globals.@fixed[Definitions.STEPS])
                        state = 1;
                    else
                        state = globals.prop[i];
                    items.Add(Database.pspeak(i, state));
                }
            }
            if (globals.tally == globals.tally2 && globals.tally != 0 && globals.limit > 35)
                globals.limit = 35;
            return string.Join("\n", items);
        }

        /*
            Routine to handle (globals.motion) requests
        */
        static string domove(State globals)
        {
            string result = string.Empty;
            result += Database.gettrav(globals, globals.loc);
            switch ((globals.motion))
            {
                case Definitions.NULLX:
                    break;
                case Definitions.BACK:
                    result += goback(globals);
                    break;
                case Definitions.LOOK:
                    globals.wzdark = false;
                    globals.visited[globals.loc] = 0;
                    globals.newloc = globals.loc;
                    globals.loc = 0;
                    break;
                case Definitions.CAVE:
                    if (globals.loc < 8)
                        result += Database.rspeak(57);
                    else
                        result += Database.rspeak(58);
                    break;
                default:
                    globals.oldloc2 = globals.oldloc;
                    globals.oldloc = globals.loc;
                    result += dotrav(globals);
                    break;
            }

            return result;
        }

        /*
            Routine to handle request to return
            from whence we came!
        */
        static string goback(State globals)
        {
            int kk, k2, want, temp;
            string output = string.Empty;
            Definitions.trav[] strav = new Definitions.trav[Definitions.MAXTRAV];

            if (Database.forced(globals, globals.oldloc))
                want = globals.oldloc2;
            else
                want = globals.oldloc;
            globals.oldloc2 = globals.oldloc;
            globals.oldloc = globals.loc;
            k2 = 0;
            if (want == globals.loc)
            {
                return Database.rspeak(91);
            }

            for (int i = 0; i < Definitions.MAXTRAV; ++i)
            {
                strav[i].tdest = globals.travel[i].tdest;
                strav[i].tverb = globals.travel[i].tverb;
                strav[i].tcond = globals.travel[i].tcond;
            }

            for (kk = 0; globals.travel[kk].tdest != -1; ++kk)
            {
                if (globals.travel[kk].tcond == 0 && globals.travel[kk].tdest == want)
                {
                    (globals.motion) = globals.travel[kk].tverb;

                    return dotrav(globals);
                }
                if (globals.travel[kk].tcond == 0)
                {
                    k2 = kk;
                    temp = globals.travel[kk].tdest;

                    output += Database.gettrav(globals, temp);
                    if (Database.forced(globals, temp) && globals.travel[0].tdest == want)
                        k2 = temp;
                        
                    for (int i = 0; i < Definitions.MAXTRAV; ++i)
                    {
                        globals.travel[i].tdest = strav[i].tdest;
                        globals.travel[i].tverb = strav[i].tverb;
                        globals.travel[i].tcond = strav[i].tcond;
                    }
                }
            }

            if (k2 != 0)
            {
                (globals.motion) = globals.travel[k2].tverb;
                return output + dotrav(globals);
            }
            else
                return output + Database.rspeak(140);
        }


        /*
            Routine to figure out a new globals.location
            given current globals.location and a (globals.motion).
        */
        public static string dotrav(State globals)
        {
            int mvflag, hitflag;
            int rdest, rverb, rcond, robject;
            int pctt;
            string output = string.Empty;

            rdest = 0;
            globals.newloc = globals.loc;
            mvflag = hitflag = 0;
            pctt = new Random().Next() % 100;

            for (int kk = 0; globals.travel[kk].tdest >= 0 && mvflag == 0; ++kk)
            {
                rdest = globals.travel[kk].tdest;
                rverb = globals.travel[kk].tverb;
                rcond = globals.travel[kk].tcond;
                robject = rcond % 100;

                if ((rverb != 1) && (rverb != (globals.motion)) && hitflag == 0)
                    continue;
                ++hitflag;
                switch (rcond / 100)
                {
                    case 0:
                        if ((rcond == 0) || (pctt < rcond))
                            ++mvflag;
                        break;
                    case 1:
                        if (robject == 0)
                            ++mvflag;
                        else if (Database.toting(globals, robject))
                            ++mvflag;
                        break;
                    case 2:
                        if (Database.toting(globals, robject) || Database.at(globals, robject))
                            ++mvflag;
                        break;
                    case 3:
                    case 4:
                    case 5:
                    case 7:
                        if (globals.prop[robject] != (rcond / 100) - 3)
                            ++mvflag;
                        break;
                    default:
                        output += Database.bug(37);
                        break;
                }
            }
            if (mvflag == 0)
                return output + badmove(globals);
            else if (rdest > 500)
                return output + Database.rspeak(rdest - 500);
            else if (rdest > 300)
                return output + spcmove(globals, rdest);
            else
            {
                globals.newloc = rdest;
                return output;
            }
        }

        /*
            The player tried a poor move option.
        */
        public static string badmove(State globals)
        {
            int msg;

            msg = 12;
            if ((globals.motion) >= 43 && (globals.motion) <= 50) msg = 9;
            if ((globals.motion) == 29 || (globals.motion) == 30) msg = 9;
            if ((globals.motion) == 7 || (globals.motion) == 36 || (globals.motion) == 37) msg = 10;
            if ((globals.motion) == 11 || (globals.motion) == 19) msg = 11;
            if (globals.verb == Definitions.FIND || globals.verb == Definitions.INVENTORY) msg = 59;
            if ((globals.motion) == 62 || (globals.motion) == 65) msg = 42;
            if ((globals.motion) == 17) msg = 80;
            return Database.rspeak(msg);
        }

        /*
            Routine to handle very special movement.
        */
        public static string spcmove(State globals, int rdest)
        {
            string output = string.Empty;
            switch (rdest - 300)
            {
                case 1:  /* plover movement via alcove */
                    if (globals.holding != 0 || (globals.holding == 1 && Database.toting(globals, Definitions.EMERALD)))
                        globals.newloc = (99 + 100) - globals.loc;
                    else
                        return Database.rspeak(117);
                    break;
                case 2:  /* trying to remove plover, bad route */

                    Database.drop(globals, Definitions.EMERALD, globals.loc);
                    break;
                case 3:  /* troll bridge */
                    if (globals.prop[Definitions.TROLL] == 1)
                    {
                        output += Database.pspeak(Definitions.TROLL, 1);
                        globals.prop[Definitions.TROLL] = 0;

                        Database.move(globals, Definitions.TROLL2, 0);
                        Database.move(globals, (Definitions.TROLL2 + Definitions.MAXOBJ), 0);
                        Database.move(globals, Definitions.TROLL, 117);
                        Database.move(globals, (Definitions.TROLL + Definitions.MAXOBJ), 122);
                        Database.juggle(Definitions.CHASM);
                        globals.newloc = globals.loc;
                    }
                    else
                    {
                        globals.newloc = (globals.loc == 117 ? 122 : 117);
                        if (globals.prop[Definitions.TROLL] == 0)
                            ++globals.prop[Definitions.TROLL];
                        if (!Database.toting(globals, Definitions.BEAR))
                            return string.Empty;

                        output += Database.rspeak(162);
                        globals.prop[Definitions.CHASM] = 1;
                        globals.prop[Definitions.TROLL] = 2;

                        Database.drop(globals, Definitions.BEAR, globals.newloc);
                        globals.@fixed[Definitions.BEAR] = -1;
                        globals.prop[Definitions.BEAR] = 3;
                        if (globals.prop[Definitions.SPICES] < 0)
                            ++globals.tally2;
                        globals.oldloc2 = globals.newloc;

                        output += death(globals);
                    }
                    break;
                default:
                    output += Database.bug(38);
                    break;
            }

            return output;
        }


        /*
            Routine to handle player's demise via
            waking up the dwarves...
        */
        public static string dwarfend(State globals)
        {
            string output = string.Empty;
            output += death(globals);
            output += normend(globals);
            return output;
        }

        /*
            normal end of game
        */
        public static string normend(State globals)
        {
            globals.Initialized = false;
            globals.IfFirstRun = true;
            globals.LastLineSuccessful = false;
            return score(globals);
        }

        /*
            scoring
        */
        public static string score(State globals)
        {
            string output = string.Empty;
            int t, i, k, s;
            s = t = k = 0;
            for (i = 50; i <= Definitions.MAXTRS; ++i)
            {
                if (i == Definitions.CHEST)
                    k = 14;
                else if (i > Definitions.CHEST)
                    k = 16;
                else
                    k = 12;
                if (globals.prop[i] >= 0)
                    t += 2;
                if (globals.place[i] == 3 && globals.prop[i] == 0)
                    t += k - 2;
            }

            output += $"Treasures: {s=t}" + Environment.NewLine;
            s += t;
            t = globals.dflag > 0 ? 25 : 0;
            if (t > 0)
                output += ($"Getting well in: {t}" + Environment.NewLine);
            s += t;
            t = globals.closing ? 25 : 0;
            if (t > 0)
                output += ($"Masters section: {t}" + Environment.NewLine);
            s += t;
            if (globals.closed)
            {
                if (globals.bonus == 0)
                    t = 10;
                else if (globals.bonus == 135)
                    t = 25;
                else if (globals.bonus == 134)
                    t = 30;
                else if (globals.bonus == 133)
                    t = 45;
                output += ($"Bonus: {t}" + Environment.NewLine);
                s += t;
            }
            if (globals.place[Definitions.MAGAZINE] == 108)
                s += 1;
            s += 2;
            output += ($"Score: {s}" + Environment.NewLine);

            return output;
        }

        /*
            Routine to handle the passing on of one
            of the player's incarnations...
        */
        public static string death(State globals)
        {
            /*
               globals.closing -- no resurrection...
            */
            string output = Database.rspeak(131);
            output += normend(globals);
            return output;
        }

        /*
            Routine to process an object.
        */
        public static string doobj(State globals)
        {
            string output = string.Empty;

            /*
               is object here?  if so, transitive
            */
            if (globals.@fixed[globals.@object] == globals.loc || Database.here(globals, globals.@object))
                output += trobj(globals);
            /*
                did he give Definitions.GRATE as destination?
            */
            else if (globals.@object == Definitions.GRATE)
            {
                if (globals.loc == 1 || globals.loc == 4 || globals.loc == 7)
                {
                    (globals.motion) = Definitions.DEPRESSION;
                    output += domove(globals);
                }
                else if (globals.loc > 9 && globals.loc < 15)
                {
                    (globals.motion) = Definitions.ENTRANCE;
                    output += domove(globals);
                }
            }
            /*
                is it a Definitions.DWARF he is after?
            */
            else if (Database.dcheck(globals) > 0 && globals.dflag >= 2)
            {
                globals.@object = Definitions.DWARF;
                output += trobj(globals);
            }
            /*
               is he trying to get/use a liquid?
            */
            else if ((Database.liq(globals) == globals.@object && Database.here(globals, Definitions.BOTTLE)) || Database.liqloc(globals, globals.loc) == globals.@object)
                output += trobj(globals);
            else if (globals.@object == Definitions.PLANT && Database.at(globals, Definitions.PLANT2) && globals.prop[Definitions.PLANT2] == 0)
            {
                globals.@object = Definitions.PLANT2;
                output += trobj(globals);
            }
            /*
               is he trying to grab a knife?
            */
            else if (globals.@object == Definitions.KNIFE && globals.knfloc == globals.loc)
            {
                output += Database.rspeak(116);
                globals.knfloc = -1;
            }
            /*
               is he trying to get at dynamite?
            */
            else if (globals.@object == Definitions.ROD && Database.here(globals, Definitions.ROD2))
            {
                globals.@object = Definitions.ROD2;
                output += trobj(globals);
            }
            else
                output += $"I see no {probj(globals)} here.";

            return output;
        }

        /*
            Routine to process an object being
            referred to.
        */
        public static string trobj(State globals)
        {
            if (globals.verb != 0)
                return Verb.trverb(globals);
            else
                return ($"What do you want to do with the {probj(globals)}?");
        }

        /*
            Routine to print word corresponding to object
        */
        public static string probj(State globals)
        {
            int wtype, wval;
            wtype = wval = 0;
            Tuple<bool, string> analysisResult = English.analyze(globals.word1, ref wtype, ref wval);
            return analysisResult.Item2 + Environment.NewLine + (wtype == 1 ? globals.word1 : globals.word2);
        }
        /*
            Definitions.DWARF stuff.
        */
        public static string dwarves(State globals)
        {
            string output = string.Empty;
            int i, j, k, @try, attack, stick, dtotal;
            j = 0;
            /*
                see if dwarves allowed here
            */
            if (globals.newloc == 0 || Database.forced(globals, globals.newloc) || (globals.cond[globals.newloc] & Definitions.NOPIRAT) != 0)
                return output;
            /*
                see if dwarves are active.
            */
            if (globals.dflag == 0)
            {
                if (globals.newloc > 15)
                    ++globals.dflag;
                return output;
            }
            /*
                if first close encounter (of 3rd kind)
                kill 0, 1 or 2
            */
            if (globals.dflag == 1)
            {
                if (globals.newloc < 15 || Database.pct(95))
                    return output;
                ++globals.dflag;
                for (i = 1; i < 3; ++i)
                    if (Database.pct(50))
                        globals.dloc[new Random().Next() % 5 + 1] = 0;
                for (i = 1; i < (Definitions.DWARFMAX - 1); ++i)
                {
                    if (globals.dloc[i] == globals.newloc)
                        globals.dloc[i] = globals.daltloc;
                    globals.odloc[i] = globals.dloc[i];
                }
                output += Database.rspeak(3);
                Database.drop(globals, Definitions.AXE, globals.newloc);
                return output;
            }

            dtotal = attack = stick = 0;
            for (i = 1; i < Definitions.DWARFMAX; ++i)
            {
                if (globals.dloc[i] == 0)
                    continue;
                /*
                    move a Definitions.DWARF at random.  we don't
                    have a matrix around to do it
                    as in the original version...
                */
                for (@try = 1; @try < 20; ++@try)
                {
                    j = new Random().Next() % 106 + 15; /* allowed area */
                    if (j != globals.odloc[i] && j != globals.dloc[i] &&
                        !(i == (Definitions.DWARFMAX - 1) && (globals.cond[j] & Definitions.NOPIRAT) == 1))
                        break;
                }
                if (j == 0)
                    j = globals.odloc[i];
                globals.odloc[i] = globals.dloc[i];
                globals.dloc[i] = j;
                if ((globals.dseen[i] && globals.newloc >= 15) ||
                    globals.dloc[i] == globals.newloc || globals.odloc[i] == globals.newloc)
                    globals.dseen[i] = true;
                else
                    globals.dseen[i] = false;
                if (!globals.dseen[i])
                    continue;
                globals.dloc[i] = globals.newloc;
                if (i == 6)
                    output += dopirate(globals);
                else
                {
                    ++dtotal;
                    if (globals.odloc[i] == globals.dloc[i])
                    {
                        ++attack;
                        if (globals.knfloc >= 0)
                            globals.knfloc = globals.newloc;
                        if (new Random().Next() % 1000 < 95 * (globals.dflag - 2))
                            ++stick;
                    }
                }
            }

            if (dtotal == 0)
                return output;
            if (dtotal > 1)
                output += $"There are {dtotal} threatening little dwarves in the room with you!\n";
            else
                output += Database.rspeak(4);
            if (attack == 0)
                return output;
            if (globals.dflag == 2)
                ++globals.dflag;
            if (attack > 1)
            {
                output += $"{attack} of them throw knives at you!!\n";
                k = 6;
            }
            else
            {
                output += Database.rspeak(5);
                k = 52;
            }
            if (stick <= 1)
            {
                output += Database.rspeak(stick + k);
                if (stick == 0)
                    return output;
            }
            else
                output += $"{stick} of them get you !!!\n";
            globals.oldloc2 = globals.newloc;
            output += death(globals);
            return output;
        }
        /*
            pirate stuff
        */
        public static string dopirate(State globals)
        {
            string output = string.Empty;
            int j, k;
            if (globals.newloc == globals.chloc || globals.prop[Definitions.CHEST] >= 0)
                return string.Empty;
            k = 0;
            for (j = 50; j <= Definitions.MAXTRS; ++j)
                if (j != Definitions.PYRAMID ||
                    (globals.newloc != globals.place[Definitions.PYRAMID] &&
                     globals.newloc != globals.place[Definitions.EMERALD]))
                {
                    if (Database.toting(globals, j))
                        goto stealit;
                    if (Database.here(globals, j))
                        ++k;
                }
            if (globals.tally == globals.tally2 + 1 && k == 0 && globals.place[Definitions.CHEST] == 0 &&
                Database.here(globals, Definitions.LAMP) && globals.prop[Definitions.LAMP] == 1)
            {
                output += Database.rspeak(186);
                Database.move(globals, Definitions.CHEST, globals.chloc);
                Database.move(globals, Definitions.MESSAGE, globals.chloc2);
                globals.dloc[6] = globals.chloc;
                globals.odloc[6] = globals.chloc;
                globals.dseen[6] = false;
                return output;
            }
            if (globals.odloc[6] != globals.dloc[6] && Database.pct(20))
            {
                output += Database.rspeak(127);
                return output;
            }

            return output;

            stealit:

            output += Database.rspeak(128);
            if (globals.place[Definitions.MESSAGE] == 0)
                Database.move(globals, Definitions.CHEST, globals.chloc);
            Database.move(globals, Definitions.MESSAGE, globals.chloc2);
            for (j = 50; j <= Definitions.MAXTRS; ++j)
            {
                if (j == Definitions.PYRAMID &&
                    (globals.newloc == globals.place[Definitions.PYRAMID] ||
                     globals.newloc == globals.place[Definitions.EMERALD]))
                    continue;
                if (Database.at(globals, j) && globals.@fixed[j] == 0)
                    Database.carry(globals, j, globals.newloc);
                if (Database.toting(globals, j))
                    Database.drop(globals, j, globals.chloc);
            }
            globals.dloc[6] = globals.chloc;
            globals.odloc[6] = globals.chloc;
            globals.dseen[6] = false;
            return output;
        }

        /*
            special time globals.limit stuff...
        */
        public static Tuple<bool, string> stimer(State globals)
        {
            string output = string.Empty;
            int i;
            globals.foobar = globals.foobar > 0 ? -globals.foobar : 0;
            if (globals.tally == 0 && globals.loc >= 15 && globals.loc != 33)
                --globals.clock;
            if (globals.clock == 0)
            {
                /*
                    start globals.closing the cave
                */
                globals.prop[Definitions.GRATE] = 0;
                globals.prop[Definitions.FISSURE] = 0;
                for (i = 1; i < Definitions.DWARFMAX; ++i)
                    globals.dseen[i] = false;
                Database.move(globals, Definitions.TROLL, 0);
                Database.move(globals, (Definitions.TROLL + Definitions.MAXOBJ), 0);
                Database.move(globals, Definitions.TROLL2, 117);
                Database.move(globals, (Definitions.TROLL2 + Definitions.MAXOBJ), 122);
                Database.juggle(Definitions.CHASM);
                if (globals.prop[Definitions.BEAR] != 3)
                    Database.dstroy(globals, Definitions.BEAR);
                globals.prop[Definitions.CHAIN] = 0;
                globals.@fixed[Definitions.CHAIN] = 0;
                globals.prop[Definitions.AXE] = 0;
                globals.@fixed[Definitions.AXE] = 0;
                output += Database.rspeak(129);
                globals.clock = -1;
                globals.closing = true;
                return Tuple.Create(false, output);
            }
            if (globals.clock < 0)
                --globals.clock2;
            if (globals.clock2 == 0)
            {
                /*
                    set up storage room...
                    and close the cave...
                */
                globals.prop[Definitions.BOTTLE] = Database.put(globals, Definitions.BOTTLE, 115, 1);
                globals.prop[Definitions.PLANT] =  Database.put(globals, Definitions.PLANT, 115, 0);
                globals.prop[Definitions.OYSTER] = Database.put(globals, Definitions.OYSTER, 115, 0);
                globals.prop[Definitions.LAMP] =   Database.put(globals, Definitions.LAMP, 115, 0);
                globals.prop[Definitions.ROD] =    Database.put(globals, Definitions.ROD, 115, 0);
                globals.prop[Definitions.DWARF] =  Database.put(globals, Definitions.DWARF, 115, 0);
                globals.loc = 115;
                globals.oldloc = 115;
                globals.newloc = 115;
                Database.put(globals, Definitions.GRATE, 116, 0);
                globals.prop[Definitions.SNAKE] =  Database.put(globals, Definitions.SNAKE, 116, 1);
                globals.prop[Definitions.BIRD] =   Database.put(globals, Definitions.BIRD, 116, 1);
                globals.prop[Definitions.CAGE] =   Database.put(globals, Definitions.CAGE, 116, 0);
                globals.prop[Definitions.ROD2] =   Database.put(globals, Definitions.ROD2, 116, 0);
                globals.prop[Definitions.PILLOW] = Database.put(globals, Definitions.PILLOW, 116, 0);
                globals.prop[Definitions.MIRROR] = Database.put(globals, Definitions.MIRROR, 115, 0);
                globals.@fixed[Definitions.MIRROR] = 116;
                for (i = 1; i <= Definitions.MAXOBJ; ++i)
                    if (Database.toting(globals, i))
                        Database.dstroy(globals, i);
                output += Database.rspeak(132);
                globals.closed = true;
                return Tuple.Create(true, output);
            }
            if (globals.prop[Definitions.LAMP] == 1)
                --globals.limit;
            if (globals.limit <= 30 &&
                Database.here(globals, Definitions.BATTERIES) && globals.prop[Definitions.BATTERIES] == 0 &&
                Database.here(globals, Definitions.LAMP))
            {
                output += Database.rspeak(188);
                globals.prop[Definitions.BATTERIES] = 1;
                if (Database.toting(globals, Definitions.BATTERIES))
                    Database.drop(globals, Definitions.BATTERIES, globals.loc);
                globals.limit += 2500;
                globals.lmwarn = 0;
                return Tuple.Create(false, output);
            }
            if (globals.limit == 0)
            {
                --globals.limit;
                globals.prop[Definitions.LAMP] = 0;
                if (Database.here(globals, Definitions.LAMP))
                    output += Database.rspeak(184);
                return Tuple.Create(false, output);
            }
            if (globals.limit < 0 && globals.loc <= 8)
            {
                output += Database.rspeak(185);
                normend(globals);
            }
            if (globals.limit <= 30)
            {
                if (globals.lmwarn != 0 || !Database.here(globals, Definitions.LAMP))
                    return Tuple.Create(false, output);
                globals.lmwarn = 1;
                i = 187;
                if (globals.place[Definitions.BATTERIES] == 0)
                    i = 183;
                if (globals.prop[Definitions.BATTERIES] == 1)
                    i = 189;
                output += Database.rspeak(i);
                return Tuple.Create(false, output);
            }
            return Tuple.Create(false, output);
        }
    }
}