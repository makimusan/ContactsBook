using ContactsBook.Design.Interfaces;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ContactsBook.Design.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для BaseDialog.xaml
    /// </summary>
    public partial class BaseDialog : Window, IView
    {
        MessageBoxResult Result = MessageBoxResult.None;

        public BaseDialog()
        {
            InitializeComponent();
        }

        public static BaseDialog ShowInfoDialog(string _caption, string _message)
        {
            BaseDialog dialog = new BaseDialog();
            dialog.lbl_Title.Text = _caption;
            dialog.MessageContainer.Text = _message;
            dialog.AddButtons(MessageBoxButton.OK);
            return dialog;
        }

        public static BaseDialog ShowQuestionDialog(string title, string question)
        {
            BaseDialog dialog = new BaseDialog();
            dialog.lbl_Title.Text = title;
            dialog.MessageContainer.Text = question;
            dialog.AddButtons(MessageBoxButton.YesNo);
            return dialog;
        }

        /// <summary>
        /// Добавляет необходимые кнопки на к диалогу
        /// </summary>
        /// <param name="buttons"></param>
        private void AddButtons(MessageBoxButton buttons)
        {
            switch (buttons)
            {
                case MessageBoxButton.OK:
                    AddButton("OK", MessageBoxResult.OK);
                    break;
                case MessageBoxButton.YesNo:
                    AddButton("Да ", MessageBoxResult.Yes, dialogResult: true);
                    AddButton("Нет", MessageBoxResult.No, true);
                    break;
                default:
                    throw new ArgumentException("Unknown button value", "buttons");
            }
        }

        private void AddButton(string text, MessageBoxResult result, bool isCancel = false, bool dialogResult = false)
        {
            var button = new Button() { Content = text, IsCancel = isCancel };
            button.Click += (o, args) => { Result = result; DialogResult = dialogResult; };

            ButtonContainer.Children.Add(button);
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
