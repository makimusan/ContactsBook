using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Infrastructure.Managers
{
    public static class DialogManager
    {
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="_msg"></param>
        //public static void ShowInfoMessage(string _msg)
        //{
        //    //MessageDialog.ShowMessage("Hura!", _msg, System.Windows.MessageBoxButton.OK);
        //    MessageDialog t_dlg = MessageDialog.GetInfoDialog("Внимание!", _msg);
        //    // Ссылка на родительское окно
        //    var _mainW = GetMainWindow();

        //    if (_mainW == null)
        //    {
        //        t_dlg.Close();
        //        return;
        //    }

        //    t_dlg.Owner = _mainW;

        //    ApplyEffect(t_dlg.Owner);

        //    t_dlg.ShowDialog();

        //    ClearEffect(t_dlg.Owner);
        //}

        ///// <summary>
        ///// Показывает вопросительный диалог
        ///// </summary>
        ///// <param name="_msg"></param>
        //public static bool ShowQuestionDialog(string _msg)
        //{
        //    // Переменная для результата
        //    bool t_rez = false;

        //    // Получение диалогового окна
        //    MessageDialog t_dlg = MessageDialog.GetQuestionDialog("Внимание!", _msg);
        //    // Ссылка на родительское окно
        //    var _mainW = GetMainWindow();
        //    // Проверить есть ли владелец
        //    if (_mainW == null)
        //    {
        //        t_dlg.Close();
        //        return t_rez;
        //    }
        //    // Установка владельца для текущего окна
        //    t_dlg.Owner = _mainW;
        //    // Применение эффекта для владельца
        //    ApplyEffect(t_dlg.Owner);
        //    // Получение результата от текущего диалогового окна
        //    t_rez = MessageBoxResult.Yes == MessageDialog.ShowModalDialog(t_dlg) ? true : false;
        //    // Очистка эффекта для владельца
        //    ClearEffect(t_dlg.Owner);
        //    t_dlg = null;
        //    return t_rez;
        //}

        /// <summary>
        /// Отображает произвольный диалог переданный пользователем
        /// </summary>
        /// <param name="_uDialog"></param>
        public static void ShowUserDialog(object uDialog)
        {
            var modalWindow = uDialog as Window;

            if (modalWindow == null) return;

            modalWindow.ShowDialog();

            modalWindow.Owner = null;

            if (modalWindow != null) modalWindow = null;

        }


    }
}
