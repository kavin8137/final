using System.Collections.Generic;
using Final.Game.Casting;
using Final.Game.Services;


namespace Final.Game.Scripting
{
    public class CollideBrickAction : Action
    {
        private AudioService _audioService;
        private PhysicsService _physicsService;
        
        public CollideBrickAction(PhysicsService physicsService, AudioService audioService)
        {
            this._physicsService = physicsService;
            this._audioService = audioService;
        }

        public void Execute(Cast cast, Script script, ActionCallback callback)
        {
            Ball ball = (Ball)cast.GetFirstActor(Constants.BALL_GROUP);
            List<Actor> bricks = cast.GetActors(Constants.BRICK_GROUP);
            Stats stats = (Stats)cast.GetFirstActor(Constants.STATS_GROUP);
            Body ballBody = ball.GetBody();
            
            foreach (Actor actor in bricks)
            {
                Brick brick = (Brick)actor;
                Body brickBody = brick.GetBody();

                if (_physicsService.HasCollided(brickBody, ballBody))
                {
                    ball.BounceX();
                    Sound sound = new Sound(Constants.BOUNCE_SOUND);
                    _audioService.PlaySound(sound);
                    int points = brick.GetPoints();
                    stats.AddPoints(points);
                    cast.RemoveActor(Constants.BRICK_GROUP, brick);
                }
            }
        }
    }
}