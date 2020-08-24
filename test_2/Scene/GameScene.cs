using System;
using CocosSharp;

using Xamarin.Forms;

namespace test_2.Scene
{
    public class GameScene : CCScene
    {
        
        

        public GameScene(CCGameView gameView) : base(gameView)
        {
            this.AddLayer(new StartLayer());

        }

        
    }
}

