using Final.Game.Casting;
using Final.Game.Services;


namespace Final.Game.Scripting
{
    public class ReleaseDevicesAction : Action
    {
        private AudioService _audioService;
        private VideoService _videoService;
        
        public ReleaseDevicesAction(AudioService audioService, VideoService videoService)
        {
            this._audioService = audioService;
            this._videoService = videoService;
        }

        public void Execute(Cast cast, Script script, ActionCallback callback)
        {
            _audioService.Release();
            _videoService.Release();
        }
    }
}