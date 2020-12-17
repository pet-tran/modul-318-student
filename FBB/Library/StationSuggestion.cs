using SwissTransport.Core;
using SwissTransport.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBB.Library
{
    public static class StationSuggestion
    {
        public static ObservableCollection<string> FindSuggestion(string searchTerm)
        {
            ObservableCollection<string> ret = new ObservableCollection<string>();

            ITransport iTransport = new Transport();

            Stations stations = iTransport.GetStations(searchTerm);


            if (stations != null && stations.StationList.Count > 0)
            {
                foreach (Station station in stations.StationList)
                {
                    ret.Add(station.Name);
                }

            }

            return ret;
        }

    }
}
