using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoColumnOrder
{
    class Program
    {
        static void Main(string[] args)
        {
            var df = new DelimitedFile("C:\\temp\\data.dat");
            while (!df.EndOfFile)
            {
                df.GetNextRecord();
            }
        }
    }
}
