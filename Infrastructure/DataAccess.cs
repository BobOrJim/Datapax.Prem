﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Configuration;
using Dapper;
using Models;
using Interfaces;
using GlobalStringsReadOnly;
using System.Diagnostics;

namespace Infrastructure
{
    public class DataAccess : IDataAccess
    {

        public void GeneralTable_createIOTemplateTable(string tableName)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["myDB"].ConnectionString))
                {
                    string _sqlSp = GlobalReadOnlyStrings.IOTable_createIOTemplateTable + " @TABLE";
                    var _sqlSpParams = new { TABLE = tableName };
                    connection.Execute(_sqlSp, _sqlSpParams);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Dear developer and fellow collegue, it seams to be an exception in the dataAccess interface. Im sorry, this is not your fault!" +
                                "The error is solely caused by me(Jimmy). Furthermore im also terribly sorry that i cant give you any valuable information about how to solve it. " +
                                "However, if you stand up and scream JIMMY EXCETPTION as loud as you can , Ill come running with two cups of coffee ASAP. ");
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_createIOTemplateTable: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_createIOTemplateTable: ex.StackTrace = " + ex.StackTrace);
            }
        }
        public void GeneralTable_delete(string tableName)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["myDB"].ConnectionString))
                {
                    string _sqlSp = GlobalReadOnlyStrings.IOTable_deleteTable + " @TABLE";
                    var _sqlSpParams = new { TABLE = tableName };
                    connection.Execute(_sqlSp, _sqlSpParams);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_delete: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_delete: ex.StackTrace = " + ex.StackTrace);
            }
        }
        public void GeneralTable_flush(string tableName)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["myDB"].ConnectionString))
                {
                    string _sqlSp = GlobalReadOnlyStrings.IOTable_deleteAllPostsInTable + " @TABLE";
                    var _sqlSpParams = new { TABLE = tableName };
                    connection.Execute(_sqlSp, _sqlSpParams);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_flush: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_flush: ex.StackTrace = " + ex.StackTrace);
            }
        }
        public void GeneralTable_insertIOObject(string tableName, List<IOSampleModel2> _samples)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["myDB"].ConnectionString))
                {
                    try
                    {
                        string _sql_part1 = "";
                        switch (tableName)
                        {
                            case "IOOddTable": //Denna rackare accepterar inte mina Enum
                                _sql_part1 = GlobalReadOnlyStrings.IOOddTable_insert;
                                //Debug.WriteLine("Trying with GeneralTable_insertIOObject. Table IOOddTable");
                                break;
                            case "IOEvenTable":
                                _sql_part1 = GlobalReadOnlyStrings.IOEvenTable_insert;
                                //Debug.WriteLine("Trying with GeneralTable_insertIOObject. Table IOEvenTable");
                                break;
                            case "IOKeepTable":
                                _sql_part1 = GlobalReadOnlyStrings.IOKeepTable_insert;
                                //Debug.WriteLine("Trying with GeneralTable_insertIOObject. Table IOKeepTable");
                                break;
                            case "IODeviationTable":
                                _sql_part1 = GlobalReadOnlyStrings.IODeviationTable_Insert;
                                //Debug.WriteLine("Trying with GeneralTable_insertIOObject. Table IODeviationTable");
                                break;
                            default:
                                System.Diagnostics.Debug.WriteLine($"In GeneralTable_insert: Wrong tableName inparam");
                                break;
                        }
                        string _sqlSp = _sql_part1 + GlobalReadOnlyStrings.IOTablesTemplateColumnNames;
                        connection.Execute(_sqlSp, _samples);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Exception in DataAccess : GeneralTable_insertIOObject (in): ex.Message = " + ex.Message);
                        Debug.WriteLine($"Exception in DataAccess : GeneralTable_insertIOObject (in): ex.StackTrace = " + ex.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_insertIOObject: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_insertIOObject: ex.StackTrace = " + ex.StackTrace);
            }
        }
        public int GeneralTable_getNrOfRows(string tableName)
        {
            int _rowsInFactoryTable = -1;
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["myDB"].ConnectionString))
                {
                    string _sqlSp = GlobalReadOnlyStrings.IOTable_getPostCountInTable + " @TABLE";
                    var _sqlSpParams = new { TABLE = tableName };
                    _rowsInFactoryTable = connection.Query<int>(_sqlSp, _sqlSpParams).SingleOrDefault();
                    //System.Diagnostics.Debug.WriteLine($"ANTAL RADER ÄR: {_rowsInFactoryTable}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_getNrOfRows: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_getNrOfRows: ex.StackTrace = " + ex.StackTrace);
            }
            return _rowsInFactoryTable;
        }
        public List<IOSampleModel2> GeneralTable_cutAllPostsInTable(string tableName)
        {
            List<IOSampleModel2> _factorySamples = new List<IOSampleModel2>();
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["myDB"].ConnectionString))
                {
                    string _sqlSp = GlobalReadOnlyStrings.IOTable_getAllPostsInTable + " @TABLE";
                    var _sqlSpParams = new { TABLE = tableName };
                    _factorySamples = connection.Query<IOSampleModel2>(_sqlSp, _sqlSpParams).ToList();
                    foreach (IOSampleModel2 element in _factorySamples)
                    {
                        //System.Diagnostics.Debug.WriteLine($"In GeneralTable_getAllRows {element.Datestamp_TEXT}");
                    }
                    GeneralTable_flush(tableName);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_cutAllPostsInTable: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_cutAllPostsInTable: ex.StackTrace = " + ex.StackTrace);
            }
            return _factorySamples;
        }
        public List<IOSampleModel2> GeneralTable_getAllPostsInTable(string tableName)
        {
            List<IOSampleModel2> _factorySamples = new List<IOSampleModel2>();
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["myDB"].ConnectionString))
                {
                    string _sqlSp = GlobalReadOnlyStrings.IOTable_getAllPostsInTable + " @TABLE";
                    var _sqlSpParams = new { TABLE = tableName };
                    _factorySamples = connection.Query<IOSampleModel2>(_sqlSp, _sqlSpParams).ToList();
                    foreach (IOSampleModel2 element in _factorySamples)
                    {
                        //System.Diagnostics.Debug.WriteLine($"In GeneralTable_getAllRows {element.Datestamp_TEXT}");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_getAllPostsInTable: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_getAllPostsInTable: ex.StackTrace = " + ex.StackTrace);
            }
            return _factorySamples;
        }
        public void GeneralTable_inTableRemovePost(string tableName, IOSampleModel2 samples)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["myDB"].ConnectionString))
                {
                    string sql = "dbo.GeneralTable_inTableRemovePost @TABLE, @POST";

                    var sqlParams = new { TABLE = tableName, POST = samples.Timestamp_unix_BIGINT };
                    connection.Execute(sql, sqlParams);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_inTableRemovePost: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_inTableRemovePost: ex.StackTrace = " + ex.StackTrace);
            }
        }
        public List<IOSampleModel2> GeneralTable_cutPostsBetweenInTable(string tableName, Int64 startTime, Int64 endTime)
        {
            List<IOSampleModel2> _result = new List<IOSampleModel2>();
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["myDB"].ConnectionString))
                {
                    string _sqlSp = GlobalReadOnlyStrings.IOTable_cutPostsBetweenInTable + " @TABLE, @startTime, @endTime";
                    var _sqlSpParams = new { TABLE = tableName, startTime = startTime.ToString(), endTime = endTime.ToString() };
                    _result = connection.Query<IOSampleModel2>(_sqlSp, _sqlSpParams).ToList();

                    //Stämplar samplingar med vilken Trigger som var aktuell när denna metod anropades.
                    if (GetLatestDeviation() != null)
                    {
                        for (var i = 0; i < _result.Count; i++)
                        {
                            _result[i].DeviationID_TEXT = GetLatestDeviation().DeviationID_TEXT;
                        }
                    }
                    GeneralTable_flush(tableName); //Spolar hela databasen, ty den data som spolas "i onödan" är ändå för gammal.
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_cutPostsBetweenInTable: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_cutPostsBetweenInTable: ex.StackTrace = " + ex.StackTrace);
            }
            return _result;
        }
        public void GeneralTable_cutPastePostsbetween(string cutTable, string pasteTable, int startTimeBefore, int endTimeAfter)
        {
            //string sqlDELETE = "dbo." + tableName + "_getAllRows";
            //List<IOSampleKEP> _factorySamples = new List<IOSampleKEP>();
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["myDB"].ConnectionString))
                {
                    string _startTime = "0";
                    string _endTime = "0";
                    if (GetLatestDeviation() != null)
                    {
                        _startTime = (GetLatestDeviation().Timestamp_unix_BIGINT - startTimeBefore).ToString();
                        _endTime = (GetLatestDeviation().Timestamp_unix_BIGINT + endTimeAfter).ToString();
                    }
                    //Console.WriteLine($"Senaste avvikelsen är : {GetLatestDeviation()}");
                    var sql = "SELECT * FROM " + cutTable + " WHERE" +
                        " Timestamp_unix_BIGINT < " + _endTime + "AND " +
                        "Timestamp_unix_BIGINT > " + _startTime;
                    var _iOSampleModelList = connection.Query<IOSampleModel2>(sql);
                    //Console.WriteLine($"Antal samplingar relaterade till sista avvikelse är: {_iOSampleKEPList.Count()}");

                    //Markera samplingar med vilken avvikelse de blev flyttade av
                    foreach (var item in _iOSampleModelList)
                    {
                        item.DeviationID_TEXT = GetLatestDeviation().DeviationID_TEXT;
                        //Console.WriteLine($"DeviationID är = {item.DeviationID_TEXT}");
                    }
                    GeneralTable_insertIOObject(pasteTable, _iOSampleModelList.ToList());
                    foreach (var item in _iOSampleModelList)
                    {
                        string sql2 = "dbo.GeneralTable_inTableRemovePost @TABLE, @POST";
                        var sqlParams = new { TABLE = cutTable, POST = item.Timestamp_unix_BIGINT };
                        connection.Execute(sql2, sqlParams);
                    }
                    //2. Ta bort dem i factory databasen
                    //3. Ändra factory till Odd och Even.
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_cutPastePostsbetween: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_cutPastePostsbetween: ex.StackTrace = " + ex.StackTrace);
            }
        }
        private IOSampleModel2 GetLatestDeviation()
        {
            Int64 _latestDeviation = -1;
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["myDB"].ConnectionString))
                {
                    var _timestamp_unix_BIGINT = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds(); // System.Int64 // 1607963957552
                    string _timestamp_unix_BIGINT_STRING = (_timestamp_unix_BIGINT - 120000).ToString();
                    var sql = "select * from IODeviationTable where Timestamp_unix_BIGINT > " + _timestamp_unix_BIGINT_STRING;
                    var _deviationList = connection.Query<IOSampleModel2>(sql);
                    try
                    {
                        //Console.WriteLine($"_deviationList datatype =  {_deviationList.GetType()}"); //System.Collections.Generic.List`1[DataAccess.IOSampleKEP]
                        //Console.WriteLine($"_deviationList elements count =  {_deviationList.Count()}");
                        if (_deviationList.Count() > 0)
                        {
                            _latestDeviation = _deviationList.Max(t => t.Timestamp_unix_BIGINT);
                            foreach (IOSampleModel2 item in _deviationList)
                            {
                                if (item.Timestamp_unix_BIGINT == _latestDeviation)
                                {
                                    return item;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Exception in DataAccess : GetLatestDeviation (in) : ex.Message = " + ex.Message);
                        Debug.WriteLine($"Exception in DataAccess : GetLatestDeviation (in) : ex.StackTrace = " + ex.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in DataAccess : GetLatestDeviation: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in DataAccess : GetLatestDeviation: ex.StackTrace = " + ex.StackTrace);
            }
            return null;
        }
        public void GeneralTable_createPictureTemplateTable(string tableName)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["myDB"].ConnectionString))
                {
                    string _sqlSp = GlobalReadOnlyStrings.PictureTable_createPictureTemplateTable + " @TABLE";
                    var _sqlSpParams = new { TABLE = tableName };
                    connection.Execute(_sqlSp, _sqlSpParams);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_createPictureTemplateTable: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_createPictureTemplateTable: ex.StackTrace = " + ex.StackTrace);
            }
        }
        public void GeneralTable_insertPictureObject(string tableName, List<PictureSampleModel> _samples)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["myDB"].ConnectionString))
                {
                    try
                    {
                        string _sql_part1 = "";
                        switch (tableName)
                        {
                            case "Cam1OddTable": //Does not accept an static readonly string :(
                                _sql_part1 = GlobalReadOnlyStrings.Cam1OddTable_Insert;
                                break;
                            case "Cam1EvenTable":
                                _sql_part1 = GlobalReadOnlyStrings.Cam1EvenTable_Insert;
                                break;
                            case "Cam1KeepTable":
                                _sql_part1 = GlobalReadOnlyStrings.Cam1KeepTable_Insert;
                                break;
                            case "Cam1ThrowTable":
                                _sql_part1 = GlobalReadOnlyStrings.Cam1ThrowTable_Insert;
                                break;
                            case "Cam2OddTable":
                                _sql_part1 = GlobalReadOnlyStrings.Cam2OddTable_Insert;
                                break;
                            case "Cam2EvenTable":
                                _sql_part1 = GlobalReadOnlyStrings.Cam2EvenTable_Insert;
                                break;
                            case "Cam2KeepTable":
                                _sql_part1 = GlobalReadOnlyStrings.Cam2KeepTable_Insert;
                                break;
                            case "Cam2ThrowTable":
                                _sql_part1 = GlobalReadOnlyStrings.Cam2ThrowTable_Insert;
                                break;
                            default:
                                System.Diagnostics.Debug.WriteLine($"In GeneralTable_insertPictureObject: Wrong tableName inparam!!!!!!!! {tableName}");
                                break;
                        }
                        string _sqlSp = _sql_part1 + GlobalReadOnlyStrings.PictureTablesTemplateColumnNames;
                        //sqlPictureColumns;
                        //System.Diagnostics.Debug.WriteLine($"In GeneralTable_insertPictureObject: Skickar sql med grejor till {_sql_part1}");
                        connection.Execute(_sqlSp, _samples);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Exception in DataAccess : GeneralTable_insertPictureObject: ex.Message = " + ex.Message);
                        Debug.WriteLine($"Exception in DataAccess : GeneralTable_insertPictureObject: ex.StackTrace = " + ex.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_insertPictureObject: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in DataAccess : GeneralTable_insertPictureObject: ex.StackTrace = " + ex.StackTrace);
            }
        }
        public List<PictureSampleModel> PictureTable_cutPostsBetweenInTable(string tableName, Int64 startTime, Int64 endTime)
        {
            List<PictureSampleModel> _result = new List<PictureSampleModel>();
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["myDB"].ConnectionString))
                {
                    string _sqlSp = GlobalReadOnlyStrings.PictureTable_cutPostsBetweenInTable + " @TABLE, @startTime, @endTime";
                    var _sqlSpParams = new { TABLE = tableName, startTime = startTime.ToString(), endTime = endTime.ToString() };
                    _result = connection.Query<PictureSampleModel>(_sqlSp, _sqlSpParams).ToList();

                    //Stämplar samplingar med vilken avvikelse som var aktuell när denna metod anropades.
                    if (GetLatestDeviation() != null)
                    {
                        for (var i = 0; i < _result.Count; i++)
                        {
                            _result[i].DeviationID_TEXT = GetLatestDeviation().DeviationID_TEXT;
                        }
                    }
                    GeneralTable_flush(tableName); //Spolar hela databasen, ty den data som spolas "i onödan" är ändå för gammal.
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in DataAccess : PictureTable_cutPostsBetweenInTable: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in DataAccess : PictureTable_cutPostsBetweenInTable: ex.StackTrace = " + ex.StackTrace);
            }
            return _result;
        }
    }
}


