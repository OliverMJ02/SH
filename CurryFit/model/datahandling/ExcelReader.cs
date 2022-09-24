using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace CurryFit.model
{
    internal class ExcelReader
    {
        private static readonly String defaultpath = "../../../resources/";

        static Dictionary<String, Excercise> convertExcelFileToHandledData(String path)
        {
            Dictionary<String, Excercise> converted = new Dictionary<String, Excercise>();
            String line;
            path = "OvningsDataBas-CurryFit.csv";
            String file = Path.Combine(defaultpath + path);
            using (var stream = new StreamReader(file))
            {
                stream.ReadLine();
                while ((line = stream.ReadLine()) != null)
                {
                    String[] values = line.Split(';');

                    String name = values[0];
                    int variant = Int32.Parse(values[5]);
                    Console.WriteLine(name);

                }

            }

            return converted;
        }


    }
}