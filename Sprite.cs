using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using System.Collections;
using System.Collections.Generic;

namespace NubGameEngine
{
	public class Sprite : GameObject
	{
		public float x { get { return vertices[0] - xOffset; } }
		public float y { get { return vertices[1] - yOffset; } }
		
		Pivot _pivot = Pivot.TopLeft;
		public Pivot pivot
		{
			get { return _pivot; }
			set
			{
				if (value == _pivot)
					return;
				
				switch (value)
				{
				case Pivot.TopLeft:
					xOffset = 0;
					yOffset = 0;
					break;
				case Pivot.Top:
					xOffset = -width/2;
					yOffset = 0;
					break;
				case Pivot.TopRight:
					xOffset = -width;
					yOffset = 0;
					break;
				case Pivot.Left:
					xOffset = 0;
					yOffset = -height/2;
					break;
				case Pivot.Center:
					xOffset = -width/2;
					yOffset = -height/2;
					break;
				case Pivot.Right:
					xOffset = -width;
					yOffset = -height/2;
					break;
				case Pivot.BottomLeft:
					xOffset = 0;
					yOffset = -height;
					break;
				case Pivot.Bottom:
					xOffset = -width/2;
					yOffset = -height;
					break;
				case Pivot.BottomRight:
					xOffset = -width;
					yOffset = -height;
					break;
				}
				_pivot = value;
			}
		}
		float xOffset = 0;
		float yOffset = 0;
		
		float _width = 0;
		public float width { get { return _width * scale.X; } }
		float _height = 0;
		public float height { get { return _height * scale.Y; } }
		public bool hasAnimationFrames { get; private set; }
		
		public Texture2D texture;
		
		public string tag = "Untagged";
		public bool active = true;
		
		protected Collider collider = null;
		public List<Behaviour> behaviourList = new List<Behaviour>();
		public List<Sprite> children = new List<Sprite>();
		
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
		
		Vector2 scale = new Vector2(1, 1);
		
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
		
		public Sprite (float x, float y, Texture2D texture, Pivot pivot) : this(x, y, texture, 0, 0)
		{
			this.pivot = pivot;
			SetPosition(vertices[0], vertices[1]);
		}
		
		public Sprite (float x, float y, Texture2D texture, float width, float height, Pivot pivot) : this(x, y, texture, width, height)
		{
			this.pivot = pivot;
			SetPosition(vertices[0], vertices[1]);
		}
		
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
		public Sprite (float x, float y, Texture2D texture, float width, float height) : base()
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

		private void InitializeSprite(float x, float y, float width, float height, bool divideSprite)
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
			base.Update ();
			
			if (active)
			{
				for (int i = 0; i < behaviourList.Count; i++)
				{
					behaviourList [i].Update ();
				}
			}
		}
		
