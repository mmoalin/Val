using System;
using System.Collections.Generic;
using System.Text;

namespace MigrationValidator.TableModels
{
    class PIM12ERPUOMModel
    {
        public int MATERIALNUMBER_MATNR_MARA { get; set; }
        public int ALTUOM_MEINH_MARM { get; set; }
        public int ALTUOMEXT_MSEH3_T006A { get; set; }
        public int NUMBASEUOM_UMREZ_MARM { get; set; }
        public int DENBASEUOM_UMREN_MARM { get; set; }
        public int LENGTH_LAENG_MARM { get; set; }
        public int WIDTH_BREIT_MARM { get; set; }
        public int HEIGHT_HOEHE_MARM { get; set; }
        public int VOLUME_VOLUM_MARM { get; set; }
        public int GROSSWEIGHT_BRGEW_MARM { get; set; }
        public int NETWEIGHT_NTGEW_MARA { get; set; }
        public int BARCODE_EAN11_MARA { get; set; }
        public int WASTPACKINGSTAGE_ZZPACK_STAGE_MARM { get; set; }
        public int UNITOFDIM_MEABM_MARM { get; set; }
        public int UNITOFVOLUME_VOLEH_MARM { get; set; }
        public int UNITOFPACHWEIGHT_ZZMAT1_WGHT_UOM_MARM { get; set; }
    }
}
