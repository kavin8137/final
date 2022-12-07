using Final.Game.Casting;
using Final.Game.Services;


namespace Final.Game.Scripting
{
    public class DrawHudAction : Action
    {
        private VideoService _videoService;
        
        public DrawHudAction(VideoService videoService)
        {
            this._videoService = videoService;
        }

        public void Execute(Cast cast, Script script, ActionCallback callback)
        {
            Stats stats = (Stats)cast.GetFirstActor(Constants.STATS_GROUP);
            Stats stats1 = (Stats)cast.GetSecondActor(Constants.STATS_GROUP);
            // DrawLabel(cast, Constants.LEVEL_GROUP, Constants.LEVEL_FORMAT, stats.GetLevel());
            DrawLabel(cast, (Label)cast.GetFirstActor(Constants.LIVES_GROUP), Constants.LIVES_FORMAT, stats.GetLives());
            DrawLabel(cast, (Label)cast.GetSecondActor(Constants.LIVES_GROUP), Constants.LIVES_FORMAT1, stats1.GetLives());
            // DrawLabel(cast, Constants.SCORE_GROUP, Constants.SCORE_FORMAT, stats.GetScore());
        }

        // **********************************************************************************************
        // You found the bug. Great job!
        // **********************************************************************************************
        // todo: fix the bug by making sure the text value is set to the appropriate variable.
        private void DrawLabel(Cast cast, Label group_label, string format, int data)
        {
            string theValueToDisplay = string.Format(format, data);
            
            // Label label = (Label)cast.GetFirstActor(group);
            // Label label1 = (Label)cast.GetSecondActor(group);
            Label label = group_label;
            Text text = label.GetText();
            // Text text1 = label1.GetText();
            text.SetValue(theValueToDisplay);
            Point position = label.GetPosition();
            // Point position1 = label1.GetPosition();
            _videoService.DrawText(text, position);
            // _videoService.DrawText(text1, position1);
        }
    }
}