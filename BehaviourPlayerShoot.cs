using System;
using Sce.PlayStation.Core.Graphics;

namespace NubGameEngine
{
	public class BehaviourPlayerShoot : Behaviour
	{
		public const long SHOT_CD = 100L;
		public const int PROJECTILES_LENGTH = 30;
		
		Sprite[] projectiles;
		int nextIndex = 0;
		Layer projectilesLayer;
		long lastShot = 0;
		
		public BehaviourPlayerShoot (Layer projectilesLayer)
		{
			this.projectilesLayer = projectilesLayer;
			projectiles = new Sprite[PROJECTILES_LENGTH];
			
			for (int i = 0; i < projectiles.Length; i++)
			{
				projectiles[i] = new Sprite(Game.rectScreen.Width/2, Game.rectScreen.Height, new Texture2D("/Application/resource/textures/shot.png", false), Pivot.Center);
				projectiles[i].AddCollider(10, 10);
				projectiles[i].active = false;
				projectilesLayer.AddSprite(projectiles[i]);
				projectiles[i].AddBehaviour(new BehaviourShot());
				projectiles[i].tag = "player";
			}
		}
		
		public override void Update ()
		{
			if (Game.time - lastShot >= SHOT_CD)
			{
				lastShot = Game.time;
				Shoot();
			}
		}
		
		public void Shoot ()
		{
			if (!projectiles[nextIndex].active)
			{
				projectiles[nextIndex].SetPosition(sprite.x, sprite.y - 50);
				projectiles[nextIndex].active = true;
				nextIndex++;
				if (nextIndex >= projectiles.Length)
				{
					nextIndex = 0;
				}
			}
			
		}
	}
}

