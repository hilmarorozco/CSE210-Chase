using System;
using System.Collections.Generic;
using System.Data;
using Unit05.Game.Casting;
using Unit05.Game.Services;

namespace Unit05.Game.Scripting
{
    /// <summary>
    /// <para>An update action that handles interactions between the actors.</para>
    /// <para>
    /// The responsibility of HandleCollisionsAction is to handle the situation when the snake
    /// collides with the food, or the snake collides with its segments, or the game is over.
    /// </para>
    /// </summary>
    public class HandleCollisionsAction : Action
    {
        private bool _isGameOver = false;
        private bool winnerCycleA = false;
        private bool winnerCycleB = false;
        private int i = 0;
        private int total;

        /// <summary>
        /// Constructs a new instance of HandleCollisionsAction.
        /// </summary>
        public HandleCollisionsAction() { }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            if (_isGameOver == false)
            {
                HandleIncrement(cast);
                HandleSegmentCollisions(cast);
                HandleGameOver(cast);
                HandleFoodCollisions(cast);
            }
        }

        /// <summary>
        /// Updates the score nd moves the food if the snake collides with it.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private void HandleFoodCollisions(Cast cast)
        {
            //Snake snake = (Snake)cast.GetFirstActor("snake");
            Cycle cycleA = (Cycle)cast.GetFirstActor("cycleA");
            Score score = (Score)cast.GetFirstActor("score");
            List<Actor> food = cast.GetActors("food");
            foreach (Actor dot in food)
            {
                if (cycleA.GetHead().GetPosition().Equals(dot.GetPosition()))
                {
                    int points = dot.GetPoints();
                    //cycleA.GrowTail(points);
                    total=score.AddPoints(points);
                    
                }
            }
        }

        /// <summary>
        /// Updates the score nd moves the food if the snake collides with it.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>

        private void HandleIncrement(Cast cast)
        {
            Cycle cycleA = (Cycle)cast.GetFirstActor("cycleA");
            Cycle cycleB = (Cycle)cast.GetFirstActor("cycleB");

            //ScoreA scoreA = (ScoreA)cast.GetFirstActor("scoreA");
            //ScoreB scoreB = (ScoreB)cast.GetFirstActor("scoreB");


            // cycleA.GrowTail(1);
            // cycleB.GrowTail(1);
        }

        /// <summary>
        /// Sets the game over flag if the snake collides with one of its segments.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private void HandleSegmentCollisions(Cast cast)
        {
            Cycle cycleA = (Cycle)cast.GetFirstActor("cycleA");
            Actor headA = cycleA.GetHead();
            List<Actor> bodyA = cycleA.GetBody();

            Cycle cycleB = (Cycle)cast.GetFirstActor("cycleB");
            Actor headB = cycleB.GetHead();
            List<Actor> bodyB = cycleB.GetBody();

            if (headA.GetPosition().Equals(headB.GetPosition()))
            {
                _isGameOver = true;
                winnerCycleB = true;
            }
            if (total>=20)
                    {
                        _isGameOver = true;
                        winnerCycleA = true;
                    }

            // foreach (Actor segmentA in bodyA)
            // {
            //     if (segmentA.GetPosition().Equals(headB.GetPosition()))
            //     {
            //         _isGameOver = true;
            //         winnerCycleA = true;
            //     }
            //     if (segmentA.GetPosition().Equals(headA.GetPosition()))
            //     {
            //         _isGameOver = true;
            //         winnerCycleB = true;
            //     }
            // }

            // foreach (Actor segmentB in bodyB)
            // {
            //     if (segmentB.GetPosition().Equals(headA.GetPosition()))
            //     {
            //         _isGameOver = true;
            //         winnerCycleB = true;
            //     }
            //     if (segmentB.GetPosition().Equals(headB.GetPosition()))
            //     {
            //         _isGameOver = true;
            //         winnerCycleA = true;
            //     }
            // }
        }

        private void HandleGameOver(Cast cast)
        {
            if (_isGameOver == false)
            {
                Cycle cycleA = (Cycle)cast.GetFirstActor("cycleA");
                List<Actor> segmentsA = cycleA.GetSegments();
                //Food food = (Food)cast.GetFirstActor("food");
                Cycle cycleB = (Cycle)cast.GetFirstActor("cycleB");
                List<Actor> segmentsB = cycleB.GetSegments();
                //Food food = (Food)cast.GetFirstActor("food");

                foreach (Actor segment in segmentsA)
                {
                    segment.SetColor(Constants.LIGHTRED);
                }

                foreach (Actor segment in segmentsB)
                {
                    segment.SetColor(Constants.LIGHTBLUE);
                }
            }
            else if (_isGameOver == true)
            {
                Cycle cycleA = (Cycle)cast.GetFirstActor("cycleA");
                List<Actor> segmentsA = cycleA.GetSegments();
                //Food food = (Food)cast.GetFirstActor("food");
                Cycle cycleB = (Cycle)cast.GetFirstActor("cycleB");
                List<Actor> segmentsB = cycleB.GetSegments();
                //Food food = (Food)cast.GetFirstActor("food");


                if (winnerCycleA == true)
                {
                    // create a "game over" message
                    int x = Constants.MAX_X / 8;
                    int y = Constants.MAX_Y / 2;
                    Point position = new Point(x, y);

                    Actor message = new Actor();
                    message.SetFontSize(50);
                    message.SetColor(Constants.RED);
                    message.SetText("Game Over! Player 1 Wins");
                    message.SetPosition(position);
                    cast.AddActor("messages", message);

                    foreach (Actor segment in segmentsB)
                    {
                        segment.SetColor(Constants.WHITE);
                    }
                    foreach (Actor segment in segmentsA)
                    {
                        segment.SetColor(Constants.RED);
                    }
                }
                else if (winnerCycleB == true)
                {
                    // create a "game over" message
                    int x = Constants.MAX_X / 8;
                    int y = Constants.MAX_Y / 2;
                    Point position = new Point(x, y);

                    Actor message = new Actor();
                    message.SetFontSize(40);
                    message.SetColor(Constants.BLUE);
                    message.SetText("Game Over! The Chaser Caught You!");
                    message.SetPosition(position);
                    cast.AddActor("messages", message);

                    foreach (Actor segment in segmentsA)
                    {
                        segment.SetColor(Constants.WHITE);
                    }
                    foreach (Actor segment in segmentsB)
                    {
                        segment.SetColor(Constants.BLUE);
                    }
                }

                // // make everything white
                // foreach (Actor segment in segmentsA)
                // {
                //     segment.SetColor(Constants.WHITE);
                // }
                // foreach (Actor segment in segmentsB)
                // {
                //     segment.SetColor(Constants.WHITE);
                // }
            }
        }
    }
}
