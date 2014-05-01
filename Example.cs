using System;
using NubGameEngine;
using Sce.PlayStation.Core.Graphics;

/// <summary>
/// Classe que mostra um exemplo do funcionamento da engine.
/// </summary>
public class Example
{
	
	
	public static void Main(string[] args)
	{
		
		FireInTheSkyVita game = new FireInTheSkyVita();
		
		game.Run();
		
		/*
		Game game = new Game();
		
		//Quando um level eh instanciado, ele ja eh adicionado ao game automaticamente.
		level1 = new Level();
		
		//Sprites so podem ser criados dentro de uma Layer, entao eh necessario criar uma antes...
		Layer l1 = new Layer();
		//...e adiciona-la ao nosso Level
		level1.AddLayer(l1);
		//Por fim, colocar sprites la dentro.
		l1.AddSprite(new Sprite(0.0f, 0.0f, new Texture2D("/Application/resource/textures/jumpingpumpkin.png", false)));
		l1.AddSprite(new Sprite(Game.rectScreen.Width / 2, Game.rectScreen.Height / 2, new Texture2D("/Application/resource/textures/airplane.png", false)));
		
		//Para animar um sprite, e preciso usar o construtor Sprite(float, float, Texture2D, int, int)
		Sprite animatedPumpkin = new Sprite(0.0f, 100.0f, new Texture2D("/Application/resource/textures/jumpingpumpkin.png", false), 41, 57);
		//Instanciar uma animacao com referencia ao Sprite
		Animation pumpkinJump = new Animation(animatedPumpkin, "Jump", 100L, 0, 8);
		l1.AddSprite(animatedPumpkin);
		//Tocar a animaçao com loop
		pumpkinJump.Play(true);
		
		//Roda o jogo
		game.Run();
		
		*/
	}	
}

