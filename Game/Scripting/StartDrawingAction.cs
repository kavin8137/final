using Final.Game.Casting;
using Final.Game.Services;


namespace Final.Game.Scripting
{
    public class StartDrawingAction : Action
    {
        private VideoService _videoService;
        
        public StartDrawingAction(VideoService videoService)
        {
            this._videoService = videoService;
        }

        public void Execute(Cast cast, Script script, ActionCallback callback)
        {
            _videoService.ClearBuffer();
        }
    }
}