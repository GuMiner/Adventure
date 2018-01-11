﻿using System;
using System.Collections.Generic;

namespace Adventure
{
    [Serializable]
    public class State
    {
        public static readonly Definitions.wac[] wc = new Definitions.wac[]
        {
            new Definitions.wac("spelunker today",  1016),
            new Definitions.wac("?",        3051),
            new Definitions.wac("above",    29),
            new Definitions.wac("abra",     3050),
            new Definitions.wac("abracadabra",  3050),
            new Definitions.wac("across",   42),
            new Definitions.wac("ascend",   29),
            new Definitions.wac("attack",   2012),
            new Definitions.wac("awkward",  26),
            new Definitions.wac("axe",      1028),
            new Definitions.wac("back",     8),
            new Definitions.wac("barren",   40),
            new Definitions.wac("bars",     1052),
            new Definitions.wac("batteries",    1039),
            new Definitions.wac("battery",  1039),
            new Definitions.wac("beans",    1024),
            new Definitions.wac("bear",     1035),
            new Definitions.wac("bed",      16),
            new Definitions.wac("bedquilt", 70),
            new Definitions.wac("bird",     1008),
            new Definitions.wac("blast",    2023),
            new Definitions.wac("blowup",   2023),
            new Definitions.wac("bottle",   1020),
            new Definitions.wac("box",      1055),
            new Definitions.wac("break",    2028),
            new Definitions.wac("brief",    2026),
            new Definitions.wac("broken",   54),
            new Definitions.wac("building", 12),
            new Definitions.wac("cage",     1004),
            new Definitions.wac("calm",     2010),
            new Definitions.wac("canyon",   25),
            new Definitions.wac("capture",  2001),
            new Definitions.wac("carpet",   1040),
            new Definitions.wac("carry",    2001),
            new Definitions.wac("catch",    2001),
            new Definitions.wac("cave",     67),
            new Definitions.wac("cavern",   73),
            new Definitions.wac("chain",    1064),
            new Definitions.wac("chant",    2003),
            new Definitions.wac("chasm",    1032),
            new Definitions.wac("chest",    1055),
            new Definitions.wac("clam",     1014),
            new Definitions.wac("climb",    56),
            new Definitions.wac("close",    2006),
            new Definitions.wac("cobblestone",  18),
            new Definitions.wac("coins",    1054),
            new Definitions.wac("continue", 2011),
            new Definitions.wac("crack",    33),
            new Definitions.wac("crap",     3079),
            new Definitions.wac("crawl",    17),
            new Definitions.wac("cross",    69),
            new Definitions.wac("d",        30),
            new Definitions.wac("damn",     3079),
            new Definitions.wac("damnit",   3079),
            new Definitions.wac("dark",     22),
            new Definitions.wac("debris",   51),
            new Definitions.wac("depression",   63),
            new Definitions.wac("descend",  30),
            new Definitions.wac("describe", 57),
            new Definitions.wac("detonate", 2023),
            new Definitions.wac("devour",   2014),
            new Definitions.wac("diamonds", 1051),
            new Definitions.wac("dig",      3066),
            new Definitions.wac("discard",  2002),
            new Definitions.wac("disturb",  2029),
            new Definitions.wac("dome",     35),
            new Definitions.wac("door",     1009),
            new Definitions.wac("down",     30),
            new Definitions.wac("downstream",   4),
            new Definitions.wac("downward", 30),
            new Definitions.wac("dragon",   1031),
            new Definitions.wac("drawing",  1029),
            new Definitions.wac("drink",    2015),
            new Definitions.wac("drop",     2002),
            new Definitions.wac("dump",     2002),
            new Definitions.wac("dwarf",    1017),
            new Definitions.wac("dwarves",  1017),
            new Definitions.wac("e",        43),
            new Definitions.wac("east",     43),
            new Definitions.wac("eat",      2014),
            new Definitions.wac("egg",      1056),
            new Definitions.wac("eggs",     1056),
            new Definitions.wac("emerald",  1059),
            new Definitions.wac("enter",    3),
            new Definitions.wac("entrance", 64),
            new Definitions.wac("examine",  57),
            new Definitions.wac("excavate", 3066),
            new Definitions.wac("exit",     11),
            new Definitions.wac("explore",  2011),
            new Definitions.wac("extinguish",   2008),
            new Definitions.wac("fee",      2025),
            new Definitions.wac("fee",      3001),
            new Definitions.wac("feed",     2021),
            new Definitions.wac("fie",      2025),
            new Definitions.wac("fie",      3002),
            new Definitions.wac("fight",    2012),
            new Definitions.wac("figure",   1027),
            new Definitions.wac("fill",     2022),
            new Definitions.wac("find",     2019),
            new Definitions.wac("fissure",  1012),
            new Definitions.wac("floor",    58),
            new Definitions.wac("foe",      2025),
            new Definitions.wac("foe",      3003),
            new Definitions.wac("follow",   2011),
            new Definitions.wac("foo",      2025),
            new Definitions.wac("foo",      3004),
            new Definitions.wac("food",     1019),
            new Definitions.wac("forest",   6),
            new Definitions.wac("fork",     77),
            new Definitions.wac("forward",  7),
            new Definitions.wac("free",     2002),
            new Definitions.wac("fuck",     3079),
            new Definitions.wac("fum",      2025),
            new Definitions.wac("fum",      3005),
            new Definitions.wac("get",      2001),
            new Definitions.wac("geyser",   1037),
            new Definitions.wac("giant",    27),
            new Definitions.wac("go",       2011),
            new Definitions.wac("gold",     1050),
            new Definitions.wac("goto",     2011),
            new Definitions.wac("grate",    1003),
            new Definitions.wac("gully",    13),
            new Definitions.wac("h2o",      1021),
            new Definitions.wac("hall",     38),
            new Definitions.wac("headlamp", 1002),
            new Definitions.wac("help",     3051),
            new Definitions.wac("hill",     2),
            new Definitions.wac("hit",      2012),
            new Definitions.wac("hocus",    3050),
            new Definitions.wac("hole",     52),
            new Definitions.wac("hours",    2031),
            new Definitions.wac("house",    12),
            new Definitions.wac("i",        2020),
            new Definitions.wac("ignite",   2023),
            new Definitions.wac("in",       19),
            new Definitions.wac("info",     3142),
            new Definitions.wac("information",  3142),
            new Definitions.wac("inside",   19),
            new Definitions.wac("inventory",    2020),
            new Definitions.wac("inward",   19),
            new Definitions.wac("issue",    1016),
            new Definitions.wac("jar",      1020),
            new Definitions.wac("jewel",    1053),
            new Definitions.wac("jewelry",  1053),
            new Definitions.wac("jewels",   1053),
            new Definitions.wac("jump",     39),
            new Definitions.wac("keep",     2001),
            new Definitions.wac("key",      1001),
            new Definitions.wac("keys",     1001),
            new Definitions.wac("kill",     2012),
            new Definitions.wac("knife",    1018),
            new Definitions.wac("knives",   1018),
            new Definitions.wac("l",        57),
            new Definitions.wac("lamp",     1002),
            new Definitions.wac("lantern",  1002),
            new Definitions.wac("leave",    11),
            new Definitions.wac("left",     36),
            new Definitions.wac("light",    2007),
            new Definitions.wac("lite",     2007),
            new Definitions.wac("load",     2033),
            new Definitions.wac("lock",     2006),
            new Definitions.wac("log",      2032),
            new Definitions.wac("look",     57),
            new Definitions.wac("lost",     3068),
            new Definitions.wac("low",      24),
            new Definitions.wac("machine",  1038),
            new Definitions.wac("magazine", 1016),
            new Definitions.wac("main",     76),
            new Definitions.wac("message",  1036),
            new Definitions.wac("ming",     1058),
            new Definitions.wac("mirror",   1023),
            new Definitions.wac("mist",     3069),
            new Definitions.wac("moss",     1040),
            new Definitions.wac("mumble",   2003),
            new Definitions.wac("n",        45),
            new Definitions.wac("ne",       47),
            new Definitions.wac("nest",     1056),
            new Definitions.wac("north",    45),
            new Definitions.wac("nothing",  2005),
            new Definitions.wac("nowhere",  21),
            new Definitions.wac("nugget",   1050),
            new Definitions.wac("null",     21),
            new Definitions.wac("nw",       50),
            new Definitions.wac("off",      2008),
            new Definitions.wac("office",   76),
            new Definitions.wac("oil",      1022),
            new Definitions.wac("on",       2007),
            new Definitions.wac("onward",   7),
            new Definitions.wac("open",     2004),
            new Definitions.wac("opensesame",   3050),
            new Definitions.wac("oriental", 72),
            new Definitions.wac("out",      11),
            new Definitions.wac("outdoors", 32),
            new Definitions.wac("outside",  11),
            new Definitions.wac("over",     41),
            new Definitions.wac("oyster",   1015),
            new Definitions.wac("passage",  23),
            new Definitions.wac("pause",    2030),
            new Definitions.wac("pearl",    1061),
            new Definitions.wac("persian",  1062),
            new Definitions.wac("peruse",   2027),
            new Definitions.wac("pillow",   1010),
            new Definitions.wac("pirate",   1030),
            new Definitions.wac("pit",      31),
            new Definitions.wac("placate",  2010),
            new Definitions.wac("plant",    1024),
            new Definitions.wac("plant",    1025),
            new Definitions.wac("platinum", 1060),
            new Definitions.wac("plover",   71),
            new Definitions.wac("plugh",    65),
            new Definitions.wac("pocus",    3050),
            new Definitions.wac("pottery",  1058),
            new Definitions.wac("pour",     2013),
            new Definitions.wac("proceed",  2011),
            new Definitions.wac("pyramid",  1060),
            new Definitions.wac("quit",     2018),
            new Definitions.wac("rations",  1019),
            new Definitions.wac("read",     2027),
            new Definitions.wac("release",  2002),
            new Definitions.wac("reservoir",    75),
            new Definitions.wac("restore",  2033),
            new Definitions.wac("retreat",  8),
            new Definitions.wac("return",   8),
            new Definitions.wac("right",    37),
            new Definitions.wac("road",     2),
            new Definitions.wac("rock",     15),
            new Definitions.wac("rod",      1005),
            new Definitions.wac("rod",      1006),
            new Definitions.wac("room",     59),
            new Definitions.wac("rub",      2016),
            new Definitions.wac("rug",      1062),
            new Definitions.wac("run",      2011),
            new Definitions.wac("s",        46),
            new Definitions.wac("save",     2030),
            new Definitions.wac("say",      2003),
            new Definitions.wac("score",    2024),
            new Definitions.wac("se",       48),
            new Definitions.wac("secret",   66),
            new Definitions.wac("sesame",   3050),
            new Definitions.wac("shadow",   1027),
            new Definitions.wac("shake",    2009),
            new Definitions.wac("shard",    1058),
            new Definitions.wac("shatter",  2028),
            new Definitions.wac("shazam",   3050),
            new Definitions.wac("shell",    74),
            new Definitions.wac("shit",     3079),
            new Definitions.wac("silver",   1052),
            new Definitions.wac("sing",     2003),
            new Definitions.wac("slab",     61),
            new Definitions.wac("slit",     60),
            new Definitions.wac("smash",    2028),
            new Definitions.wac("snake",    1011),
            new Definitions.wac("south",    46),
            new Definitions.wac("spelunker",    1016),
            new Definitions.wac("spice",    1063),
            new Definitions.wac("spices",   1063),
            new Definitions.wac("stairs",   10),
            new Definitions.wac("stalactite",   1026),
            new Definitions.wac("steal",    2001),
            new Definitions.wac("steps",    1007),
            new Definitions.wac("steps",    34),
            new Definitions.wac("stop",     3139),
            new Definitions.wac("stream",   14),
            new Definitions.wac("strike",   2012),
            new Definitions.wac("surface",  20),
            new Definitions.wac("suspend",  2030),
            new Definitions.wac("sw",       49),
            new Definitions.wac("swim",     3147),
            new Definitions.wac("swing",    2009),
            new Definitions.wac("tablet",   1013),
            new Definitions.wac("take",     2001),
            new Definitions.wac("tame",     2010),
            new Definitions.wac("throw",    2017),
            new Definitions.wac("toss",     2017),
            new Definitions.wac("tote",     2001),
            new Definitions.wac("touch",    57),
            new Definitions.wac("travel",   2011),
            new Definitions.wac("treasure", 1055),
            new Definitions.wac("tree",     3064),
            new Definitions.wac("trees",    3064),
            new Definitions.wac("trident",  1057),
            new Definitions.wac("troll",    1033),
            new Definitions.wac("troll",    1034),
            new Definitions.wac("tunnel",   23),
            new Definitions.wac("turn",     2011),
            new Definitions.wac("u",        29),
            new Definitions.wac("unlock",   2004),
            new Definitions.wac("up",       29),
            new Definitions.wac("upstream", 4),
            new Definitions.wac("upward",   29),
            new Definitions.wac("utter",    2003),
            new Definitions.wac("valley",   9),
            new Definitions.wac("vase",     1058),
            new Definitions.wac("velvet",   1010),
            new Definitions.wac("vending",  1038),
            new Definitions.wac("view",     28),
            new Definitions.wac("volcano",  1037),
            new Definitions.wac("w",        44),
            new Definitions.wac("wake",     2029),
            new Definitions.wac("walk",     2011),
            new Definitions.wac("wall",     53),
            new Definitions.wac("water",    1021),
            new Definitions.wac("wave",     2009),
            new Definitions.wac("west",     44),
            new Definitions.wac("xyzzy",    62),
            new Definitions.wac("y2",       55)
        };

