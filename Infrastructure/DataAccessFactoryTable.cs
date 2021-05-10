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
using System.Diagnostics;

namespace Infrastructure
{
    public class DataAccessFactoryTable
    {

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
                        string _sqlSp = GlobalReadOnlyStrings.FactoryTable_insert + GlobalReadOnlyStrings.IOTablesTemplateColumnNames;

                        //string _sqlSp = "FactoryTable_insert" + sqlColumns;
                        //System.Diagnostics.Debug.WriteLine($"_sqlSp = : {_sqlSp}");
                        for (var i = 0; i < _samples.Count; i++)
                        {
                            _samples[i].ToTable_TEXT = "Hej";
                        }
                        connection.Execute(_sqlSp, _samples);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Exception in DataAccessFactoryTable : FactoryTable_insert (inside): ex.Message = " + ex.Message);
                        Debug.WriteLine($"Exception in DataAccessFactoryTable : FactoryTable_insert (inside): ex.StackTrace = " + ex.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in DataAccessFactoryTable : FactoryTable_insert: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in DataAccessFactoryTable : FactoryTable_insert: ex.StackTrace = " + ex.StackTrace);
            }
        }
    }
}
