using System;

namespace NubGameEngine
{
	public class BehaviourShot : Behaviour
	{
		public float speed = 8;
				
		public override void Update ()
		{
			sprite.Translate(0, -speed);
			
			if (sprite.y < -10)
				sprite.active = false;
		}
		
		public override void OnCollision (Collider other)
		{
			base.OnCollision (other);
			
			if (other.sprite.tag != sprite.tag)
			{
				sprite.active = false;
			}
		}
	}
}