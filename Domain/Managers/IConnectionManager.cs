namespace ContactsBook.Domain.Managers
{
    public interface IConnectionManager
    {
        /// <summary>
        /// Возвращает строку подключения к БД
        /// </summary>
        /// <returns>Строка подключения</returns>
        string GetConnectionString();
    }
}
