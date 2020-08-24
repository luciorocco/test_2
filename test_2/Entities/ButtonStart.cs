using System;
using CocosSharp;
using test_2.Scene;


using Xamarin.Forms;
using System.Collections.Generic;

namespace test_2.Entities
{
    public class ButtonStart : CCLayer
    {
        
        CCSprite start;
        CCSprite options;
        
        CCLabel userInfo;

        public ButtonStart()
        {
            createButtonStart();

            //createButtonOptions();
        }

        //private void createButtonOptions()
        //{
        //    options = new CCSprite("images/gear.png");
        //    options.AnchorPoint = new CCPoint(0, 0);

        //    options.PositionX = ContentSize.Center.X ;
        //    options.PositionY = ContentSize.Center.Y ;
        //    //options.Scale = .0f;
        //    options.IsAntialiased = false;

        //    this.AddChild(options);

        //    var touchListener = new CCEventListenerTouchAllAtOnce();
        //    touchListener.OnTouchesBegan = this.OnTouchBegan;
        //    AddEventListener(touchListener, this);

        //}

        private void createButtonStart()
        {
            start = new CCSprite("images/buttonStart.png");


            start.PositionX = ContentSize.Center.X + 35;
            start.PositionY = ContentSize.Center.Y+ 400;
            //start.Scale = .5f;
            start.IsAntialiased = false;



            this.AddChild(start);

            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesBegan = this.OnTouchBegan;
            AddEventListener(touchListener, this);



        }

        void OnTouchBegan(List<CCTouch> touches, CCEvent touchEvent)
        {
            //if (options.BoundingBoxTransformedToWorld.ContainsPoint(touches[0].Location))
            //{
            //    userInfo = new CCLabel("Premere Start per iniziare", "arial", 12);
            //    userInfo.PositionX = ContentSize.Center.X + 20;
            //    userInfo.PositionY = ContentSize.Center.Y + 20;
            //    this.AddChild(userInfo);
            //}

            if (start.BoundingBoxTransformedToWorld.ContainsPoint(touches[0].Location))
            {
                //var transition = new CCTransitionRotoZoom(.5f,  PlayScene);
                Director.PushScene(new  PlayScene(GameView));
            }


        }
    }
}

