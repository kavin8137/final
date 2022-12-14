using System;
using System.Collections.Generic;
using System.IO;
using Final.Game.Casting;
using Final.Game.Scripting;
using Final.Game.Services;


namespace Final.Game.Directing
{
    public class SceneManager
    {
        public static AudioService AudioService = new RaylibAudioService();
        public static KeyboardService KeyboardService = new RaylibKeyboardService();
        public static MouseService MouseService = new RaylibMouseService();
        public static PhysicsService PhysicsService = new RaylibPhysicsService();
        public static VideoService VideoService = new RaylibVideoService(Constants.GAME_NAME,
            Constants.SCREEN_WIDTH, Constants.SCREEN_HEIGHT, Constants.BLACK);
        private PlaySoundAction sg = new PlaySoundAction(AudioService, Constants.START_SOUND);
        public SceneManager()
        {
        }

        public void PrepareScene(string scene, Cast cast, Script script)
        {
            if (scene == Constants.NEW_GAME)
            {
                PrepareNewGame(cast, script);
            }
            else if (scene == Constants.NEXT_LEVEL)
            {
                PrepareNextLevel(cast, script);
            }
            else if (scene == Constants.TRY_AGAIN)
            {
                PrepareTryAgain(cast, script);
            }
            else if (scene == Constants.IN_PLAY)
            {
                PrepareInPlay(cast, script);
            }
            else if (scene == Constants.GAME_OVER1)
            {
                PrepareGameOver(cast, script, Constants.WAS_GOOD_GAME);
            }
            else if (scene == Constants.GAME_OVER)
            {
                PrepareGameOver(cast, script, Constants.WAS_GOOD_GAME1);
            }
        }

        private void PrepareNewGame(Cast cast, Script script)
        {
            AddStats(cast);
            AddLevel(cast);
            AddScore(cast);
            AddLives(cast);
            AddBall(cast);
            AddBricks(cast);
            AddRacket(cast);
            AddDialog(cast, Constants.ENTER_TO_START);

            script.ClearAllActions();
            AddInitActions(script);
            AddLoadActions(script);

            ChangeSceneAction a = new ChangeSceneAction(KeyboardService, Constants.NEXT_LEVEL);
            script.AddAction(Constants.INPUT, a);

            AddOutputActions(script);
            AddUnloadActions(script);
            AddReleaseActions(script);

            // PlaySoundAction sg = new PlaySoundAction(AudioService, Constants.START_SOUND);
            script.AddAction(Constants.OUTPUT, sg);
        }

        private void ActivateBall(Cast cast)
        {
            Ball ball = (Ball)cast.GetFirstActor(Constants.BALL_GROUP);
            ball.Release();
        }

        private void PrepareNextLevel(Cast cast, Script script)
        {
            AddBall(cast);
            AddBricks(cast);
            AddRacket(cast);
            AddDialog(cast, Constants.PREP_TO_LAUNCH);

            script.ClearAllActions();

            TimedChangeSceneAction ta = new TimedChangeSceneAction(Constants.IN_PLAY, 2, DateTime.Now);
            script.AddAction(Constants.INPUT, ta);

            AddOutputActions(script);

            PlaySoundAction sa = new PlaySoundAction(AudioService, Constants.WELCOME_SOUND);
            script.AddAction(Constants.OUTPUT, sa);
        }

        private void PrepareTryAgain(Cast cast, Script script)
        {
            AddBall(cast);
            AddRacket(cast);
            AddDialog(cast, Constants.PREP_TO_LAUNCH);

            script.ClearAllActions();
            
            TimedChangeSceneAction ta = new TimedChangeSceneAction(Constants.IN_PLAY, 2, DateTime.Now);
            script.AddAction(Constants.INPUT, ta);
            
            AddUpdateActions(script);
            AddOutputActions(script);
        }

        private void PrepareInPlay(Cast cast, Script script)
        {
            ActivateBall(cast);
            cast.ClearActors(Constants.DIALOG_GROUP);

            script.ClearAllActions();

            ControlRacketAction action = new ControlRacketAction(KeyboardService);
            script.AddAction(Constants.INPUT, action);

            AddUpdateActions(script);    
            AddOutputActions(script);
        
        }

        private void PrepareGameOver(Cast cast, Script script, string message)
        {
            AddBall(cast);
            AddRacket(cast);
            AddDialog(cast, message);
            // script.RemoveAction(Constants.OUTPUT, sa);
            // AudioService.UnloadSound(Constants.START_SOUND);
            script.ClearAllActions();


            TimedChangeSceneAction ta = new TimedChangeSceneAction(Constants.NEW_GAME, 5, DateTime.Now);
            script.AddAction(Constants.INPUT, ta);

            AddOutputActions(script);
        }

        // -----------------------------------------------------------------------------------------
        // casting methods
        // -----------------------------------------------------------------------------------------

