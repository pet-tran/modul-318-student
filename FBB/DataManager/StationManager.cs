using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FBB.Library;
using SwissTransport.Core;
using SwissTransport.Models;
using Xceed.Wpf.Toolkit;

namespace FBB.Model
{
    public class StationManager : NotifyPropertyChanged
    {

        #region Fields

        private string _stationName;

        private ITransport _transport;

        private Stations _stations;

        private Station _selectedStation;

        private StationBoardRoot _stationBoardRoot;

        private ObservableCollection<string> _startSuggestion = new ObservableCollection<string>();

        private string _selectedSuggestion;

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
                if (!string.IsNullOrWhiteSpace(StationName))
                {
                   StationSuggestion = Library.StationSuggestion.FindSuggestion(StationName);

                }
                OnPropertyChanged(nameof(StationName));
            }
        }

        public Stations Stations
        {
            get
            {
                if (_stations is null)
                {
                    _stations = new Stations
                    {
                        StationList = new ObservableCollection<Station>()
                    };
                }
                return _stations;
            }
        }

        public ObservableCollection<string> StationSuggestion
        {
            get
            {
                return _startSuggestion;
            }
            set
            {
                _startSuggestion = value;
                OnPropertyChanged(nameof(StationSuggestion));
            }
        }
        public string SelectedSuggestion
        {
            get
            {
                return _selectedSuggestion;
            }
            set
            {
                _selectedSuggestion = value;
                OnPropertyChanged(nameof(SelectedSuggestion));
            }
        }

        public Station SelectedStation
        {
            get
            {
                return _selectedStation;
            }
            set
            {
                _selectedStation = value;
                GetStationBoard();
                OnPropertyChanged(nameof(SelectedStation));
            }
        }

        public StationBoardRoot StationBoardRoot
        {
            get
            {
                if (_stationBoardRoot is null)
                {
                    _stationBoardRoot = new StationBoardRoot
                    {
                        Entries = new ObservableCollection<StationBoard>()
                    };
                }
                return _stationBoardRoot;
            }
            set
            {
                _stationBoardRoot = value;
                OnPropertyChanged(nameof(StationBoardRoot));
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
            this.Stations.StationList.Clear();

            Stations getStations = Transport.GetStations(StationName);
            if (getStations.StationList.Count > 0)
            {
                foreach (Station station in getStations.StationList)
                    Stations.StationList.Add(station);
            }
            else
            {
                MessageBox.Show("Es wurden keine Elemente gefunden.");
            }
        }

        private bool CanSearchStation()
        {
            return !string.IsNullOrWhiteSpace(StationName);
        }

        private void GetStationBoard()
        {
            this.StationBoardRoot.Entries.Clear();
            StationBoardRoot getStationBoard = Transport.GetStationBoard(SelectedStation.Name, SelectedStation.Id);
            if (getStationBoard.Entries.Count > 0)
            {
                foreach (StationBoard entry in getStationBoard.Entries)
                {
                    this.StationBoardRoot.Entries.Add(entry);
                }
            }
        }

        #endregion

    }
}
