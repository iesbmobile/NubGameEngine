using System;

namespace NubGameEngine
{
	public class BehaviourFloorMove : Behaviour
	{
		
		public override void Update ()
		{
			base.Update ();
			
			if (sprite.y > Game.rectScreen.Height + sprite.texture.Height / 2)
			{
				sprite.SetPosition(sprite.x, -sprite.height * 2 + Game.rectScreen.Height);
			}
		}
	}
}

