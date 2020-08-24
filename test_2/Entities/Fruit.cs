using System;
using CocosSharp;
using test_2.Geometry;

using Xamarin.Forms;

namespace test_2.Entities
{
    public class Fruit : CCNode
    {
		CCSprite graphic;
		CCDrawNode debugGrahic;
		CCLabel extraPointsLabel;

		public CCPoint Velocity;
		public CCPoint Acceleration;

		
			
		

		
		public float Radius
		{
			get
			{
				return GameCoefficients.FruitRadius;
			}
		}

		public Circle Collision
		{
			get;
			private set;
		}

		FruitColor fruitColor;

		public FruitColor FruitColor
		{
			get
			{
				return fruitColor;
			}
			set
			{
				fruitColor = value;
				UpdateGraphics();
			}
		}

		public Fruit()
		{
			CreateFruitGraphic();

			if (GameCoefficients.ShowCollisionAreas)
			{
				CreateDebugGraphic();
			}

			CreateCollision();

			CreateExtraPointsLabel();

			Acceleration.Y = GameCoefficients.FruitGravity;
		}

		private void CreateFruitGraphic()
		{
			graphic = new CCSprite("images/cherry.png");
			graphic.IsAntialiased = false;
			this.AddChild(graphic);
		}

		private void CreateDebugGraphic()
		{
			debugGrahic = new CCDrawNode();
			this.AddChild(debugGrahic);
		}

		private void CreateCollision()
		{
			Collision = new Circle();
			Collision.Radius = GameCoefficients.FruitRadius;
			this.AddChild(Collision);
		}

		private void CreateExtraPointsLabel()
		{
			extraPointsLabel = new CCLabel("", "Arial", 12, CCLabelFormat.SystemFont);
			extraPointsLabel.IsAntialiased = false;
			extraPointsLabel.Color = CCColor3B.Black;
			this.AddChild(extraPointsLabel);
		}

		private void UpdateGraphics()
		{
			if (GameCoefficients.ShowCollisionAreas)
			{
				debugGrahic.Clear();
				const float borderWidth = 4;
				debugGrahic.DrawSolidCircle(
					CCPoint.Zero,
					GameCoefficients.FruitRadius,
					CCColor4B.Black);
				debugGrahic.DrawSolidCircle(
					CCPoint.Zero,
					GameCoefficients.FruitRadius - borderWidth,
					fruitColor.ToCCColor());
			}


			if (this.FruitColor == FruitColor.Yellow)
			{
				graphic.Texture = CCTextureCache.SharedTextureCache.AddImage("images/lemon.png");
				

			}
			else
			{
				if (this.FruitColor == FruitColor.Red)
                {
					graphic.Texture = CCTextureCache.SharedTextureCache.AddImage("images/cherry.png");
					

				}
                else
                {
					graphic.Texture = CCTextureCache.SharedTextureCache.AddImage("images/uva1.png");
					
				}
			}
		}

		public void Activity(float frameTimeInSeconds)
		{
			

			
			this.Velocity += Acceleration * frameTimeInSeconds;

			
			this.Velocity -= Velocity * GameCoefficients.FruitDrag * frameTimeInSeconds;

			this.Position += Velocity * frameTimeInSeconds;

		}

		

	}
}

