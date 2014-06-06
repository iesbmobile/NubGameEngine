using System;

namespace NubGameEngine
{
	public class BehaviourDestroyable : Behaviour
	{
		public int hp = 100;
		public string enemyTag;
		
		public BehaviourDestroyable (string enemyTag)
		{
			this.enemyTag = enemyTag;
		}
		
		public override void OnCollision (Collider other)
		{
			base.OnCollision (other);
			if (other.sprite.tag == enemyTag)
			{
				hp -= 10;
				sprite.FlashForMiliseconds(500);
				
				if (hp <= 0)
				{
					sprite.active = false;
				}
			}
		}
	}
}

