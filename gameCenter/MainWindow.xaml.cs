using gameCenter.Projects;
using gameCenter.Projects.Calculator;
using gameCenter.Projects.CurrencyConverter;
using gameCenter.Projects.Snake;
using gameCenter.Projects.Tetris;
using gameCenter.Projects.TicTacToe;
using gameCenter.Projects.TodoList;
using gameCenter.Projects.UserManegment;
using GameCenter.Projects.CarGame;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace gameCenter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer clock = new()
            {
                Interval =TimeSpan.FromSeconds(1)
            };

            clock.Tick += ShowCurrentDate!;
            clock.Start();
        }




        private void ShowCurrentDate(object sender, EventArgs e)
        {
            DateLabel.Content = DateTime.UtcNow.ToString("dddd dd MMMM yyyy HH:mm:ss");
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Image image = (sender as Image)!;
            image.Opacity = 0.6;
            GameText.Content = (image.Name) switch
            {
                "Image1" => "a User Management System",
                "Image2" => "To do list Project",
                "Image3" => "Currency Convertor Project",
                "Image4" => "Car Game Project",
                "Image5" => "Tic Tac Toe Project",
                "Image6" => "Calculator Project",
                "Image7" => "Snake Game",
                "Image8" => "Tetris Game",
                "UserAvatar" => "Developer Information",

                _ => "please pick a game"
            };
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Image)!.Opacity = 1;
            GameText.Content = "please pick a game";
        }

        private void Image1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Project1 project1 = new();
            //Hide();
            //project1.ShowDialog();
            //Show();

            UserManegment UserManegmentProject = new();
            ProjectPresentationPage presentetion = new();
            presentetion.OnStart(
               "Users Managment System", "" +
                "- This is a Users-Managment Program.\n" +
                "- The Technologies I used in this project: C# based on WPF Interface, .NET.\n\n" +
                "- This Program will help you manage all the users on your website.\n" +
                "- You can do all the crud operation like add/delete/edit and update for all your users's name and email.\n" +
                "- You can also sign-in/out your users.\n" +
                "- In addition you can decide to freeze-up/down any of the users.\n\n" +
                "- To Start the program click on the Image at the left.\n" +
                "- To Return to the Home Page please click the user's avatar on the left side at the header above.",

                Image1.Source,
                UserManegmentProject
            );
            presentetion.ShowDialog();
        }

        private void Image2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //TodoList todoListProject = new();
            //Hide();
            //todoListProject.ShowDialog();
            //Show();
            TodoList project2 = new();
            ProjectPresentationPage presentetion = new();
            presentetion.OnStart(
                "To-Do List Program", "" +
                "- This is a To-do List Project.\n" +
                "- The Technologies I used in this project: C# based on WPF Interface, .NET.\n\n" +
                "- The application will help you manage your schedule by organizing all tasks in one place.\n" +
                "- You can edit, delete, add or mark as complete your to-do missions.\n" +
                "- You can also see the created date for each mission.\n" +
                "- To edit some tasks, double click on the task, then edit or Delete the task.\n" +
                "- To create a new task, please double click on 'Enter a new task' TextBox on the bottom of the page, then click the 'Plus' button to add the task to your To-Do List.\n\n" +
                "- To Start the program click on the Image at the left.\n" +
                "- To Return to the Home Page please click the user's avatar on the left side at the header above.",
                Image2.Source,
                project2
            );
            presentetion.ShowDialog();
        }

        private void Image3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //CurrencyConverterView currencyProject = new();
            //Hide();
            //currencyProject.ShowDialog();
            //Show();
            CurrencyConverterView project3 = new();
            ProjectPresentationPage presentetion = new();
            presentetion.OnStart(
                "currency convertor program", "" +
                "- This is a currencies convertor project.\n" +
                "- The Technologies I used in this project: C# based on WPF Interface .Net, HttpClient, Dictionary, JsonSerializer, Async, Api.\n\n" +
                "- This project will show you the current value of money you convert by the currencies on that exact day.\n" +
                "- The program also communicate with a currencies api to get the updated currencies's value every single day.\n" +
                "- Enter the amount you want to convert and pick a currencies from and to to see the exchange, then click convert button.\n" +
                "- Notice: my api key for this program is encoded, so make sure you enter the key by the instructions i have sent by the project (in the github).\n" +
                "- To Start the program click on the Image at the left.\n" +
                "- To Return to the Home Page please click the user's avatar on the left side at the header above.",
                Image3.Source,
                project3
            );
            presentetion.ShowDialog();

        }
        private void Image4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //CurrencyConverterView currencyProject = new();
            //Hide();
            //currencyProject.ShowDialog();
            //Show();
            CarGame project4 = new();
            ProjectPresentationPage presentetion = new();
            presentetion.OnStart(
               "Car Game", "" +
                "- In this racing game, your skill will pushed to the limit as you navigate a perilous highway filled with explosive obstacles.\n" +
                "- The Technologies I used in this project: C# based on WPF Interface, .Net, Dispatcher Time and the Controls Library.\n\n" +
                "- Your mission is to skillfully evade all the bombs and strive for the highest score possible.\n" +
                "- Every bomb you successfully evade and maneuver out of the way will contribute to your score.\n" +
                "- To move the car press the right and the left keys on your keyboard. \n" +
                "- The game will end the moment you collide with a bomb, and your score will be reset.\n\n" +
                "- To Start the program click on the Car Image at the left.\n" +
                "- To Return to the Home Page please click the user's avatar on the left side at the header above.",
                Image4.Source,
                project4
            );
            presentetion.ShowDialog();

        }



        private void Image5_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //CurrencyConverterView currencyProject = new();
            //Hide();
            //currencyProject.ShowDialog();
            //Show();
            TicTacToe project5 = new();
            ProjectPresentationPage presentetion = new();
            presentetion.OnStart(
                "Tic Tac Toe Game", "" +
                "- This is a classic Tic Tac Toe Game.\n" +
                "- The Technologies I used in this project: C# based on WPF Interface, .Net, UIElement Class, Task.Delay.\n\n" +
                "- This game you'll play againts the computer.\n" +
                "- To start play, click one of the buttons on the panel, you will begin the game by sympole 'X'.\n" +
                "- The winner is the first one to reach 3 buttons in a row or column or diagonal of his symbol (X/O).\n" +
                "- Each win will add to the player/computer score.\n\n" +
                "- To Start the program click on the TicTacToe Image at the left.\n" +
                "- To Return to the Home Page please click the user's avatar on the left side at the header above.",
                Image5.Source,
                project5
            );
            presentetion.ShowDialog();
       }


        private void Image6_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //CurrencyConverterView currencyProject = new();
            //Hide();
            //currencyProject.ShowDialog();
            //Show();
            Calculator project6 = new();
            ProjectPresentationPage presentetion = new();
            presentetion.OnStart(
                "Calculator ", "" +
                "- This is a Calculator Project.\n" +
                "- The Technologies I used in this project: C# based on WPF Interface, .NET. and Math library\n\n" +
                "- The application will help you Calculate your Calculations, you can also use Addition, subtraction, multiplication and other operations in one place.\n" +
                "- To calculate a specific operation please click on a number, an operaton and begin calculate, if you mistake click on the red button (C) to reset the calculator.\n" +
                "- At the end of any process, please click on the equal button to see the result.\n\n" +
                "- To Start the program click on the Calculator Image at the left.\n" +
                "- To Return to the Home Page please click the user's avatar on the left side at the header above.",
                Image6.Source,
                project6
            );
            presentetion.ShowDialog();

        }

        private void Image7_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //CurrencyConverterView currencyProject = new();
            //Hide();
            //currencyProject.ShowDialog();
            //Show();
            SnakeGame project7 = new();
            ProjectPresentationPage presentetion = new();
            presentetion.OnStart(
                "Snake Game ", "" +
                "- This is a classic Snake Game.\n" +
                "- The Technologies I used in this project: C# based on WPF Interface, .Net, Enum, IEnumerable,Dictionary,LinkedList, Task Delay, Transform Class and Key Control library.\n\n" +
                "- In This game you'll need to eat the food which appears in different places at the grid.\n" +
                "- To start the game, click any button on your keyboard, you will begin the game by Countdown from 3.\n" +
                "- To direct the snake press the right, the left, the down and the up key on your keyboard. \n" +
                "- Your mission is to skillfully eat all the foods and strive for the highest score possible.\n" +
                "- every time you eat the snake will grow up. \n" +
                "- You must not hit the sides of the window or the snake itself, otherwise you will die, if you die click any button to restart. \n" +

                "- To Start the program click on the Snake Image at the left.\n" +
                "- To Return to the Home Page please click the user's avatar on the left side at the header above.",
                Image7.Source,
                project7
            );
            presentetion.ShowDialog();

        }


        private void Image8_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //CurrencyConverterView currencyProject = new();
            //Hide();
            //currencyProject.ShowDialog();
            //Show();
            Tetris project8 = new();
            ProjectPresentationPage presentetion = new();
            presentetion.OnStart(
                "Tetris Game ", "" +
                "- This is a classic Tetris Game.\n" +
                "- The Technologies I used in this project: C# based on WPF Interface, .Net, IEnumerable, Dispatcher Time, Math Class, Task Delay, BitMapImage, Canvas and Key Control library.\n\n" +
                "- In This game you'll need to Use the arrow keys to move and rotate the Tetrimino to fit it into the desired location.\n" +
                "- To start the game, click any button on your keyboard, you will begin the game by Countdown from 3.\n" +
                "- Try to complete one or more horizontal lines with no gaps. When you do this, the line will disappear, and you'll earn points. \n" +
                "- Your mission is to skillfully fit Tetrimino in the desired location and strive for the highest score possible.\n" +
                "- As the game progresses, Tetriminos fall faster, making it more challenging. \n" +
                "- The game ends when a Tetrimino reaches the top of the playfield, if Game Over you can click any button to restart. \n" +

                "- To Start the program click on the Tetris Image at the left.\n" +
                "- To Return to the Home Page please click the user's avatar on the left side at the header above.",
                Image8.Source,
                project8
            );
            presentetion.ShowDialog();

        }


        private void Avatar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Name: Sultan Nassar\nEmail: sultannassar11@gmail.com\nPhone: 0543133603","Contact Information");
        }


    }
}
