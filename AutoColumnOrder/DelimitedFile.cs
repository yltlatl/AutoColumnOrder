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
        public DelimitedFile(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException("Null or empty path.");
            if (!File.Exists(path)) throw new ArgumentException(string.Format("File {0} does not exist.", path));

            _str = new StreamReader(path);

            string line = _str.ReadLine();
            if (string.IsNullOrEmpty(line)) throw new ApplicationException(string.Format("Empty first line in {0}.", path));
            CurrentLine = line;
        }


        #endregion

        #region Fields
        private char _RecordDelimiter = '\n';
        private char _FieldDelimiter = (char)20;
        private char _MultiValueDelimiter = (char)59;
        private char _Quote = (char)254;

        #endregion

        #region Properties
        public char RecordDelimiter
        {
            get
            {
                return _RecordDelimiter;
            }
            private set
            {
                _RecordDelimiter = value;
            }
        }

        public char FieldDelimiter
        {
            get
            {
                return _FieldDelimiter;
            }
            private set
            {
                _FieldDelimiter = value;
            }
        }
        
        public char MultiValueDelimiter
        {
            get
            {
                return _MultiValueDelimiter;
            }
            private set
            {
                _MultiValueDelimiter = value;
            }
        }

        public char Quote
        {
            get
            {
                return _Quote;
            }
            private set
            {
                _Quote = value;
            }
        }

        private StreamReader _str { get; set; }

        public string CurrentLine { get; private set; }

        public string[] CurrentRecord { get; private set; }



        #endregion

        //GetNextRecord

        //GetFieldFromRecord

        //ParseLine
        //Separates a line into fields and strips out quotes
        private IEnumerable<string> ParseLine(string line)
        {
            if (string.IsNullOrEmpty(line)) throw new ApplicationException(string.Format("String {0} is null or empty.", line));

            char[] delimiter = { _FieldDelimiter };
            string[] record = line.Split(delimiter);

            if (record == null) throw new ApplicationException(string.Format("No fields found in {0}", line));
            
            //TODO: write a lambda that will strip the quotes
        }
    }
}
