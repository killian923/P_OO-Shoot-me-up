using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drones
{
    // La classe AirSpace représente le territoire au dessus duquel les drones peuvent voler
    // Il s'agit d'un formulaire (une fenêtre) qui montre une vue 2D depuis en dessus
    // Il n'y a donc pas de notion d'altitude qui intervient

    public partial class AirSpace : Form
    {
        public static readonly int WIDTH = 1200;        // Dimensions of the airspace
        public static readonly int HEIGHT = 600;

        // La flotte est l'ensemble des drones qui évoluent dans notre espace aérien
        private List<Drone> fleet;
        private List<Obstacle> Champ;

        BufferedGraphicsContext currentContext;
        BufferedGraphics airspace;

        // Initialisation de l'espace aérien avec un certain nombre de drones
        public AirSpace(List<Drone> fleet, List<Obstacle> Champ)
        {
            InitializeComponent();
            // Gets a reference to the current BufferedGraphicsContext
            currentContext = BufferedGraphicsManager.Current;
            // Creates a BufferedGraphics instance associated with this form, and with
            // dimensions the same size as the drawing surface of the form.
            airspace = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
            this.fleet = fleet;
            this.Champ = Champ;

            this.KeyPreview = true;

            InitializeComponent();

            this.KeyPreview = true; // Ensures the form captures key events before child controls
            this.KeyUp += Form1_KeyUp;
            this.KeyDown += Form1_KeyDown;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    foreach (var drone in fleet)
                    {
                        drone.avancer();
                    }
                    break;
                case Keys.S:
                    foreach (var drone in fleet)
                    {
                        drone.reculer();
                    }
                    break;
                case Keys.A:
                    foreach (var drone in fleet)
                    {
                        drone.gauche();
                    }
                    break;
                case Keys.D:
                    foreach (var drone in fleet)
                    {
                        drone.droite();
                    }
                    break;
            }
        }
        public static bool DetecterCollision(Drone drone, Obstacle obstacle)
        {
            return
                drone.X < obstacle.X + obstacle.Largeur &&
                drone.X + Drone.TAILLE > obstacle.X &&
                drone.Y < obstacle.Y + obstacle.Profondeur &&
                drone.Y + Drone.TAILLE > obstacle.Y;
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            foreach (var drone in fleet)
            {
                drone.etat(); // Ajuste la vitesse à 2 (comme dans ta classe)

                for (int i = 0; i < 5; i++)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.W:
                            drone.avancer();
                            break;
                        case Keys.D:
                            drone.droite();
                            break;
                        case Keys.S:
                            drone.reculer();
                            break;
                        case Keys.A:
                            drone.gauche();
                            break;
                        default:
                            break;
                    }

                    drone.Update(100, Champ); // obstaclesList = ta liste d'obstacles
                    Render();
                    Thread.Sleep(50);
                }
            }
        }


        // Affichage de la situation actuelle
        private void Render()
        {
            airspace.Graphics.Clear(Color.AliceBlue);

            // draw drones
            foreach (Drone drone in fleet)
            {
                drone.Render(airspace);
            }
            foreach (Obstacle obstacle in Champ)
            {
                obstacle.Render(airspace);
            }

            airspace.Render();
        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval, List<Obstacle> obstacles)
        {
            foreach (Drone drone in fleet)
            {
                drone.Update(100, Champ);
            }
        }

        // Méthode appelée à chaque frame
        private void NewFrame(object sender, EventArgs e)
        {
            this.Update(ticker.Interval, Champ);
            this.Render();
        }
    }
}