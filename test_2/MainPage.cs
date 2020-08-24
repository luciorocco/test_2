using System;
using System.Collections.Generic;
using CocosSharp;
using test_2.Scene;


using Xamarin.Forms;

namespace test_2
{
    public class MainPage : ContentPage
    {
        GameScene gameScene;
        CocosSharpView gameView;
        



        public MainPage()
        {
            
            gameView = new CocosSharpView()
            {
                // Notice it has the same properties as other XamarinForms Views
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                // This gets called after CocosSharp starts up:
                ViewCreated = LoadGame

            };

            Content = gameView;
        }


        void LoadGame(object sender, EventArgs e)
        {
            var gameView = sender as CCGameView;
            if (gameView != null)

            {

                var contentSearchPaths = new List<string>()
            {
                "Images", "Sounds"
            };

                CCAudioEngine.SharedEngine.PreloadEffect("Sounds/FruitEdgeBounce");
                //CCAudioEngine.SharedEngine.PreloadBackgroundMusic("Sounds/FruityFallsSong");
                CCAudioEngine.SharedEngine.PreloadEffect("Sounds/GameOver");
                CCAudioEngine.SharedEngine.PreloadEffect("Sounds/FruitPaddleBounce");



                CCSizeI viewSize = gameView.ViewSize;
                int width = 384;
                int height = 512;

                gameView.DesignResolution = new CCSizeI(width, height);
                gameView.ResolutionPolicy = CCViewResolutionPolicy.ShowAll;


                gameView.ContentManager.SearchPaths = contentSearchPaths;

                //CCAudioEngine.SharedEngine.PlayBackgroundMusic("Sounds/original_menu1.mp3", true);


                gameView.Stats.Enabled = true;  //  <- Enable stats view !!!!!
                // GameScene is the root of the CocosSharp rendering hierarchy:
                gameScene = new GameScene(gameView);
                // Starts CocosSharp:
                gameView.RunWithScene(gameScene);

            }

        }



    
            



    }
}

