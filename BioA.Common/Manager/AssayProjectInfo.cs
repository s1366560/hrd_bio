// ================================================================================================
//
// 文件名（File Name）：              AssayProjectInfo.cs
//
// 功能描述（Description）：          生化项目实体类
//
// 数据表（Tables）：                 对应数据库AssayProjectInfo表
//
// 作者（Author）：                   冯旗
//
// 日期（Create Date）：              2017-07-18
//
// 修改记录（Revision History）：
//      R1:
//          修改人：
//          修改日期：
//          修改理由：
//
// ================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public class AssayProjectInfo
    {
        public AssayProjectInfo()
        {
            projectName = string.Empty;
            sampleType = string.Empty;
            proFullName = string.Empty;
            channelNum = string.Empty;
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }

        /// <summary>
        /// 样本类型
        /// </summary>
        public string SampleType
        {
            get { return sampleType; }
            set { sampleType = value; }
        }

        /// <summary>
        /// 项目全称（报告名称）
        /// </summary>
        public string ProFullName
        {
            get { return proFullName; }
            set { proFullName = value; }
        }

        /// <summary>
        /// 通道号
        /// </summary>
        public string ChannelNum
        {
            get { return channelNum; }
            set { channelNum = value; }
        }

        private string projectName;
        private string sampleType;
        private string proFullName;
        private string channelNum;
    }
}
