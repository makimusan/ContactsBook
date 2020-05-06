using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ContactsBook.Design.Interfaces;

namespace ContactsBook.Locator.Services
{
    public interface IServiceLocator
    {
        void ActivateMainWindow<T>(T dataContext);

        bool? ActivateContactWindowDialog<T>(T dataContext);

        /// <summary>
        /// Показывает диалог с вопросом
        /// </summary>
        /// <param name="message">Заголовок диалога</param>
        /// <param name="question">Сообщение</param>
        /// <returns></returns>
        bool? ShowQuestionDialog(string title, string question);

        /// <summary>
        /// Показывает информационный диалог
        /// </summary>
        /// <param name="message">Заголовок диалога</param>
        /// <param name="question">Сообщение</param>
        /// <returns></returns>
        bool? ShowInfoDialog(string title, string question);
    }
}
