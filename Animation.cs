using System;
using Sce.PlayStation.Core.Graphics;

namespace NubGameEngine
{
	public class Animation
	{
		public int id;
		public string name;
		public float duration;
		public Texture2D[] frames;
		
		public Animation (int id, string name, float duration, params Texture2D[] frames)
		{
			this.id = id;
			this.name = name;
			this.duration = duration;
			this.frames = frames;
		}
		
		public Texture2D GetNextFrame ()
		{
			return null;
		}
	}
}

