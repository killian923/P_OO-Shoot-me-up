namespace Drones
{
    // Cette partie de la classe Drone définit ce qu'est un drone par un modèle numérique
    public partial class Drone
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
        public Drone(int x, int y, string name)
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

        // Cette méthode calcule le nouvel état dans lequel le drone se trouve après
        // que 'interval' millisecondes se sont écoulées
        public void Update(int interval)
        {

            _x = _speedx;
            _y = _speedy;
            _charge--;                                  // Il a dépensé de l'énergie
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

    }
}
