using System;
using Sce.PlayStation.Core.Graphics;
using System.Collections;

namespace NubGameEngine
{
	public class Sprite : GameObject
	{
		public float x { get { return vertices[0]; } }
		public float y { get { return vertices[1]; } }
		int _width = 0;
		public int width { get { return _width; } }
		int _height = 0;
		public int height { get { return _height; } }
		public bool hasAnimationFrames { get; private set; }
		
		public Texture2D texture;
		
		ShaderProgram shaderProgram;
		float[] vertices = new float[12];
		ushort[] indices;
		VertexBuffer vertexBuffer;
		int indexSize = 4;
		float currentFrame;
		
		float[] texcoords = {
			0.0f, 0.0f,	// 0 top left.
			0.0f, 1.0f,	// 1 bottom left.
			1.0f, 0.0f,	// 2 top right.
			1.0f, 1.0f,	// 3 bottom right.
		};

		float[] colors = {
			1.0f,	1.0f,	1.0f,	1.0f,	// 0 top left.
			1.0f,	1.0f,	1.0f,	1.0f,	// 1 bottom left.
			1.0f,	1.0f,	1.0f,	1.0f,	// 2 top right.
			1.0f,	1.0f,	1.0f,	1.0f,	// 3 bottom right.
		};
		
		/// <summary>
		/// Cria um Sprite simples nao animado.
		/// </summary>
		/// <param name='x'>
		/// Posiçao X onde sera criado o Sprite.
		/// </param>
		/// <param name='y'>
		/// Posiçao Y onde sera criado o Sprite.
		/// </param>
		/// <param name='texture'>
		/// A textura a ser aplicada.
		/// </param>
		public Sprite (float x, float y, Texture2D texture) : this(x, y, texture, 0, 0) {}
		
		/// <summary>
		/// Cria um Sprite com animaçao. Os frames devem estar dispostos todos um ao lado do outro na textura.
		/// </summary>
		/// <param name='x'>
		/// Posiçao X onde sera criado o Sprite.
		/// </param>
		/// <param name='y'>
		/// Posiçao Y onde sera criado o Sprite.
		/// </param>
		/// <param name='texture'>
		/// A textura a ser aplicada.
		/// </param>
		/// <param name='width'>
		/// Largura de um unico frame contido na textura.
		/// </param>
		/// <param name='height'>
		/// Altura de um unico frame contido na textura.
		/// </param>
		public Sprite (float x, float y, Texture2D texture, int width, int height) : base()
		{
			this.hasAnimationFrames = width > 0 && height > 0;
			this.texture = texture;
			this.shaderProgram = new ShaderProgram("/Application/shaders/Sprite.cgx");
			shaderProgram.SetUniformBinding(0, "u_ScreenMatrix");
			
			if (hasAnimationFrames)
			{
				InitializeSprite(x, y, width, height, true);
			}
			else
			{
				InitializeSprite(x, y, texture.Width, texture.Height, false);
			}
		}

		private void InitializeSprite(float x, float y, int width, int height, bool divideSprite)
		{
			this._width = width;
			this._height = height;
			
			vertices[0] = x;	// x0
			vertices[1] = y;	// y0
			vertices[2] = 0.0f;	// z0

			vertices[3] = x;	// x1
			vertices[4] = y + height;	// y1
			vertices[5] = 0.0f;	// z1

			vertices[6] = x + width;	// x2
			vertices[7] = y;	// y2
			vertices[8] = 0.0f;	// z2

			vertices[9] = x + width;	// x3
			vertices[10] = y + height;	// y3
			vertices[11] = 0.0f;	// z3
			
			indices = new ushort[indexSize];
			indices[0] = 0;
			indices[1] = 1;
			indices[2] = 2;
			indices[3] = 3;
			
			if (divideSprite)
			{
				// 0 top left.
				texcoords[0] = 0.0f;
				texcoords[1] = 0.0f;
				
				// 1 bottom left.
				texcoords[2] = 0.0f;
				texcoords[3] = 1.0f;
				
				// 2 top right.
				texcoords[4] = width / texture.Width;
				texcoords[5] = 0.0f;
				
				// 3 bottom right.
				texcoords[6] = width / texture.Width;
				texcoords[7] = 1.0f;
				
				SetFrame(0);
			}

			//												vertex pos,               texture,       color
			vertexBuffer = new VertexBuffer(4, indexSize, VertexFormat.Float3, VertexFormat.Float2, VertexFormat.Float4);
			//SetPosition(x, y);
		}
		
