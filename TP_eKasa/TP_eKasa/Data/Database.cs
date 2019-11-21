using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TP_eKasa.Models;
using Xamarin.Forms;

namespace TP_eKasa.Data
{
    //testovacia DB
    public class Database
    {
        SQLiteAsyncConnection connection;
        public Database()
        {
            connection = DependencyService.Get<ILocalFileHelper>().GetConnection();
            connection.CreateTablesAsync<Configuration1,Profile>().Wait();
        }

        public Task<List<Configuration1>> GetConf1ItemsAsync()
        {
            return connection.Table<Configuration1>().ToListAsync();
        }
        public Task<Configuration1> GetConf1ItemAsync(int _ID)
        {
            return connection.Table<Configuration1>().Where(i => i.ID == _ID).FirstOrDefaultAsync();
        }

        public Task<int> SaveConfiguration1Async(Configuration1 configuration1)
        {
            
            if(configuration1.ID != 0)
            {
                return connection.UpdateAsync(configuration1);
            }
            else
            {
                return connection.InsertAsync(configuration1);
            }
        }
        public Task<int> DeleteConf1ItemAsync(Configuration1 configuration1)
        {
            return connection.DeleteAsync(configuration1);
        }
        public Task<int> ProfileSave(Profile profile)
        {
             return connection.InsertAsync(profile);
        }
        public Task<int> ProfileUpdate(Profile profile)
        {
            return connection.UpdateAsync(profile);
        }
        public Task<int> DeleteProfile(Profile profile)
        {
            return connection.DeleteAsync(profile);
        }
        public Task<List<Profile>> GetProfiles()
        {
            return connection.Table<Profile>().ToListAsync();
        }
        public Task<int> SaveDefault(Profile profile)
        {
            return connection.InsertAsync(profile);
        }
    }
}
