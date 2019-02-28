using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
    public class LISSettingService : DataTransmit
    {
        /// <summary>
        /// 获取LIS常规设置
        /// </summary>
        /// <returns></returns>
        public object QueryLISSettingInfo()
        {
            return myBatis.QueryLISSettingInfo();
        }
        /// <summary>
        /// 更新LIS常规设置信息和网络设置信息或者串口设置信息
        /// </summary>
        /// <param name="obj"></param>
        public void UpdateLISSetingAndNetworkORSerialInfo(object[] obj)
        {
            myBatis.UpdateLISSetingAndNetworkORSerialInfo(obj);
        }
    }
}
