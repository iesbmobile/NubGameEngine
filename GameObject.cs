using System;
using Sce.PlayStation.Core.Graphics;

namespace NubGameEngine
{
	/// <summary>
	/// Classe base para todos os elementos do jogo.
	/// </summary>
	public class GameObject
	{
		
		public GameObject ()
		{
		}
		
		public virtual void Update ()
		{
		}

		public virtual void Draw(GraphicsContext graphics)
		{
		}
	}
}

