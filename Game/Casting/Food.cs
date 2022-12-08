using System;

namespace Unit05.Game.Casting
{
    /// <summary>
    /// <para>A tasty item that snakes like to eat.</para>
    /// <para>
    /// The responsibility of Food is to select a random position and points that it's worth.
    /// </para>
    /// </summary>
    public class Food : Actor
    {
        private bool counter=true;

        /// <summary>
        /// Constructs a new instance of an Food.
        /// </summary>
        public Food()
        {
            SetText("O");
            //☢
            SetColor(Constants.GREEN);
        }

        /// <summary>
        /// Gets the points this food is worth.
        /// </summary>
        /// <returns>The points.</returns>
    

        /// <summary>
        /// Selects a random position and points that the food is worth.
        /// </summary>
        public Cast Reset(Cast cast)
        {
            Random random = new Random();
            // _points = random.Next(9);
            for (int i = 0; i < 25; i++)
            {
                int x = random.Next(Constants.COLUMNS);
                int y = random.Next(Constants.ROWS);
                Point position = new Point(x, y);
                position = position.Scale(Constants.CELL_SIZE);
                Food dot = new Food();
                dot.SetPosition(position);
                if (counter == true)
                {
                    dot.SetText("O");
                    dot.SetColor(Constants.GREEN);
                    dot.SetPoints(1);
                    cast.AddActor("food", dot);
                    counter = !counter;
                }
                else if (counter ==false)
                {
                    dot.SetText("X");
                    dot.SetColor(Constants.LIGHTRED);
                    dot.SetPoints(-5);
                    cast.AddActor("food", dot);
                    counter = !counter;   
                }
            }
            return cast;
        }
    }
}
