namespace ContactsBook.Helpers.Validation
{
    /// <summary>
    /// Сущность с наборами методов валидации
    /// </summary>
    public static class ValidationManager
    {
        /// <summary>
        /// Возвращает результат проверки поля на содержание только букв.
        /// Включает проверку на пустое поле
        /// </summary>
        /// <param name="correntString"></param>
        /// <returns></returns>
        public static bool ContainsOnlyLetters(string currentString)
        {
            if (string.IsNullOrEmpty(currentString)) return false;

            foreach (char _c in currentString)
            {
                if ((_c >= 'a' && _c <= 'z') ||
                    (_c >= 'A' && _c <= 'Z') ||
                    (_c >= 'а' && _c <= 'я') ||
                    (_c >= 'А' && _c <= 'Я')) continue;

                return false;
            }

            return true;
        }

        /// <summary>
        /// Возвращает результат проверки поля на содержание только букв.
        /// Включает проверку на пустое поле
        /// </summary>
        /// <param name="correntString"></param>
        /// <returns></returns>
        public static bool ContainsOnlyNumbers(string currentString)
        {
            if (string.IsNullOrEmpty(currentString)) return false;

            foreach (char _c in currentString)
            {
                if ((_c >= '0' && _c <= '9')) continue;

                return false;
            }

            return true;
        }

        /// <summary>
        /// Возвращает результат проверки поля на содержание только букв.
        /// Включает проверку на пустое поле
        /// </summary>
        /// <param name="correntString"></param>
        /// <returns></returns>
        public static bool IsEMail(string currentString)
        {
            if (string.IsNullOrEmpty(currentString)) return false;

            if (currentString.Contains("@")) return true;

            return false;
        }

    }
}
