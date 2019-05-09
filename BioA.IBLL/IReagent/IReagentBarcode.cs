using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.IBLL
{
    public interface IReagentBarcode
    {
        /// <summary>
        /// 试剂条码生化项目参数信息
        /// </summary>
        /// <param name="disk"></param>
        /// <param name="pos"></param>
        /// <param name="reagentBracode"></param>
        /// <returns></returns>
        object GetRgBracodePara(int disk, string pos, string reagentBracode);
        /// <summary>
        /// 试剂条码扫描失败
        /// </summary>
        /// <param name="disk"></param>
        /// <param name="pos"></param>
        void BarcodeScanningFailed(int disk, string pos);
        /// <summary>
        /// 样本条码信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        SampleInfo GetSampleByBarcode(string code, DateTime dt);
    }
}
