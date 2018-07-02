using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*************************************************************************************
 * CLR版本：        4.0.30319.261
 * 类 名 称：       IEncode
 * 机器名称：       WENSION
 * 命名空间：       CLMode.Interface
 * 文 件 名：       IEncode
 * 创建时间：       4/26/2012 8:40:04 AM
 * 作    者：       李文山
 * 
 * 修改时间：
 * 修 改 人：
*************************************************************************************/

namespace BioA.PLCController.Interface
{
    public interface IEncode
    {
        byte[] Encode(object o);
    }
}
