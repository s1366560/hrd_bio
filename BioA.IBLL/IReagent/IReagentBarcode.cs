using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.IBLL
{
    public interface IReagentBarcode
    {
        object GetRgBracodePara(int disk, string pos, string reagentBracode);

        void BarcodeScanningFailed(int disk, string pos);
    }
}
