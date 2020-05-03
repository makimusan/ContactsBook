using ContactsBook.Design.Interfaces;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ContactsBook.Design.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для BaseDialogWindow.xaml
    /// </summary>
    public partial class BaseDialogWindow : Window, IView
    {

        MessageBoxResult Result = MessageBoxResult.None;

        public BaseDialogWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Добавляет необходимые кнопки на к диалогу
        /// </summary>
        /// <param name="buttons"></param>
        private void AddButtons(MessageBoxButton buttons)
        {
            switch (buttons)
            {
                case MessageBoxButton.YesNo:
                    AddButton("Сохранить ", MessageBoxResult.Yes, dialogResult:true, pathToCommandBinding: "SaveCommand");
                    AddButton("Отмена", MessageBoxResult.No, true);
                    break;
                default:
                    throw new ArgumentException("Unknown button value", "buttons");
            }
        }

        private void AddButton(string text, MessageBoxResult result, bool isCancel = false, bool dialogResult = false, string pathToCommandBinding = null)
        {
            var button = new Button() { Content = text, IsCancel = isCancel };
            button.Click += (o, args) => { Result = result; DialogResult = dialogResult; };

            if(pathToCommandBinding != null)
            button.SetBinding(Button.CommandProperty, pathToCommandBinding);
            
            ButtonContainer.Children.Add(button);
        }

        //private Binding AddBinding(string bindingPath)
        //{
        //    switch (bindingPath)
        //    {
        //        case "SaveCommand":
        //            return new Binding(bindingPath);
        //        default:
        //            throw new ArgumentException("Unknown binding value", "Binding command");
        //    }
        //}

        public static BaseDialogWindow GetWindowDialog(UserControl userControl)
        {
            BaseDialogWindow dialog = new BaseDialogWindow();
            //dialog.lbl_Title.Content = _caption;
            dialog.ContentContainer.Content = userControl;
            //dialog.Height = userControl.Height;
            //dialog.Width = userControl.Width;
            dialog.AddButtons(MessageBoxButton.YesNo);
            return dialog;
        }

        /// <summary>
        /// Реагирует на зажатую клавишу мыши.Осуществляет перетаскивание окна по области экрана
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grd_Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Метод позволяет перемещать окно за указателем мыши
            //при зажатой клавише мыши
            DragMove();
        }
    }
}
