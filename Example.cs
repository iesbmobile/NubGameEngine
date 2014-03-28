using System;
using NubGameEngine;
using Sce.PlayStation.Core.Graphics;

public class Example
{
	static Level level1;
	
	public static void Main(string[] args)
	{
		Game game = new Game();
		//game.Initialize();
				
		level1 = new Level();
		
		Layer l1 = new Layer();
		level1.AddLayer(l1);
		l1.AddSprite(new Sprite(Game.rectScreen.Width / 2, Game.rectScreen.Height / 2, new Texture2D("/Application/resource/textures/airplane.png", false)));
		
		game.Run();
	}	
}

