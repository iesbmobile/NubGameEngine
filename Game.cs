using System;
using System.Collections.Generic;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;

namespace NubGameEngine
{	
	/// <summary>
	/// Instancia do jogo rodando. Deve ter apenas um ativo a cada execuçao do programa.
	/// </summary>
	/// <exception cref='ApplicationException'>
	/// Eh lancado quando o metodo Run() eh chamado sem que haja um Level adicionado.
	/// </exception>
	public class Game
	{
		public static GraphicsContext graphics;
		public static ImageRect rectScreen;
		public static Matrix4 screenMatrix;
		
		
		public static List<Level> levelList = new List<Level>();
		public static List<Animation> playingAnimationsList = new List<Animation>();
		public static List<Collider> colliderList = new List<Collider>();
		
		/// <summary>
		/// Tempo desde o inicio do programa em milisegundos.
		/// </summary>
		public static long time
		{
			get 
			{
				return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
			}
		}
		
		static int currentLevel = 0;
		
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
			
			Level emptyLevel = new Level();
		}
		
		public void Run ()
		{
//			if (levelList.Count == 0)
//			{
//				throw new ApplicationException ("Nao ha nenhum Level criado. Eh necessario adicionar pelo menos um Level ao Game para inicia-lo. Game.cs Run()");
//			}
			
			while (true)
			{
				SystemEvents.CheckEvents ();
				Update ();
				UpdatePhysics();
				UpdateAnimations ();
				Render ();
			}
		}
		
		protected virtual void Update ()
		{
			levelList[currentLevel].Update();
		}
		
		protected virtual void Render ()
		{
			graphics.Clear();
			
			levelList[currentLevel].Draw(graphics);
			
			graphics.SwapBuffers();
		}
		
		void UpdateAnimations ()
		{
			for (int i = 0; i < Game.playingAnimationsList.Count; i++)
			{
				if (Game.playingAnimationsList [i].playing)
				{
					Game.playingAnimationsList [i].UpdateAnimation ();
				}
				else
				{
					Game.playingAnimationsList.RemoveAt (i);
					i--;
				}
			}
		}
		
		void UpdatePhysics ()
		{
			for (int i = 0; i < colliderList.Count; i++)
			{
				Game.colliderList[i].UpdateAccordingToSprite ();
			}
			
			for (int i = 0; i < colliderList.Count; i++)
			{
				for (int j = i+1; j < colliderList.Count; j++)
				{
					Game.colliderList[i].CheckCollision(colliderList [j]);
				}
			}
		}
		
		public static void SetLevel (int index)
		{
			if (index >= levelList.Count)
			{
				return;
			}
			currentLevel = index;
		}
	}
}

