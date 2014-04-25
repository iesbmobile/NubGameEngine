using System;
using System.Collections.Generic;
using Sce.PlayStation.Core.Graphics;

namespace NubGameEngine
{
	/// <summary>
	/// O Level guarda todas as informacoes de uma cena do jogo. Ele eh um grupo de Layers.
	/// </summary>
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
			
			for (int i = layerList.Count - 1; i >= 0; i--)
			{
				layerList[i].Update();
			}
		}
		
		public override void Draw (GraphicsContext graphics)
		{
			base.Draw (graphics);
			
			for (int i = layerList.Count - 1; i >= 0; i--)
			{
				layerList [i].Draw (graphics);
			}
		}
	}
}

