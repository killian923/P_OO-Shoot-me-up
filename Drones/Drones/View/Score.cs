using Drones.Helpers;
using Drones.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Drones.View
{
    public partial class Score
    {
        public void Render(BufferedGraphics drawingSpace)
        {
            //drawingSpace.Graphics.DrawRectangle();
            //drawingSpace.Graphics.DrawString($"{this}", TextHelpers.drawFont, TextHelpers.writingBrush);
        }

        // De manière textuelle
        public override string ToString()
        {
            return $"({((int)((double)_score)).ToString()}%)";
        }

    }
}
