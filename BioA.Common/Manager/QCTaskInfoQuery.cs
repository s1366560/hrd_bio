using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// 获取质控任务信息通过顺序号查找
    /// </summary>
    public class QCTaskInfoQuery
    {
        public QCTaskInfoQuery()
        {
            sampleNum = string.Empty;
            sampleType = string.Empty;
            qCName = string.Empty;
            position = string.Empty;
            manufacturer = string.Empty;
            lotNum = string.Empty;
            levelConc = string.Empty;
            projects = new List<string>();
        }

        private string sampleNum;
        private string sampleType;
        private string qCName;
        private string position;
        private string manufacturer;
        private string lotNum;
        private string levelConc;
        private List<string> projects;
        private List<string> qCRelativePros;

        public string SampleNum
        {
            get { return sampleNum; }
            set { sampleNum = value; }
        }

        public string SampleType
        {
            get { return sampleType; }
            set { sampleType = value; }
        }

        public string QCName
        {
            get { return qCName; }
            set { qCName = value; }
        }

        public string Position
        {
            get { return position; }
            set { position = value; }
        }

        public string Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = value; }
        }
        public string LotNum
        {
            get { return lotNum; }
            set { lotNum = value; }
        }

        public string LevelConc
        {
            get { return levelConc; }
            set { levelConc = value; }
        }

        /// <summary>
        /// 此顺序号下包含的项目任务
        /// </summary>
        public List<string> Projects
        {
            get { return projects; }
            set { projects = value; }
        }
        /// <summary>
        /// 质控品关联项目信息
        /// </summary>
        public List<string> QCRelativePros
        {
            get { return qCRelativePros; }
            set { qCRelativePros = value; }
        }
    }
}
