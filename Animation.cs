using System;
using Sce.PlayStation.Core.Graphics;

namespace NubGameEngine
{
	/// <summary>
	/// Classe que guarda informaçoes de uma animaçao. Quando estanciada, eh necessario apontar um <see cref="NubGameEngine.Sprite"/> alvo.
	/// </summary>
	public class Animation
	{
		public Sprite targetSprite;
		public string name;
		public long frameDuration;
		public int startIndex;
		public int endIndex;
		public bool loop = false;
		
		bool _playing = false;
		public bool playing { get { return _playing; } }
		long startTime;
		
		/// <summary>
		/// Inicializa uma nova Animaçao.
		/// </summary>
		/// <param name='targetSprite'>
		/// Referencia ao Sprite que sera animado.
		/// </param>
		/// <param name='name'>
		/// Nome da animaçao.
		/// </param>
		/// <param name='frameDuration'>
		/// Duraçao de cada frame (em milisegundos).
		/// </param>
		/// <param name='startIndex'>
		/// Indice do frame a partir do qual começa esta animaçao.
		/// </param>
		/// <param name='endIndex'>
		/// Indice do frame em que termina esta animaçao.
		/// </param>
		public Animation (ref Sprite targetSprite, string name, long frameDuration, int startIndex, int endIndex)
		{
			this.targetSprite = targetSprite;
			this.name = name;
			this.frameDuration = frameDuration;
			this.startIndex = startIndex;
			this.endIndex = endIndex;
		}
		
		/// <summary>
		/// Toca a animaçao. Ao final, vai repetir ou nao dependendo do valor da variavel 'loop'. <seealso cref="Play(bool)"/>
		/// </summary>
		public void Play ()
		{
			if (!_playing)
			{
				startTime = Game.time;
				_playing = true;
			}
			if (!Game.playingAnimationsList.Contains(this))
				Game.playingAnimationsList.Add(this);
		}
		
		/// <summary>
		/// Toca a animacao ja atribuindo antes o valor da variavel 'loop'.
		/// </summary>
		/// <param name='loop'>
		/// true = Repete a animaçao quando chegar ao final.
		/// false = Nao repete a animaçao quando chegar ao final.
		/// </param>
		public void Play (bool loop)
		{
			this.loop = loop;
			Play();
		}
		
		/// <summary>
		/// Para a animaçao. O Sprite ficara com o ultimo frame mostrado no momento que este metodo foi chamado.
		/// </summary>
		public void Stop ()
		{
			_playing = false;
		}
		
		/// <summary>
		/// Metodo invocado automaticamente pela instancia de Game rodando e atualiza todas as animacoes ativas.
		/// </summary>
		public void UpdateAnimation()
		{
			long elapsedTime = Game.time - startTime;
			
			if (!loop && elapsedTime > (endIndex - startIndex) * frameDuration)
			{
				Stop();
				return;
			}
			
			int nextFrame = unchecked ( (int) ( (elapsedTime % ((endIndex - startIndex) * frameDuration)) / frameDuration ) );
			targetSprite.SetFrame(nextFrame);
		}
	}
}

