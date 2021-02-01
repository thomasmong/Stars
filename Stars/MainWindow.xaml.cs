using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Stars
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Point Direction;
        private List<Star> ListeStars;
        private Random Rdm;

        public MainWindow()
        {
            InitializeComponent();

            //Initialisation direction
            Direction = new Point(MainCanvas.Width / 2, MainCanvas.Height / 2);

            //Timer
            DispatcherTimer Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(20);
            Timer.Tick += Update;

            //Création des étoiles
            ListeStars = new List<Star>(10);
            Rdm = new Random();
            for (int i = 0; i < 500; i++)
            {
                Point pos = new Point(Rdm.NextDouble() * MainCanvas.Width, Rdm.NextDouble() * MainCanvas.Height);
                Star star = new Star(MainCanvas, pos);
                ListeStars.Add(star);
            }

            foreach (var star in ListeStars)
            {
                Console.WriteLine(star.Position);
            }

            MainCanvas.Cursor = Cursors.None;

            //Lancer 
            Timer.Start();
        }

        public void Update(object sender, EventArgs e)
        {
            Direction = Mouse.GetPosition(MainCanvas);
            List<Star> listeStarsToRemove = new List<Star>();
            foreach (var star in ListeStars)
            {
                star.Update(Direction);
                if (!star.IsVisible())
                {
                    MainCanvas.Children.Remove(star.Shape);
                    listeStarsToRemove.Add(star);
                }
            }

            NewList(listeStarsToRemove);
        }

        private void NewList(List<Star> list)
        {
            foreach (var star in list)
            {
                ListeStars.Remove(star);
                Point pos = new Point(Rdm.NextDouble() * MainCanvas.Width, Rdm.NextDouble() * MainCanvas.Height);
                ListeStars.Add(new Star(MainCanvas, pos));
            }
        }
    }
}