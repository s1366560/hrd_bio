using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLMode.Interface
{
    interface IReagentBarcode
    {
        object Process(string barcode, int disk, string position);
    }
}
