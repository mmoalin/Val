using System;
using System.Globalization;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Linq;
using Newtonsoft.Json;
using MigrationValidator;

namespace ConsoleApp1
{
    class BasicTableModel
    {
        public string EXTERNALID { get; set; }
        public string ENTITYTYPENAME { get; set; }
        public string ATTRIBUTE_ATTRIBUTEID { get; set; }
        public string ATTRIBUTENAME { get; set; }
        public string VALUE { get; set; }
        public string MATERIALNUMBER_MATNR_MARA { get; set; }
        public string PRODCTDESC_ZZPRODUCT_DESCR_MARA { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //MapTransformations<BasicTableModel> mapTransformations = new MapTransformations<BasicTableModel>(WhichTable.PIMMaterialMaster);
            DataRetrieval<BasicTableModel> retData = new DataRetrieval<BasicTableModel>();
            //var items = retData.GetData(mapTransformations.GetSqlStatement());

            string PIMColumns = "ENTITY.EXTERNALID, ENTITY.ENTITYTYPENAME, ATTRIBUTE.ATTRIBUTEID AS ATTRIBUTE_ATTRIBUTEID, ATTRIBUTE.ATTRIBUTETYPE, ATTRIBUTE.ATTRIBUTENAME,	ATTVALUES.VALUEID, ATTVALUES.VALUE, [PIM_DEV].dbo.PadNumericValuesWithZeros(ATTVALUES.VALUE) as 'Padded Mat Codes', ";
            string SAPERPColumns = "PIMI12_MaterialMaster.MATERIALNUMBER_MATNR_MARA, PIMI12_MaterialMaster.[PRODCTDESC_ZZPRODUCT_DESCR_MARA] ";
            string FromANdJoinsStatement = "FROM dbo.PIMI08_ENTITY_FULL AS ENTITY INNER JOIN dbo.PIMI08_ATTRIBUTE_FULL AS ATTRIBUTE ON ENTITY.EXTERNALID = ATTRIBUTE.EXTERNALID LEFT OUTER JOIN dbo.PIMI08_VALUES_FULL AS ATTVALUES ON ATTVALUES.EXTERNALID = ATTRIBUTE.EXTERNALID AND ATTVALUES.ATTRIBUTEID = ATTRIBUTE.ATTRIBUTEID AND ATTVALUES.DEPTH = ATTRIBUTE.DEPTH AND ATTVALUES.ARCOATTRIBUTESEQUENCE = ATTRIBUTE.ARCOATTRIBUTESEQUENCE left join " +
                "[dbo].[PIMI12_ERP_MATERIAL_MASTER] PIMI12_MaterialMaster " +
                " on PIMI12_MaterialMaster.MATERIALNUMBER_MATNR_MARA = [PIM_DEV].dbo.PadNumericValuesWithZeros(ATTVALUES.VALUE)";
            string WhereStatement = "where ( ENTITY.EXTERNALID = 'PIMM00000000052892') and ( ATTRIBUTE.ATTRIBUTENAME = 'Product Description' or ATTRIBUTE.ATTRIBUTENAME = 'Sap Material code') ";
            string testQuery = "SELECT " + PIMColumns + SAPERPColumns + FromANdJoinsStatement + WhereStatement;
            var items = retData.GetData(testQuery);
            foreach (var t in items)
            {
                Console.WriteLine("ExternalID: " + t.EXTERNALID + '\n' + "ENTITYTYPENAME: " + t.ENTITYTYPENAME + '\n'+ "ATTRIBUTENAME: " + t.ATTRIBUTENAME + '\n' + "VALUE: " 
                    + t.VALUE + '\n' + "PRODCTDESC_ZZPRODUCT_DESCR_MARA: " + t.PRODCTDESC_ZZPRODUCT_DESCR_MARA);
                
            }
            Console.ReadLine();
        }
    }
}
