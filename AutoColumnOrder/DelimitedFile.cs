using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace AutoColumnOrder
{
    class DelimitedFile
    {
        #region Constructors
        //optionally specify the delimiters to use
        public DelimitedFile(string path, char recordDelimiter = '\n', char fieldDelimiter = (char)20, char multiValueDelimiter = (char)59, char quote = (char)254)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Null or empty path.", path);
            if (!File.Exists(path)) throw new ArgumentException(string.Format("File {0} does not exist.", path));
            _str = new StreamReader(path, Encoding.Default);

            RecordDelimiter = recordDelimiter;
            FieldDelimiter = fieldDelimiter;
            MultiValueDelimiter = multiValueDelimiter;
            Quote = quote;

            GetNextRecord();
            HeaderRecord = CurrentRecord;
        }


        #endregion


        #region Properties
        public char RecordDelimiter { get; private set; }

        public char FieldDelimiter { get; private set; }

        public char MultiValueDelimiter { get; private set; }

        public char Quote { get; private set; }

        public bool EndOfFile { get; private set; }

        private StreamReader _str { get; set; }

        public IEnumerable<string> CurrentRecord { get; private set; }

        public IEnumerable<string> HeaderRecord { get; private set; }

        #endregion

        //Get a particular field from the record by its zero-indexed position
        public string GetFieldByPosition(int position)
        {
            if (position < 0 || position >= HeaderRecord.Count())
                throw new ArgumentOutOfRangeException(position.ToString(CultureInfo.InvariantCulture), "Position is out of range.");

            return CurrentRecord.ElementAt(position);
        }


        //Get the next record in the file
        public void GetNextRecord()
        {
            var line = _str.ReadLine();
            if (_str.EndOfStream)
            {
                EndOfFile = true;
                return;
            }

            if (string.IsNullOrEmpty(line)) throw new ApplicationException("Empty line.");

            char[] delimiter = { FieldDelimiter };
            var record = line.Split(delimiter).AsEnumerable();

            if (record == null) throw new ApplicationException(string.Format("No fields found in {0}", line));

            List<string> nakedRecordList = new List<string>();

            var q = Quote.ToString(CultureInfo.InvariantCulture);

            var currentRecord = record as IList<string> ?? record.ToList();
            nakedRecordList.AddRange(currentRecord.Select(s => s.Replace(q, "")));

            CurrentRecord = nakedRecordList;
        }
    }
}
