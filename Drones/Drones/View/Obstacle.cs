using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones
{
    public partial class Obstacle
    {
        public void Render(BufferedGraphics drawingSpace)
        {
            Brush BuildingBrush = new SolidBrush(BuildingColor);
            drawingSpace.Graphics.FillRectangle(BuildingBrush, new Rectangle(X, Y, Largeur, Profondeur));
        }
    }
}
