using System;
using Sce.PlayStation.Core.Graphics;

namespace NubGameEngine
{
	public class FireInTheSkyVita : Game
	{
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
			player = new Sprite (Game.rectScreen.Width / 2, 400, new Texture2D ("/Application/resource/textures/plane.png", false), 164, 128, Pivot.Center);
			player.tag = "player";
			Animation helix = new Animation (player, "Spin", 50L, 0, 3);
			helix.Play (true);
			playerLayer.AddSprite (player);
			player.AddCollider (100, 100);
			player.AddBehaviour(new BehaviourPlayerMove(player));
			
			Layer projectiles = new Layer();
			player.AddBehaviour(new BehaviourPlayerShoot(projectiles));
			player.AddBehaviour(new BehaviourDestroyable("enemy"));
			
			//---------Enemy
			Layer enemiesLayer = new Layer ();
			Sprite enemy = new Sprite (Game.rectScreen.Width / 2, 100, new Texture2D ("/Application/resource/textures/airplane.png", false), 164, 128, Pivot.Center);
			enemiesLayer.AddSprite(enemy);
			enemy.AddCollider (100, 100);
			enemy.AddBehaviour(new BehaviourDestroyable("player"));
			
			level1.AddLayer(playerLayer);
			level1.AddLayer(projectiles);
			level1.AddLayer(enemiesLayer);
			level1.AddLayer(background);
			
		}
		
//		float scale = 1;
		protected override void Update ()
		{
			base.Update ();
			
			//player.SetScale(scale, scale);
		}
		
	}
}

