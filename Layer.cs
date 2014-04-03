using System;
using System.Collections.Generic;
using Sce.PlayStation.Core.Graphics;

namespace NubGameEngine
{
	/// <summary>
	/// A Layer representa uma camada de jogo. Cada grupo de Sprites sera mostrado em uma layer diferente, de acordo
	/// com a ordem que eles devem ser mostrados.
	/// Exemplo: O fundo do cenario do jogo ficara em uma layer (que sera desenhada primeiro), os objetos que interagem
    /// com o personagem principal em outra layer e o proprio personagem e os inimigos em outra layer.
	/// </summary>
	public class Layer : GameObject
	{
		List<Sprite> spriteList = new List<Sprite>();
		
		public Layer () : base() {}
		
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