        public static readonly string[] cave = new string[]{
            "2002000,2044000,2029000,3003000,3012000,3019000,3043000,4005000,4013000,4014000,4046000,4030000,5006000,5045000,5043000,8063000,",
            "1002000,1012000,1007000,1043000,1045000,1030000,5006000,5045000,5046000,",
            "1003000,1011000,1032000,1044000,11062000,33065000,79005000,79014000,",
            "1004000,1012000,1045000,5006000,5043000,5044000,5029000,7005000,7046000,7030000,8063000,",
            "4009000,4043000,4030000,5006050,5007050,5045050,6006000,5044000,5046000,",
            "1002000,1045000,4009000,4043000,4044000,4030000,5006000,5046000,",
            "1012000,4004000,4045000,5006000,5043000,5044000,8005000,8015000,8016000,8046000,595060000,595014000,595030000,",
            "5006000,5043000,5046000,5044000,1012000,7004000,7013000,7045000,9003303,9019303,9030303,593003000,",
            "8011303,8029303,593011000,10017000,10018000,10019000,10044000,14031000,11051000,",
            "9011000,9020000,9021000,9043000,11019000,11022000,11044000,11051000,14031000,",
            "8063303,9064000,10017000,10018000,10023000,10024000,10043000,12025000,12019000,12029000,12044000,3062000,14031000,",
            "8063303,9064000,11030000,11043000,11051000,13019000,13029000,13044000,14031000,",
            "8063303,9064000,11051000,12025000,12043000,14023000,14031000,14044000,",
            "8063303,9064000,11051000,13023000,13043000,20030150,20031150,20034150,15030000,16033000,16044000,",
            "18036000,18046000,17007000,17038000,17044000,19010000,19030000,19045000,22029150,22031150,22034150,22035150,22023150,22043150,14029000,34055000,",
            "14001000,",
            "15038000,15043000,596039312,21007412,597041412,597042412,597044412,597069412,27041000,",
            "15038000,15011000,15045000,",
            "15010000,15029000,15043000,28045311,28036311,29046311,29037311,30044311,30007311,32045000,74049035,32049211,74066000,",
            "001000,",
            "001000,",
            "15001000,",
            "67043000,67042000,68044000,68061000,25030000,25031000,648052000,",
            "67029000,67011000,",
            "23029000,23011000,31056724,26056000,",
            "88001000,",
            "596039312,21007412,597041412,597042412,597043412,597069412,17041000,40045000,41044000,",
            "19038000,19011000,19046000,33045000,33055000,36030000,36052000,",
            "19038000,19011000,19045000,",
            "19038000,19011000,19043000,62044000,62029000,",
            "89001524,90001000,",
            "19001000,",
            "3065000,28046000,34043000,34053000,34054000,35044000,302071159,100071000,",
            "33030000,33055000,15029000,",
            "33043000,33055000,20039000,",
            "37043000,37017000,28029000,28052000,39044000,65070000,",
            "36044000,36017000,38030000,38031000,38056000,",
            "37056000,37029000,37011000,595060000,595014000,595030000,595004000,595005000,",
            "36043000,36023000,64030000,64052000,64058000,65070000,",
            "41001000,",
            "42046000,42029000,42023000,42056000,27043000,59045000,60044000,60017000,",
            "41029000,42045000,43043000,45046000,80044000,",
            "42044000,44046000,45043000,",
            "43043000,48030000,50046000,82045000,",
            "42044000,43045000,46043000,47046000,87029000,87030000,",
            "45044000,45011000,",
            "45043000,45011000,",
            "44029000,44011000,",
            "50043000,51044000,",
            "44043000,49044000,51030000,52046000,",
            "49044000,50029000,52043000,53046000,",
            "50044000,51043000,52046000,53029000,55045000,86030000,",
            "51044000,52045000,54046000,",
            "53044000,53011000,",
            "52044000,55045000,56030000,57043000,",
            "55029000,55011000,",
            "13030000,13056000,55044000,58046000,83045000,84043000,",
            "57043000,57011000,",
            "27001000,",
            "41043000,41029000,41017000,61044000,62045000,62030000,62052000,",
            "60043000,62045000,107046100,",
            "60044000,63045000,30043000,61046000,",
            "62046000,62011000,",
            "39029000,39056000,39059000,65044000,65070000,103045000,103074000,106043000,",
            "64043000,66044000,556046080,68061000,556029080,70029050,39029000,556045060,72045075,71045000,556030080,106030000,",
            "65047000,67044000,556046080,77025000,96043000,556050050,97072000,",
            "66043000,23044000,23042000,24030000,24031000,",
            "23046000,69029000,69056000,65045000,",
            "68030000,68061000,120046331,119046000,109045000,113075000,",
            "71045000,65030000,65023000,111046000,",
            "65048000,70046000,110045000,",
            "65070000,118049000,73045000,97048000,97072000,",
            "72046000,72017000,72011000,",
            "19043000,120044331,121044000,75030000,",
            "76046000,77045000,",
            "75045000,",
            "75043000,78044000,66045000,66017000,",
            "77046000,",
            "3001000,",
            "42045000,80044000,80046000,81043000,",
            "80044000,80011000,",
            "44046000,44011000,",
            "57046000,84043000,85044000,",
            "57045000,83044000,114050000,",
            "83043000,83011000,",
            "52029000,52011000,",
            "45029000,45030000,",
            "25030000,25056000,25043000,20039000,92044000,92027000,",
            "25001000,",
            "23001000,",
            "95045000,95073000,95023000,72030000,72056000,",
            "88046000,93043000,94045000,",
            "92046000,92027000,92011000,",
            "92046000,92027000,92023000,95045309,95003309,95073309,611045000,",
            "94046000,94011000,92027000,91044000,",
            "66044000,66011000,",
            "66048000,72044000,72017000,98029000,98045000,98073000,",
            "97046000,97072000,99044000,",
            "98050000,98073000,301043000,301023000,100043000,",
            "301044000,301023000,301011000,99044000,302071159,33071000,101047000,101022000,",
            "100046000,100071000,100011000,",
            "103030000,103074000,103011000,",
            "102029000,102038000,104030000,618046114,619046115,64046000,",
            "103029000,103074000,105030000,",
            "104029000,104011000,103074000,",
            "64029000,65044000,108043000,",
            "131046000,132049000,133047000,134048000,135029000,136050000,137043000,138044000,139045000,61030000,",
            "556043095,556045095,556046095,556047095,556048095,556049095,556050095,556029095,556030095,106043000,626044000,",
            "69046000,113045000,113075000,",
            "71044000,20039000,",
            "70045000,50030040,50039040,50056040,53030050,45030000,",
            "131049000,132045000,133043000,134050000,135048000,136047000,137044000,138030000,139029000,140046000,",
            "109046000,109011000,109109000,",
            "84048000,",
            "116049000,",
            "115047000,593030000,",
            "118049000,660041233,660042233,660069233,660047233,661041332,303041000,21039332,596039000,",
            "72030000,117029000,",
            "69045000,69011000,653043000,65307000,",
            "69045000,74043000,",
            "74043000,74011000,653045000,653007000,",
            "123047000,660041233,660042233,660069233,660049233,303041000,596039000,124077000,126028000,129040000,",
            "122044000,124043000,124077000,126028000,129040000,",
            "123044000,125047000,125036000,128048000,128037000,128030000,126028000,129040000,",
            "124046000,124077000,126045000,126028000,127043000,127017000,",
            "125046000,125023000,125011000,124077000,610030000,610039000,",
            "125044000,125011000,125017000,124077000,126028000,",
            "124045000,124029000,124077000,129046000,129030000,129040000,126028000,",
            "128044000,128029000,124077000,130043000,130019000,130040000,130003000,126028000,",
            "129044000,124077000,126028000,",
            "107044000,132048000,133050000,134049000,135047000,136029000,137030000,138045000,139046000,112043000,",
            "107050000,131029000,133045000,134046000,135044000,136049000,137047000,138043000,139030000,112048000,",
            "107029000,131030000,132044000,134047000,135049000,136043000,137045000,138050000,139048000,112046000,",
            "107047000,131045000,132050000,133048000,135043000,136030000,137046000,138029000,139044000,112049000,",
            "107045000,131048000,132030000,133046000,134043000,136044000,137049000,138047000,139050000,112029000,",
            "107043000,131044000,132029000,133049000,134030000,135046000,137050000,138048000,139047000,112045000,",
            "107048000,131047000,132046000,133030000,134029000,135050000,136045000,138049000,139043000,112044000,",
            "107030000,131043000,132047000,133029000,134044000,135045000,136046000,137048000,139049000,112050000,",
            "107049000,131050000,132043000,133044000,134045000,135030000,136048000,137029000,138046000,112047000,",
            "112045000,112011000,"
        };

