using System;
using Sce.PlayStation.Core.Graphics;
using System.Collections;

namespace NubGameEngine
{
	public class Sprite : GameObject
	{
		public float x { get; private set; }
		public float y { get; private set; }
		
		public Texture2D texture;
		
		ShaderProgram shaderProgram;
		float[] vertices = new float[12];
		ushort[] indices;
		VertexBuffer vertexBuffer;
		int indexSize = 4;
		
		Hashtable animations = new Hashtable();

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
		
		public Sprite (float x, float y, Texture2D texture) : base()
		{
			ShaderProgram shaderProgram = new ShaderProgram("/Application/shaders/Sprite.cgx");
			shaderProgram.SetUniformBinding(0, "u_ScreenMatrix");
			InitializeSprite(x, y, texture, shaderProgram);
		}

		private void InitializeSprite(float x, float y, Texture2D texture, ShaderProgram shaderProgram)
		{
			this.texture = texture;
			this.shaderProgram = shaderProgram;
			
			vertices[0] = 0.0f;	// x0
			vertices[1] = 0.0f;	// y0
			vertices[2] = 0.0f;	// z0

			vertices[3] = 0.0f;	// x1
			vertices[4] = texture.Height;	// y1
			vertices[5] = 0.0f;	// z1

			vertices[6] = texture.Width;	// x2
			vertices[7] = 0.0f;	// y2
			vertices[8] = 0.0f;	// z2

			vertices[9] = texture.Width;	// x3
			vertices[10] = texture.Height;	// y3
			vertices[11] = 0.0f;	// z3
			
			indices = new ushort[indexSize];
			indices[0] = 0;
			indices[1] = 1;
			indices[2] = 2;
			indices[3] = 3;

			//												vertex pos,               texture,       color
			vertexBuffer = new VertexBuffer(4, indexSize, VertexFormat.Float3, VertexFormat.Float2, VertexFormat.Float4);
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
			vertices[4] = texture.Height + y;	// y1
			//vertices[5]=0.0f;	// z1

			vertices[6] = texture.Width + x;	// x2
			vertices[7] = y;	// y2
			//vertices[8]=0.0f;	// z2

			vertices[9] = texture.Width + x;	// x3
			vertices[10] = texture.Height + y;	// y3
			//vertices[11]=0.0f;	// z3
		}
		
		public int SetAnimation (string name, float duration, params Texture2D[] frames)
		{
			return -1;
		}
		
		public void PlayAnimation (string name)
		{
			
		}
	}
	
	
}

