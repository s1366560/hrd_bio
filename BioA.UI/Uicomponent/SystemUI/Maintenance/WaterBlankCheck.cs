using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BioA.Common.Machine;
using BioA.Common;
using BioA.Common.IO;
using BioA.Service;

namespace BioA.UI
{
    public partial class WaterBlankCheck : DevExpress.XtraEditors.XtraUserControl
    {
        public event SendNetworkDelegate SendNetworkEvent;

        public event SendMaintenanceNameDelegate SendMaintenanceNameEvent;
        public WaterBlankCheck()
        {
            InitializeComponent();
        }

        private List<CuvetteBlankInfo> lstCuvBlk = new List<CuvetteBlankInfo>();

        public List<CuvetteBlankInfo> LstCuvBlk
        {
            set 
            { 
                lstCuvBlk = value;
                BeginInvoke(new Action(() => {
                    if (lstCuvBlk.Count > 0)
                    {
                        if (textEdit2.Text == "" || textEdit1.Text == "")
                        {
                            return;
                        }
                        GBA(txtNWB001, lstCuvBlk[0].Cuv1Blk.ToString("#0.0000"));
                        GBA(txtNWB002, lstCuvBlk[0].Cuv2Blk.ToString("#0.0000"));
                        GBA(txtNWB003, lstCuvBlk[0].Cuv3Blk.ToString("#0.0000"));
                        GBA(txtNWB004, lstCuvBlk[0].Cuv4Blk.ToString("#0.0000"));
                        GBA(txtNWB005, lstCuvBlk[0].Cuv5Blk.ToString("#0.0000"));
                        GBA(txtNWB006, lstCuvBlk[0].Cuv6Blk.ToString("#0.0000"));
                        GBA(txtNWB007, lstCuvBlk[0].Cuv7Blk.ToString("#0.0000"));
                        GBA(txtNWB008, lstCuvBlk[0].Cuv8Blk.ToString("#0.0000"));
                        GBA(txtNWB009, lstCuvBlk[0].Cuv9Blk.ToString("#0.0000"));
                        GBA(txtNWB010, lstCuvBlk[0].Cuv10Blk.ToString("#0.0000"));
                        GBA(txtNWB011, lstCuvBlk[0].Cuv11Blk.ToString("#0.0000"));
                        GBA(txtNWB012, lstCuvBlk[0].Cuv12Blk.ToString("#0.0000"));
                        GBA(txtNWB013, lstCuvBlk[0].Cuv13Blk.ToString("#0.0000"));
                        GBA(txtNWB014, lstCuvBlk[0].Cuv14Blk.ToString("#0.0000"));
                        GBA(txtNWB015, lstCuvBlk[0].Cuv15Blk.ToString("#0.0000"));
                        GBA(txtNWB016, lstCuvBlk[0].Cuv16Blk.ToString("#0.0000"));
                        GBA(txtNWB017, lstCuvBlk[0].Cuv17Blk.ToString("#0.0000"));
                        GBA(txtNWB018, lstCuvBlk[0].Cuv18Blk.ToString("#0.0000"));
                        GBA(txtNWB019, lstCuvBlk[0].Cuv19Blk.ToString("#0.0000"));
                        GBA(txtNWB020, lstCuvBlk[0].Cuv20Blk.ToString("#0.0000"));
                        GBA(txtNWB021, lstCuvBlk[0].Cuv21Blk.ToString("#0.0000"));
                        GBA(txtNWB022, lstCuvBlk[0].Cuv22Blk.ToString("#0.0000"));
                        GBA(txtNWB023, lstCuvBlk[0].Cuv23Blk.ToString("#0.0000"));
                        GBA(txtNWB024, lstCuvBlk[0].Cuv24Blk.ToString("#0.0000"));
                        GBA(txtNWB025, lstCuvBlk[0].Cuv25Blk.ToString("#0.0000"));
                        GBA(txtNWB026, lstCuvBlk[0].Cuv26Blk.ToString("#0.0000"));
                        GBA(txtNWB027, lstCuvBlk[0].Cuv27Blk.ToString("#0.0000"));
                        GBA(txtNWB028, lstCuvBlk[0].Cuv28Blk.ToString("#0.0000"));
                        GBA(txtNWB029, lstCuvBlk[0].Cuv29Blk.ToString("#0.0000"));
                        GBA(txtNWB030, lstCuvBlk[0].Cuv30Blk.ToString("#0.0000"));
                        GBA(txtNWB031, lstCuvBlk[0].Cuv31Blk.ToString("#0.0000"));
                        GBA(txtNWB032, lstCuvBlk[0].Cuv32Blk.ToString("#0.0000"));
                        GBA(txtNWB033, lstCuvBlk[0].Cuv33Blk.ToString("#0.0000"));
                        GBA(txtNWB034, lstCuvBlk[0].Cuv34Blk.ToString("#0.0000"));
                        GBA(txtNWB035, lstCuvBlk[0].Cuv35Blk.ToString("#0.0000"));
                        GBA(txtNWB036, lstCuvBlk[0].Cuv36Blk.ToString("#0.0000"));
                        GBA(txtNWB037, lstCuvBlk[0].Cuv37Blk.ToString("#0.0000"));
                        GBA(txtNWB038, lstCuvBlk[0].Cuv38Blk.ToString("#0.0000"));
                        GBA(txtNWB039, lstCuvBlk[0].Cuv39Blk.ToString("#0.0000"));
                        GBA(txtNWB040, lstCuvBlk[0].Cuv40Blk.ToString("#0.0000"));
                        GBA(txtNWB041, lstCuvBlk[0].Cuv41Blk.ToString("#0.0000"));
                        GBA(txtNWB042, lstCuvBlk[0].Cuv42Blk.ToString("#0.0000"));
                        GBA(txtNWB043, lstCuvBlk[0].Cuv43Blk.ToString("#0.0000"));
                        GBA(txtNWB044, lstCuvBlk[0].Cuv44Blk.ToString("#0.0000"));
                        GBA(txtNWB045, lstCuvBlk[0].Cuv45Blk.ToString("#0.0000"));
                        GBA(txtNWB046, lstCuvBlk[0].Cuv46Blk.ToString("#0.0000"));
                        GBA(txtNWB047, lstCuvBlk[0].Cuv47Blk.ToString("#0.0000"));
                        GBA(txtNWB048, lstCuvBlk[0].Cuv48Blk.ToString("#0.0000"));
                        GBA(txtNWB049, lstCuvBlk[0].Cuv49Blk.ToString("#0.0000"));
                        GBA(txtNWB050, lstCuvBlk[0].Cuv50Blk.ToString("#0.0000"));
                        GBA(txtNWB051, lstCuvBlk[0].Cuv51Blk.ToString("#0.0000"));
                        GBA(txtNWB052, lstCuvBlk[0].Cuv52Blk.ToString("#0.0000"));
                        GBA(txtNWB053, lstCuvBlk[0].Cuv53Blk.ToString("#0.0000"));
                        GBA(txtNWB054, lstCuvBlk[0].Cuv54Blk.ToString("#0.0000"));
                        GBA(txtNWB055, lstCuvBlk[0].Cuv55Blk.ToString("#0.0000"));
                        GBA(txtNWB056, lstCuvBlk[0].Cuv56Blk.ToString("#0.0000"));
                        GBA(txtNWB057, lstCuvBlk[0].Cuv57Blk.ToString("#0.0000"));
                        GBA(txtNWB058, lstCuvBlk[0].Cuv58Blk.ToString("#0.0000"));
                        GBA(txtNWB059, lstCuvBlk[0].Cuv59Blk.ToString("#0.0000"));
                        GBA(txtNWB060, lstCuvBlk[0].Cuv60Blk.ToString("#0.0000"));
                        GBA(txtNWB061, lstCuvBlk[0].Cuv61Blk.ToString("#0.0000"));
                        GBA(txtNWB062, lstCuvBlk[0].Cuv62Blk.ToString("#0.0000"));
                        GBA(txtNWB063, lstCuvBlk[0].Cuv63Blk.ToString("#0.0000"));
                        GBA(txtNWB064, lstCuvBlk[0].Cuv64Blk.ToString("#0.0000"));
                        GBA(txtNWB065, lstCuvBlk[0].Cuv65Blk.ToString("#0.0000"));
                        GBA(txtNWB066, lstCuvBlk[0].Cuv66Blk.ToString("#0.0000"));
                        GBA(txtNWB067, lstCuvBlk[0].Cuv67Blk.ToString("#0.0000"));
                        GBA(txtNWB068, lstCuvBlk[0].Cuv68Blk.ToString("#0.0000"));
                        GBA(txtNWB069, lstCuvBlk[0].Cuv69Blk.ToString("#0.0000"));
                        GBA(txtNWB070, lstCuvBlk[0].Cuv70Blk.ToString("#0.0000"));
                        GBA(txtNWB071, lstCuvBlk[0].Cuv71Blk.ToString("#0.0000"));
                        GBA(txtNWB072, lstCuvBlk[0].Cuv72Blk.ToString("#0.0000"));
                        GBA(txtNWB073, lstCuvBlk[0].Cuv73Blk.ToString("#0.0000"));
                        GBA(txtNWB074, lstCuvBlk[0].Cuv74Blk.ToString("#0.0000"));
                        GBA(txtNWB075, lstCuvBlk[0].Cuv75Blk.ToString("#0.0000"));
                        GBA(txtNWB076, lstCuvBlk[0].Cuv76Blk.ToString("#0.0000"));
                        GBA(txtNWB077, lstCuvBlk[0].Cuv77Blk.ToString("#0.0000"));
                        GBA(txtNWB078, lstCuvBlk[0].Cuv78Blk.ToString("#0.0000"));
                        GBA(txtNWB079, lstCuvBlk[0].Cuv79Blk.ToString("#0.0000"));
                        GBA(txtNWB080, lstCuvBlk[0].Cuv80Blk.ToString("#0.0000"));
                        GBA(txtNWB081, lstCuvBlk[0].Cuv81Blk.ToString("#0.0000"));
                        GBA(txtNWB082, lstCuvBlk[0].Cuv82Blk.ToString("#0.0000"));
                        GBA(txtNWB083, lstCuvBlk[0].Cuv83Blk.ToString("#0.0000"));
                        GBA(txtNWB084, lstCuvBlk[0].Cuv84Blk.ToString("#0.0000"));
                        GBA(txtNWB085, lstCuvBlk[0].Cuv85Blk.ToString("#0.0000"));
                        GBA(txtNWB086, lstCuvBlk[0].Cuv86Blk.ToString("#0.0000"));
                        GBA(txtNWB087, lstCuvBlk[0].Cuv87Blk.ToString("#0.0000"));
                        GBA(txtNWB088, lstCuvBlk[0].Cuv88Blk.ToString("#0.0000"));
                        GBA(txtNWB089, lstCuvBlk[0].Cuv89Blk.ToString("#0.0000"));
                        GBA(txtNWB090, lstCuvBlk[0].Cuv90Blk.ToString("#0.0000"));
                        GBA(txtNWB091, lstCuvBlk[0].Cuv91Blk.ToString("#0.0000"));
                        GBA(txtNWB092, lstCuvBlk[0].Cuv92Blk.ToString("#0.0000"));
                        GBA(txtNWB093, lstCuvBlk[0].Cuv93Blk.ToString("#0.0000"));
                        GBA(txtNWB094, lstCuvBlk[0].Cuv94Blk.ToString("#0.0000"));
                        GBA(txtNWB095, lstCuvBlk[0].Cuv95Blk.ToString("#0.0000"));
                        GBA(txtNWB096, lstCuvBlk[0].Cuv96Blk.ToString("#0.0000"));
                        GBA(txtNWB097, lstCuvBlk[0].Cuv97Blk.ToString("#0.0000"));
                        GBA(txtNWB098, lstCuvBlk[0].Cuv98Blk.ToString("#0.0000"));
                        GBA(txtNWB099, lstCuvBlk[0].Cuv99Blk.ToString("#0.0000"));
                        GBA(txtNWB100, lstCuvBlk[0].Cuv100Blk.ToString("#0.0000"));
                        GBA(txtNWB101, lstCuvBlk[0].Cuv101Blk.ToString("#0.0000"));
                        GBA(txtNWB102, lstCuvBlk[0].Cuv102Blk.ToString("#0.0000"));
                        GBA(txtNWB103, lstCuvBlk[0].Cuv103Blk.ToString("#0.0000"));
                        GBA(txtNWB104, lstCuvBlk[0].Cuv104Blk.ToString("#0.0000"));
                        GBA(txtNWB105, lstCuvBlk[0].Cuv105Blk.ToString("#0.0000"));
                        GBA(txtNWB106, lstCuvBlk[0].Cuv106Blk.ToString("#0.0000"));
                        GBA(txtNWB107, lstCuvBlk[0].Cuv107Blk.ToString("#0.0000"));
                        GBA(txtNWB108, lstCuvBlk[0].Cuv108Blk.ToString("#0.0000"));
                        GBA(txtNWB109, lstCuvBlk[0].Cuv109Blk.ToString("#0.0000"));
                        GBA(txtNWB110, lstCuvBlk[0].Cuv110Blk.ToString("#0.0000"));
                        GBA(txtNWB111, lstCuvBlk[0].Cuv111Blk.ToString("#0.0000"));
                        GBA(txtNWB112, lstCuvBlk[0].Cuv112Blk.ToString("#0.0000"));
                        GBA(txtNWB113, lstCuvBlk[0].Cuv113Blk.ToString("#0.0000"));
                        GBA(txtNWB114, lstCuvBlk[0].Cuv114Blk.ToString("#0.0000"));
                        GBA(txtNWB115, lstCuvBlk[0].Cuv115Blk.ToString("#0.0000"));
                        GBA(txtNWB116, lstCuvBlk[0].Cuv116Blk.ToString("#0.0000"));
                        GBA(txtNWB117, lstCuvBlk[0].Cuv117Blk.ToString("#0.0000"));
                        GBA(txtNWB118, lstCuvBlk[0].Cuv118Blk.ToString("#0.0000"));
                        GBA(txtNWB119, lstCuvBlk[0].Cuv119Blk.ToString("#0.0000"));
                        GBA(txtNWB120, lstCuvBlk[0].Cuv120Blk.ToString("#0.0000"));
                        GBA(txtNWB121, lstCuvBlk[0].Cuv121Blk.ToString("#0.0000"));
                        GBA(txtNWB122, lstCuvBlk[0].Cuv122Blk.ToString("#0.0000"));
                        GBA(txtNWB123, lstCuvBlk[0].Cuv123Blk.ToString("#0.0000"));
                        GBA(txtNWB124, lstCuvBlk[0].Cuv124Blk.ToString("#0.0000"));
                        GBA(txtNWB125, lstCuvBlk[0].Cuv125Blk.ToString("#0.0000"));
                        GBA(txtNWB126, lstCuvBlk[0].Cuv126Blk.ToString("#0.0000"));
                        GBA(txtNWB127, lstCuvBlk[0].Cuv127Blk.ToString("#0.0000"));
                        GBA(txtNWB128, lstCuvBlk[0].Cuv128Blk.ToString("#0.0000"));
                        GBA(txtNWB129, lstCuvBlk[0].Cuv129Blk.ToString("#0.0000"));
                        GBA(txtNWB130, lstCuvBlk[0].Cuv130Blk.ToString("#0.0000"));
                        GBA(txtNWB131, lstCuvBlk[0].Cuv131Blk.ToString("#0.0000"));
                        GBA(txtNWB132, lstCuvBlk[0].Cuv132Blk.ToString("#0.0000"));
                        GBA(txtNWB133, lstCuvBlk[0].Cuv133Blk.ToString("#0.0000"));
                        GBA(txtNWB134, lstCuvBlk[0].Cuv134Blk.ToString("#0.0000"));
                        GBA(txtNWB135, lstCuvBlk[0].Cuv135Blk.ToString("#0.0000"));
                        GBA(txtNWB136, lstCuvBlk[0].Cuv136Blk.ToString("#0.0000"));
                        GBA(txtNWB137, lstCuvBlk[0].Cuv137Blk.ToString("#0.0000"));
                        GBA(txtNWB138, lstCuvBlk[0].Cuv138Blk.ToString("#0.0000"));
                        GBA(txtNWB139, lstCuvBlk[0].Cuv139Blk.ToString("#0.0000"));
                        GBA(txtNWB140, lstCuvBlk[0].Cuv140Blk.ToString("#0.0000"));
                        GBA(txtNWB141, lstCuvBlk[0].Cuv141Blk.ToString("#0.0000"));
                        GBA(txtNWB142, lstCuvBlk[0].Cuv142Blk.ToString("#0.0000"));
                        GBA(txtNWB143, lstCuvBlk[0].Cuv143Blk.ToString("#0.0000"));
                        GBA(txtNWB144, lstCuvBlk[0].Cuv144Blk.ToString("#0.0000"));
                        GBA(txtNWB145, lstCuvBlk[0].Cuv145Blk.ToString("#0.0000"));
                        GBA(txtNWB146, lstCuvBlk[0].Cuv146Blk.ToString("#0.0000"));
                        GBA(txtNWB147, lstCuvBlk[0].Cuv147Blk.ToString("#0.0000"));
                        GBA(txtNWB148, lstCuvBlk[0].Cuv148Blk.ToString("#0.0000"));
                        GBA(txtNWB149, lstCuvBlk[0].Cuv149Blk.ToString("#0.0000"));
                        GBA(txtNWB150, lstCuvBlk[0].Cuv150Blk.ToString("#0.0000"));
                        GBA(txtNWB151, lstCuvBlk[0].Cuv151Blk.ToString("#0.0000"));
                        GBA(txtNWB152, lstCuvBlk[0].Cuv152Blk.ToString("#0.0000"));
                        GBA(txtNWB153, lstCuvBlk[0].Cuv153Blk.ToString("#0.0000"));
                        GBA(txtNWB154, lstCuvBlk[0].Cuv154Blk.ToString("#0.0000"));
                        GBA(txtNWB155, lstCuvBlk[0].Cuv155Blk.ToString("#0.0000"));
                        GBA(txtNWB156, lstCuvBlk[0].Cuv156Blk.ToString("#0.0000"));
                        GBA(txtNWB157, lstCuvBlk[0].Cuv157Blk.ToString("#0.0000"));
                        GBA(txtNWB158, lstCuvBlk[0].Cuv158Blk.ToString("#0.0000"));
                        GBA(txtNWB159, lstCuvBlk[0].Cuv159Blk.ToString("#0.0000"));
                        GBA(txtNWB160, lstCuvBlk[0].Cuv160Blk.ToString("#0.0000"));
                    }
                    if (lstCuvBlk.Count == 2 && lstCuvBlk[1] != null )
                    {
                        GBA(txtOld001, lstCuvBlk[1].Cuv1Blk.ToString("#0.0000"));
                        GBA(txtOld002, lstCuvBlk[1].Cuv2Blk.ToString("#0.0000"));
                        GBA(txtOld003, lstCuvBlk[1].Cuv3Blk.ToString("#0.0000"));
                        GBA(txtOld004, lstCuvBlk[1].Cuv4Blk.ToString("#0.0000"));
                        GBA(txtOld005, lstCuvBlk[1].Cuv5Blk.ToString("#0.0000"));
                        GBA(txtOld006, lstCuvBlk[1].Cuv6Blk.ToString("#0.0000"));
                        GBA(txtOld007, lstCuvBlk[1].Cuv7Blk.ToString("#0.0000"));
                        GBA(txtOld008, lstCuvBlk[1].Cuv8Blk.ToString("#0.0000"));
                        GBA(txtOld009, lstCuvBlk[1].Cuv9Blk.ToString("#0.0000"));
                        GBA(txtOld010, lstCuvBlk[1].Cuv10Blk.ToString("#0.0000"));
                        GBA(txtOld011, lstCuvBlk[1].Cuv11Blk.ToString("#0.0000"));
                        GBA(txtOld012, lstCuvBlk[1].Cuv12Blk.ToString("#0.0000"));
                        GBA(txtOld013, lstCuvBlk[1].Cuv13Blk.ToString("#0.0000"));
                        GBA(txtOld014, lstCuvBlk[1].Cuv14Blk.ToString("#0.0000"));
                        GBA(txtOld015, lstCuvBlk[1].Cuv15Blk.ToString("#0.0000"));
                        GBA(txtOld016, lstCuvBlk[1].Cuv16Blk.ToString("#0.0000"));
                        GBA(txtOld017, lstCuvBlk[1].Cuv17Blk.ToString("#0.0000"));
                        GBA(txtOld018, lstCuvBlk[1].Cuv18Blk.ToString("#0.0000"));
                        GBA(txtOld019, lstCuvBlk[1].Cuv19Blk.ToString("#0.0000"));
                        GBA(txtOld020, lstCuvBlk[1].Cuv20Blk.ToString("#0.0000"));
                        GBA(txtOld021, lstCuvBlk[1].Cuv21Blk.ToString("#0.0000"));
                        GBA(txtOld022, lstCuvBlk[1].Cuv22Blk.ToString("#0.0000"));
                        GBA(txtOld023, lstCuvBlk[1].Cuv23Blk.ToString("#0.0000"));
                        GBA(txtOld024, lstCuvBlk[1].Cuv24Blk.ToString("#0.0000"));
                        GBA(txtOld025, lstCuvBlk[1].Cuv25Blk.ToString("#0.0000"));
                        GBA(txtOld026, lstCuvBlk[1].Cuv26Blk.ToString("#0.0000"));
                        GBA(txtOld027, lstCuvBlk[1].Cuv27Blk.ToString("#0.0000"));
                        GBA(txtOld028, lstCuvBlk[1].Cuv28Blk.ToString("#0.0000"));
                        GBA(txtOld029, lstCuvBlk[1].Cuv29Blk.ToString("#0.0000"));
                        GBA(txtOld030, lstCuvBlk[1].Cuv30Blk.ToString("#0.0000"));
                        GBA(txtOld031, lstCuvBlk[1].Cuv31Blk.ToString("#0.0000"));
                        GBA(txtOld032, lstCuvBlk[1].Cuv32Blk.ToString("#0.0000"));
                        GBA(txtOld033, lstCuvBlk[1].Cuv33Blk.ToString("#0.0000"));
                        GBA(txtOld034, lstCuvBlk[1].Cuv34Blk.ToString("#0.0000"));
                        GBA(txtOld035, lstCuvBlk[1].Cuv35Blk.ToString("#0.0000"));
                        GBA(txtOld036, lstCuvBlk[1].Cuv36Blk.ToString("#0.0000"));
                        GBA(txtOld037, lstCuvBlk[1].Cuv37Blk.ToString("#0.0000"));
                        GBA(txtOld038, lstCuvBlk[1].Cuv38Blk.ToString("#0.0000"));
                        GBA(txtOld039, lstCuvBlk[1].Cuv39Blk.ToString("#0.0000"));
                        GBA(txtOld040, lstCuvBlk[1].Cuv40Blk.ToString("#0.0000"));
                        GBA(txtOld041, lstCuvBlk[1].Cuv41Blk.ToString("#0.0000"));
                        GBA(txtOld042, lstCuvBlk[1].Cuv42Blk.ToString("#0.0000"));
                        GBA(txtOld043, lstCuvBlk[1].Cuv43Blk.ToString("#0.0000"));
                        GBA(txtOld044, lstCuvBlk[1].Cuv44Blk.ToString("#0.0000"));
                        GBA(txtOld045, lstCuvBlk[1].Cuv45Blk.ToString("#0.0000"));
                        GBA(txtOld046, lstCuvBlk[1].Cuv46Blk.ToString("#0.0000"));
                        GBA(txtOld047, lstCuvBlk[1].Cuv47Blk.ToString("#0.0000"));
                        GBA(txtOld048, lstCuvBlk[1].Cuv48Blk.ToString("#0.0000"));
                        GBA(txtOld049, lstCuvBlk[1].Cuv49Blk.ToString("#0.0000"));
                        GBA(txtOld050, lstCuvBlk[1].Cuv50Blk.ToString("#0.0000"));
                        GBA(txtOld051, lstCuvBlk[1].Cuv51Blk.ToString("#0.0000"));
                        GBA(txtOld052, lstCuvBlk[1].Cuv52Blk.ToString("#0.0000"));
                        GBA(txtOld053, lstCuvBlk[1].Cuv53Blk.ToString("#0.0000"));
                        GBA(txtOld054, lstCuvBlk[1].Cuv54Blk.ToString("#0.0000"));
                        GBA(txtOld055, lstCuvBlk[1].Cuv55Blk.ToString("#0.0000"));
                        GBA(txtOld056, lstCuvBlk[1].Cuv56Blk.ToString("#0.0000"));
                        GBA(txtOld057, lstCuvBlk[1].Cuv57Blk.ToString("#0.0000"));
                        GBA(txtOld058, lstCuvBlk[1].Cuv58Blk.ToString("#0.0000"));
                        GBA(txtOld059, lstCuvBlk[1].Cuv59Blk.ToString("#0.0000"));
                        GBA(txtOld060, lstCuvBlk[1].Cuv60Blk.ToString("#0.0000"));
                        GBA(txtOld061, lstCuvBlk[1].Cuv61Blk.ToString("#0.0000"));
                        GBA(txtOld062, lstCuvBlk[1].Cuv62Blk.ToString("#0.0000"));
                        GBA(txtOld063, lstCuvBlk[1].Cuv63Blk.ToString("#0.0000"));
                        GBA(txtOld064, lstCuvBlk[1].Cuv64Blk.ToString("#0.0000"));
                        GBA(txtOld065, lstCuvBlk[1].Cuv65Blk.ToString("#0.0000"));
                        GBA(txtOld066, lstCuvBlk[1].Cuv66Blk.ToString("#0.0000"));
                        GBA(txtOld067, lstCuvBlk[1].Cuv67Blk.ToString("#0.0000"));
                        GBA(txtOld068, lstCuvBlk[1].Cuv68Blk.ToString("#0.0000"));
                        GBA(txtOld069, lstCuvBlk[1].Cuv69Blk.ToString("#0.0000"));
                        GBA(txtOld070, lstCuvBlk[1].Cuv70Blk.ToString("#0.0000"));
                        GBA(txtOld071, lstCuvBlk[1].Cuv71Blk.ToString("#0.0000"));
                        GBA(txtOld072, lstCuvBlk[1].Cuv72Blk.ToString("#0.0000"));
                        GBA(txtOld073, lstCuvBlk[1].Cuv73Blk.ToString("#0.0000"));
                        GBA(txtOld074, lstCuvBlk[1].Cuv74Blk.ToString("#0.0000"));
                        GBA(txtOld075, lstCuvBlk[1].Cuv75Blk.ToString("#0.0000"));
                        GBA(txtOld076, lstCuvBlk[1].Cuv76Blk.ToString("#0.0000"));
                        GBA(txtOld077, lstCuvBlk[1].Cuv77Blk.ToString("#0.0000"));
                        GBA(txtOld078, lstCuvBlk[1].Cuv78Blk.ToString("#0.0000"));
                        GBA(txtOld079, lstCuvBlk[1].Cuv79Blk.ToString("#0.0000"));
                        GBA(txtOld080, lstCuvBlk[1].Cuv80Blk.ToString("#0.0000"));
                        GBA(txtOld081, lstCuvBlk[1].Cuv81Blk.ToString("#0.0000"));
                        GBA(txtOld082, lstCuvBlk[1].Cuv82Blk.ToString("#0.0000"));
                        GBA(txtOld083, lstCuvBlk[1].Cuv83Blk.ToString("#0.0000"));
                        GBA(txtOld084, lstCuvBlk[1].Cuv84Blk.ToString("#0.0000"));
                        GBA(txtOld085, lstCuvBlk[1].Cuv85Blk.ToString("#0.0000"));
                        GBA(txtOld086, lstCuvBlk[1].Cuv86Blk.ToString("#0.0000"));
                        GBA(txtOld087, lstCuvBlk[1].Cuv87Blk.ToString("#0.0000"));
                        GBA(txtOld088, lstCuvBlk[1].Cuv88Blk.ToString("#0.0000"));
                        GBA(txtOld089, lstCuvBlk[1].Cuv89Blk.ToString("#0.0000"));
                        GBA(txtOld090, lstCuvBlk[1].Cuv90Blk.ToString("#0.0000"));
                        GBA(txtOld091, lstCuvBlk[1].Cuv91Blk.ToString("#0.0000"));
                        GBA(txtOld092, lstCuvBlk[1].Cuv92Blk.ToString("#0.0000"));
                        GBA(txtOld093, lstCuvBlk[1].Cuv93Blk.ToString("#0.0000"));
                        GBA(txtOld094, lstCuvBlk[1].Cuv94Blk.ToString("#0.0000"));
                        GBA(txtOld095, lstCuvBlk[1].Cuv95Blk.ToString("#0.0000"));
                        GBA(txtOld096, lstCuvBlk[1].Cuv96Blk.ToString("#0.0000"));
                        GBA(txtOld097, lstCuvBlk[1].Cuv97Blk.ToString("#0.0000"));
                        GBA(txtOld098, lstCuvBlk[1].Cuv98Blk.ToString("#0.0000"));
                        GBA(txtOld099, lstCuvBlk[1].Cuv99Blk.ToString("#0.0000"));
                        GBA(txtOld100, lstCuvBlk[1].Cuv100Blk.ToString("#0.0000"));
                        GBA(txtOld101, lstCuvBlk[1].Cuv101Blk.ToString("#0.0000"));
                        GBA(txtOld102, lstCuvBlk[1].Cuv102Blk.ToString("#0.0000"));
                        GBA(txtOld103, lstCuvBlk[1].Cuv103Blk.ToString("#0.0000"));
                        GBA(txtOld104, lstCuvBlk[1].Cuv104Blk.ToString("#0.0000"));
                        GBA(txtOld105, lstCuvBlk[1].Cuv105Blk.ToString("#0.0000"));
                        GBA(txtOld106, lstCuvBlk[1].Cuv106Blk.ToString("#0.0000"));
                        GBA(txtOld107, lstCuvBlk[1].Cuv107Blk.ToString("#0.0000"));
                        GBA(txtOld108, lstCuvBlk[1].Cuv108Blk.ToString("#0.0000"));
                        GBA(txtOld109, lstCuvBlk[1].Cuv109Blk.ToString("#0.0000"));
                        GBA(txtOld110, lstCuvBlk[1].Cuv110Blk.ToString("#0.0000"));
                        GBA(txtOld111, lstCuvBlk[1].Cuv111Blk.ToString("#0.0000"));
                        GBA(txtOld112, lstCuvBlk[1].Cuv112Blk.ToString("#0.0000"));
                        GBA(txtOld113, lstCuvBlk[1].Cuv113Blk.ToString("#0.0000"));
                        GBA(txtOld114, lstCuvBlk[1].Cuv114Blk.ToString("#0.0000"));
                        GBA(txtOld115, lstCuvBlk[1].Cuv115Blk.ToString("#0.0000"));
                        GBA(txtOld116, lstCuvBlk[1].Cuv116Blk.ToString("#0.0000"));
                        GBA(txtOld117, lstCuvBlk[1].Cuv117Blk.ToString("#0.0000"));
                        GBA(txtOld118, lstCuvBlk[1].Cuv118Blk.ToString("#0.0000"));
                        GBA(txtOld119, lstCuvBlk[1].Cuv119Blk.ToString("#0.0000"));
                        GBA(txtOld120, lstCuvBlk[1].Cuv120Blk.ToString("#0.0000"));
                        GBA(txtOld121, lstCuvBlk[1].Cuv121Blk.ToString("#0.0000"));
                        GBA(txtOld122, lstCuvBlk[1].Cuv122Blk.ToString("#0.0000"));
                        GBA(txtOld123, lstCuvBlk[1].Cuv123Blk.ToString("#0.0000"));
                        GBA(txtOld124, lstCuvBlk[1].Cuv124Blk.ToString("#0.0000"));
                        GBA(txtOld125, lstCuvBlk[1].Cuv125Blk.ToString("#0.0000"));
                        GBA(txtOld126, lstCuvBlk[1].Cuv126Blk.ToString("#0.0000"));
                        GBA(txtOld127, lstCuvBlk[1].Cuv127Blk.ToString("#0.0000"));
                        GBA(txtOld128, lstCuvBlk[1].Cuv128Blk.ToString("#0.0000"));
                        GBA(txtOld129, lstCuvBlk[1].Cuv129Blk.ToString("#0.0000"));
                        GBA(txtOld130, lstCuvBlk[1].Cuv130Blk.ToString("#0.0000"));
                        GBA(txtOld131, lstCuvBlk[1].Cuv131Blk.ToString("#0.0000"));
                        GBA(txtOld132, lstCuvBlk[1].Cuv132Blk.ToString("#0.0000"));
                        GBA(txtOld133, lstCuvBlk[1].Cuv133Blk.ToString("#0.0000"));
                        GBA(txtOld134, lstCuvBlk[1].Cuv134Blk.ToString("#0.0000"));
                        GBA(txtOld135, lstCuvBlk[1].Cuv135Blk.ToString("#0.0000"));
                        GBA(txtOld136, lstCuvBlk[1].Cuv136Blk.ToString("#0.0000"));
                        GBA(txtOld137, lstCuvBlk[1].Cuv137Blk.ToString("#0.0000"));
                        GBA(txtOld138, lstCuvBlk[1].Cuv138Blk.ToString("#0.0000"));
                        GBA(txtOld139, lstCuvBlk[1].Cuv139Blk.ToString("#0.0000"));
                        GBA(txtOld140, lstCuvBlk[1].Cuv140Blk.ToString("#0.0000"));
                        GBA(txtOld141, lstCuvBlk[1].Cuv141Blk.ToString("#0.0000"));
                        GBA(txtOld142, lstCuvBlk[1].Cuv142Blk.ToString("#0.0000"));
                        GBA(txtOld143, lstCuvBlk[1].Cuv143Blk.ToString("#0.0000"));
                        GBA(txtOld144, lstCuvBlk[1].Cuv144Blk.ToString("#0.0000"));
                        GBA(txtOld145, lstCuvBlk[1].Cuv145Blk.ToString("#0.0000"));
                        GBA(txtOld146, lstCuvBlk[1].Cuv146Blk.ToString("#0.0000"));
                        GBA(txtOld147, lstCuvBlk[1].Cuv147Blk.ToString("#0.0000"));
                        GBA(txtOld148, lstCuvBlk[1].Cuv148Blk.ToString("#0.0000"));
                        GBA(txtOld149, lstCuvBlk[1].Cuv149Blk.ToString("#0.0000"));
                        GBA(txtOld150, lstCuvBlk[1].Cuv150Blk.ToString("#0.0000"));
                        GBA(txtOld151, lstCuvBlk[1].Cuv151Blk.ToString("#0.0000"));
                        GBA(txtOld152, lstCuvBlk[1].Cuv152Blk.ToString("#0.0000"));
                        GBA(txtOld153, lstCuvBlk[1].Cuv153Blk.ToString("#0.0000"));
                        GBA(txtOld154, lstCuvBlk[1].Cuv154Blk.ToString("#0.0000"));
                        GBA(txtOld155, lstCuvBlk[1].Cuv155Blk.ToString("#0.0000"));
                        GBA(txtOld156, lstCuvBlk[1].Cuv156Blk.ToString("#0.0000"));
                        GBA(txtOld157, lstCuvBlk[1].Cuv157Blk.ToString("#0.0000"));
                        GBA(txtOld158, lstCuvBlk[1].Cuv158Blk.ToString("#0.0000"));
                        GBA(txtOld159, lstCuvBlk[1].Cuv159Blk.ToString("#0.0000"));
                        GBA(txtOld160, lstCuvBlk[1].Cuv160Blk.ToString("#0.0000"));
                    }
                }));
            }
        }

