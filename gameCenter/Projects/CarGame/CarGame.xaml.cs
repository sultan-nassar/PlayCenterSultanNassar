using GameCenter.Projects.CarGame.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace GameCenter.Projects.CarGame
{
    public partial class CarGame : Window
    {
        private PlayerCar playerCar;
        private List<Obstacle> obstacles;
        private Random random;
        private int score;
        private int countdownDuration = 3; // Countdown duration in seconds
        private DispatcherTimer countdownTimer;
        public CarGame()
        {

            InitializeComponent();

            backgroundVideo.Source = new Uri("Projects/CarGame/Assets/natureRoad.mp4", UriKind.Relative);

            backgroundVideo.Play();

            // Initialize other game-related components...

            playerCar = new PlayerCar(500, 450, 15, playerCarImage);

            obstacles = new List<Obstacle>();
            random = new Random();

            // Display "Press any key to start" initially
            StartScreenOverlay.Visibility = Visibility.Visible;
            PressAnyKeyText.Visibility = Visibility.Visible;

            // Start countdown when any key is pressed
            PreviewKeyDown += Window_PreviewKeyDown;
        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (StartScreenOverlay.Visibility == Visibility.Visible)
            {
                StartCountdown();
                e.Handled = true;
                PressAnyKeyText.Visibility = Visibility.Collapsed;
            }
            else if (countdownDuration <= 0)
            {
                switch (e.Key)
                {
                    case Key.Left:
                        playerCar.LeftKeyPressed = true;
                        break;
                    case Key.Right:
                        playerCar.RightKeyPressed = true;
                        break;
                }
            }
        }
        private async void StartCountdown()
        {
            countdownTimer = new DispatcherTimer();
            countdownTimer.Interval = TimeSpan.FromSeconds(1);
            countdownTimer.Tick += CountdownTimer_Tick;
            countdownTimer.Start();
            CountdownText.Text = countdownDuration.ToString();
        }
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    playerCar.LeftKeyPressed = true;
                    break;
                case Key.Right:
                    playerCar.RightKeyPressed = true;
                    break;
            }
        }
        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    playerCar.LeftKeyPressed = false;
                    break;
                case Key.Right:
                    playerCar.RightKeyPressed = false;
                    break;
            }
        }
        private async void CountdownTimer_Tick(object sender, EventArgs e)
        {
            countdownDuration--;

            if (countdownDuration == 0)
            {
                CountdownText.Text = "Go!";
                countdownTimer.Stop();

                // Introduce a delay here to display "Go!" before hiding the overlay and text
                await Task.Delay(1000); // Adjust the delay duration as needed

                // Hide the overlay screen and "Press any key" text
                StartScreenOverlay.Visibility = Visibility.Collapsed;
                PressAnyKeyText.Visibility = Visibility.Collapsed;

                // Start the game here
                StartGame();
            }
            else if (countdownDuration > 0)
            {
                CountdownText.Text = countdownDuration.ToString();
            }
        }
        private async void StartGame()
        {
            CountdownText.Visibility = Visibility.Collapsed;

            // Perform any game initialization here
            playerCar.X = 500;
            playerCar.Y = 450;
            obstacles.Clear();
            score = 0;
            scoreTextBlock.Text = "Score: " + score;

            DispatcherTimer gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(13);
            gameTimer.Tick += GameLoop;
            gameTimer.Start();
        }
        private void GameLoop(object sender, EventArgs e)
        {
       
            bool isGameOver = false;

            playerCar.Move();

            if (random.Next(0, 50) == 1)
            {
                Image obstacleImage = new Image();

                obstacleImage.Source = new BitmapImage(new Uri("Assets/Bomb.png", UriKind.Relative));

                obstacleImage.Width =60;
                obstacleImage.Height = 60;
                Obstacle obstacle = new Obstacle(random.Next(0, (int)Width), 0, 2, obstacleImage);
                obstacles.Add(obstacle);
                gameCanvas.Children.Add(obstacleImage);
            }

            double collisionBuffer = 7;
            List<Obstacle> copiedList = new List<Obstacle>(obstacles);

            foreach (var obstacle in copiedList)
            {
                obstacle.Move();

                if (obstacle.Representation.Margin.Top > this.Height ||
                    obstacle.Representation.Margin.Left + obstacle.Representation.Width < 0 ||
                    obstacle.Representation.Margin.Left > this.Width)
                {
                    gameCanvas.Children.Remove(obstacle.Representation);
                    obstacles.Remove(obstacle);
                    score++;
                    scoreTextBlock.Text = "score: " + score;
                }

                if (playerCar.Representation.Margin.Left + playerCar.Representation.Width - collisionBuffer >= obstacle.Representation.Margin.Left + collisionBuffer
                    && playerCar.Representation.Margin.Left + collisionBuffer <= obstacle.Representation.Margin.Left + obstacle.Representation.Width - collisionBuffer
                    && playerCar.Representation.Margin.Top + collisionBuffer <= obstacle.Representation.Margin.Top + obstacle.Representation.Height - collisionBuffer
                    && playerCar.Representation.Margin.Top + playerCar.Representation.Height - collisionBuffer >= obstacle.Representation.Margin.Top + collisionBuffer)
                {
                    // End the game
                    (sender as DispatcherTimer).Stop();
                    isGameOver = true;
                    break;
                }
            }

            if (isGameOver)
            {
                GameOverMenu.Visibility = Visibility.Visible;
                backgroundVideo.Stop();

                FinalScoreText.Text = "Your Score is: " + score;

                MessageBoxResult gameOverResult = MessageBox.Show($"Game Over! Would you like to play again?", "Game Over", MessageBoxButton.YesNo);
                if (gameOverResult == MessageBoxResult.Yes)
                {
                    RestartGame();
                    backgroundVideo.Play();

                }
                if (gameOverResult == MessageBoxResult.No)
                {
                    Close();
                }
            }
        }
        private void RestartGame()
        {
            score = 0;
            scoreTextBlock.Text = "Score: " + score;

            foreach (var obstacle in obstacles)
            {
                gameCanvas.Children.Remove(obstacle.Representation);
            }

            obstacles.Clear();
            playerCar.X = 500;
            playerCar.Y = 450;

            DispatcherTimer gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(13);
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            GameOverMenu.Visibility = Visibility.Collapsed;
            Congradution.Visibility = Visibility.Collapsed;
        }
        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            backgroundVideo.Position = TimeSpan.Zero;
            backgroundVideo.Play();
        }
        private void MediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(e.ErrorException.Message);
        }
    }
}