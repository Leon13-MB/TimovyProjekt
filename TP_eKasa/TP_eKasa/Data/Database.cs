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
            connection.CreateTablesAsync<advertxt,dpt,ecrloc,functext,measunit>().Wait();
            connection.CreateTablesAsync<plu, @operator, surdisc, textlogo, traillog>().Wait();
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

            if (configuration1.ID != 0)
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
        //dpt
        public Task<List<dpt>> getDPT()
        {
            return connection.Table<dpt>().ToListAsync();
        }
        public Task<int> saveDPT(dpt dpt)
        {
            return connection.InsertAsync(dpt);
        }

        public Task<int> updateDPT(dpt dpt)
        {
                return connection.UpdateAsync(dpt);
        }
        public Task<int> deleteDPT(dpt dpt)
        {
            return connection.DeleteAsync(dpt);
        }
        //advertxt
        public Task<List<advertxt>> getADVERTXT()
        {
            return connection.Table<advertxt>().ToListAsync();
        }

        public Task<int> saveADVERTXT(advertxt advertxt)
        {
            return connection.InsertAsync(advertxt);
        }

        public Task<int> updateADVERTXT(advertxt advertxt)
        {
            return connection.UpdateAsync(advertxt);
        }
        public Task<int> deleteADVERTXT(advertxt advertxt)
        {
            return connection.DeleteAsync(advertxt);
        }
        //ecrloc
        public Task<List<ecrloc>> getECRLOC()
        {
            return connection.Table<ecrloc>().ToListAsync();
        }

        public Task<int> saveECRLOC(ecrloc ecrloc)
        {
            return connection.InsertAsync(ecrloc);
        }
        public Task<int> updateECRLOC(ecrloc ecrloc)
        {
            return connection.UpdateAsync(ecrloc);
        }
        public Task<int> deleteECRLOC(ecrloc ecrloc)
        {
            return connection.DeleteAsync(ecrloc);
        }
        //functext
        public Task<List<functext>> getFUNCTEXT()
        {
            return connection.Table<functext>().ToListAsync();
        }

        public Task<int> saveFUNCTEXT(functext functext)
        {
            return connection.InsertAsync(functext);
        }
        public Task<int> updateFUNCTEXT(functext functext)
        {
            return connection.UpdateAsync(functext);
        }
        public Task<int> deleteFUNCTEXT(functext functext)
        {
            return connection.DeleteAsync(functext);
        }
        //measunit
        public Task<List<measunit>> getMEASUNIT()
        {
            return connection.Table<measunit>().ToListAsync();
        }

        public Task<int> saveMEASUNIT(measunit measunit)
        {
            return connection.InsertAsync(measunit);
        }
        public Task<int> updateMEASUNIT(measunit measunit)
        {
            return connection.UpdateAsync(measunit);
        }
        public Task<int> deleteMEASUNIT(measunit measunit)
        {
            return connection.DeleteAsync(measunit);
        }
        //operator
        public Task<List<@operator>> getOPERATOR()
        {
            return connection.Table<@operator>().ToListAsync();
        }

        public Task<int> saveOPERATOR(@operator op)
        {
            return connection.InsertAsync(op);
        }
        public Task<int> updateOPERATOR(@operator op)
        {
            return connection.UpdateAsync(op);
        }
        public Task<int> deleteOPERATOR(@operator op)
        {
            return connection.DeleteAsync(op);
        }
        //plu
        public Task<List<plu>> getPLU()
        {
            return connection.Table<plu>().ToListAsync();
        }

        public Task<int> savePLU(plu plu)
        {
            return connection.InsertAsync(plu);
        }
        public Task<int> updatePLU(plu plu)
        {
            return connection.UpdateAsync(plu);
        }
        public Task<int> deletePLU(plu plu)
        {
            return connection.DeleteAsync(plu);
        }
        //surdisc
        public Task<List<surdisc>> getSURDISC()
        {
            return connection.Table<surdisc>().ToListAsync();
        }

        public Task<int> saveSURDISC(surdisc surdisc)
        {
            return connection.InsertAsync(surdisc);
        }
        public Task<int> updateSURDISC(surdisc surdisc)
        {
            return connection.UpdateAsync(surdisc);
        }
        public Task<int> deleteSURDISC(surdisc surdisc)
        {
            return connection.DeleteAsync(surdisc);
        }
        //textlogo
        public Task<List<textlogo>> getTEXTLOGO()
        {
            return connection.Table<textlogo>().ToListAsync();
        }

        public Task<int> saveTEXTLOGO(textlogo textlogo)
        {
            return connection.InsertAsync(textlogo);
        }
        public Task<int> updateTEXTLOGO(textlogo textlogo)
        {
            return connection.UpdateAsync(textlogo);
        }
        public Task<int> deleteTEXTLOGO(textlogo textlogo)
        {
            return connection.DeleteAsync(textlogo);
        }
        //traillog
        public Task<List<traillog>> getTRAILLOG()
        {
            return connection.Table<traillog>().ToListAsync();
        }

        public Task<int> saveTRAILLOG(traillog traillog)
        {
            return connection.InsertAsync(traillog);
        }
        public Task<int> updateTRAILLOG(traillog traillog)
        {
            return connection.UpdateAsync(traillog);
        }
        public Task<int> deleteTRAILLOG(traillog traillog)
        {
            return connection.DeleteAsync(traillog);
        }
    }
}