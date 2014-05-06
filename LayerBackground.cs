using System;

namespace NubGameEngine
{
	public class LayerBackground : Layer
	{
		public LayerBackground ()
		{
		}
		
		public override void Update ()
		{
			Translate(0, 2);	
			
			base.Update ();
		}
	}
}

