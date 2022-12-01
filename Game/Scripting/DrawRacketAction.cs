using Final.Game.Casting;
using Final.Game.Services;


namespace Final.Game.Scripting
{
    public class DrawRacketAction : Action
    {
        private VideoService _videoService;
        
        public DrawRacketAction(VideoService videoService)
        {
            this._videoService = videoService;
        }

        public void Execute(Cast cast, Script script, ActionCallback callback)
        {
            Racket racket = (Racket)cast.GetFirstActor(Constants.RACKET_GROUP);
            Racket racket1 = (Racket)cast.GetSecondActor(Constants.RACKET_GROUP);
            Body body = racket.GetBody();
            Body body1 = racket1.GetBody();

            if (racket.IsDebug() && racket1.IsDebug())
            {
                Rectangle rectangle = body.GetRectangle();
                Point size = rectangle.GetSize();
                Point pos = rectangle.GetPosition();
                _videoService.DrawRectangle(size, pos, Constants.PURPLE, false);
            }

            Animation animation = racket.GetAnimation();
            Animation animation1 = racket1.GetAnimation();
            Image image = animation.NextImage();
            Image image1 = animation1.NextImage();
            Point position = body.GetPosition();
            Point position1 = body1.GetPosition();
            _videoService.DrawImage(image, position);
            _videoService.DrawImage(image1, position1);
        }
    }
}