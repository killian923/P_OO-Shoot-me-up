using Drones.Properties;
using System.Reflection;

namespace Drones
{
    // Cette partie de la classe Player définit ce qu'est un drone par un modèle numérique
    public partial class Player
    {
        public static readonly int FULLCHARGE = 1000;   // Charge maximale de la batterie
        private int _charge;                            // La charge actuelle de la batterie
        private string _name;                           // Un nom
        private int _x;                                 // Position en X depuis la gauche de l'espace aérien
        private int _y;                                 // Position en Y depuis le haut de l'espace aérien
        private int _speedx;
        private int _speedy;
        private int _speed;
        private DateTime lastTireCall = DateTime.MinValue;

        // Constructeur
        public Player(int x, int y, string name, int nextX, int nextY)
        {
            _x = x;
            _y = y;
            _speedx = 10;
            _speedy = 0;
            _speed = 4;
            _name = name;
            _charge = GlobalHelpers.alea.Next(FULLCHARGE); // La charge initiale de la batterie est choisie aléatoirement


        }
        public int X { get { return _x; } }
        public int Y { get { return _y; } }
        public string Name { get { return _name; } }
        public static readonly int TAILLE = 47; 


        // Cette méthode calcule le nouvel état dans lequel le drone se trouve après
        // que 'interval' millisecondes se sont écoulées
        public void Update(int interval, List<Obstacle> obstacles)
        {
            int nextX = _x + _speedx;
            int nextY = _y + _speedy;

            bool collision = false;

            foreach (Obstacle obstacle in obstacles)
            {
                if (Collision(nextX, nextY, obstacle))
                {
                    collision = true;
                    break;
                }
            }

            if (!collision)
            {
                _x = nextX;
                _y = nextY;
            }

            _speedx = 0;
            _speedy = 0;
            _charge--;
        }

        public void avancer()
        {
            _speedy -= _speed;
        }
        public void reculer()
        {
            _speedy += _speed;
        }
        public void gauche()
        {
            _speedx -= _speed;
        }
        public void droite()
        {
            _speedx += _speed;
        }
        public void etat()
        {
            _speed = 2; 
        }
        private bool Collision(int nextX, int nextY, Obstacle obstacle)
        {
            return
                nextX < obstacle.X + obstacle.Largeur &&
                nextX + TAILLE > obstacle.X &&
                nextY < obstacle.Y + obstacle.Profondeur &&
                nextY + TAILLE > obstacle.Y;
        }
        public Image RotateImage(Image img, float angle)
        {
            Bitmap rotatedBmp = new Bitmap(img.Width, img.Height); //nouveau bitmap(information graphique) avec la taille de l'image
            rotatedBmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);
        

            using (Graphics g = Graphics.FromImage(rotatedBmp)) //permet de dessinner dans la nouvelle image (bitmap)
            {
                g.TranslateTransform((float)img.Width / 2, (float)img.Height / 2); // centre
                g.RotateTransform(angle); //rotation de l'angle
                g.TranslateTransform(-(float)img.Width / 2, -(float)img.Height / 2); //remet en haud a gauche
                g.DrawImage(img, new Point(0, 0)); //
            }

            return rotatedBmp; //envoie du nouveau bitmap avec l'iimage pivoter
        }


        public void shoot(List<Shoot> pulls, Point targetPosition)
        {
            DateTime now = DateTime.Now;

            if ((now - lastTireCall).TotalSeconds >= 0.3)
            {
                Image img = Resources.bullet;
                
                // 1. Calcul de l’angle en radians
                double dx = targetPosition.X - this.X;
                double dy = targetPosition.Y - this.Y;
                double angleRad = Math.Atan2(dy, dx);

                // 2. Conversion en degrés
                float angleDeg = (float)(angleRad * (180.0 / Math.PI));

                // 3. On crée l’image pivotée
                Image imgRotated = RotateImage(img, angleDeg);

                pulls.Add(new Shoot(this.X, this.Y, imgRotated, 35, targetPosition.X, targetPosition.Y));

                lastTireCall = now;
            }
        }
    } 
}
