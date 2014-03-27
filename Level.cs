using System;
using System.Collections.Generic;
using Sce.PlayStation.Core.Graphics;

namespace NubGameEngine
{
	public class Level : GameObject
	{
		List<Layer> layerList = new List<Layer>();
		
		public Level () : base()
		{
			Game.levelList.Add(this);
		}
		
		public void AddLayer (Layer newLayer)
		{
			layerList.Add(newLayer);
		}
		
		public override void Update ()
		{
			base.Update ();
			
			foreach (Layer l in layerList)
			{
				l.Update();
			}
		}
		
		public override void Draw (GraphicsContext graphics)
		{
			base.Draw (graphics);
			
			foreach (Layer l in layerList)
			{
				l.Draw(graphics);
			}
		}
	}
}

