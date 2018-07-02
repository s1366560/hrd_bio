using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLMode.Interface
{
    interface ISaveAdjust
    {
        byte[] Save(string node,int dir,int count);
    }
}
