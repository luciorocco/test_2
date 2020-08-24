using System;
using CocosSharp;

using Xamarin.Forms;

namespace test_2.Entities
{
    public class ScoreIncreaseText : CCNode
    {
		CCLabel label2;

		CCDrawNode background2;

		public int bounch_lost1
		{
			set
			{
				label2.Text = "bounch_lost: " + value;
			}
		}

		public ScoreIncreaseText()
		{
			background2 = new CCDrawNode();

			const int width2 = 120;
			const int height2 = 27;

			background2.DrawRect(new CCRect(-5, -height2,
				width2, height2),
				new CCColor4B(100, 100, 100));

			this.AddChild(background2);


			label2 = new CCLabel("bounch_lost: 9999", "Arial", 20, CCLabelFormat.SystemFont);
			label2.AnchorPoint = new CCPoint(0, 1);
			this.AddChild(label2);
		}

	}
}

