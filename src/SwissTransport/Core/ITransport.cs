namespace SwissTransport.Core
{
    using System.Threading.Tasks;

    using SwissTransport.Models;

    using System;

    public interface ITransport
    {
        Stations GetStations(string query);

        StationBoardRoot GetStationBoard(string station, string id);

        Connections GetConnections(string fromStation, string toStation, DateTime from, DateTime to);
    }
}