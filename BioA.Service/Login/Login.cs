using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioA.Common;

namespace BioA.Service
{
    public class Login : DataTransmit
    {
        public string UserLogin(string strMethodName, string[] strCommunicates)
        {
            return myBatis.UserLogin(strMethodName, strCommunicates);
        }

        public UserInfo QueryUserAuthority(string strMethodName, string UserName)
        {
            return myBatis.QueryUserAuthority(strMethodName, UserName);
        }
    }
}
