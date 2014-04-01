using System;
using NubGameEngine;
using Sce.PlayStation.Core.Graphics;

public class Example
{
	static Level level1;
	
	public static void Main(string[] args)
	{
		Game game = new Game();
		
		//Quando um level eh instanciado, ele ja eh adicionado ao game automaticamente.
		level1 = new Level();
		
		//Sprites so podem ser criados dentro de uma Layer, entao eh necessario criar uma antes...
		Layer l1 = new Layer();
		//...e adiciona-la ao nosso Level
		level1.AddLayer(l1);
		//Por fim, colocar um sprite la dentro.
		l1.AddSprite(new Sprite(0.0f, 0.0f, new Texture2D("/Application/resource/textures/jumpingpumpkin.png", false)));
		l1.AddSprite(new Sprite(Game.rectScreen.Width / 2, Game.rectScreen.Height / 2, new Texture2D("/Application/resource/textures/airplane.png", false)));
		//l1.AddSprite(new Sprite(0.0f, 0.0f, new Texture2D("/Application/resource/textures/jumpingpumpkin.png", false)));
		
		game.Run();
	}	
}

