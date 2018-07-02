using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class CalItem : CLItem
    {
        public CalItem()
        {
        }

        public CalItem(string name)
            :base(name)
        {
        }
        List<string> _AssayItems = new List<string>();
        public List<string> AssayItem
        {
            get 
            {
                _AssayItems.Clear();
                if (this.Formulation != null)
                {
                    string assaystr = "";
                    for (int i = 0; i < this.Formulation.Length; i++)
                    {
                        if (this.Formulation[i] == '[')
                        {
                            assaystr = "";
                        }
                        else
                        {
                            if (this.Formulation[i] == ']')
                            {
                                _AssayItems.Add(assaystr);
                            }
                            else
                            {
                                assaystr += this.Formulation[i];
                            } 
                        }
                    }
                }
                return _AssayItems; 
            }
        }
        string _Formulation;
        public string Formulation
        {
            get { return _Formulation; }
            set { _Formulation = value; }
        }
        string _FullName;
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }
        
        
    }
}
