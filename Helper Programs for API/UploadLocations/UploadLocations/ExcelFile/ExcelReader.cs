using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;

namespace UploadLocations
{
    public class ExcelReader
    {
        private readonly FileStream _stream;
        private IExcelDataReader _excelReader;
        private bool ContainsHeaders = true;

        public ExcelReader(FileStream stream)
        {
            _stream = stream;
        }

        public List<ExcelModel> ReadFile()
        {
            _excelReader = ExcelReaderFactory.CreateReader(_stream);
            var data = new List<ExcelModel>();
            while (_excelReader.Read())
            {
                if (_excelReader.VisibleState == "hidden")
                {
                    _excelReader.NextResult();
                    continue;
                }
                else
                {
                    data.Add(new ExcelModel
                    {
                        Column1 = ReadField(0),
                        Column2 = ReadField(1),
                        Column3 = ReadField(2),
                        Column4 = ReadField(3),
                        Column5 = ReadField(4),
                        Column6 = ReadField(5),
                        Column7 = ReadField(6),
                        Column8 = ReadField(7),
                        Column9 = ReadField(8),
                        //Column10 = ReadField(9),
                    });
                }
            }
            if (ContainsHeaders)
            {
                data.RemoveRange(0, 1);
            }
            return data;
        }


        private string ReadField(int index)
        {
            return (Convert.ToString(_excelReader.GetValue(index)));
        }
    }
}
