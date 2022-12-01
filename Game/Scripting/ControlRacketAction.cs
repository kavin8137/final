using Final.Game.Casting;
using Final.Game.Services;


namespace Final.Game.Scripting
{
    public class ControlRacketAction : Action
    {
        private KeyboardService _keyboardService;

        public ControlRacketAction(KeyboardService keyboardService)
        {
            this._keyboardService = keyboardService;
        }

        public void Execute(Cast cast, Script script, ActionCallback callback)
        {
            Racket racket = (Racket)cast.GetFirstActor(Constants.RACKET_GROUP);
            Racket racket1 = (Racket)cast.GetSecondActor(Constants.RACKET_GROUP);
            if (_keyboardService.IsKeyDown(Constants.UP1))
            {
                racket.SwingUp();
            }
            else if (_keyboardService.IsKeyDown(Constants.DOWN1))
            {
                racket.SwingDown();
            }
            else
            {
                racket.StopMoving();
            }

            if (_keyboardService.IsKeyDown(Constants.UP))
            {
                racket1.SwingUp();
            }
            else if (_keyboardService.IsKeyDown(Constants.DOWN))
            {
                racket1.SwingDown();
            }
            else
            {
                racket1.StopMoving();
            }
        }
    }
}