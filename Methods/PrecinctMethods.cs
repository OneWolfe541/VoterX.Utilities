//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using VoterX.Core.Models.Database;
//using VoterX.Core.Containers;
//using VoterX.Core.Repositories;

//namespace VoterX.Utilities.Methods
//{
//    public static class PrecinctMethods
//    {
//        public static bool Exists(VoterContainer container)
//        {
//            return container.Resolve<PrecinctMasterRepo>().Exists(0);
//        }

//        // Get a query for the Precinct Master table
//        private static IQueryable<tblPrecinctsMaster> PrecinctsQuery(VoterContainer container)
//        {
//            return container.Resolve<PrecinctMasterRepo>().Query(0);
//        }

//        public static IEnumerable<tblPrecinctsMaster> GetPrecinctsList(VoterContainer container)
//        {
//            return PrecinctsQuery(container).ToList();
//        }

//        public static Task<List<tblPrecinctsMaster>> GetPrecinctsListAsync(VoterContainer container)
//        {
//            return Task.Run(() => PrecinctsQuery(container).ToList());
//        }
//    }
//}
