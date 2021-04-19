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
    }
}