		/// <summary>
		/// Desenha o Sprite na tela usando o GraphicsContext especificado.
		/// </summary>
		/// <param name='graphics'>
		/// Contexto grafico em que o Sprite sera mostrado
		/// </param>
		public override void Draw (GraphicsContext graphics)
		{
			base.Draw (graphics);
			
			if (active)
			{
				graphics.Enable (EnableMode.Blend);
				graphics.SetBlendFunc (BlendFuncMode.Add, BlendFuncFactor.SrcAlpha, BlendFuncFactor.OneMinusSrcAlpha);
			
				graphics.SetShaderProgram (shaderProgram);
				graphics.SetTexture (0, texture);
				shaderProgram.SetUniformValue (0, ref Game.screenMatrix);
			
				// Atualizar a posiçao dos vertices.
				vertexBuffer.SetVertices (0, vertices);
				vertexBuffer.SetVertices (1, texcoords);
				vertexBuffer.SetVertices (2, colors);

				vertexBuffer.SetIndices (indices);
				Game.graphics.SetVertexBuffer (0, vertexBuffer);
				graphics.DrawArrays (DrawMode.TriangleStrip, 0, indexSize);
			
				for (int i = 0; i < children.Count; i++)
				{
					children [i].Draw (graphics);
				}
			}
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
		public void Translate (float x, float y)
		{
			vertices [0] += x;	// x0
			vertices [1] += y;	// y0
			vertices [2] = 0.0f;	// z0

			vertices [3] += x;	// x1
			vertices [4] += y;	// y1
			vertices [5] = 0.0f;	// z1

			vertices [6] += x;	// x2
			vertices [7] += y;	// y2
			vertices [8] = 0.0f;	// z2

			vertices [9] += x; // x3
			vertices [10] += y; // y3
			vertices [11] = 0.0f;	// z3
			
			for (int i = 0; i < children.Count; i++)
			{
				children [i].Translate(x, y);
			}
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
		public void SetPosition (float x, float y)
		{
			Translate (x - this.x, y - this.y);
			
			/*
			vertices [0] = x + xOffset;	// x0
			vertices [1] = y + yOffset;	// y0
			//vertices[2]=0.0f;	// z0

			vertices [3] = x + xOffset;	// x1
			vertices [4] = _height + y + yOffset;	// y1
			//vertices[5]=0.0f;	// z1

			vertices [6] = _width + x + xOffset;	// x2
			vertices [7] = y + yOffset;	// y2
			//vertices[8]=0.0f;	// z2

			vertices [9] = _width + x + xOffset;	// x3
			vertices [10] = _height + y + yOffset;	// y3
			//vertices[11]=0.0f;	// z3
			*/
		}
		
		public void SetScale (float x, float y)
		{
			scale.X = x;
			scale.Y = y;
			
			vertices [0] -= scale.X / 2;	// x0
			vertices [1] -= scale.Y / 2;	// y0
			//vertices[2]=0.0f;	// z0

			vertices [3] -= scale.X / 2;	// x1
			vertices [4] += scale.Y / 2;		// y1
			//vertices[5]=0.0f;	// z1

			vertices [6] += scale.X / 2;	// x2
			vertices [7] -= scale.Y / 2;	// y2
			//vertices[8]=0.0f;	// z2

			vertices [9] += scale.X / 2;	// x3
			vertices [10] += scale.Y / 2;	// y3
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
			{
				return;
			}
			
			if (index > texture.Width / _width)
			{
				throw new ApplicationException ("The Sprite " + ToString () + " doesn't have a frame of index " + index);
			}
			
			currentFrame = index;
			// 0 top left.
			texcoords [0] = (index * (float)_width) / (float)texture.Width;
			texcoords [1] = 0.0f;
			
			// 1 bottom left.
			texcoords [2] = (index * _width) / texture.Width;
			texcoords [3] = 1.0f;
			
			// 2 top right.
			texcoords [4] = (_width + index * _width) / texture.Width;
			texcoords [5] = 0.0f;
			
			// 3 bottom right.
			texcoords [6] = (_width + index * _width) / texture.Width;
			texcoords [7] = 1.0f;
		}
		
		public void AddCollider (int width, int height)
		{
			collider = new Collider(this, width, height);
		}
		
		public virtual void OnCollision (Collider other)
		{
			Console.WriteLine ("Collision " + this.ToString ());
			for (int i = 0; i < behaviourList.Count; i++)
			{
				behaviourList [i].OnCollision (other);
			}
		}
		
		/// <summary>
		/// Adds a child Sprite to this Sprite. The child will receive Update and Draw calls and will be translated when Translate is called in this Sprite.
		/// </summary>
		/// <param name='child'>
		/// New child Sprite to be added.
		/// </param>
		public void AddChild (Sprite child)
		{
			children.Add(child);
		}
		
		/// <summary>
		/// Gets the child with tag.
		/// </summary>
		/// <returns>
		/// The child with tag or null if it didn't find any.
		/// </returns>
		/// <param name='tag'>
		/// Tag.
		/// </param>
		public Sprite GetChildWithTag (string tag)
		{
			for (int i = 0; i < children.Count; i++)
			{
				if (children[i].tag == tag)
					return children[i];
			}
			return null;
		}
		
		/// <summary>
		/// Gets the children with tag.
		/// </summary>
		/// <returns>
		/// The children with tag or null if didn't find any.
		/// </returns>
		/// <param name='tag'>
		/// Tag.
		/// </param>
		public Sprite[] GetChildrenWithTag (string tag)
		{
			List<Sprite> tagSprites = new List<Sprite>();
			for (int i = 0; i < children.Count; i++)
			{
				if (children[i].tag == tag)
					tagSprites.Add(children[i]);
			}
			if (tagSprites.Count == 0)
				return null;
			return tagSprites.ToArray();
		}
		
		/// <summary>
		/// Adds a Behaviour to the Sprite.
		/// </summary>
		/// <param name='newBehaviour'>
		/// New behaviour.
		/// </param>
		public void AddBehaviour (Behaviour newBehaviour)
		{
			behaviourList.Add (newBehaviour);
			newBehaviour.SetSprite(this);
		}
		
		public void SetAlpha (float alpha)
		{
			colors[3] = colors[7] = colors[11] = colors[15] = alpha;
		}
		
		
	}
	
}

