using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones
{
    public partial class Obstacle
    {
        private int _x;                                // Position en X depuis la gauche de l'espace aérien
        private int _y;                                 // Position en Y depuis le haut de l'espace aérien
        private Color _color;
        private int _largeur;
        private int _profondeur;

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public int Largeur { get => _largeur; set => _largeur = value; }
        public int Profondeur { get => _profondeur; set => _profondeur = value; }

        public Color BuildingColor { get => _color; set => _color = value; }

        public Obstacle(int x, int y, Color colorDef, int largeur, int profondeur)
        {
            _x = x;
            _y = y;
            _color = colorDef;
            _largeur = largeur;
            _profondeur = profondeur;
        }
    }
}
