using System;
using System.Collections.Generic;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;

namespace NubGameEngine
{
	public class Game
	{
		public static GraphicsContext graphics;
		public static ImageRect rectScreen;
		public static Matrix4 screenMatrix;
		public static List<Level> levelList = new List<Level>();
		int currentLevel = 0;
		
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
			levelList[currentLevel].Update();
		}
		
		void Render ()
		{
			levelList[currentLevel].Draw(graphics);
		}
	}
}

