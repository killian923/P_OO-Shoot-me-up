using System.Collections.Generic;
using System.Numerics;
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
        private Point mousePosition;

        // La flotte est l'ensemble des drones qui évoluent dans notre espace aérien
        private List<Player> fleet;
        private List<Obstacle> fields;
        private List<Shoot> pulls;
        private Player _player;
        BufferedGraphicsContext currentContext;
        BufferedGraphics airspace;

        // Initialisation de l'espace aérien avec un certain nombre de drones
        public AirSpace(Player player, List<Obstacle> fields, List<Shoot> pulls)
        {
            InitializeComponent();
            this.MouseMove += AirSpace_MouseMove;
            this.MouseClick += AirSpace_MouseClick;
            // Gets a reference to the current BufferedGraphicsContext
            currentContext = BufferedGraphicsManager.Current;
            // Creates a BufferedGraphics instance associated with this form, and with
            // dimensions the same size as the drawing surface of the form.
            airspace = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
            this._player = player;
            this.fields = fields;
            this.pulls = pulls;

            this.KeyPreview = true;
            _player = new Player();


            this.KeyPreview = true; // Ensures the form captures key events before child controls
            this.KeyUp += Form1_KeyUp;
            this.KeyDown += Form1_KeyDown;
        }
        private void AirSpace_MouseMove(object sender, MouseEventArgs e)
        {
            mousePosition = e.Location;
        }
        private void AirSpace_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                _player.shoot(pulls, mousePosition);
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:

                    _player.avancer();
                    break;
                case Keys.S:
                    _player.reculer();
                    break;
                case Keys.A:
                    _player.gauche();
                    break;
                case Keys.D:
                    _player.droite();
                    break;

            }
        }
        public static bool DetecterCollision(Player _player, Obstacle obstacle)
        {
            return
                _player.playerX < obstacle.X + obstacle.Largeur &&
                _player.playerX + Player.TAILLE > obstacle.X &&
                _player.playerY < obstacle.Y + obstacle.Profondeur &&
                _player.playerY + Player.TAILLE > obstacle.Y;
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            _player.etat(); // Ajuste la vitesse à 2 (comme dans ta classe)

            for (int i = 0; i < 5; i++)
            {
                switch (e.KeyCode)
                {
                    case Keys.W:
                        _player.avancer();
                        break;
                    case Keys.D:
                        _player.droite();
                        break;
                    case Keys.S:
                        _player.reculer();
                        break;
                    case Keys.A:
                        _player.gauche();
                        break;
                    default:
                        break;
                }

                _player.Update(100, fields);
                Render();
                Thread.Sleep(5);
            }
        }


        // Affichage de la situation actuelle
        private void Render()
        {
            airspace.Graphics.Clear(Color.AliceBlue);

            // draw drones

            _player.Render(airspace);

            foreach (Obstacle obstacle in fields)
            {
                obstacle.Render(airspace);
            }
            foreach (Shoot pulls in pulls)
            {
                pulls.Render(airspace);
            }

            airspace.Render();
        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval, List<Obstacle> obstacles)
        {


            _player.Update(10, fields);

            foreach (Shoot pulls in pulls)
            {
                pulls.Update();
            }

            List<Shoot> projectilesToRemove = new List<Shoot>();
            List<Obstacle> obstaclesToRemove = new List<Obstacle>();

            foreach (var projectile in pulls)
            {


                Rectangle projRect = projectile.GetRectangle();

                bool handled = false;


                foreach (var obstacle in fields)
                {
                    if (projRect.IntersectsWith(obstacle.GetRectangle()))
                    {
                        projectilesToRemove.Add(projectile);
                        obstacle.Vie--;
                        if (obstacle.Vie == 0)
                            obstaclesToRemove.Add(obstacle);

                        handled = true;
                        break;
                    }
                }
                if (handled) continue;
            }
            foreach (var o in obstaclesToRemove)
                fields.Remove(o);

            foreach (var p in projectilesToRemove)
                pulls.Remove(p);

        }

        // Méthode appelée à chaque frame
        private void NewFrame(object sender, EventArgs e)
        {
            this.Update(ticker.Interval, fields);
            this.Render();
        }
    }
}