        public static Dictionary<int, string> fd1, fd2, fd3, fd4; // Main text files 1, 2, 3, and 4

        public static readonly int[] actmsg = new int[]        /* action messages	*/
        {
            0, 24, 29, 0, 33, 0, 33, 38, 38, 42, 14, 43, 110, 29, 110, 73, 75, 29, 13, 59, 59, 174, 109, 67, 13, 147, 155, 195, 146, 110, 13, 13
        };

        public Definitions.trav[] travel = new Definitions.trav[Definitions.MAXTRAV];
        public bool Initialized;
        public bool IfFirstRun;

        /*
            English variables
        */
        public int verb, @object, motion;
        public string word1;
        public string word2;
        public bool LastLineSuccessful;
        /*
            Play variables
        */
        public int turns;
        public int loc, oldloc, oldloc2, newloc;    /* location variables */
        public int[] cond = new int[Definitions.MAXLOC];          /* location status	*/
        public int[] place = new int[Definitions.MAXOBJ + 1];     /* object location	*/
        public int[] @fixed = new int[Definitions.MAXOBJ];     /* second object loc	*/
        public int[] visited = new int[Definitions.MAXLOC];       /* >0 if has been here	*/
        public int[] prop = new int[Definitions.MAXOBJ];          /* status of object	*/
        public int tally, tally2;       /* item counts		*/
        public int limit;           /* time limit		*/
        public int lmwarn;          /* lamp warning flag	*/
        public bool wzdark, closing, closed; /* game state flags	*/
        public int holding;     /* count of held items	*/
        public int detail;          /* LOOK count		*/
        public int knfloc;          /* knife location	*/
        public int clock, clock2, panic;    /* timing variables	*/
        public int[] dloc = new int[Definitions.DWARFMAX];          /* dwarf locations	*/
        public int dflag;           /* dwarf flag		*/
        public bool[] dseen = new bool[Definitions.DWARFMAX];     /* dwarf seen flag	*/
        public int[] odloc = new int[Definitions.DWARFMAX];     /* dwarf old locations	*/
        public int daltloc;     /* alternate appearance	*/
        public int dkill;           /* dwarves killed	*/
        public int chloc, chloc2;       /* chest locations	*/
        public int bonus;           /* to pass to end	*/
        public int object1;     /* to help intrans.	*/
        public int foobar;          /* fie fie foe foo...	*/
    }
}