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
    public class TransportViewModel : NotifyPropertyChanged
    {

        #region Fields

        private string _start;

        private string _destination;

        private ITransport _transport;

        private Connections _connections;

        private DateTime _from = DateTime.Now;

        private DateTime _to = DateTime.Now;

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
                OnPropertyChanged(nameof(Start));
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
                OnPropertyChanged(nameof(Destination));
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

        public Connections Connections
        {
            get
            {
                if (_connections is null)
                {
                    _connections = new Connections();
                    _connections.ConnectionList = new ObservableCollection<Connection>();
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
            foreach (Connection connection in Transport.GetConnections(Start, Destination).ConnectionList)
                Connections.ConnectionList.Add(connection);
        }

        private bool CanSearchConnection()
        {
            return !string.IsNullOrWhiteSpace(Start) && !string.IsNullOrWhiteSpace(Destination);
        }

        #endregion

    }
}