        public void GBA(TextBox box, string cuv)
        {
            box.Clear();
            float f = float.Parse(textEdit2.Text);
            float f1 = float.Parse(textEdit1.Text);
            float c = float.Parse(cuv);
            box.Text = c.ToString();
            if (c > f1 && c < f)
            {
                box.BackColor = Color.White;
            }
            else if (c > f)
            {
                box.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            }
            else
            {
                box.BackColor = Color.Yellow;
            }
        }

        private List<CuvetteBlankInfo> listCuveBlankInfo = new List<CuvetteBlankInfo>();
        public List<CuvetteBlankInfo> ListCuveBlankInfo
        {
            get { return listCuveBlankInfo; }
            set 
            {
                listCuveBlankInfo = value;
                List<CuvetteBlankInfo> cuves = new List<CuvetteBlankInfo>();
                foreach (CuvetteBlankInfo cuve in listCuveBlankInfo)
                {
                    if (cuve.WaveLength == 340)
                    {
                        cuves.Add(cuve);
                    }
                }
                this.LstCuvBlk = cuves;
            }
        }
       
        /// <summary>
        /// 清洗比色杯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartCleaning_Click(object sender, EventArgs e)
        {
            string strSender = "";
            strSender = MachineInfo.SubsystemList.Find(str => str.Name == "Common").ComponetList.Find(componet => componet.Name == "Maintance").CommandList.Find(command => command.FullName == btnStartCleaning.Text).Name;
            
            if (SendNetworkEvent != null && strSender != "")
            {
                SendNetworkEvent(strSender);
            }
            if(SendMaintenanceNameEvent != null)
            {
                SendMaintenanceNameEvent(btnStartCleaning.Text);
            }
        }

