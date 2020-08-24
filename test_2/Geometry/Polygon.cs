using System;
using CocosSharp;

using Xamarin.Forms;

namespace test_2.Geometry
{
	public class Polygon : CCNode
	{
		CCPoint[] points;
		CCPoint[] absolutePoints;
		public CCPoint LastCollisionPoint;
		float boundingRadius;


		float rotation;
		
		public new float Rotation
		{
			get
			{
				return rotation;
			}
			set
			{
				rotation = value;
				base.Rotation = rotation;
			}
		}

		public CCPoint[] Points
		{
			get
			{
				return points;
			}
			set
			{
				points = value;
				absolutePoints = new CCPoint[points.Length];
				ReactToPointsSet();
			}
		}

		public Polygon()
		{
		}

		public static Polygon CreateRectangle(float width, float height)
		{
			var polygon = new Polygon();

			var points = new CCPoint[] {
				new CCPoint(-width/2, -height/2),
				new CCPoint(-width/2, height/2),
				new CCPoint(width/2, height/2),
				new CCPoint(width/2, -height/2),
				new CCPoint(-width/2, -height/2)
			};

			polygon.Points = points;

			return polygon;
		}

		public bool CollideAgainst(Circle circle)
		{
			UpdateAbsolutePoints();

			

			if ((circle.Radius + boundingRadius) * (circle.Radius + boundingRadius) >

				(circle.PositionX - PositionX) * (circle.Position.X - Position.X) +
				(circle.PositionY - PositionY) * (circle.Position.Y - Position.Y))
			{
				
				if (IsPointInside(circle.PositionWorldspace.X, circle.PositionWorldspace.Y))
				{
					LastCollisionPoint.X = circle.Position.X;
					LastCollisionPoint.Y = circle.Position.Y;
					return true;
				}

				int i;
				
				for (i = 0; i < absolutePoints.Length; i++)
				{
					if (circle.IsPointInside(absolutePoints[i]))
					{
						LastCollisionPoint.X = absolutePoints[i].X;
						LastCollisionPoint.Y = absolutePoints[i].Y;
						return true;
					}
				}
				
			}
			return false;
		}

		private void ReactToPointsSet()
		{
			
			float boundingRadiusSquared = 0;
			for (int i = 0; i < points.Length; i++)
			{
				var p = points[i];
				boundingRadiusSquared =
					System.Math.Max(boundingRadiusSquared, (float)(p.X * p.X + p.Y * p.Y));
			}

			boundingRadius = (float)System.Math.Sqrt(boundingRadiusSquared);
		}

		void UpdateAbsolutePoints()
		{
			var absolutePosition = this.PositionWorldspace;

			var rotationInClockwiseRadians = CCMathHelper.ToRadians(this.Rotation);

			
			var rotationInRadians = -rotationInClockwiseRadians;

			CCPoint rotatedXAxis = new CCPoint((float)System.Math.Cos(rotationInRadians), (float)System.Math.Sin(rotationInRadians));
			CCPoint rotatedYAxis = new CCPoint(-rotatedXAxis.Y, rotatedXAxis.X);

			for (int i = 0; i < points.Length; i++)
			{

				absolutePoints[i] =
					absolutePosition +
					(rotatedXAxis * points[i].X) +
					(rotatedYAxis * points[i].Y);


			}
		}

		public bool IsPointInside(float x, float y)
		{
			bool b = false;

			for (int i = 0, j = absolutePoints.Length - 1; i < absolutePoints.Length; j = i++)
			{
				if ((((absolutePoints[i].Y <= y) && (y < absolutePoints[j].Y)) || ((absolutePoints[j].Y <= y) && (y < absolutePoints[i].Y))) &&
					(x < (absolutePoints[j].X - absolutePoints[i].X) * (y - absolutePoints[i].Y) / (absolutePoints[j].Y - absolutePoints[i].Y) + absolutePoints[i].X)) b = !b;
			}

			return b;
		}

		
	}

}

