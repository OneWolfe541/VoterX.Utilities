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
//    public static class PollWorkerMethods
//    {
//        //private static VoterDatabase _VoterDB = ((App)Application.Current).voterContainer.Resolve<VoterDatabase>();

//        // Get a query for the Poll Workers table
//        private static IQueryable<tblPollWorker> PollWorkerQuery(VoterContainer container)
//        {
//            //return _VoterDB.tblPollWorkers;
//            return container.Resolve<PollWorkerRepo>().Query(0);
//        }

//        // Check if the user name and password match any of the poll workers in the database
//        public static tblPollWorker UserLogin(VoterContainer container, string user, string password)
//        {
//            try
//            {
//                return PollWorkerQuery(container)
//                        .Where(pollworker => pollworker.username.ToLower() == user.ToLower())
//                        .Where(pollworker => pollworker.login.ToLower() == password.ToLower())
//                        .FirstOrDefault();
//            }
//            catch
//            {
//                return null;
//            }
//        }

//        // Get a list of all the poll workers at a given site
//        public static IEnumerable<string> UserList(VoterContainer container, int site)
//        {
//            return PollWorkerQuery(container)
//                    .Where(pollworker => pollworker.poll_id == site)
//                    .Select(user => user.username)
//                    .ToList();
//        }
//    }
//}
