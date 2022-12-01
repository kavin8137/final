using System.Collections.Generic;
using Final.Game.Casting;
using Final.Game.Services;


namespace Final.Game.Scripting
{
    public class DrawDialogAction : Action
    {
        private VideoService _videoService;
        
        public DrawDialogAction(VideoService videoService)
        {
            this._videoService = videoService;
        }

        public void Execute(Cast cast, Script script, ActionCallback callback)
        {
            List<Actor> actors = cast.GetActors(Constants.DIALOG_GROUP);
            foreach (Actor actor in actors)
            {
                Label label = (Label)actor;
                Text text = label.GetText();
                Point position = label.GetPosition();
                _videoService.DrawText(text, position);
            }
        }
    }
}