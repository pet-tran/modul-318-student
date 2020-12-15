using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBB.Model
{
    class FBBModules
    {

        private ConnectionManager _connectionManager;
        public ConnectionManager Connection
        {
            get
            {
                if (_connectionManager is null)
                {
                    _connectionManager = new ConnectionManager();
                }

                return _connectionManager;
            }
        }

        private StationManager _station;
        public StationManager Station
        {
            get
            {
                if (_station is null)
                {
                    _station = new StationManager();
                }

                return _station;
            }
        }
    }
}
