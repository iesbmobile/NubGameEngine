using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

namespace NubGameEngine
{	
	public class AppMain
	{
		public static void Main(string[] args)
		{
			Game game = new Game();
			game.Initialize();
			game.Run();
		}	
	}
}
