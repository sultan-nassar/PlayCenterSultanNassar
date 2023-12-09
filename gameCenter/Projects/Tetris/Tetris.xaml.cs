using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace gameCenter.Projects.Tetris
{
    /// <summary>
    /// Interaction logic for Tetris.xaml
    /// </summary>
    public partial class Tetris : Window
    {
        private DispatcherTimer countdownTimer;
        private int countdownDuration = 3; // Change this to the desired countdown duration in seconds

        private readonly ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileRed.png", UriKind.Relative))
        };
        private readonly ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-I.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-O.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-S.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-T.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-Z.png", UriKind.Relative))
        };
        private readonly Image[,] imageControls;
        private readonly int maxDelay = 1000; //this for control the delay between moving the block down
        private readonly int minDelay = 75;//this for control the delay between moving the block down
        private readonly int delayDecrease = 25;
        private Models.GameState gameState = new Models.GameState();
        public Tetris()
        {
            InitializeComponent();
            StartScreenOverlay.Visibility = Visibility.Visible;
            TextBoxStartGame.Visibility = Visibility.Visible;
            imageControls = SetupGameCanvas(gameState.GameGrid);
        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (StartScreenOverlay.Visibility == Visibility.Visible)
            {
                StartScreenOverlay.Visibility = Visibility.Collapsed;
                StartCountdown();
                TextBoxStartGame.Visibility = Visibility.Collapsed;
                e.Handled = true;
            }
            else if (countdownDuration <= 0 && !gameState.GameOver)
            {
                // Handle the key press for controlling the game (e.g., moving the block or rotating) here
                switch (e.Key)
                {
                    case Key.Left:
                        gameState.MoveBlockLeft();
                        break;
                    case Key.Right:
                        gameState.MoveBlockRight();
                        break;
                    case Key.Down:
                        gameState.MoveBlockDown();
                        break;
                    case Key.Up:
                        gameState.RotateBlockCW();
                        break;
                    case Key.Z:
                        gameState.RotateBlockCCW();
                        break;
                    case Key.C:
                        gameState.HoldBlock();
                        break;
                    case Key.Space:
                        gameState.DropBlock();
                        break;
                }
                Draw(gameState);
            }
        }
        private Image[,] SetupGameCanvas(Models.GameGrid grid)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            int cellSize = 25;
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    Image imageControl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize
                    };
                    Canvas.SetTop(imageControl, (r - 2) * cellSize + 10);
                    Canvas.SetLeft(imageControl, c * cellSize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[r, c] = imageControl;
                }
            }
            return imageControls;
        }
        private void DrawGrid(Models.GameGrid grid)  //method which draw the game grid
        {
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    int id = grid[r, c];
                    imageControls[r, c].Opacity = 1;
                    imageControls[r, c].Source = tileImages[id];
                }
            }
        }
        private void DrawBlock(Models.BlockShape block) //method which draw the game block
        {
            foreach (Models.Position p in block.TilePositions())
            {
                imageControls[p.Row, p.Column].Opacity = 1;
                imageControls[p.Row, p.Column].Source = tileImages[block.Id];
            }
        }
        private void DrawNextBlock(Models.BlockQueue blockQueue)
        {
            Models.BlockShape next = blockQueue.NextBlock;
            NextImage.Source = blockImages[next.Id];
        }
        private void DrawHeldBlock(Models.BlockShape heldBlock)
        {
            if (heldBlock == null)
            {
                HoldImage.Source = blockImages[0];
            }
            else
            {
                HoldImage.Source = blockImages[heldBlock.Id];
            }
        }
        private void DrawGhostBlock(Models.BlockShape block) //method which help us see where the block will land
        {
            int dropDistance = gameState.BlockDropDistance();

            foreach (Models.Position p in block.TilePositions())
            {
                imageControls[p.Row + dropDistance, p.Column].Opacity = 0.25;
                imageControls[p.Row + dropDistance, p.Column].Source = tileImages[block.Id];
            }
        }
        private void Draw(Models.GameState gameState) // draw method its usefull for drawing game grid, gameblock,nextblock,heldblock,score and the ghostblock
        {
            DrawGrid(gameState.GameGrid);
            DrawGhostBlock(gameState.CurrentBlock);
            DrawBlock(gameState.CurrentBlock);
            DrawNextBlock(gameState.BlockQueue);
            DrawHeldBlock(gameState.HeldBlock);
            ScoreText.Text = $"Score: {gameState.Score}";
        }
        private async Task GameLoop() // game loop it must be async becuase we want to await without blocking the UI 
        {
            Draw(gameState);

            while (!gameState.GameOver)
            {
                int delay = Math.Max(minDelay, maxDelay - (gameState.Score * delayDecrease)); //the delay decrease by the score number
                await Task.Delay(delay);
                gameState.MoveBlockDown();
                Draw(gameState);
            }
            GameOverMenu.Visibility = Visibility.Visible;
            FinalScoreText.Text = $"Score: {gameState.Score}";
        }
        private async void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            gameState = new Models.GameState();
            GameOverMenu.Visibility = Visibility.Hidden;
            await GameLoop();

        }
        private async void StartGame()
        {
            // Hide the start game elements like countdown and instructions
            StartScreenOverlay.Visibility = Visibility.Collapsed;
            TextBoxStartGame.Visibility = Visibility.Collapsed;
            CountdownText.Visibility = Visibility.Collapsed;

            // Perform any game initialization here
            gameState = new Models.GameState();

            // Draw the initial game state
            Draw(gameState);

            // Start the game loop
            await GameLoop();
        }
        private async void CountdownTimer_Tick(object sender, EventArgs e)
        {
            countdownDuration--;

            if (countdownDuration == 0)
            {
                CountdownText.Text = "Go!";
                await Task.Delay(1000); // Wait for 1 second (you can adjust this delay as needed)
                countdownTimer.Stop();
                 StartGame(); //  this line to start the game after the countdown
            }
            else if (countdownDuration > 0)
            {
                CountdownText.Text = countdownDuration.ToString();
            }
        }
        private void StartCountdown()
        {
            countdownTimer = new DispatcherTimer();
            countdownTimer.Interval = TimeSpan.FromSeconds(1);
            countdownTimer.Tick += CountdownTimer_Tick!;
            countdownTimer.Start();
            CountdownText.Text = countdownDuration.ToString();
        }
    }
}
