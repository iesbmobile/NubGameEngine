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
			if (levelList.Count == 0)
			{
				throw new ApplicationException("Nao ha nenhum Level criado. Eh necessario adicionar pelo menos um Level ao Game para inicia-lo. Game.cs Run()");
			}
			
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
			graphics.Clear();
			
			levelList[currentLevel].Draw(graphics);
			
			graphics.SwapBuffers();
		}
	}
}

