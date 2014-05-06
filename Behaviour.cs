using System;

namespace NubGameEngine
{
	public class Behaviour
	{
		protected Sprite sprite;
		
		public Behaviour ()
		{
		}
		
		public void SetSprite (Sprite sprite)
		{
			this.sprite = sprite;
		}
		
		public virtual void Update ()
		{
		}
		
		public virtual void OnCollision (Collider other)
		{
		}
	}
}

