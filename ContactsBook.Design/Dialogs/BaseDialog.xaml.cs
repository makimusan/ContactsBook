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

namespace ContactsBook.Design.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для BaseDialog.xaml
    /// </summary>
    public partial class BaseDialog : Window
    {
        public BaseDialog()
        {
            InitializeComponent();
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
