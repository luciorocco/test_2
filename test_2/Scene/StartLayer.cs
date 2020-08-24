using System;
using CocosSharp;
using test_2.Entities;

using Xamarin.Forms;

namespace test_2.Scene
{
    public class StartLayer : CCLayerColor
    {
        
        CCSprite Background;
        ButtonStart button;

        public StartLayer() : base(CCColor4B.Transparent)
        {
            
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            Background = new CCSprite("images/farmers.png");
            Background.PositionX = ContentSize.Center.X;
            Background.PositionY = ContentSize.Center.Y;
            Background.IsAntialiased = false;



            this.AddChild(Background);

            StartMusic();
            

            //StartMusic();

            button = new ButtonStart();
            this.AddChild(button);



        }

        
            private void StartMusic()
            {

                //CCAudioEngine.SharedEngine.PlayBackgroundMusic("Sounds/");
            }
           
    }
}

