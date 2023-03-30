using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace CurryFit.model.datahandling
{
    class ExcelReader : IExerciseDataRetriever
    {
        private readonly string defaultpath = "../../../resources/excel-files";

        public Dictionary<int, Exercise> RetrieveData()
        {
            Dictionary<int, Exercise> converted = new Dictionary<int, Exercise>();
            string line;
            string path = "OvningsDataBas-CurryFit.csv";
            string file = Path.Combine(defaultpath + path);
            using (var stream = new StreamReader(file))
            {
                stream.ReadLine();
                while ((line = stream.ReadLine()) != null)
                {

                    String[] values = line.Split(';');
                    int id = int.Parse(values[0]);
                    Exercise ex = new Exercise();
                    ex.Id = id;
                    ex.Name = values[1];
                    ex.MainMuscle = values[2];
                    ex.MainEquipment = values[3];

                }

            }

            return converted;
        }

    }
}