Adventure
=========
This is a port of the BDS C version of Adventure to C#.
Effectively, it's a port of [this GitHub repository port](https://github.com/troglobit/advent4), 
although I used source code for this from [elsewhere](http://rickadams.org/adventure/e_downloads.html).

There are a lot of versions of Adventure on [GitHub](https://github.com/search?q=adventure+colossal&type=Repositories), although not many for C#.
The only other [full C# port](https://github.com/scurrier/adventure) directly converts an older version of Adventure to C#. 

This port is slightly less accurate, but much easier to use outside of the console.

Changes
-------
* Compiles down to a single C# assembly. No external files, file access, or other unusual permissions required.
* Reorganized for conversational usage. That is, each user query returns a user response -- no "out-of-band" communication or state updates happen. This change means that time-sensitive operations in the original Adventure game are reorganized or were removed entirely. Sorry.
* Added additional top-level error handling. This helps diagnose input parsing failures over (potentially buggy) network links.

Usage
-----
```csharp
using Adventure;
using System;
...

Game game = new Game(null); // If you want to restore a game, provide a State object.

string nextLine = "SomethingToBypassInputValidationAndGetTheIntroText";
while(true)
{
  Console.WriteLine(game.Run(nextLine);
  
  // At any time before or after Run(...) calls you can save game state by calling:
  State currentState = game.State;
  
  Console.WriteLine(">");
  nextLine = Console.ReadLine();
}
```

**State** is both binary and [JSON](https://www.newtonsoft.com/json) serializable, so you can easily save / restore it for network-based sessions.

Miscellaneous
-------------
This source is generally perceived to be in the public domain, so this port is as well.

For more information about Adventure, see [http://en.wikipedia.org/wiki/Colossal_Cave_Adventure](http://en.wikipedia.org/wiki/Colossal_Cave_Adventure)
