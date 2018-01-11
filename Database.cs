using System;
using System.Linq;

namespace Adventure
{
    internal class Database
    {
        /*
            Routine to fill travel array for a given location
        */
        public static string gettrav(State globals, int loc)
        {
            long[] travelSpots = State.cave[loc - 1].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(item => Convert.ToInt64(item)).ToArray();
            for (int i = 0; i < Definitions.MAXTRAV; ++i)
            {
                if (i >= travelSpots.Length)
                {
                    globals.travel[i].tdest = -1;
                    return string.Empty;
                }
                long value = travelSpots[i];
                globals.travel[i].tcond = (int)(value % 1000L);
                value /= 1000L;
                globals.travel[i].tverb = (int)(value % 1000L);
                value /= 1000L;
                globals.travel[i].tdest = (int)(value % 1000L);
            }

            return Database.bug(33);
        }

        /*
            Print a location description from "advent4.txt"
        */
        public static string rspeak(int msg)
        {
            return State.fd4[msg];
        }

        /*
            Print an item message for a given state from "advent3.txt"
        */
        public static string pspeak(int item, int state)
        {
            return State.fd3[item].Split('/')[(state + 2) % 256]; // This code used to rely on overflow. That's nasty.
        }

        /*
            Print a long location description from "advent1.txt"
        */
        public static string desclg(int loc)
        {
            return State.fd1[loc];
        }

        /*
            Print a short location description from "advent2.txt"
        */
        public static string descsh(int loc)
        {
            return State.fd2[loc];
        }

        /*
            look-up vocabulary word in lex-ordered table.  words may have
            two entries with different codes. if minimum acceptable value
            = 0, then return minimum of different codes.  last word CANNOT
            have two entries(due to binary sort).
            word is the word to look up.
            val  is the minimum acceptable value,
                if != 0 return %1000
        */
        public static int vocab(string word, int val)
        {
            int v1, v2;

            if ((v1 = binary(word, State.wc, true)) >= 0)
            {
                v2 = binary(word, State.wc, false);
                if (v2 < 0)
                    v2 = v1;
                if (val == 0)
                    return (State.wc[v1].acode < State.wc[v2].acode
                           ? State.wc[v1].acode : State.wc[v2].acode);
                if (val <= State.wc[v1].acode)
                    return (State.wc[v1].acode % 1000);
                else if (val <= State.wc[v2].acode)
                    return (State.wc[v2].acode % 1000);
                else
                    return (-1);
            }
            else
                return (-1);
        }

        public static int binary(string w, Definitions.wac[] wctable, bool front)
        {
            // To avoid messiness in converting C to C#, this is no longer a binary search.
            if (front)
            {
                for (int i = 0; i < wctable.Length; i++)
                {
                    if (wctable[i].aword.Equals(w, StringComparison.OrdinalIgnoreCase))
                    {
                        return i;
                    }
                }

                return -1;
            }
            else
            {
                for (int i = wctable.Length - 1; i >= 0; i--)
                {
                    if (wctable[i].aword.Equals(w, StringComparison.OrdinalIgnoreCase))
                    {
                        return i;
                    }
                }

                return -1;
            }
        }


        /*
            Utility Routines
        */

        /*
            Routine to test for darkness
        */
        public static bool dark(State globals)
        {
            return ((globals.cond[globals.loc] & Definitions.LIGHT) != 0 &&
                (globals.prop[Definitions.LAMP] != 0 ||
                !here(globals, Definitions.LAMP)));
        }

        /*
            Routine to tell if an item is present.
        */
        public static bool here(State globals, int item)
        {
            return (globals.place[item] == globals.loc || toting(globals, item));
        }

        /*
            Routine to tell if an item is being carried.
        */
        public static bool toting(State globals, int item)
        {
            return (globals.place[item] == -1);
        }

        /*
            Routine to tell if a location causes
            a forced move.
        */
        public static bool forced(State globals, int atloc)
        {
            return (globals.cond[atloc] == 2);
        }

        /*
            Routine true x% of the time.
        */
        public static bool pct(int x)
        {
            return (new Random().Next() % 100 < x);
        }

        /*
            Routine to tell if player is on
            either side of a two sided object.
        */
        public static bool at(State globals, int item)
        {
            return (globals.place[item] == globals.loc || globals.@fixed[item] == globals.loc);
        }

        /*
            Routine to destroy an object
        */
        public static void dstroy(State globals, int obj)
        {
            move(globals, obj, 0);
        }

        /*
            Routine to move an object
        */
        public static void move(State globals, int obj, int where)
        {
            int from;

            from = (obj < Definitions.MAXOBJ) ? globals.place[obj] : globals.@fixed[obj];
            if (from > 0 && from <= 300)
                carry(globals, obj, from);
            drop(globals, obj, where);
        }

        /*
            Juggle an object
            currently a no-op
        */
        public static void juggle(int loc)
        {
        }

        /*
            Routine to carry an object
        */
        public static void carry(State globals, int obj, int where)
        {
            if (obj < Definitions.MAXOBJ)
            {
                if (globals.place[obj] == -1)
                    return;
                globals.place[obj] = -1;
                ++globals.holding;
            }
        }

        /*
            Routine to drop an object
        */
        public static void drop(State globals, int obj, int where)
        {
            if (obj < Definitions.MAXOBJ)
            {
                if (globals.place[obj] == -1)
                    --globals.holding;
                globals.place[obj] = where;
            }
            else
                globals.@fixed[obj - Definitions.MAXOBJ] = where;
        }

        /*
            routine to move an object and return a
            value used to set the negated prop values
            for the repository.
        */
        public static int put(State globals, int obj, int where, int pval)
        {
            move(globals, obj, where);
            return ((-1) - pval);
        }

        /*
            Routine to check for presence
            of dwarves..
        */
        public static int dcheck(State globals)
        {
            int i;

            for (i = 1; i < (Definitions.DWARFMAX - 1); ++i)
                if (globals.dloc[i] == globals.loc)
                    return (i);
            return (0);
        }

        /*
            Determine liquid in the bottle
        */
        public static int liq(State globals)
        {
            int i, j;
            i = globals.prop[Definitions.BOTTLE];
            j = -1 - i;
            return (liq2(i > j ? i : j));
        }

        /*
            Determine liquid at a location
        */
        public static int liqloc(State globals, int loc)
        {
            if ((globals.cond[loc] & Definitions.LIQUID) != 0)
                return (liq2(globals.cond[loc] & Definitions.WATOIL));
            else
                return (liq2(1));
        }

        /*
            Convert  0 to WATER
                 1 to nothing
                 2 to OIL
        */
        public static int liq2(int pbottle)
        {
            return ((1 - pbottle) * Definitions.WATER + (pbottle >> 1) * (Definitions.WATER + Definitions.OIL));
        }

        /*
            Fatal error routine
        */
        public static string bug(int n)
        {
            return string.Format("Fatal error number %d\n Unfortunately, the game will restart\n", n);
        }
    }
}