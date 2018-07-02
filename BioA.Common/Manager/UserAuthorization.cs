using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
   public class UserAuthorization
    {
        private string userID;
        private string authorizationKey;
        public UserAuthorization()
        {
            userID = "";
            authorizationKey = "";
        }

        public string UserID { get; set; }

        public string AuthorizationKey { get; set; }
    }
}
