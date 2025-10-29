using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.Model
{
    public partial class Score
    {
        private int score;

        public int _score { get => score; set => score = value; }

        public Score()
        {
            _score = 0;
        }


        public void update()
        {
            _score += 1;
        }
    }
}
