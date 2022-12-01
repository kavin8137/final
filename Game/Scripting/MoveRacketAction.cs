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
            Body body1 = racket1.GetBody();
            Point position = body.GetPosition();
            Point position1 = body1.GetPosition();
            Point velocity = body.GetVelocity();
            Point velocity1 = body1.GetVelocity();
            int y = position.GetY();
            int y1 = position1.GetY();

            position = position.Add(velocity);
            position1 = position1.Add(velocity1);
            if (y < 0)
            {
                position = new Point(position.GetX(), 0);
            }
            else if (y > Constants.SCREEN_HEIGHT - Constants.RACKET_HEIGHT)
            {
                position = new Point(position.GetX(), 
                    Constants.SCREEN_HEIGHT - Constants.RACKET_HEIGHT);
            }

            if (y1 < 0)
            {
                position1 = new Point(position1.GetX(), 0);
            }
            else if (y1 > Constants.SCREEN_HEIGHT - Constants.RACKET_HEIGHT)
            {
                position1 = new Point(position1.GetX(), 
                    Constants.SCREEN_HEIGHT - Constants.RACKET_HEIGHT);
            }

            body.SetPosition(position);
            body1.SetPosition(position1);       
        }
    }
}