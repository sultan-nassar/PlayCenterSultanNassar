
using gameCenter.Projects.Snake.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace gameCenter.Projects.Snake
{
    /// <summary>
    /// Interaction logic for SnakeGame.xaml
    /// </summary>
    public partial class SnakeGame : Window
    {
        private readonly Dictionary<GridValue, ImageSource> gridValToImage = new()
        {
            {GridValue.Empty, Images.Empty },
            {GridValue.Snake, Images.Body },
            {GridValue.Food, Images.Food },
        };

        //we add a dictionary which map direction to rotation (for the head)
        private readonly Dictionary<Direction, int> dirToRotation = new()
        {
            {Direction.Up, 0},
            {Direction.Right,90 },
            {Direction.Down,180 },
            {Direction.Left,270 }
        };


        private readonly int rows=20,cols=40;
        private readonly Image[,] gridImages;
        private GameState gameState;
        private bool gameRunning;


        public SnakeGame()
        {
            InitializeComponent();
            gridImages = SetupGrid();
            gameState=new GameState(rows,cols);
        }
        private async Task RunGame()
        
        {
            Draw();
            GameOverMenu.Visibility = Visibility.Hidden;
            await ShowCountDown();
            Overlay.Visibility = Visibility.Hidden;
            await GameLoop();
            await ShowGameOver();
            gameState = new GameState(rows,cols);
        }

        private async void Window_PreviewKeyDown(object sender, KeyEventArgs e)//The primary purpose of this method is to start or restart the game when a key is pressed. It serves as a way to initiate the game.
        {
            if(Overlay.Visibility == Visibility.Visible)
            {
                e.Handled = true; //this will prevent window key down from calling
            }

            if(!gameRunning)
            {
                gameRunning= true;
                await RunGame();
                gameRunning= false;
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)//This method allows the player to control the snake's movement during the game.

        {
            if (gameState.GameOver)
            {
                return;
            }

            switch(e.Key)
            {
                case Key.Left:
                    gameState.ChangeDirection(Direction.Left);
                    break;
                case Key.Right:
                    gameState.ChangeDirection(Direction.Right);
                    break;
                case Key.Up:
                    gameState.ChangeDirection(Direction.Up);
                    break;
                case Key.Down:
                    gameState.ChangeDirection(Direction.Down);
                    break;
            }
        }
        private async Task GameLoop()
        {
            while(!gameState.GameOver) 
            {
                await Task.Delay(100);
                gameState.Move();
                Draw();
            }
            GameOverMenu.Visibility = Visibility.Collapsed;
        }
        private Image[,] SetupGrid()
        {
            Image[,] images = new Image[rows,cols];
            GameGrid.Rows = rows;
            GameGrid.Columns = cols;
            GameGrid.Width=GameGrid.Height*(cols/(double)rows);


            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Image image = new Image { Source = Images.Empty,
                    RenderTransformOrigin=new Point(0.5,0.5),
                    };

                    images[i, j] = image;
                    GameGrid.Children.Add(image);
                }
            }
            return images;
        }
        private void Draw() //general draw method which call the DrawGrid method
        {
            DrawGrid();
            DrawSnakeHead();
            ScoreText.Text=$"SCORE {gameState.Score}";
        }

        private void DrawGrid()// this method will look at the grid array  and the gameState and update the grid images to reflected
        {
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < cols; j++)
                {
                    GridValue gridVal = gameState.Grid[i,j];
                    gridImages[i, j].Source = gridValToImage[gridVal];
                    gridImages[i,j].RenderTransform=Transform.Identity;// this insure that the only rotated image is the one with the snake head
                }
            }
        }

        //method for drawing the head of snake
        private void DrawSnakeHead()
        {
            Position headPos = gameState.HeadPosition();
            Image image = gridImages[headPos.Row, headPos.Col];
            image.Source = Images.Head;

            int rotation = dirToRotation[gameState.Dir];

            image.RenderTransform=new RotateTransform(rotation); //rotate the image by that amount
        }

        //method for drawing dead snake 
        private async Task DrawDeadSnake()
        {
            List<Position> positions = new List<Position>(gameState.SnakePositions());
            for(int i = 0;i < positions.Count;i++)
            {
                Position pos = positions[i];
                ImageSource source = (i == 0) ? Images.DeadHead : Images.DeadBody;
                gridImages[pos.Row, pos.Col].Source=source;
                await Task.Delay(50);
            }
        }

        //sample count down
        private async Task ShowCountDown()
        {
            for(int i = 3; i >=1; i--) 
            {
                OverlayText.Text=i.ToString();
                await Task.Delay(500);
            }
        }
        //methos to make restart situation for the game
        private async Task ShowGameOver()
        {
            await DrawDeadSnake();
            await Task.Delay(1000);
            GameOverMenu.Visibility = Visibility.Visible;
            FinalScoreText.Text = $"Score: {gameState.Score}";
            Overlay.Visibility = Visibility.Visible;
        }
       
    }
}
