using System;
using System.Collections.Generic;

namespace NubGameEngine
{
	public class Game
	{
		protected static GraphicsContext graphics;
		public static ImageRect rectScreen;
		public static Matrix4 screenMatrix;
		public static List<Sprite> spriteList;
		
		public Game ()
		{
			graphics = new GraphicsContext();
			rectScreen = graphics.Screen.Rectangle;
				
			screenMatrix = new Matrix4(
				 2.0f / rectScreen.Width, 0.0f, 0.0f, 0.0f,
				 0.0f, -2.0f / rectScreen.Height, 0.0f, 0.0f,
				 0.0f, 0.0f, 1.0f, 0.0f,
				 -1.0f, 1.0f, 0.0f, 1.0f
			);
		}
		
		public void Run ()
		{
			while (true)
			{
				SystemEvents.CheckEvents();
				Update();
				Render();
			}
		}
		
		void Update ()
		{
			foreach (Sprite s in spriteList)
			{
				s.Update();
			}
		}
		
		void Render ()
		{
			foreach (Sprite s in spriteList)
			{
				s.Draw(graphics);
			}
		}
	}
}

