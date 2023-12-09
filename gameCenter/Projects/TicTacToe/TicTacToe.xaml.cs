using gameCenter.Projects.TicTacToe.Models;
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
using System.Windows.Shapes;

namespace gameCenter.Projects.TicTacToe
{
    /// <summary>
    /// Interaction logic for TicTacToe.xaml
    /// </summary>
    public partial class TicTacToe : Window
    {
        GameTicTacToe GameModel;
        int UserScore = 0;
        int ComputerScore = 0;
        public TicTacToe()
        {
            InitializeComponent();
            GameModel = new GameTicTacToe();
        }

        private async void UserPlay_Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int row = Grid.GetRow(btn);
            int col = Grid.GetColumn(btn);

            if (GameModel.GameBoard[row, col] == 0 && GameModel.CurrentPlayer == 'X')
            {
                GameModel.GameBoard[row, col] = 'X';
                btn.Content = "X";

                if (GameModel.CheckForWin())
                {
                    UserScore++;

                    UserScore1.Text = "Your Score: "+ UserScore.ToString();
                    MessageBox.Show($" You wins!, Computer Score: {ComputerScore} \n Your Score: {UserScore}", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetGame();
                    return;
                }
                else if (GameModel.IsBoardFull())
                {
                    MessageBox.Show($"It's a draw! \n Computer Score: {ComputerScore} \n Your Score: {UserScore}", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetGame();
                    return;
                }

                GameModel.ToggleCurrentPlayer();
                txtCurrentPlayer.Text = $"Current Player: {GameModel.ComputerPlayer}";

                // Now it's the computer's turn
                await Task.Delay(100); // Delay to simulate computer's "thinking" time
                PerformComputerMove();
            }
        }


        private void ResetGame()
        {
            GameModel = new GameTicTacToe();
            txtCurrentPlayer.Text = $"Current Player: {GameModel.CurrentPlayer}";

            foreach (Button btn in MainGrid.Children)
            {
                btn.Content = "";
                btnRestart.Content = "Restart Game"; // Show the Restart button
            }
        }
        private async void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(50);// this delay is to ensure that the computer take its time to play
                                 // this is critic if i press reset game immeditly after the current player play

            ResetGame();
            UserScore = 0;
            ComputerScore = 0;
            UserScore1.Text = "Your Score: " + UserScore.ToString();
            ComputerScore1.Text = "Computer Score: " + ComputerScore.ToString();
        }


        private async void PerformComputerMove()
        {
            Random random = new Random();

            // Simulate "thinking" time
            await Task.Delay(100);

            // Generate random coordinates for the computer's move
            int row, col;
            do
            {
                row = random.Next(0, 3);
                col = random.Next(0, 3);
            } while (GameModel.GameBoard[row, col] != 0); // Keep trying until an empty cell is found

            // Update the game board and UI
            GameModel.GameBoard[row, col] = 'O';
            Button btn = (Button)MainGrid.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == col);
            btn.Content = "O";

            // Check for win/draw and toggle the player
            if (GameModel.CheckForWin())
            {
                ComputerScore++;
                ComputerScore1.Text = "Computer Score: " + ComputerScore.ToString();


                MessageBox.Show($"Computer wins! Computer Score: {ComputerScore} \n Your Score: {UserScore}", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
                ResetGame();
            }
            else if (GameModel.IsBoardFull())
            {
                MessageBox.Show($"It's a draw! \n Computer Score: {ComputerScore} \n Your Score: {UserScore}", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
                ResetGame();
            }
            else
            {
                GameModel.ToggleCurrentPlayer();
                txtCurrentPlayer.Text = $"Current Player: {GameModel.CurrentPlayer}"; // Back to human's turn
            }
        }

    }
}
