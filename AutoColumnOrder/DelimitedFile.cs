using System;
using System.Collections.Generic;
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
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException("Null or empty path.");
            if (!File.Exists(path)) throw new ArgumentException(string.Format("File {0} does not exist.", path));
            _str = new StreamReader(path);

            if (recordDelimiter == null) throw new ArgumentNullException("Null record delimiter.");
            if (fieldDelimiter == null) throw new ArgumentNullException("Null field delimiter.");
            if (multiValueDelimiter == null) throw new ArgumentNullException("Null multi-value delimiter.");
            if (quote == null) throw new ArgumentNullException("Null quote");

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

        //GetFieldFromRecord

        public void GetNextRecord()
        {
            string line = _str.ReadLine();
            if (_str.EndOfStream)
            {
                EndOfFile = true;
                return;
            }

            if (string.IsNullOrEmpty(line)) throw new ApplicationException("Empty line.");

            char[] delimiter = { FieldDelimiter };
            IEnumerable<string> record = line.Split(delimiter).AsEnumerable<string>();

            if (record == null) throw new ApplicationException(string.Format("No fields found in {0}", line));

            string q = Quote.ToString();

            foreach (var s in record)
            {
                s.Replace(q, "");
            }

            CurrentRecord = record;
        }
    }
}
