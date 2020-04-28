using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsBook.Domain.Managers
{
    public class SQLiteConnectionManager : IConnectionManager
    {

        #region Поля DB_RAS_Manager

        static string _providerName = "System.Data.SQLite.EF6";//"System.Data.SQLite.EF6";
        static string _serverName = @"|DataDirectory|\Data\ContactsBook_DB.db";
        static string _databaseName = "ContactsBook_DB";
        /// <summary>
        /// Объект-конструктор строки подключения Entity
        /// </summary>
        static EntityConnectionStringBuilder _entityBuilder = new EntityConnectionStringBuilder();
        /// <summary>
        /// Объект-конструктор строки подключения к БД
        /// </summary>
        static SqlConnectionStringBuilder _sqlBuilder = new SqlConnectionStringBuilder();

        #endregion

        #region Реализация IConnectionManager

        #region Методы
        public string GetConnectionString()
        {
            _sqlBuilder.DataSource = _serverName;
            _sqlBuilder.InitialCatalog = _databaseName;
            _sqlBuilder.IntegratedSecurity = true;
            //_entityBuilder.Name = "SQLite Data Provider";
            _entityBuilder.Provider = _providerName;
            _entityBuilder.ProviderConnectionString = _sqlBuilder.ToString();
            _entityBuilder.Metadata = @"res://*/DataStructs.ContactsBook_DB_Model.csdl|
                                        res://*/DataStructs.ContactsBook_DB_Model.ssdl|
                                        res://*/DataStructs.ContactsBook_DB_Model.msl";

            return _entityBuilder.ToString();
        }
        #endregion

        #endregion
    }
}
