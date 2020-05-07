namespace ContactsBook.Helpers.Interfaces
{
    public interface IClosable
    {
        void Close();

        bool? DialogResult { get; set; }
    }
}
