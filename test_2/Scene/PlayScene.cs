using System;
using CocosSharp;
using test_2.Geometry;

using System.Collections.Generic;

using test_2.Entities;

using Xamarin.Forms;

namespace test_2.Scene
{
    public class PlayScene : CCScene
    {
        private bool hasGameEnded;
        int score = 0;

        CCLayer BackgroundLayer;
        CCLayer hudLayer;
        CCLayer gameplayLayer;

        int count = 3;
        
        CCSprite exit;

        CCLabel debugLabel;

        ScoreText scoreText;
        ScoreIncreaseText bounch_lost;

        Tractor tractor;

        FruitSpawner spawner;

        List<Fruit> fruitList;



        


        public PlayScene (CCGameView gameview):base(gameview)
        {
            CreateLayers();

            fruitList = new List<Fruit>();

            CreateBackground();

            //CreateExit();

            

            CreateTractor();

            CreateTouchListener();

            CreateHud();

            CreateSpawner();

            CreateDebugLabel();

            Schedule(Activity);

        }

        

        private void Activity(float frameTimeInSeconds)
        {
            
            tractor.Activity(frameTimeInSeconds);

            foreach (var fruit in fruitList)
            {
                fruit.Activity(frameTimeInSeconds);
            }

            spawner.Activity(frameTimeInSeconds);

            DebugActivity();

            PerformCollision();
        }


        


        private void CreateTouchListener()
        {
            

            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = OnTouchEnded;
            
            gameplayLayer.AddEventListener(touchListener);
        }

        void OnTouchEnded(List<CCTouch> touches, CCEvent touchEvent)

        {
            if (touches.Count > 0)
            {

                
                var locationOnScreen = touches[0].Location;
                CCPoint posizione = new CCPoint(locationOnScreen.X, 100);

                tractor.HandleInput(posizione);
            }
            
        }





        private void CreateDebugLabel()
        {
            debugLabel = new CCLabel("DebugLabel", "Arial", 20, CCLabelFormat.SystemFont);
            debugLabel.PositionX = hudLayer.ContentSize.Width / 2.0f;
            debugLabel.PositionY = 650;
            debugLabel.HorizontalAlignment = CCTextAlignment.Left;
            if (GameCoefficients.ShowDebugInfo)
            {
                hudLayer.AddChild(debugLabel);
            }
        }


        private void DebugActivity()
        {
            if (GameCoefficients.ShowDebugInfo)
            {
                debugLabel.Text = spawner.DebugInfo;
            }

        }

            private void PerformCollision()
        {
            
            for (int i = fruitList.Count - 1; i > -1; i--)
            {
                var fruit = fruitList[i];

                FruitVsTractor(fruit);

                

                FruitVsBorders(fruit);

                

                
            }
        }

        

        private void FruitVsTractor(Fruit fruit)
        {
            

            bool didCollideWithTractor = FruitPolygonCollision(fruit, tractor.Polygon);
            if (didCollideWithTractor && fruit.FruitColor== FruitColor.Violet)
            {
                score += 1;
                scoreText.Score = score;
                //CCAudioEngine.SharedEngine.PlayEffect("Sounds/");
                Destroy(fruit);

            }
            else
            {
                if(didCollideWithTractor && (fruit.FruitColor == FruitColor.Yellow || fruit.FruitColor == FruitColor.Red))
                {
                    Destroy(fruit);
                    //CCAudioEngine.SharedEngine.PlayEffect("Sounds/");
                    EndGame();
                }
            }

            if ((fruit.PositionY < 0) && fruit.FruitColor == FruitColor.Violet)
            {
                Destroy(fruit);
                score = score-1;
                scoreText.Score = score;
                count = count - 1;
                bounch_lost.bounch_lost1 = count;
               // CCAudioEngine.SharedEngine.PlayEffect("Sounds/");
            }


            if (count == 0)
            {
                //CCAudioEngine.SharedEngine.PlayEffect("Sounds/");
                EndGame();
            }

        }


        
        

        private void Destroy(Fruit fruit)
        {
            fruit.RemoveFromParent();
            fruitList.Remove(fruit);
        }

