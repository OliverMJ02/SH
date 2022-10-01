using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace CurryFit.model
{
    internal class ExcelReader
    {
        private static readonly String defaultpath = "../../../resources/excel-files";

        static Dictionary<Int32, Exercise> convertExcelFileToHandledData(String path)
        {
            Dictionary<Int32, Exercise> converted = new Dictionary<Int32, Exercise>();
            String line;
            path = "OvningsDataBas-CurryFit.csv";
            String file = Path.Combine(defaultpath + path);
            using (var stream = new StreamReader(file))
            {
                int i = 0;
                stream.ReadLine();
                while ((line = stream.ReadLine()) != null)
                {

                    String[] values = line.Split(';');
                    String name = values[0];
                    int variant = Int32.Parse(values[5]);
                    Console.WriteLine(name); //test

                }

            }

            return converted;
        }


    }
}