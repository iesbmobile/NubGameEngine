using System;
using Sce.PlayStation.Core.Input;

namespace NubGameEngine
{
	public class BehaviourPlayerMove : Behaviour
	{
		float speed = 7f;
		float minX;
		float maxX;
		float minY;
		float maxY;
		
		public BehaviourPlayerMove (Sprite player)
		{
			minX = player.width / 2;
			maxX = Game.rectScreen.Width - minX;
			minY = player.height / 2;
			maxY = Game.rectScreen.Height - minY;
		}
		
		public override void Update ()
		{
			base.Update ();
			
			if ((Game.gamePadData.Buttons & GamePadButtons.Left) != 0 && sprite.x - speed > minX)
			{
				sprite.Translate (-speed, 0);
			}
			else
			if ((Game.gamePadData.Buttons & GamePadButtons.Right) != 0 && sprite.x + speed < maxX)
			{
				sprite.Translate (speed, 0);
			}
			
			if ((Game.gamePadData.Buttons & GamePadButtons.Up) != 0 && sprite.y - speed > minY)
			{
				sprite.Translate (0, -speed);
			}
			else
			if ((Game.gamePadData.Buttons & GamePadButtons.Down) != 0 && sprite.y + speed < maxY)
			{
				sprite.Translate (0, speed);
			}
		}
	}
}