        private void EndGame()
        {
            hasGameEnded = true;
            spawner.IsSpawning = false;
            tractor.Visible = false;


            
            var drawNode = new CCDrawNode();
            drawNode.DrawRect(
                new CCRect(0, 0, 2000, 2000),
                new CCColor4B(0, 0, 0, 160));
            hudLayer.Children.Add(drawNode);


            if (count == 0)
            {
                var endGameLabel = new CCLabel("Game Over\nThree bunches lost\nFinal Score:" + score,
                    "Arial", 40, CCLabelFormat.SystemFont);
                endGameLabel.HorizontalAlignment = CCTextAlignment.Center;
                endGameLabel.Color = CCColor3B.White;
                endGameLabel.VerticalAlignment = CCVerticalTextAlignment.Center;
                endGameLabel.PositionX = hudLayer.ContentSize.Width / 2.0f;
                endGameLabel.PositionY = hudLayer.ContentSize.Height / 2.0f;
                hudLayer.Children.Add(endGameLabel);

            }
            else
            {




                var endGameLabel = new CCLabel("Game Over\nGot the wrong fruit\nFinal Score:" + score,
                    "Arial", 40, CCLabelFormat.SystemFont);
                endGameLabel.HorizontalAlignment = CCTextAlignment.Center;
                endGameLabel.Color = CCColor3B.White;
                endGameLabel.VerticalAlignment = CCVerticalTextAlignment.Center;
                endGameLabel.PositionX = hudLayer.ContentSize.Width / 2.0f;
                endGameLabel.PositionY = hudLayer.ContentSize.Height / 2.0f;
                hudLayer.Children.Add(endGameLabel);
            }


        }

        private static bool FruitPolygonCollision(Fruit fruit, Polygon polygon)
        {
            
            bool didCollide = polygon.CollideAgainst(fruit.Collision);

            
            return didCollide;
        }




        

        private void CreateSpawner()
        {
            spawner = new FruitSpawner();
            spawner.FruitSpawned += HandleFruitAdded;
            spawner.Layer = gameplayLayer;
        }

        private void HandleFruitAdded(Fruit fruit)
        {
            fruitList.Add(fruit);
            gameplayLayer.AddChild(fruit);
        }


        private void CreateHud()
        {
            scoreText = new ScoreText();
            scoreText.PositionX = 10;
            scoreText.PositionY = hudLayer.ContentSize.Height - 30;
            scoreText.Score = 0;
            hudLayer.AddChild(scoreText);

            bounch_lost = new ScoreIncreaseText();
            bounch_lost.PositionX = hudLayer.ContentSize.Width /2;
            bounch_lost.PositionY = hudLayer.ContentSize.Height -30;
            bounch_lost.bounch_lost1 = 3;
            hudLayer.AddChild(bounch_lost);


        }

        //private void CreateExit()
        //{
        //    exit = new CCSprite("images/home.png");
        //    exit.PositionX = 340;
        //    exit.PositionY = 490;
        //    exit.IsAntialiased = false;
        //    BackgroundLayer.AddChild(exit);

        //}

        

        

        

        

        private void CreateTractor()
        {
            tractor = new Tractor();
            tractor.PositionX = 10;
            tractor.PositionY = 100;
            tractor.SetDesiredPositionToCurrentPosition();

            gameplayLayer.AddChild(tractor);
        }

        private void CreateLayers()
        {
            BackgroundLayer = new CCLayer();
            this.AddLayer(BackgroundLayer);

            hudLayer = new CCLayer();
            this.AddLayer(hudLayer);

            gameplayLayer = new CCLayer();
            this.AddLayer(gameplayLayer);
        }

        public void CreateBackground()
        {
            var background = new CCSprite("images/full.png");
            background.AnchorPoint = new CCPoint(0, 0);
            background.IsAntialiased = false;
            BackgroundLayer.AddChild(background);

        }

        private void FruitVsBorders(Fruit fruit)
        {
            if (fruit.PositionX - fruit.Radius < 0 && fruit.Velocity.X < 0)
            {
                fruit.Velocity.X *= -1 * GameCoefficients.FruitCollision;
                
            }
            if (fruit.PositionX + fruit.Radius > gameplayLayer.ContentSize.Width && fruit.Velocity.X > 0)
            {
                fruit.Velocity.X *= -1 * GameCoefficients.FruitCollision;
                

            }
            if (fruit.PositionY + fruit.Radius > gameplayLayer.ContentSize.Height && fruit.Velocity.Y > 0)
            {
                fruit.Velocity.Y *= -1 * GameCoefficients.FruitCollision;
                
            }
        }


    }
}

