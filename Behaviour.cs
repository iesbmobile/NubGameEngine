using System;

namespace NubGameEngine
{
	public class Behaviour
	{
		protected Sprite sprite;
		
		public Behaviour (Sprite sprite)
		{
			sprite.AddBehaviour(this);
		}
		
		public virtual void Update ()
		{
		}
		
		public virtual void OnCollision (Collider other)
		{
		}
	}
}

