using System;
using Sce.PlayStation.Core.Graphics;

namespace NubGameEngine
{
	public class FireInTheSkyVita : Game
	{
		Sprite projectile;
		
		public FireInTheSkyVita () : base()
		{
			Level level1 = FireInTheSkyVita.levelList[0];
			
			Layer background = new Layer();
			Sprite road = new Sprite(Game.rectScreen.Width/2, Game.rectScreen.Height/2, new Texture2D("/Application/resource/textures/Pista.png", false), Pivot.Center);
			background.AddSprite(road);
			
			Layer playerLayer = new Layer();
			Sprite player = new Sprite(Game.rectScreen.Width/2, Game.rectScreen.Height/2, new Texture2D("/Application/resource/textures/plane.png", false), 164, 128, Pivot.Center);
			Animation helix = new Animation(player, "Spin", 50L, 0, 3);
			helix.Play(true);
			playerLayer.AddSprite(player);
			player.AddCollider(100, 100);
			
			Layer projectiles = new Layer();
			projectile = new Sprite(Game.rectScreen.Width/2, Game.rectScreen.Height, new Texture2D("/Application/resource/textures/shot.png", false), Pivot.Center);
			projectiles.AddSprite(projectile);
			projectile.AddCollider(10, 10);
			projectile.AddBehaviour(new Behaviour(projectile));
			
			level1.AddLayer(projectiles);
			level1.AddLayer(playerLayer);
			level1.AddLayer(background);
		}
		
		protected override void Update ()
		{
			base.Update ();
			
			projectile.Translate(0, -10);
			if (projectile.y < 0)
				projectile.SetPosition(Game.rectScreen.Width/2, Game.rectScreen.Height);
		}
		
	}
}

