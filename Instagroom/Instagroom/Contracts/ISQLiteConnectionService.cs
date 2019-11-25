using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Instagroom.Contracts
{
    public interface ISQLiteConnectionService
    {
        string GetDatabasePath(string filename);
        SQLiteAsyncConnection GetConnection();
    }
}