        private void AddBall(Cast cast)
        {
            cast.ClearActors(Constants.BALL_GROUP);
        
            int x = Constants.CENTER_X - Constants.BALL_WIDTH / 2;
            int y = Constants.SCREEN_HEIGHT / 2 - Constants.BALL_HEIGHT;
        
            Point position = new Point(x, y);
            Point size = new Point(Constants.BALL_WIDTH, Constants.BALL_HEIGHT);
            Point velocity = new Point(0, 0);
        
            Body body = new Body(position, size, velocity);
            Image image = new Image(Constants.BALL_IMAGE);
            Ball ball = new Ball(body, image, false);
        
            cast.AddActor(Constants.BALL_GROUP, ball);
        }

        private void AddBricks(Cast cast)
        {
            cast.ClearActors(Constants.BRICK_GROUP);

            Stats stats = (Stats)cast.GetFirstActor(Constants.STATS_GROUP);
            int level = stats.GetLevel() % Constants.BASE_LEVELS;
            string filename = string.Format(Constants.LEVEL_FILE, level);
            List<List<string>> rows = LoadLevel(filename);

            for (int r = 0; r < rows.Count; r++)
            {
                for (int c = 0; c < rows[r].Count; c++)
                {
                    int x = Constants.FIELD_LEFT + c * Constants.BRICK_WIDTH;
                    int y = Constants.FIELD_TOP + r * Constants.BRICK_HEIGHT;
                    int x1 = Constants.FIELD_RIGHT - Constants.BRICK_WIDTH;

                    string color = rows[r][c][0].ToString();
                    int frames = (int)Char.GetNumericValue(rows[r][c][1]);
                    int points = Constants.BRICK_POINTS;

                    Point position = new Point(x, y);
                    Point position1 = new Point(x1, y);
                    Point size = new Point(Constants.BRICK_WIDTH, Constants.BRICK_HEIGHT);
                    Point velocity = new Point(0, 0);
                    List<string> images = Constants.BRICK_IMAGES[color].GetRange(0, frames);

                    Body body = new Body(position, size, velocity);
                    Body body1 = new Body(position1, size, velocity);
                    Animation animation = new Animation(images, Constants.BRICK_RATE, 1);
                    
                    Brick brick = new Brick(body, animation, points, false);
                    Brick brick1 = new Brick(body1, animation, points, false);
                    cast.AddActor(Constants.BRICK_GROUP, brick);
                    cast.AddActor(Constants.BRICK_GROUP, brick1);
                }
            }
        }

        private void AddDialog(Cast cast, string message)
        {
            cast.ClearActors(Constants.DIALOG_GROUP);

            Text text = new Text(message, Constants.FONT_FILE, Constants.FONT_SIZE, 
                Constants.ALIGN_CENTER, Constants.WHITE);
            Point position = new Point(Constants.CENTER_X, Constants.CENTER_Y);

            Label label = new Label(text, position);
            cast.AddActor(Constants.DIALOG_GROUP, label);   
        }

        private void AddLevel(Cast cast)
        {
            cast.ClearActors(Constants.LEVEL_GROUP);

            Text text = new Text(Constants.LEVEL_FORMAT, Constants.FONT_FILE, Constants.FONT_SIZE, 
                Constants.ALIGN_LEFT, Constants.WHITE);
            Point position = new Point(Constants.HUD_MARGIN, Constants.HUD_MARGIN);

            Label label = new Label(text, position);
            // cast.AddActor(Constants.LEVEL_GROUP, label);
        }

        private void AddLives(Cast cast)
        {
            cast.ClearActors(Constants.LIVES_GROUP);

            Text text = new Text(Constants.LIVES_FORMAT, Constants.FONT_FILE, Constants.FONT_SIZE, 
                Constants.ALIGN_LEFT, Constants.WHITE);
            Text text1 = new Text(Constants.LIVES_FORMAT1, Constants.FONT_FILE, Constants.FONT_SIZE,
                Constants.ALIGN_RIGHT, Constants.WHITE);
            Point position = new Point(0, Constants.HUD_MARGIN);
            Point position1 = new Point(Constants.SCREEN_WIDTH - Constants.HUD_MARGIN, Constants.HUD_MARGIN);

            Label label = new Label(text, position);
            Label label1 = new Label(text1, position1);
            cast.AddActor(Constants.LIVES_GROUP, label); 
            cast.AddActor(Constants.LIVES_GROUP, label1);
        }

