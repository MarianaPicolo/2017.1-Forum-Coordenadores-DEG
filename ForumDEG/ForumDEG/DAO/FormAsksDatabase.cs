﻿using ForumDEG.Models;
using ForumDEG.Utils;
using SQLite;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ForumDEG.DAO {
    public class FormAsksDatabase {

        readonly SQLiteAsyncConnection _database;

        private static FormAsksDatabase _formAsksDatabase = null;

        private FormAsksDatabase(string databasePath) {
            _database = new SQLiteAsyncConnection(databasePath);

            _database.CreateTableAsync<FormAsk>().Wait();
            Debug.WriteLine("FormAsksDatabase: Database created.");
        }

        public static FormAsksDatabase getFormDB {
            get {
                if (_formAsksDatabase == null) {
                    _formAsksDatabase = new FormAsksDatabase(DependencyService.Get<InterfaceSQLite>().GetLocalFilePath("FormAsks.db3"));
                    Debug.WriteLine("FormAsksDatabase getFormDB: _formAsksDatabase getted.");
                }

                Debug.WriteLine("FormAsksDatabase getFormDB: returning _formAsksDatabase.");
                return _formAsksDatabase;
            }
        }

        public Task<List<FormAsk>> GetAllFormsAsks() {
            return _database.Table<FormAsk>().ToListAsync();
        }

        public Task<FormAsk> GetFormAsks(int id) {
            return _database.Table<FormAsk>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveFormAsks(FormAsk newFormAsks) {

            if (newFormAsks.Id == 0) {
                return _database.InsertAsync(newFormAsks);
            } else {
                return _database.UpdateAsync(newFormAsks);
            }
        }

        public Task<int> DeleteFormAsks(FormAsk formAsks) {
            return _database.DeleteAsync(formAsks);
        }
    }
}
