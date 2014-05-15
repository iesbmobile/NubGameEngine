using System;
using Sce.PlayStation.Core.Graphics;

namespace NubGameEngine
{
	public class FireInTheSkyVita : Game
	{
		Sprite projectile;
		Sprite player;
		
		public FireInTheSkyVita () : base()
		{
			Level level1 = FireInTheSkyVita.levelList [0];
			
			LayerBackground background = new LayerBackground ();
			Texture2D floor = new Texture2D ("/Application/resource/textures/Pista.png", false);
			Sprite road1 = new Sprite (Game.rectScreen.Width / 2, Game.rectScreen.Height / 2 + floor.Height, floor, Pivot.Center);
			Sprite road2 = new Sprite (Game.rectScreen.Width / 2, Game.rectScreen.Height / 2, floor, Pivot.Center);
			Sprite road3 = new Sprite (Game.rectScreen.Width / 2, Game.rectScreen.Height / 2 - floor.Height, floor, Pivot.Center);
			background.AddSprite (road1);
			background.AddSprite (road2);
			background.AddSprite (road3);
			road1.AddBehaviour (new BehaviourFloorMove ());
			road2.AddBehaviour (new BehaviourFloorMove ());
			road3.AddBehaviour (new BehaviourFloorMove ());
			
			Layer playerLayer = new Layer ();
			player = new Sprite (Game.rectScreen.Width / 2, Game.rectScreen.Height / 2, new Texture2D ("/Application/resource/textures/plane.png", false), 164, 128, Pivot.Center);
			Animation helix = new Animation (player, "Spin", 50L, 0, 3);
			helix.Play (true);
			playerLayer.AddSprite (player);
			player.AddCollider (100, 100);
			player.AddBehaviour(new BehaviourPlayerMove());
			
			Layer projectiles = new Layer();
			projectile = new Sprite(Game.rectScreen.Width/2, Game.rectScreen.Height, new Texture2D("/Application/resource/textures/shot.png", false), Pivot.Center);
			projectiles.AddSprite(projectile);
			projectile.AddCollider(10, 10);
			
			level1.AddLayer(projectiles);
			level1.AddLayer(playerLayer);
			level1.AddLayer(background);
			
		}
		
		float scale = 1;
		protected override void Update ()
		{
			base.Update ();
			
			projectile.Translate(0, -10);
			if (projectile.y < 0)
				projectile.SetPosition(Game.rectScreen.Width/2, Game.rectScreen.Height);
			
			scale += 0.01f;
			//player.SetScale(scale, scale);
		}
		
	}
}

