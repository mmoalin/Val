using System;
using System.Collections.Generic;
using System.Text;

namespace MigrationValidator
{
    enum UoMColumns
    {
        SAP_Material_Code, UnitofMeasure, Numerator, Denominator, Length, Width, Height,
        Volume, Gross_Weight, Net_Weight, Barcode, Unit_Type, Waste_Packaging_Stage
    };
    enum WhichTable
    {
        PIMI12_ERP_UOM, PIMMaterialMaster, PIMUoM
    };
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">e.g., enum UoMColumns</typeparam>
    class SQLToObjectsMapper<T>
    {
        private string WhereClauses;
        private string PIMColumns;
        private string FromANdJoinsStatement;
        private string SAPERPColumns;
        public SQLToObjectsMapper(WhichTable mode)
        {
            PIMColumns = "ENTITY.EXTERNALID, ATTRIBUTE.ATTRIBUTEID AS ATTRIBUTE_ATTRIBUTEID, ATTRIBUTE.ATTRIBUTETYPE," +
                        " ATTRIBUTE.ATTRIBUTENAME,	ATTVALUES.VALUEID, ATTVALUES.VALUE, [PIM_DEV].dbo.PadNumericValuesIfMaterialCode(ATTVALUES.VALUE, ATTRIBUTE.ATTRIBUTENAME) as 'Padded Mat Codes', ";
            switch (mode)
            {
                case WhichTable.PIMMaterialMaster:
                    
                    SAPERPColumns = "PIMI12_ERPUOM.MATERIALNUMBER_MATNR_MARA, PIMI12_MaterialMaster.[PRODCTDESC_ZZPRODUCT_DESCR_MARA] ";
                    WhereClauses = "where ( ENTITY.EXTERNALID = 'PIMM00000000052892') and " +
                "( " + returnPIMI12_ERP_UOMColumns()  + " ) ";
                    break;
                case WhichTable.PIMI12_ERP_UOM:
                     SAPERPColumns = "PIMI12_ERPUOM.MATERIALNUMBER_MATNR_MARA, " +
                        "PIMI12_ERPUOM.[ALTUOM_MEINH_MARM], PIMI12_ERPUOM.[ALTUOMEXT_MSEH3_T006A],PIMI12_ERPUOM.[NUMBASEUOM_UMREZ_MARM],PIMI12_ERPUOM.[DENBASEUOM_UMREN_MARM]" +
                        ",PIMI12_ERPUOM.[LENGTH_LAENG_MARM],PIMI12_ERPUOM.[WIDTH_BREIT_MARM],PIMI12_ERPUOM.[HEIGHT_HOEHE_MARM],PIMI12_ERPUOM.[VOLUME_VOLUM_MARM],PIMI12_ERPUOM.[GROSSWEIGHT_BRGEW_MARM]" +
                        ",PIMI12_ERPUOM.[NETWEIGHT_NTGEW_MARA],PIMI12_ERPUOM.[BARCODE_EAN11_MARA],PIMI12_ERPUOM.[WASTPACKINGSTAGE_ZZPACK_STAGE_MARM],PIMI12_ERPUOM.[UNITOFDIM_MEABM_MARM]" +
                        ",PIMI12_ERPUOM.[UNITOFVOLUME_VOLEH_MARM],PIMI12_ERPUOM.[UNITOFPACHWEIGHT_ZZMAT1_WGHT_UOM_MARM] ";
                     FromANdJoinsStatement = "FROM dbo.PIMI08_ENTITY_FULL AS ENTITY INNER JOIN " +
                        "dbo.PIMI08_ATTRIBUTE_FULL AS ATTRIBUTE ON ENTITY.EXTERNALID = ATTRIBUTE.EXTERNALID " +
                        "LEFT OUTER JOIN dbo.PIMI08_VALUES_FULL AS ATTVALUES " +
                        "ON ATTVALUES.EXTERNALID = ATTRIBUTE.EXTERNALID AND ATTVALUES.ATTRIBUTEID = ATTRIBUTE.ATTRIBUTEID " +
                        "AND ATTVALUES.DEPTH = ATTRIBUTE.DEPTH " +
                        "AND ATTVALUES.ARCOATTRIBUTESEQUENCE = ATTRIBUTE.ARCOATTRIBUTESEQUENCE " +
                        "left join [PIM_DEV].[dbo].[PIMI12_ERP_UOM] PIMI12_ERPUOM " +
                        "on PIMI12_ERPUOM.MATERIALNUMBER_MATNR_MARA = [PIM_DEV].dbo.PadNumericValuesWithZeros(ATTVALUES.VALUE) ";
                     WhereClauses = "where ( ENTITY.EXTERNALID = 'PIMM00000000052892') and" +
                        " ( " + returnPIMI08UOMColumns() + " ) ";
                    break;
            }
        }
        public string returnPIMI12_ERP_UOMColumns()
        {
            return "ATTRIBUTE.ATTRIBUTENAME = 'Product Description'" +
                   "or ATTRIBUTE.ATTRIBUTENAME = 'Sap Material code'";
        }
        public string returnPIMI08UOMColumns()
        {
            return "ATTRIBUTE.ATTRIBUTENAME = 'Unit of Measure Product is packaged in' or" +
                "ATTRIBUTE.ATTRIBUTENAME = 'Numerator' or" +
                "ATTRIBUTE.ATTRIBUTENAME = 'Denominator' or" +
                "ATTRIBUTE.ATTRIBUTENAME = 'Length' or" +
                "ATTRIBUTE.ATTRIBUTENAME = 'Width' or" +
                "ATTRIBUTE.ATTRIBUTENAME = 'Height' or" +
                "ATTRIBUTE.ATTRIBUTENAME = 'Volume' or" +
                "ATTRIBUTE.ATTRIBUTENAME = 'Gross Weight' or" +
                "ATTRIBUTE.ATTRIBUTENAME = 'Net Weight' or" +
                "ATTRIBUTE.ATTRIBUTENAME = 'Barcode' or" +
                "ATTRIBUTE.ATTRIBUTENAME = 'Unit Type' or" +
                "ATTRIBUTE.ATTRIBUTENAME = 'Waste Packaging Stage' or" +
                "ATTRIBUTE.ATTRIBUTENAME = 'Unit of Dimension' or" +
                "ATTRIBUTE.ATTRIBUTENAME = 'Unit of Volume' or" +
                "ATTRIBUTE.ATTRIBUTENAME = 'Unit of Weight' or" +
                "ATTRIBUTE.ATTRIBUTENAME = 'Sap Material code' ";
        }
        public string GetSqlStatement()
        {
            
            
            
            string testQuery = "SELECT " + PIMColumns + SAPERPColumns + FromANdJoinsStatement + WhereClauses;
            return testQuery;
        }
    }
    
    class PimColumns
    {
        public string UoM() { return "test"; }
    }
}
