using System;
using CocosSharp;
using Xamarin.Forms;
using test_2.Geometry;
using System.Collections.Generic;

namespace test_2.Entities
{
    public class Tractor : CCNode
    {
        
        CCDrawNode debugGraphic;


        CCPoint desiredLocation;
        public CCPoint Velocity;

        CCSprite graphic;

        public Polygon Polygon
        {
            get;
            private set;
        }

        const float width = 35 * 1.5f;
        const float height = 32 * 1.5f;

        public Tractor()
        {
            
            CreateSpriteGraphic();

            if (GameCoefficients.ShowCollisionAreas)
            {
                CreateDebugGraphic();
            }

            CreateCollision();

            

        }

        private void CreateDebugGraphic()
        {
            debugGraphic = new CCDrawNode();

            debugGraphic.DrawRect(
                new CCRect(-width / 2 , -height / 2 , width, height),
                fillColor: new CCColor4B(180, 180, 180, 180));

            this.AddChild(debugGraphic);
        }

        private void CreateCollision()
        {
            Polygon = Polygon.CreateRectangle(width, height);
            this.AddChild(Polygon);
        }

        private void CreateSpriteGraphic()
        {
            graphic = new CCSprite("images/trattore1.png");
            graphic.IsAntialiased = false;
            graphic.Scale = 1.5f;


            this.AddChild(graphic);
        }

        internal void SetDesiredPositionToCurrentPosition()
        {
            desiredLocation.X = this.PositionX;
            desiredLocation.Y = this.PositionY;
            
        }

        public void HandleInput(CCPoint touchPoint)
        {
            desiredLocation = touchPoint;
        }



        public void Activity(float frameTimeInSeconds)
        {
            
            const float velocityCoefficient = 2;

            
            Velocity = (desiredLocation - this.Position) * velocityCoefficient;

            this.Position += Velocity * frameTimeInSeconds;
            
        }

        
    }
}

