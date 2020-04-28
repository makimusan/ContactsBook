namespace ContactsBook.Domain.Models
{
    /// <summary>
    /// Сущность телефонного номера
    /// </summary>
    public class PhoneNumberModel : ModelBase
    {
        #region Свойства

        private string _phoneNumber;
        /// <summary>
        /// Телефонный номер контакта
        /// </summary>
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (value != _phoneNumber)
                {
                    _phoneNumber = value;
                    OnPropertyChanged(nameof(PhoneNumber));
                }
            }
        }

        #endregion
    }
}
