using Final.Game.Casting;

namespace Final.Game.Scripting
{
    public class MoveRacketAction : Action
    {
        public MoveRacketAction()
        {
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

            Point position = body.GetPosition();
            Point topPos = topBody.GetPosition();
            Point bottomPos = bottomBody.GetPosition();

            Point position1 = body1.GetPosition();
            Point topPos1 = topBody1.GetPosition();
            Point bottomPos1 = bottomBody1.GetPosition();

            Point velocity = body.GetVelocity();
            Point velocity1 = body1.GetVelocity();

            int y = position.GetY();
            int y1 = position1.GetY();

            position = position.Add(velocity);
            topPos = position.Add(velocity);
            bottomPos = position.Add(velocity);

            position1 = position1.Add(velocity1);
            topPos1 = position.Add(velocity);
            bottomPos1 = position.Add(velocity);
            if (y < 0)
            {
                position = new Point(position.GetX(), 0);
                topPos = new Point(topPos.GetX(), 0);
                bottomPos = new Point(bottomPos.GetX(), 0);
            }
            else if (y > Constants.SCREEN_HEIGHT - Constants.RACKET_HEIGHT)
            {
                position = new Point(position.GetX(), 
                    Constants.SCREEN_HEIGHT - Constants.RACKET_HEIGHT);
                topPos = new Point(topPos.GetX(), 
                    Constants.SCREEN_HEIGHT - Constants.RACKET_HEIGHT);
                bottomPos = new Point(bottomPos.GetX(), 
                    Constants.SCREEN_HEIGHT + Constants.RACKET_HEIGHT);
            }

            if (y1 < 0)
            {
                position1 = new Point(position1.GetX(), 0);
                topPos1 = new Point(topPos1.GetX(), 0);
                bottomPos1 = new Point(bottomPos1.GetX(), 0);
            }
            else if (y1 > Constants.SCREEN_HEIGHT - Constants.RACKET_HEIGHT)
            {
                position1 = new Point(position1.GetX(), 
                    Constants.SCREEN_HEIGHT - Constants.RACKET_HEIGHT);
                topPos1 = new Point(topPos1.GetX(), 
                    Constants.SCREEN_HEIGHT - Constants.RACKET_HEIGHT);
                bottomPos1 = new Point(bottomPos1.GetX(), 
                    Constants.SCREEN_HEIGHT + Constants.RACKET_HEIGHT);
            }

            body.SetPosition(position);
            topBody.SetPosition(position);
            bottomBody.SetPosition(position);

            body1.SetPosition(position1);
            topBody1.SetPosition(position1);
            bottomBody1.SetPosition(position1);    
        }
    }
}