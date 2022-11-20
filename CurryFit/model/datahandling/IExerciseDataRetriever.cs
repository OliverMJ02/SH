using System;
using System.Collections.Generic;
using System.Text;

namespace CurryFit.model.datahandling
{
    interface IExerciseDataRetriever
    {
        Dictionary<int, Exercise> RetrieveData();
    }
}

