using Unit05.Game.Casting;
using Unit05.Game.Directing;
using Unit05.Game.Scripting;
using Unit05.Game.Services;
using Unit05.Game;


namespace Unit05
{
    /// <summary>
    /// The program's entry point.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Starts the program using the given arguments.
        /// </summary>
        /// <param name="args">The given arguments.</param>
        static void Main(string[] args)
        {
            Cycle cycleA = new Cycle();
            cycleA.PrepareBody(new Point(-225,150));
            //cycleA.PrepareBody(new Point(-225,150), Constants.RED);
            
            Cycle cycleB = new Cycle();
            cycleB.PrepareBody(new Point(225,150));
            //cycleB.PrepareBody(new Point(225,150), Constants.BLUE);
            

            // create the cast
            Cast cast = new Cast();
            cast.AddActor("cycleA", cycleA);
            cast.AddActor("cycleB", cycleB);
            cast.AddActor("food", new Food());
            cast.AddActor("score", new Score());
            // cast.AddActor("scoreA", new ScoreA());
            // cast.AddActor("scoreB", new ScoreB());


            // create the services
            KeyboardService keyboardService = new KeyboardService();
            VideoService videoService = new VideoService(false);
           
            // create the script
            Script script = new Script();
            script.AddAction("input", new ControlActorsAction(keyboardService));
            //script.AddAction("input", new ControlActorsActionB(keyboardService));
            script.AddAction("update", new MoveActorsAction());
            script.AddAction("update", new HandleCollisionsAction());
            script.AddAction("output", new DrawActorsAction(videoService));

            // start the game
            Director director = new Director(videoService);
            director.StartGame(cast, script);
        }
    }
}