        public void WaterBlankCheck_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadQueryAllCuvetteValue));
        }

        private void loadQueryAllCuvetteValue()
        {
            List<Subsystem> lstConfigureInfo = MachineInfo.SubsystemList;
            foreach (Subsystem sub in lstConfigureInfo)
            {
                if (sub.Name == "Common")
                {
                    btnStartCleaning.Text = sub.ComponetList[1].CommandList[3].FullName;
                }
            }
            textEdit1.Text = (0.03).ToString();
            textEdit2.Text = (0.3).ToString();
            textEdit1.BackColor = Color.Yellow;
            textEdit2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            //CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.SystemMaintenance, new Dictionary<string, object[]>() { { "QueryWaterBlankValueByWave", null } });
            List<CuvetteBlankInfo> LstCuvetteBlankInfo = new SystemMaintenance().QueryWaterBlankValueByWave("QueryWaterBlankValueByWave", "340");
            this.LstCuvBlk = LstCuvetteBlankInfo;
        }

        private void btnWavelength_Click(object sender, EventArgs e)
        {
            this.lstCuvBlk.Clear();
            List<CuvetteBlankInfo> lstCuv = new List<CuvetteBlankInfo>();
            SimpleButton button = (SimpleButton)sender;
            string but = (button.Text).Substring(0,3); ;
            //for (int i = 0; i < listCuveBlankInfo.Count; i++)
            //{
            //    if (listCuveBlankInfo[i].WaveLength.ToString() == but)
            //    {
            //        lstCuv.Add(listCuveBlankInfo[i]);
            //    }
            //}
            //if (lstCuv.Count > 0)
            //{
            //    LstCuvBlk = lstCuv;
            //}
            List<CuvetteBlankInfo> LstCuvetteBlankInfo = new SystemMaintenance().QueryWaterBlankValueByWave("QueryWaterBlankValueByWave", but);
            this.LstCuvBlk = LstCuvetteBlankInfo;
        }

    }
}
