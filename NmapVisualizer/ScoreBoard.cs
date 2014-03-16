using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NmapVisualizer
{
    public class ScoreBoard : Dictionary<string, int>
    {
        public ScoreBoard() : base() { }

        public int Score(string key)
        {
            if (this.ContainsKey(key))
            {
                int score = this[key] + 1;
                this[key]++;
                return score;
            }
            else
            {
                this.Add(key, 1);
                return 1;
            }
        }

    }
}
