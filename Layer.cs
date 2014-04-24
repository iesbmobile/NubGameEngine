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
				s.Draw (graphics);
			}
		}
		
		/// <summary>
		/// Translate all Sprites within this Layer by specified x and y.
		/// </summary>
		/// <param name='x'>
		/// X value to be added.
		/// </param>
		/// <param name='y'>
		/// Y value to be added.
		/// </param>
		public void Translate (float x, float y)
		{
			foreach (Sprite s in spriteList)
			{
				s.Translate (x, y);
			}
		}
		
		/// <summary>
		/// Gets all Sprites with tag in this Layer.
		/// </summary>
		/// <returns>
		/// Array containing all Sprites in this layer with tag.
		/// </returns>
		/// <param name='tag'>
		/// Tag to be searched for.
		/// </param>
		public Sprite[] GetAllSpritesWithTag (string tag)
		{
			List<Sprite> returnList = new List<Sprite> ();
			for (int i = 0; i < spriteList.Count; i++)
			{
				if (spriteList [i].tag == tag)
				{
					returnList.Add (spriteList [i]);
				}
			}
			
			if (returnList.Count > 0)
			{
				return returnList.ToArray ();
			}
			return null;
		}
	}
}

