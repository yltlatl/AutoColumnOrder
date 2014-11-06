using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoColumnOrder
{
    class Program
    {
        static int Main(string[] args)
        {

            if (args.Length < 3)
            {
                Console.WriteLine("You must enter at least a path, an encoding, and one column name.");
                return 1;
            }

            var path = args[0];
            var encoding = args[1];
            var columns = args.AsEnumerable().Skip(2);

            try
            {
                var df = new DelimitedFile(path, encoding);
                while (!df.EndOfFile)
                {
                    df.GetNextRecord();
                    foreach (var c in columns)
                    {
                        Console.Write(df.GetFieldByName(c));
                        Console.Write("\t");
                    }

                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
            }

            Console.Read();
            return 0;
        }
    }
}
