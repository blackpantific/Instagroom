using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Instagroom.Contracts;
using SQLite;
using System.Runtime.CompilerServices;
using Instagroom.Droid.Services;

namespace Instagroom.Droid.Services
{
    class AndroidDBConnectionService : ISQLiteConnectionService
    {
        //разумизм
        public SQLiteAsyncConnection GetConnection()
        {
            var fileName = "InstagroomDB.db3";
            return new SQLiteAsyncConnection(this.GetDatabasePath(fileName));
            
        }

        public string GetDatabasePath(string filename)
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, filename);

            if (!File.Exists(path))
            {
                // получаем контекст приложения
                Context context = Android.App.Application.Context;
                var dbAssetStream = context.Assets.Open(filename);

                var dbFileStream = new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate);
                var buffer = new byte[1024];

                int b = buffer.Length;
                int length;

                while ((length = dbAssetStream.Read(buffer, 0, b)) > 0)
                {
                    dbFileStream.Write(buffer, 0, length);
                }

                dbFileStream.Flush();
                dbFileStream.Close();
                dbAssetStream.Close();
            }

            return path;
        }

        public AndroidDBConnectionService()
        {

        }
    }
}