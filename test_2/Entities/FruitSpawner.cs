using System;
using CocosSharp;
using System.Collections.Generic;

using Xamarin.Forms;

namespace test_2.Entities
{
    public class FruitSpawner 
    {

        float timeSinceLastSpawn;
        public float TimeInbetweenSpawns
        {
            get;
            set;
        }

        public string DebugInfo
        {
            get
            {
                string toReturn =
                    "Fruit per second: " + (1 / TimeInbetweenSpawns);

                return toReturn;
            }
        }

        public CCLayer Layer
        {
            get;
            set;
        }

        public Action<Fruit> FruitSpawned;

        public bool IsSpawning
        {
            get;
            set;
        }

        public FruitSpawner()
        {
            IsSpawning = true;
            TimeInbetweenSpawns = 1 / GameCoefficients.StartingFruitPerSecond;
            
            timeSinceLastSpawn = TimeInbetweenSpawns;
        }

        public void Activity(float frameTime)
        {
            if (IsSpawning)
            {
                SpawningActivity(frameTime);

                SpawnReductionTimeActivity(frameTime);
            }
        }

        private void SpawningActivity(float frameTime)
        {
            timeSinceLastSpawn += frameTime;

            if (timeSinceLastSpawn > TimeInbetweenSpawns)
            {
                timeSinceLastSpawn -= TimeInbetweenSpawns;

                Spawn();
            }
        }

        private void SpawnReductionTimeActivity(float frameTime)
        {
            
            var currentFruitPerSecond = 1 / TimeInbetweenSpawns;

            var amountToAdd = frameTime / GameCoefficients.TimeForExtraFruitPerSecond;

            var newFruitPerSecond = currentFruitPerSecond + amountToAdd;

            TimeInbetweenSpawns = 1 / newFruitPerSecond;

        }

        
        private void Spawn()
        {
            var fruit = new Fruit();
            float i = CCRandom.GetRandomFloat(0,15);

            if (Layer == null)
            {
                throw new InvalidOperationException("Need to set Layer before spawning");
            }

            fruit.PositionX = CCRandom.GetRandomFloat(0 + fruit.Radius, Layer.ContentSize.Width - fruit.Radius);
            fruit.PositionY = Layer.ContentSize.Height + fruit.Radius;

            if (i <= 5.0f)
            {
                fruit.FruitColor = FruitColor.Red;
            }
            else
            {
                if (i <= 10.0f)
                {
                    fruit.FruitColor = FruitColor.Yellow;

                }

                else
                {
                    fruit.FruitColor = FruitColor.Violet;
                }

            }

            if (FruitSpawned != null)
            {
                FruitSpawned(fruit);
            }

        }

    }
}

