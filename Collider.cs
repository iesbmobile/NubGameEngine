using System;

namespace NubGameEngine
{
	public class Collider
	{
		public Sprite sprite { get; private set; }
		
		public float[] collVertices { get; private set; }
		
		int halfWidth;
		public int width { get { return halfWidth * 2; } set { halfWidth = value / 2; } }
		int halfHeight;
		public int height { get { return halfHeight * 2; } set { halfHeight = value / 2; } }
		
		public Collider (Sprite originSprite, int width, int height)
		{
			collVertices = new float[4];
			sprite = originSprite;
			halfWidth = width / 2;
			halfHeight = height / 2;
			UpdateAccordingToSprite ();
			Game.colliderList.Add(this);
		}
		
		public void UpdateAccordingToSprite ()
		{
			collVertices [0] = sprite.x - halfWidth;
			collVertices [1] = sprite.y - halfHeight;
			collVertices [2] = sprite.x + halfWidth;
			collVertices [3] = sprite.y + halfHeight;
		}
		
		public void CheckCollision (Collider other)
		{
			bool collision = !(other.collVertices [0] > collVertices [2] || 
			          other.collVertices [2] > collVertices [0] || 
			          other.collVertices [1] > collVertices [3] ||
			          other.collVertices [3] > collVertices [1]);
			if (collision)
			{
				sprite.OnCollision (other);
				other.sprite.OnCollision(this);
			}
		}
	}
}

