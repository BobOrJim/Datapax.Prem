using Models;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IDataAccess
    {
        void GeneralTable_createIOTemplateTable(string tableName);
        void GeneralTable_createPictureTemplateTable(string tableName);
        List<IOSampleModel2> GeneralTable_cutAllPostsInTable(string tableName);
        void GeneralTable_cutPastePostsbetween(string cutTable, string pasteTable, int startTimeBefore, int endTimeAfter);
        List<IOSampleModel2> GeneralTable_cutPostsBetweenInTable(string tableName, long startTime, long endTime);
        void GeneralTable_delete(string tableName);
        void GeneralTable_flush(string tableName);
        List<IOSampleModel2> GeneralTable_getAllPostsInTable(string tableName);
        int GeneralTable_getNrOfRows(string tableName);
        void GeneralTable_insertIOObject(string tableName, List<IOSampleModel2> _samples);
        void GeneralTable_insertPictureObject(string tableName, List<PictureSampleModel> _samples);
        List<PictureSampleModel> PictureTable_cutPostsBetweenInTable(string tableName, long startTime, long endTime);
    }
}