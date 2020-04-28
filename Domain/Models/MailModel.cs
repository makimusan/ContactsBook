namespace ContactsBook.Domain.Models
{
    /// <summary>
    /// Сущность почтового адреса модели
    /// </summary>
    public class MailModel : ModelBase
    {
        #region Свойства

        private string _mailOfContact;
        /// <summary>
        /// Почтовый адрес контакта
        /// </summary>
        public string MailOfContact
        {
            get { return _mailOfContact; }
            set
            {
                if (value != _mailOfContact)
                {
                    _mailOfContact = value;
                    OnPropertyChanged(nameof(MailOfContact));
                }
            }
        }

        #endregion
    }
}
