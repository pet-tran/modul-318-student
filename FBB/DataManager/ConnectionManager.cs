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

namespace FBB.Model
{
    public class ConnectionManager : StationManager
    {

        #region Fields

        private string _start;

        private string _destination;

        private Connections _connections;

        private DateTime _from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

        private DateTime _to = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 0);

        private Connection _selectedConnection;

        private ObservableCollection<string> _startSuggestion = new ObservableCollection<string>();

        private ObservableCollection<string> _destinationSuggestion = new ObservableCollection<string>();

        private string _selectedStart;

        private string _selectedDestination;

        #endregion

        #region Properties

        public string Start
        {
            get
            {
                return _start;
            }
            set
            {
                _start = value;
                if (!string.IsNullOrWhiteSpace(Start))
                {
                    StartSuggestion = Library.StationSuggestion.FindSuggestion(Start);
                }
                OnPropertyChanged(nameof(Start));
            }
        }

        public ObservableCollection<string> StartSuggestion
        {
            get
            {
                return _startSuggestion;
            }
            set
            {
                _startSuggestion = value;
                OnPropertyChanged(nameof(StartSuggestion));
            }
        }
        public string SelectedStart
        {
            get
            {
                return _selectedStart;
            }
            set
            {
                _selectedStart = value;
                OnPropertyChanged(nameof(SelectedStart));
            }
        }

        public string Destination
        {
            get
            {
                return _destination;
            }
            set
            {
                _destination = value;
                if (!string.IsNullOrWhiteSpace(Destination))
                {
                    DestinationSuggestion = Library.StationSuggestion.FindSuggestion(Destination);
                }
                OnPropertyChanged(nameof(Destination));
            }
        }

        public ObservableCollection<string> DestinationSuggestion
        {
            get
            {
                return _destinationSuggestion;
            }
            set
            {
                _destinationSuggestion = value;
                OnPropertyChanged(nameof(DestinationSuggestion));
            }
        }

        public string SelectedDestination
        {
            get
            {
                return _selectedDestination;
            }
            set
            {
                _selectedDestination = value;
                OnPropertyChanged(nameof(SelectedDestination));
            }
        }

        public Connections Connections
        {
            get
            {
                if (_connections is null)
                {
                    _connections = new Connections
                    {
                        ConnectionList = new ObservableCollection<Connection>()
                    };
                }
                return _connections;
            }
            set
            {
                _connections = value;
                OnPropertyChanged(nameof(Connections));
            }
        }

        public DateTime From
        {
            get
            {
                return _from;
            }
            set
            {
                _from = value;
                OnPropertyChanged(nameof(From));
            }
        }

        public DateTime To
        {
            get
            {
                return _to;
            }
            set
            {
                _to = value;
                OnPropertyChanged(nameof(To));
            }
        }

        public Connection SelectedConnection
        {
            get
            {
                return _selectedConnection;
            }
            set
            {
                _selectedConnection = value;

                if (SelectedConnection != null)
                {
                    SelectedStation = SelectedConnection.To.Station;
                }

                OnPropertyChanged(nameof(SelectedConnection));
            }
        }

        #endregion

        #region Commands

        RelayCommand _commandSearchConnections;
        public ICommand CommandSearchConnections
        {
            get
            {
                if (_commandSearchConnections == null)
                {
                    _commandSearchConnections = new RelayCommand(param => this.SearchConnection(), param => CanSearchConnection());
                }
                return _commandSearchConnections;
            }
        }
        private void SearchConnection()
        {
            Connections.ConnectionList.Clear();

            Connections getConnection = Transport.GetConnections(Start, Destination, From, To);

            if (getConnection != null)
            {
                foreach (Connection connection in getConnection.ConnectionList.Where(x => x.From.Departure >= From && x.To.Arrival <= To))
                    Connections.ConnectionList.Add(connection);
            }
        }

        private bool CanSearchConnection()
        {
            return !string.IsNullOrWhiteSpace(Start) && !string.IsNullOrWhiteSpace(Destination);
        }

        #endregion

    }
}