        private void AddRacket(Cast cast)
        {
            cast.ClearActors(Constants.RACKET_GROUP);
        
            int x = Constants.CENTER_X / 2 - Constants.RACKET_WIDTH / 2;
            int x1 = 3 * x;
            int y = Constants.SCREEN_HEIGHT / 2 - Constants.RACKET_HEIGHT / 2;
            int topy = Constants.SCREEN_HEIGHT / 2 - Constants.RACKET_HEIGHT / 2 - 3;
            int bottomy = y + (Constants.RACKET_HEIGHT) - 3;
        
            Point position = new Point(x, y);

            Point topPos = new Point(x, topy);
            Point bottomPos = new Point(x, bottomy);

            Point topPos1 = new Point(x1, topy);
            Point bottomPos1 = new Point(x1, bottomy);

            Point position1 = new Point(x1, y);

            Point size = new Point(Constants.RACKET_WIDTH, Constants.RACKET_HEIGHT);

            Point topsize = new Point(Constants.RACKET_WIDTH, 5);
            Point bottomsize = new Point(Constants.RACKET_WIDTH, 5);

            Point velocity = new Point(0, 0);
        
            Body body = new Body(position, size, velocity);
            Body topBody = new Body(topPos, topsize, velocity);
            Body bottomBody = new Body(bottomPos, bottomsize, velocity);

            Body topBody1 = new Body(topPos1, topsize, velocity);
            Body bottomBody1 = new Body(bottomPos1, bottomsize, velocity);
            Body body1 = new Body(position1, size, velocity);

            Animation animation = new Animation(Constants.RACKET_IMAGES, Constants.RACKET_RATE, 0);
            Animation animation1 = new Animation(Constants.RACKET1_IMAGES, Constants.RACKET_RATE, 0);
            Racket racket = new Racket(body, topBody, bottomBody, animation, false);
            Racket racket1 = new Racket(body1, topBody1, bottomBody1, animation1, false);

        
            cast.AddActor(Constants.RACKET_GROUP, racket);
            cast.AddActor(Constants.RACKET_GROUP, racket1);
        }

        private void AddScore(Cast cast)
        {
            cast.ClearActors(Constants.SCORE_GROUP);

            Text text = new Text(Constants.SCORE_FORMAT, Constants.FONT_FILE, Constants.FONT_SIZE, 
                Constants.ALIGN_CENTER, Constants.WHITE);
            Point position = new Point(Constants.CENTER_X, Constants.HUD_MARGIN);
            
            Label label = new Label(text, position);
            // cast.AddActor(Constants.SCORE_GROUP, label);
        }

        private void AddStats(Cast cast)
        {
            cast.ClearActors(Constants.STATS_GROUP);
            Stats stats = new Stats();
            Stats stats1 = new Stats();
            cast.AddActor(Constants.STATS_GROUP, stats);
            cast.AddActor(Constants.STATS_GROUP, stats1);
        }

        private List<List<string>> LoadLevel(string filename)
        {
            List<List<string>> data = new List<List<string>>();
            using(StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string row = reader.ReadLine();
                    List<string> columns = new List<string>(row.Split(',', StringSplitOptions.TrimEntries));
                    data.Add(columns);
                }
            }
            return data;
        }

        // -----------------------------------------------------------------------------------------
        // scriptig methods
        // -----------------------------------------------------------------------------------------

        private void AddInitActions(Script script)
        {
            script.AddAction(Constants.INITIALIZE, new InitializeDevicesAction(AudioService, 
                VideoService));
        }

        private void AddLoadActions(Script script)
        {
            script.AddAction(Constants.LOAD, new LoadAssetsAction(AudioService, VideoService));
        }

        private void AddOutputActions(Script script)
        {
            script.AddAction(Constants.OUTPUT, new StartDrawingAction(VideoService));
            script.AddAction(Constants.OUTPUT, new DrawHudAction(VideoService));
            script.AddAction(Constants.OUTPUT, new DrawBallAction(VideoService));
            script.AddAction(Constants.OUTPUT, new DrawBricksAction(VideoService));
            script.AddAction(Constants.OUTPUT, new DrawRacketAction(VideoService));
            script.AddAction(Constants.OUTPUT, new DrawDialogAction(VideoService));
            script.AddAction(Constants.OUTPUT, new EndDrawingAction(VideoService));
        }

        private void AddUnloadActions(Script script)
        {
            script.AddAction(Constants.UNLOAD, new UnloadAssetsAction(AudioService, VideoService));
        }

        private void AddReleaseActions(Script script)
        {
            script.AddAction(Constants.RELEASE, new ReleaseDevicesAction(AudioService, 
                VideoService));
        }

        private void AddUpdateActions(Script script)
        {
            script.AddAction(Constants.UPDATE, new MoveBallAction());
            script.AddAction(Constants.UPDATE, new MoveRacketAction());
            script.AddAction(Constants.UPDATE, new CollideBordersAction(PhysicsService, AudioService));
            script.AddAction(Constants.UPDATE, new CollideBrickAction(PhysicsService, AudioService));
            script.AddAction(Constants.UPDATE, new CollideRacketAction(PhysicsService, AudioService));
            script.AddAction(Constants.UPDATE, new CheckOverAction());     
        }
    }
}