using System.Collections.Generic;
using Final.Game.Casting;
using Final.Game.Services;


namespace Final.Game.Scripting
{
    public class DrawBricksAction : Action
    {
        private VideoService _videoService;
        
        public DrawBricksAction(VideoService videoService)
        {
            this._videoService = videoService;
        }

        public void Execute(Cast cast, Script script, ActionCallback callback)
        {
            List<Actor> bricks = cast.GetActors(Constants.BRICK_GROUP);
            List<Actor> bricks1 = cast.GetActors(Constants.BRICK_GROUP1);
            foreach (Actor actor in bricks)
            {
                Brick brick = (Brick)actor;
                Body body = brick.GetBody();

                if (brick.IsDebug())
                {
                    Rectangle rectangle = body.GetRectangle();
                    Point size = rectangle.GetSize();
                    Point pos = rectangle.GetPosition();
                    _videoService.DrawRectangle(size, pos, Constants.PURPLE, false);
                }

                Animation animation = brick.GetAnimation();
                Image image = animation.NextImage();
                Point position = body.GetPosition();
                _videoService.DrawImage(image, position);
            }

            foreach (Actor actor1 in bricks1)
            {
                Brick brick1 = (Brick)actor1;
                Body body1 = brick1.GetBody();

                if (brick1.IsDebug())
                {
                    Rectangle rectangle = body1.GetRectangle();
                    Point size = rectangle.GetSize();
                    Point pos = rectangle.GetPosition();
                    _videoService.DrawRectangle(size, pos, Constants.PURPLE, false);
                }

                Animation animation = brick1.GetAnimation();
                Image image = animation.NextImage();
                Point position = body1.GetPosition();
                _videoService.DrawImage(image, position);
            }
        }
    }
}