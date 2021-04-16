using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using Dapper;
using Models;
using GlobalStringsReadOnly;

namespace Infrastructure
{
    public class DataAccessFactoryTable
    {
        //public static string sqlColumns { get; set; } = " @ToTable_TEXT, @Timestamp_unix_BIGINT, @Datestamp_TEXT, @DeviationID_TEXT, @Bit1, @Bit2, @Bit3";

        public static void FactoryTable_insert()
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["myDB"].ConnectionString))
                {
                    List<IOSampleModel2> _samples = new List<IOSampleModel2>();
                    IOSampleModel2 _sample = new IOSampleModel2();
                    _samples.Add(_sample);
                    try
                    {
                        string _sqlSp = StoredProceduresIO.FactoryTable_insert.ToString() + GlobalReadOnlyStrings.IOTablesTemplateColumnNames;

                        //string _sqlSp = "FactoryTable_insert" + sqlColumns;
                        //System.Diagnostics.Debug.WriteLine($"_sqlSp = : {_sqlSp}");
                        for (var i = 0; i < _samples.Count; i++)
                        {
                            _samples[i].ToTable_TEXT = "Tjabadab";
                        }
                        connection.Execute(_sqlSp, _samples);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine($"Exception in class DataAccessFactoryTable Method FactoryTable_insert: " + e);
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Exception in class DataAccessFactoryTable Method FactoryTable_insert: " + e);
            }
         }

        public static List<IOSampleModel> FactoryTable_GetAllRows()
        {
            List<IOSampleModel> _factorySamples = new List<IOSampleModel>();
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["myDB"].ConnectionString))
                {
                    _factorySamples = connection.Query<IOSampleModel>("dbo.FactoryTable_getAllRows", commandType: CommandType.StoredProcedure).ToList();
                    connection.Execute("dbo.FactoryTable_flush");
                    //System.Diagnostics.Debug.WriteLine($"_factorySamples.GetType() är: {_factorySamples.GetType()}");
                    //System.Diagnostics.Debug.WriteLine($"_factorySamples.Count() är : {_factorySamples.Count()}");
                    foreach (IOSampleModel element in _factorySamples)
                    {
                        //Console.WriteLine($"Tid var : {element.Datestamp_TEXT}");
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Exception in my FactoryTable_GetNrOfRows: " + e);
            }
            return _factorySamples;
        }
    }
}
