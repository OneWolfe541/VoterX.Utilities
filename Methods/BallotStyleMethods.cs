//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using VoterX.Core.Repositories;
//using VoterX.Core.Models.ViewModels;
//using VoterX.Core.Models.Database;
//using VoterX.Core.Containers;

//namespace VoterX.Utilities.Methods
//{
//    public static class BallotStyleMethods
//    {
//        public static IEnumerable<tblBallotstylesMaster> GetBallotStyleList(VoterContainer container)
//        {
//            using (VoterDatabase _VoterDB = container.Resolve<VoterDatabase>())
//            {
//                return _VoterDB.tblBallotstylesMasters.ToList();
//            }
//        }

//        public static Task<List<tblBallotstylesMaster>> GetBallotStyleListAsync(VoterContainer container)
//        {
//            return Task.Run(() => GetBallotStyleList(container).ToList());
//        }

//        public static IEnumerable<BallotStyleLookupModel> GetBallotStyleLookup(string precinct_part_id, string party, VoterContainer container)
//        {
//            return container.Resolve<BallotStyleLookupRepo>().Single(precinct_part_id, party);
//        }

//        public static IEnumerable<BallotStyleLookupModel> GetBallotStyleLookup(string precinct_part_id, VoterContainer container)
//        {
//            return container.Resolve<BallotStyleLookupRepo>().Single(precinct_part_id);
//        }
//    }
//}
