﻿namespace SwissTransport.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;

    public class StationBoardRoot
    {
        [JsonProperty("Station")] public Station Station { get; set; }

        [JsonProperty("stationboard")] public ObservableCollection<StationBoard> Entries { get; set; }
    }

    public class StationBoard
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("category")] public string Category { get; set; }

        [JsonProperty("Number")] public string Number { get; set; }

        [JsonProperty("to")] public string To { get; set; }

        [JsonProperty("operator")] public string Operator { get; set; }

        [JsonProperty("stop")] public Stop Stop { get; set; }
    }

    public class Stop
    {
        [JsonProperty("departure")] public DateTime Departure { get; set; }
    }
}