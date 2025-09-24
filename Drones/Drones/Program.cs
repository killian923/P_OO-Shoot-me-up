using System.Windows.Forms;

namespace Drones
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Création de la flotte de drones
            List<Drone> fleet= new List<Drone>();
            List<Obstacle> Champ = new List<Obstacle>();

            Drone drone = new Drone(AirSpace.WIDTH / 2, AirSpace.HEIGHT / 2, "Joe", 0, 0);

            Obstacle obstacle1 = new Obstacle(1000, 200, Color.Gray, 100, 100);
            Obstacle obstacle2 = new Obstacle(800, 550, Color.Gray, 100, 100);
            Obstacle obstacle3 = new Obstacle(600, 50, Color.Gray, 100, 45);
            Obstacle obstacle4 = new Obstacle(400, 450, Color.Gray, 100, 50);
            Obstacle obstacle5 = new Obstacle(100, 150, Color.Gray, 100, 70);





            fleet.Add(drone);

            Champ.Add(obstacle1);
            Champ.Add(obstacle2);
            Champ.Add(obstacle3);
            Champ.Add(obstacle4);
            Champ.Add(obstacle5);


            // Démarrage
            Application.Run(new AirSpace(fleet , Champ));
        }
    }
}