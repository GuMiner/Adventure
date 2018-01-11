using System;
using System.Linq;

namespace Adventure
{
    internal class English
    {
        /*
        Analyze a two word sentence
    */
        public static Tuple<bool, string> english(State globals, string inputLine)
        {
            string msg;
            int type1, type2, val1, val2;

            globals.verb = globals.@object = globals.motion = 0;
            type2 = val2 = -1;
            type1 = val1 = -1;
            msg = "bad grammar...";

            ParseInputLine(globals, inputLine);

            Tuple<bool, string> analysisResults;
            if (string.IsNullOrWhiteSpace(globals.word1))
                return Tuple.Create(false, string.Empty);     /* ignore whitespace	*/
            if (!(analysisResults = analyze(globals.word1, ref type1, ref val1)).Item1) /* check word1	*/
                return Tuple.Create(false, analysisResults.Item2);     /* didn't know it	*/

            if (type1 == 2 && val1 == Definitions.SAY)
            {
                globals.verb = Definitions.SAY; /* repeat word & act upon if..	*/
                globals.@object = 1;
                return Tuple.Create(true, analysisResults.Item2);
            }

            if (!string.IsNullOrWhiteSpace(globals.word2))
                if (!(analysisResults = analyze(globals.word2, ref type2, ref val2)).Item1)
                    return Tuple.Create(false, analysisResults.Item2); /* didn't know it	*/

            /* check his grammar */
            if ((type1 == 3) && (type2 == 3) && (val1 == 51) && (val2 == 51))
            {
                string output = analysisResults.Item2 + Environment.NewLine + outwords(globals);
                return Tuple.Create(false, output);
            }
            else if (type1 == 3)
            {
                string output = analysisResults.Item2 + Environment.NewLine + Database.rspeak(val1);
                return Tuple.Create(false, output);
            }
            else if (type2 == 3)
            {
                string output = analysisResults.Item2 + Environment.NewLine + Database.rspeak(val2);
                return Tuple.Create(false, output);
            }
            else if (type1 == 0)
            {
                if (type2 == 0)
                {
                    string output = analysisResults.Item2 + Environment.NewLine + msg;
                    return Tuple.Create(false, output);
                }
                else
                    globals.motion = val1;
            }
            else if (type2 == 0)
                globals.motion = val2;
            else if (type1 == 1)
            {
                globals.@object = val1;
                if (type2 == 2)
                    globals.verb = val2;
                if (type2 == 1)
                {
                    string output = analysisResults.Item2 + Environment.NewLine + msg;
                    return Tuple.Create(false, output);
                }
            }
            else if (type1 == 2)
            {
                globals.verb = val1;
                if (type2 == 1)
                    globals.@object = val2;
                if (type2 == 2)
                {
                    string output = analysisResults.Item2 + Environment.NewLine + msg;
                    return Tuple.Create(false, output);
                }
            }

            return Tuple.Create(true, analysisResults.Item2);
        }

        /*
                Routine to analyze a word.
        */
        public static Tuple<bool, string> analyze(string word, ref int type, ref int value)
        {
            int wordval, msg;

            /* make sure I understand */
            if ((wordval = Database.vocab(word, 0)) == -1)
            {
                switch (new Random().Next() % 3)
                {
                    case 0:
                        msg = 60;
                        break;
                    case 1:
                        msg = 61;
                        break;
                    default:
                        msg = 13;
                        break;
                }

                ;
                return Tuple.Create(false, Database.rspeak(msg));
            }

            type = wordval / 1000;

            value = wordval % 1000;
            return Tuple.Create(true, string.Empty);
        }

        /*
            retrieve input line, convert to lower case
             & rescan for first two words (max. WORDSIZE-1 chars).
        */
        public static void ParseInputLine(State globals, string inputLine)
        {
            globals.word1 = globals.word2 = null;
            string[] lineParts = inputLine.Split(' ');
            if (lineParts.Length > 0)
            {
                globals.word1 = lineParts[0];
            }

            if (lineParts.Length > 1)
            {
                globals.word2 = string.Join(" ", lineParts.Skip(1));
            }
        }

        /*
            output adventure word list (motion/0xxx & verb/2xxx)
        */
        public static string outwords(State globals)
        {
            string outputLine = string.Empty;
            int j = 0;
            for (int i = 0; i < Definitions.MAXWC; ++i)
            {
                if ((State.wc[i].acode < 1000) || ((State.wc[i].acode < 3000) && (State.wc[i].acode > 1999)))
                {
                    outputLine += (State.wc[i].aword + Environment.NewLine);
                    if ((++j == 6) || (i == Definitions.MAXWC - 1))
                    {
                        j = 0;
                        outputLine += Environment.NewLine;
                    }
                }
            }

            return outputLine;
        }
    }
}