		/// <summary>
		/// Atualiza o Sprite.
		/// </summary>
		public override void Update ()
		{
			base.Update();
			
		}
		
		/// <summary>
		/// Desenha o Sprite na tela usando o GraphicsContext especificado.
		/// </summary>
		/// <param name='graphics'>
		/// Contexto grafico em que o Sprite sera mostrado
		/// </param>
		public override void Draw(GraphicsContext graphics)
		{
			base.Draw(graphics);
			
			graphics.SetShaderProgram(shaderProgram);
			graphics.SetTexture(0, texture);
			shaderProgram.SetUniformValue(0, ref Game.screenMatrix);
			
			// Atualizar a posiçao dos vertices.
			vertexBuffer.SetVertices(0, vertices);
			vertexBuffer.SetVertices(1, texcoords);
			vertexBuffer.SetVertices(2, colors);

			vertexBuffer.SetIndices(indices);
			Game.graphics.SetVertexBuffer(0, vertexBuffer);
			graphics.DrawArrays(DrawMode.TriangleStrip, 0, indexSize);
		}
		
		/// <summary>
		/// Adiciona os valores de X e Y a posiçao do Sprite.
		/// </summary>
		/// <param name='x'>
		/// Quanto o Sprite sera movido em X.
		/// </param>
		/// <param name='y'>
		/// Quanto o Sprite sera movido em Y.
		/// </param>
		public void Translate(float x, float y)
		{
			vertices[0] += x;	// x0
			vertices[1] += y;	// y0
			vertices[2] = 0.0f;	// z0

			vertices[3] += x;	// x1
			vertices[4] += y;	// y1
			vertices[5] = 0.0f;	// z1

			vertices[6] += x;	// x2
			vertices[7] += y;	// y2
			vertices[8] = 0.0f;	// z2

			vertices[9] += x; // x3
			vertices[10] += y; // y3
			vertices[11] = 0.0f;	// z3
		}
		
		/// <summary>
		/// Muda o Sprite para a posiçao designada.
		/// </summary>
		/// <param name='x'>
		/// X.
		/// </param>
		/// <param name='y'>
		/// Y.
		/// </param>
		public void SetPosition(float x, float y)
		{			
			vertices[0] = x;	// x0
			vertices[1] = y;	// y0
			//vertices[2]=0.0f;	// z0

			vertices[3] = x;	// x1
			vertices[4] = _height + y;	// y1
			//vertices[5]=0.0f;	// z1

			vertices[6] = _width + x;	// x2
			vertices[7] = y;	// y2
			//vertices[8]=0.0f;	// z2

			vertices[9] = _width + x;	// x3
			vertices[10] = _height + y;	// y3
			//vertices[11]=0.0f;	// z3
		}
		
		/// <summary>
		/// Muda as coordenadas da textura do Sprite para o frame de indice especificado.
		/// </summary>
		/// <param name='index'>
		/// Indice do frame a ser mostrado.
		/// </param>
		/// <exception cref='ApplicationException'>
		/// Eh lancada quando o indice eh maior que indice maximo do Sprite.
		/// </exception>
		public void SetFrame (float index)
		{
			if (currentFrame == index)
				return;
			
			if (index > texture.Width / _width)
			{
				throw new ApplicationException("O sprite "+ToString()+" nao possui um frame de numero "+index);
			}
			
			currentFrame = index;
			// 0 top left.
			texcoords[0] = (index * (float)_width) / (float)texture.Width;
			texcoords[1] = 0.0f;
			
			// 1 bottom left.
			texcoords[2] = (index * _width) / texture.Width;
			texcoords[3] = 1.0f;
			
			// 2 top right.
			texcoords[4] = (_width + index * _width) / texture.Width;
			texcoords[5] = 0.0f;
			
			// 3 bottom right.
			texcoords[6] = (_width + index * _width) / texture.Width;
			texcoords[7] = 1.0f;
		}
	}
	
	
}

