using Final.Game.Casting;
using Final.Game.Services;


namespace Final.Game.Scripting
{
    public class CollideRacketAction : Action
    {
        private AudioService _audioService;
        private PhysicsService _physicsService;
        
        public CollideRacketAction(PhysicsService physicsService, AudioService audioService)
        {
            this._physicsService = physicsService;
            this._audioService = audioService;
        }

        public void Execute(Cast cast, Script script, ActionCallback callback)
        {
            Ball ball = (Ball)cast.GetFirstActor(Constants.BALL_GROUP);
            Racket racket = (Racket)cast.GetFirstActor(Constants.RACKET_GROUP);
            Racket racket1 = (Racket)cast.GetSecondActor(Constants.RACKET_GROUP);
            Body ballBody = ball.GetBody();
            Body racketBody = racket.GetBody();
            Body topRacketBody = racket.GetTopBody();
            Body bottomRacketBody = racket.GetBottomBody();
            Body racketBody1 = racket1.GetBody();

            if (_physicsService.HasCollided(racketBody, ballBody) || _physicsService.HasCollided(racketBody1, ballBody))
            {
                ball.BounceX();
                Sound sound = new Sound(Constants.BOUNCE_SOUND);
                _audioService.PlaySound(sound);
            }
            if (_physicsService.HasCollided(topRacketBody, ballBody) || _physicsService.HasCollided(bottomRacketBody, ballBody))
            {
                ball.BounceY();
                Sound sound = new Sound(Constants.BOUNCE_SOUND);
                _audioService.PlaySound(sound);
            }
        }
    }
}