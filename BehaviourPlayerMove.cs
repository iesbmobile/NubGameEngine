using System;
using Sce.PlayStation.Core.Input;

namespace NubGameEngine
{
	public class BehaviourPlayerMove : Behaviour
	{
		float speed = 4f;
		
		public override void Update ()
		{
			base.Update ();
			
			if ((Game.gamePadData.Buttons & GamePadButtons.Left) != 0)
			{
				sprite.Translate (-speed, 0);
			}
			else
			if ((Game.gamePadData.Buttons & GamePadButtons.Right) != 0)
			{
				sprite.Translate (speed, 0);
			}
			
			if ((Game.gamePadData.Buttons & GamePadButtons.Up) != 0)
			{
				sprite.Translate (0, -speed);
			}
			else
			if ((Game.gamePadData.Buttons & GamePadButtons.Down) != 0)
			{
				sprite.Translate (0, speed);
			}
		}
	}
}

