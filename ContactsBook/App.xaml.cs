﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ContactsBook.Design.Views;

namespace ContactsBook
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            ContactsView view = new ContactsView();
            //view.DataContext = new ViewModels.MainViewModel();
            view.Show();
        }
    }
}
