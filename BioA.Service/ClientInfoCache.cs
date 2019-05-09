using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
    /// <summary>
    /// 客户端访问服务器缓存
    /// </summary>
    public class ClientInfoCache
    {
        private static readonly object SyncObj = new object();
        private static readonly object SyncOperator = new object();
        private static ClientInfoCache instance;

        private List<ClientRegisterInfo> clientList;

        /// <summary>
        /// 实例化客户端缓存对象，唯一，只被实例化一次
        /// </summary>
        public static ClientInfoCache Instance
        {
            get
            {
                lock (SyncObj)
                {
                    if (instance == null)
                    {
                        instance = new ClientInfoCache();
                    }
                }
                return instance;
            }
        }

        private ClientInfoCache()
        {
            clientList = new List<ClientRegisterInfo>();
        }
        /// <summary>
        /// 记录客户端
        /// </summary>
        /// <param name="entity"></param>
        public void Add(ClientRegisterInfo entity)
        {
            if (entity == null)
                return;
            lock (SyncOperator)
            {
                var findClient = clientList.FirstOrDefault(
                        t => t.ClientName == entity.ClientName
                    );

                if (findClient == null)
                    clientList.Add(entity);
                else
                {
                    findClient.NotifyCallBack = entity.NotifyCallBack;
                }
            }
        }

        public void Remove(ClientRegisterInfo entity)
        {
            lock (SyncOperator)
            {
                clientList.Remove(entity);
            }
        }
        /// <summary>
        /// 客户端信息集合
        /// </summary>
        public List<ClientRegisterInfo> Clients
        {
            get
            {
                if (clientList == null)
                {
                    clientList = new List<ClientRegisterInfo>();
                }
                else
                {
                    
                }

                return clientList;
            }
        }
    }
}
