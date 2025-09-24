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


        // Constructeur
        public Player(int x, int y, string name, int nextX, int nextY)
        {
            _x = x;
            _y = y;
            _speedx = 0;
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

    }
}
