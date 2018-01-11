using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Adventure
{
    public class Game
    {
        static Game()
        {
            opentxt();
        }

        public Game(State globals = null)
        {
            this.State = globals ?? new State()
            {
                Initialized = false,
                IfFirstRun = true,
                LastLineSuccessful = false
            };
        }

        public string Run(string nextLine)
        {
            string output = Environment.NewLine;
            if (!this.State.Initialized)
            {
                initplay(this.State);

                this.State.Initialized = true;
                output += Database.rspeak(65);
                output += Database.rspeak(1);
            }
            
            string result = (string.IsNullOrWhiteSpace(output) ? string.Empty : output + Environment.NewLine) + Turn.turn(this.State, nextLine);
            return string.IsNullOrWhiteSpace(result) ?  $"Sorry, I didn't quite get that. You said: {Convert.ToBase64String(Encoding.UTF8.GetBytes(nextLine))} in base64." : result;
        }

        public State State { get; private set; }

        private static void SetAt(ref int[] array, int startIdx, int[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                array[startIdx + i] = data[i];
            }
        }

        /*
            Initialization of adventure play variables
        */
        private void initplay(State globals)
        {
            globals.turns = 0;

            /* initialize location status array */
            for (int i = 0; i < Definitions.MAXLOC; i++)
            {
                globals.cond[i] = 0;
            }
            SetAt(ref globals.cond, 1, new int[] { 5, 1, 5, 5, 1, 1, 5, 17, 1, 1 });
            SetAt(ref globals.cond, 13, new int[] { 32, 0, 0, 2, 0, 0, 64, 2 });
            SetAt(ref globals.cond, 21, new int[] { 2, 2, 0, 6, 0, 2 });
            SetAt(ref globals.cond, 31, new int[] { 2, 2, 0, 0, 0, 0, 0, 4, 0, 2 });
            SetAt(ref globals.cond, 42, new int[] { 128, 128, 128, 128, 136, 136, 136, 128, 128 });
            SetAt(ref globals.cond, 51, new int[] { 128, 128, 136, 128, 136, 0, 8, 0, 2 });
            SetAt(ref globals.cond, 79, new int[] { 2, 128, 128, 136, 0, 0, 8, 136, 128, 0, 2, 2 });
            SetAt(ref globals.cond, 95, new int[] { 4, 0, 0, 0, 0, 1 });
            SetAt(ref globals.cond, 113, new int[] { 4, 0, 1, 1 });
            SetAt(ref globals.cond, 122, new int[] { 8, 8, 8, 8, 8, 8, 8, 8, 8 });

            /* initialize object locations */
            for (int i = 0; i < Definitions.MAXOBJ; i++)
            {
                globals.place[i] = 0;
            }
            SetAt(ref globals.place, 1, new int[] { 3, 3, 8, 10, 11, 0, 14, 13, 94, 96 });
            SetAt(ref globals.place, 11, new int[] { 19, 17, 101, 103, 0, 106, 0, 0, 3, 3 });
            SetAt(ref globals.place, 23, new int[] { 109, 25, 23, 111, 35, 0, 97 });
            SetAt(ref globals.place, 31, new int[] { 119, 117, 117, 0, 130, 0, 126, 140, 0, 96 });
            SetAt(ref globals.place, 50, new int[] { 18, 27, 28, 29, 30 });
            SetAt(ref globals.place, 56, new int[] { 92, 95, 97, 100, 101, 0, 119, 127, 130 });

            /* initialize second (fixed) locations */
            for (int i = 0; i < Definitions.MAXOBJ; i++)
            {
                globals.@fixed[i] = 0;
            }

            SetAt(ref globals.@fixed, 3, new int[] { 9, 0, 0, 0, 15, 0, -1 });
            SetAt(ref globals.@fixed, 11, new int[] { -1, 27, -1, 0, 0, 0, -1 });
            SetAt(ref globals.@fixed, 23, new int[] { -1, -1, 67, -1, 110, 0, -1, -1 });
            SetAt(ref globals.@fixed, 31, new int[] { 121, 122, 122, 0, -1, -1, -1, -1, 0, -1 });
            globals.@fixed[62] = 121;
            globals.@fixed[63] = -1;

            /* initialize various flags and other variables */
            for (int i = 0; i < Definitions.MAXLOC; i++)
            {
                globals.visited[i] = 0;
            }
            for (int i = 0; i < Definitions.MAXOBJ; i++)
            {
                globals.prop[i] = 0;
                if (i >= 50)
                {
                    globals.prop[i] = 0xFF;
                }
            }

            globals.wzdark = globals.closed = globals.closing = false;
            globals.holding = globals.detail = 0;
            globals.limit = 100;
            globals.tally = 15;
            globals.tally2 = 0;
            globals.newloc = 3;
            globals.loc = globals.oldloc = globals.oldloc2 = 1;
            globals.knfloc = 0;
            globals.chloc = 114;
            globals.chloc2 = 140;
            /*	dloc[DWARFMAX-1] = chloc				*/

            globals.dloc[0] = 0;
            globals.dloc[1] = 19;
            globals.dloc[2] = 27;
            globals.dloc[3] = 33;
            globals.dloc[4] = 44;
            globals.dloc[5] = 64;
            globals.dloc[6] = 114;
            for (int i = 0; i < globals.odloc.Length; i++)
            {
                globals.odloc[i] = 0;
            }
            globals.dkill = 0;
            for (int i = 0; i < globals.dseen.Length; i++)
            {
                globals.dseen[i] = false;
            }
            globals.clock = 30;
            globals.clock2 = 50;
            globals.panic = 0;
            globals.bonus = 0;
            globals.daltloc = 18;
            globals.lmwarn = 0;
            globals.foobar = 0;
            globals.dflag = 0;
            return;
        }

        private static void opentxt()
        {
            State.fd1 = ReadFile("advent1");
            if (State.fd1 == null)
            {
                throw new InvalidDataException("Missing advent1");
            }

            State.fd2 = ReadFile("advent2");
            if (State.fd2 == null)
            {
                throw new InvalidDataException("Missing advent2");
            }

            State.fd3 = ReadFile("advent3");
            if (State.fd3 == null)
            {
                throw new InvalidDataException("Missing advent3");
            }

            State.fd4 = ReadFile("advent4");
            if (State.fd4 == null)
            {
                throw new InvalidDataException("Missing advent4");
            }
        }

        private static Dictionary<int, string> ReadFile(string filename)
        {
            string assemblyPrefix = "Adventure.";
            string assemblyPostfix = ".txt";
            Assembly assembly = Assembly.GetAssembly(typeof(Game));

            Dictionary<int, string> fileSegments = new Dictionary<int, string>();
            using (Stream stream = assembly.GetManifestResourceStream($"{assemblyPrefix}{filename}{assemblyPostfix}"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string file = reader.ReadToEnd();
                    int id = -1;
                    StringBuilder builder = new StringBuilder();
                    foreach (string line in file.Split('\n'))
                    {
                        if (line.StartsWith("#"))
                        {
                            if (id != -1)
                            {
                                fileSegments.Add(id, builder.ToString());
                                builder.Clear();
                            }

                            id = int.Parse(line.Substring(1));
                        }
                        else
                        {
                            builder.AppendLine(line);
                        }
                    }

                    if (id != -1)
                    {
                        fileSegments.Add(id, builder.ToString());
                        builder.Clear();
                    }
                }
            }

            return fileSegments;
        }
    }
}