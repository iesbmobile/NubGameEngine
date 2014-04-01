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
		public int width { get {return _width;} }
		int _height = 0;
		public int height { get {return _height;} }
		public bool hasAnimationFrames { get; private set; }
		
		public Texture2D texture;
		
		ShaderProgram shaderProgram;
		float[] vertices = new float[12];
		ushort[] indices;
		VertexBuffer vertexBuffer;
		int indexSize = 4;
		
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
		
		public Sprite (float x, float y, Texture2D texture) : this(x, y, texture, 0, 0)
		{
		}
		
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
			
			vertices[0] = 0.0f;	// x0
			vertices[1] = 0.0f;	// y0
			vertices[2] = 0.0f;	// z0

			vertices[3] = 0.0f;	// x1
			vertices[4] = height;	// y1
			vertices[5] = 0.0f;	// z1

			vertices[6] = width;	// x2
			vertices[7] = 0.0f;	// y2
			vertices[8] = 0.0f;	// z2

			vertices[9] = width;	// x3
			vertices[10] = height;	// y3
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
			}

			//												vertex pos,               texture,       color
			vertexBuffer = new VertexBuffer(4, indexSize, VertexFormat.Float3, VertexFormat.Float2, VertexFormat.Float4);
			SetPosition(x, y);
		}
		
		public override void Update ()
		{
			
			// Atualizar a posiçao dos vertices.
			vertexBuffer.SetVertices(0, vertices);
			vertexBuffer.SetVertices(1, texcoords);
			vertexBuffer.SetVertices(2, colors);

			vertexBuffer.SetIndices(indices);
			Game.graphics.SetVertexBuffer(0, vertexBuffer);
		}

		public override void Draw(GraphicsContext graphics)
		{
			graphics.SetShaderProgram(shaderProgram);
			graphics.SetTexture(0, texture);
			shaderProgram.SetUniformValue(0, ref Game.screenMatrix);

			graphics.DrawArrays(DrawMode.TriangleStrip, 0, indexSize);
		}

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
		
		public void SetFrame (int index)
		{
			if (index > texture.Width / _width)
			{
				throw new ApplicationException("O sprite "+ToString()+" nao possui um frame de numero "+index);
			}
			
			// 0 top left.
			texcoords[0] = index * _width;
			texcoords[1] = 0.0f;
			
			// 1 bottom left.
			texcoords[2] = index * _width;
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

