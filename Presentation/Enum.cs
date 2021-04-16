
enum TableNames //Dessa matchar exakt namn på tabeller i db.
{
    FactoryTable,
    IOOddTable,
    IOEvenTable,
    IOKeepTable,
    IODeviationTable,
    Cam1OddTable,
    Cam1EvenTable,
    Cam1KeepTable,
    Cam1ThrowTable,
    Cam2OddTable,
    Cam2EvenTable,
    Cam2KeepTable,
    Cam2ThrowTable
}

enum StoredProceduresIO //Dessa matchar exakt SP namn, och namn på sql fil.
{
    IOTable_createIOTemplateTable,
    IOTable_deleteTable,
    IOTable_deleteAllPostsInTable,
    IOTable_getPostCountInTable,
    IOTable_getAllPostsInTable,
    IOTable_cutPostsBetweenInTable,
    FactoryTable_insert,
    IOOddTable_insert,
    IOEvenTable_insert,
    IOKeepTable_insert,
    IODeviationTable_Insert
}

enum StoredProceduresPictures //Dessa matchar exakt SP namn, och namn på sql fil.
{
    PictureTable_createPictureTemplateTable,
    Cam1OddTable_Insert,
    Cam1EvenTable_Insert,
    Cam1KeepTable_Insert,
    Cam1ThrowTable_Insert,
    PictureTable_cutPostsBetweenInTable
}


