using System;
using CocosSharp;
using test_2.Geometry;

using Xamarin.Forms;

namespace test_2.Entities
{


    public enum FruitColor
    {
        Yellow,
        Red,
        Violet
    }

    public static class FruitColorExtensions
    {


        public static CCColor4B ToCCColor(this FruitColor color)
        {
            switch (color)
            {
                case FruitColor.Yellow:
                    return new CCColor4B(150, 150, 0, 150);
                case FruitColor.Red:
                    return new CCColor4B(150, 0, 0, 150);
                case FruitColor.Violet:
                    return new CCColor4B(238, 130, 238,150);
            }
            throw new ArgumentException("Unknown color " + color);
        }

    }


    public class FruitBin 
    {
        
    }
}

