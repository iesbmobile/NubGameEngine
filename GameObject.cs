using System;
using Sce.PlayStation.Core.Graphics;
using System.Collections.Generic;

namespace NubGameEngine
{
	/// <summary>
	/// Classe base para todos os elementos do jogo.
	/// </summary>
	public class GameObject
	{
		List<Component> components = new List<Component>();
		
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

