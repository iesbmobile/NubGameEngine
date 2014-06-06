using System;

namespace NubGameEngine
{
	public class LayerBackground : Layer
	{		
		public override void Update ()
		{
			Translate(0, 2);	
			
			base.Update ();
		}
	}
}

