using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common
{
    public  class CLItem
    {
        public CLItem()
        {
        }
        public CLItem(string name)
        {
            this._Name = name;
        }
        //对象名称
        private string _Name = "";
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        //显示序列
        int _DisplaySQ = -1;
        public int DisplaySQ
        {
            get { return _DisplaySQ; }
            set { _DisplaySQ = value; }
        }
        //项目状态 1：正常 0:非法状态
        int _State = 0;
        public int State
        {
            get { return _State; }
            set { _State = value; }
        }
        public void Reset()
        {
            _Name = "";
            _DisplaySQ = -1;
            _State = 0;
        }
    }
}
