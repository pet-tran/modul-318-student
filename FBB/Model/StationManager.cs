using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FBB.Library;
using SwissTransport.Core;
using SwissTransport.Models;

namespace FBB.Model
{
    class StationManager : NotifyPropertyChanged
    {

        #region Fields

        private string _stationName;

        private ITransport _transport;

        private Stations _stations;

        #endregion

        #region Properties

        public string StationName
        {
            get
            {
                return _stationName;
            }
            set
            {
                _stationName = value;
                OnPropertyChanged(nameof(StationName));
            }
        }

        public Stations Stations
        {
            get
            {
                if (_stations is null)
                {
                    _stations = new Stations();
                }
                return _stations;
            }
        }

        public ITransport Transport
        {
            get
            {
                if (_transport is null)
                {
                    _transport = new Transport();
                }
                return _transport;
            }
        }

        #endregion


        #region RelayCommands

        RelayCommand _commandSearchStation;
        public ICommand CommandSearchStation
        {
            get
            {
                if (_commandSearchStation == null)
                {
                    _commandSearchStation = new RelayCommand(param => this.SearchStation(), param => CanSearchStation());
                }
                return _commandSearchStation;
            }
        }
        private void SearchStation()
        {
            foreach (Station station in Transport.GetStations(StationName).StationList)
                Stations.StationList.Add(station);
        }

        private bool CanSearchStation()
        {
            return !string.IsNullOrWhiteSpace(StationName);
        }

        #endregion

    }
}
