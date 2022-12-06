using System.Collections.Generic;
using Unit05.Game.Casting;
using Unit05.Game.Services;


namespace Unit05.Game.Scripting
{
    /// <summary>
    /// <para>An output action that draws all the actors.</para>
    /// <para>The responsibility of DrawActorsAction is to draw each of the actors.</para>
    /// </summary>
    public class DrawActorsAction : Action
    {
        private VideoService _videoService;

        /// <summary>
        /// Constructs a new instance of ControlActorsAction using the given KeyboardService.
        /// </summary>
        public DrawActorsAction(VideoService videoService)
        {
            this._videoService = videoService;
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            Actor food = cast.GetFirstActor("food");
            Actor score = cast.GetFirstActor("score");

            Cycle cycleA = (Cycle)cast.GetFirstActor("cycleA");
            List<Actor> segmentsA = cycleA.GetSegments();

            Cycle cycleB = (Cycle)cast.GetFirstActor("cycleB");
            List<Actor> segmentsB = cycleB.GetSegments();

            //Actor scoreA = cast.GetFirstActor("scoreA");
            //Actor scoreB = cast.GetFirstActor("scoreB");

            //Actor food = cast.GetFirstActor("food");
            List<Actor> messages = cast.GetActors("messages");
            
            _videoService.ClearBuffer();
            _videoService.DrawActors(segmentsA);
            _videoService.DrawActors(segmentsB);
            _videoService.DrawActor(score);
            // _videoService.DrawActor(food);
            _videoService.DrawActors(messages);
            _videoService.FlushBuffer();
        }
    }
}