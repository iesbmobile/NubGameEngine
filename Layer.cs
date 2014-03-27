using System;
using System.Collections.Generic;
using Sce.PlayStation.Core.Graphics;

namespace NubGameEngine
{
	public class Layer : GameObject
	{
		List<Sprite> spriteList = new List<Sprite>();
		
		public Layer () : base()
		{
		}
		
		public void AddSprite (Sprite newSprite)
		{
			spriteList.Add(newSprite);
		}
		
		public override void Update ()
		{
			base.Update ();
			
			foreach (Sprite s in spriteList)
			{
				s.Update();
			}
		}
		
		public override void Draw (GraphicsContext graphics)
		{
			base.Draw (graphics);
			
			foreach (Sprite s in spriteList)
			{
				s.Draw(graphics);
			}
		}
	}
}

