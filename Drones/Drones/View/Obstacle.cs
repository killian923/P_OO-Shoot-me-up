using Drones.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Drones
{
    public partial class Obstacle
    {
        public void Render(BufferedGraphics drawingSpace)
        {
            Brush BuildingBrush = new SolidBrush(BuildingColor);
            drawingSpace.Graphics.FillRectangle(BuildingBrush, new Rectangle(X, Y, Largeur, Profondeur));
            drawingSpace.Graphics.DrawString($"{this}", TextHelpers.drawFont, TextHelpers.writingBrush, X , Y);

        }
        public override string ToString()
        {
            return $"{((int)((double)_vie * 10)).ToString()}%";
        }

    }
}
