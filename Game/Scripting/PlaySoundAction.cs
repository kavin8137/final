using Final.Game.Casting;
using Final.Game.Services;


namespace Final.Game.Scripting
{
    public class PlaySoundAction : Action
    {
        private AudioService _audioService;
        private string _filename;

        public PlaySoundAction(AudioService audioService, string filename)
        {
            this._audioService = audioService;
            this._filename = filename;
        }

        public void Execute(Cast cast, Script script, ActionCallback callback)
        {
            Sound sound = new Sound(_filename);
            _audioService.PlaySound(sound);
            script.RemoveAction(Constants.OUTPUT, this);
        }
    }
}