using Assets.Code.AsteroidsLogic;
using Assets.Code.PoolingLogic;
using Assets.Code.Services;

namespace Assets.Code.Infrastructure
{
    public class Game
    {
        public AsteroidPooling asteroidPooling { get; private set; }
        public AsteroidSpawner asteroidSpawner { get; private set; }
        public AsteroidWatcher asteroidWatcher { get; private set; }

        public ScreenService screenService { get; private set; }

        //BulletPooling

        public Game(ScreenService screenService, AsteroidPooling asteroidPooling, 
            AsteroidSpawner asteroidSpawner, AsteroidWatcher asteroidWatcher)
        {
            this.screenService = screenService;
            this.asteroidPooling = asteroidPooling;
            this.asteroidSpawner = asteroidSpawner;
            this.asteroidWatcher = asteroidWatcher;
        }
    }
}