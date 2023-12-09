using gameCenter.Projects.TodoList.Models;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace gameCenter.Projects.TodoList
{
    public partial class TodoList : Window
    {
        private TodoListModel _todoList;

        public TodoList()
        {
            InitializeComponent();
            InitializeTasks();
        }

        //InitializeTasks
        //1. creat new TodoListModel. 2. add 2 static Tasks. 3. listTasks.ItemSource=_todolist.Tasks
        private void InitializeTasks()
        {
            _todoList = new TodoListModel();
            listTasks.ItemsSource = _todoList.Tasks;
        }



        private void OnTaskToggled(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is TodoTask task)
            {
                _todoList.ToggleTaskIsComplete(task.Id);
            }
        }

        private void OnTextBlockMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                TextBlock textBlock = sender as TextBlock;
                StackPanel parent = textBlock.Parent as StackPanel;
                TextBox editTextBox = parent.FindName("editTaskDescription") as TextBox;
                Button btnSave = parent.FindName("btnSave") as Button;
                Button btnDel = parent.FindName("btnDel") as Button;

                // Hide the textBlock
                textBlock.Visibility = Visibility.Collapsed;

                // Show the TextBox and save button
                btnSave.Visibility = Visibility.Visible;
                btnDel.Visibility = Visibility.Visible;
                editTextBox.Visibility = Visibility.Visible;

                // Show the textBlock.Text in the TextBox
                editTextBox.Text = textBlock.Text;
            }
        }

        private void OnSaveEdit(object sender, RoutedEventArgs e)
        {
            Button btnSave = sender as Button;
            StackPanel parent = btnSave.Parent as StackPanel;
            TextBox editTextBox = parent.FindName("editTaskDescription") as TextBox;
            TextBlock textBlock = parent.FindName("txtTaskDescription") as TextBlock;

            TodoTask task = textBlock.DataContext as TodoTask;
            Button btnDel = parent.FindName("btnDel") as Button;

            editTextBox.Visibility = Visibility.Collapsed;
            btnSave.Visibility = Visibility.Collapsed;
            btnDel.Visibility = Visibility.Collapsed;
            textBlock.Visibility = Visibility.Visible;

            textBlock.Text = editTextBox.Text;
            _todoList.UpdateTask(task.Id, editTextBox.Text);
        }


        private void OnAddTask(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewTask.Text))
            {
                //creat the new task
                TodoTask newTask = new TodoTask(_todoList.Tasks.Count + 1, txtNewTask.Text);
                _todoList.AddNewTask(newTask);

                txtNewTask.Clear();
            }
        }

        private void OnDeleteTask(object sender, RoutedEventArgs e)
        {
            if (sender is Button deleteButton && deleteButton.DataContext is TodoTask task)
            {
                bool taskRemoved = _todoList.RemoveTask(task.Id);
                if (taskRemoved)
                {
                    // Optionally provide user feedback or update the UI
                }
            }
        }

        private void txtNewTask_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtNewTask.Text = "";

        }
    }
}
