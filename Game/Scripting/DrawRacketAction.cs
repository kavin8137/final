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
            Body topBody = racket.GetTopBody();
            Body bottomBody = racket.GetBottomBody();

            Body body1 = racket1.GetBody();
            Body topBody1 = racket1.GetTopBody();
            Body bottomBody1 = racket1.GetBottomBody();

            if (racket.IsDebug() && racket1.IsDebug())
            {
                Rectangle rectangle = body.GetRectangle();
                Rectangle topRectangle = topBody.GetRectangle();
                Rectangle bottomRectangle = bottomBody.GetRectangle();

                Rectangle rectangle1 = body1.GetRectangle();
                Rectangle topRectangle1 = topBody1.GetRectangle();
                Rectangle bottomRectangle1 = bottomBody1.GetRectangle();


                Point size = rectangle.GetSize();
                Point topSize = topRectangle.GetSize();
                Point bottomSize = bottomRectangle.GetSize();

                Point size1 = rectangle1.GetSize();
                Point topSize1 = topRectangle1.GetSize();
                Point bottomSize1 = bottomRectangle1.GetSize();


                Point pos = rectangle.GetPosition();
                Point topPos = topRectangle.GetPosition();
                Point bottomPos = bottomRectangle.GetPosition();

                Point pos1 = rectangle1.GetPosition();
                Point topPos1 = topRectangle1.GetPosition();
                Point bottomPos1 = bottomRectangle1.GetPosition();

                _videoService.DrawRectangle(size, pos, Constants.PURPLE, false);
                _videoService.DrawRectangle(topSize, topPos, Constants.PURPLE, false);
                _videoService.DrawRectangle(bottomSize, bottomPos, Constants.PURPLE, false);

                _videoService.DrawRectangle(size1, pos1, Constants.PURPLE, false);
                _videoService.DrawRectangle(topSize1, topPos1, Constants.PURPLE, false);
                _videoService.DrawRectangle(bottomSize1, bottomPos1, Constants.PURPLE, false);
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