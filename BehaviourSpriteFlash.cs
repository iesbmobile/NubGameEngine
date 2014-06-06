using System;

namespace NubGameEngine
{
	public class BehaviourSpriteFlash : Behaviour
	{
		public const long FLASH_DUR = 20L;
		
		long start = -1;
		long lastFlash = 0;
		bool flashSwitch = false;
		long duration = 0;
		
		public override void Update ()
		{
			base.Update ();
			
			if (start >= 0)
			{
				if (Game.time - start < duration)
				{
					if (Game.time - lastFlash >= FLASH_DUR)
					{
						lastFlash = Game.time;
						if (flashSwitch)
						{
							sprite.SetAlpha(1);
							flashSwitch = false;
						}
						else
						{
							sprite.SetAlpha(0);
							flashSwitch = true;
						}
					}
				}
				else
				{
					End();
				}
			}
		}
		
		public void FlashForMiliseconds (long mili)
		{
			if (start < 0)
			{
				start = Game.time;
				sprite.SetAlpha(0);
				flashSwitch = true;
				lastFlash = Game.time;
				duration = mili;
			}
		}
		
		void End ()
		{
			lastFlash = 0;
			flashSwitch = false;
			duration = 0;
			start = -1;
			sprite.SetAlpha(1);
		}
	}
}

