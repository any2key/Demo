using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoLk.Models.WorkStation
{
    public class WorkStation
    {
        [Key]
        public string Id { get; set; }
        public int Number { get; set; }
        public WorkStationState State { get; set; }
        public string Ip { get; set; }
        public string Info { get; set; }
        public DateTime? BusyDateTime { get; set; }
    }

    public enum WorkStationState
    {
        Wait,
        Busy,
        Off
    }

    public class WorkStationInfo
    {
        public List<Loaded> Ram { get; set; }
        public List<Loaded> Cpu { get; set; }
    }

    public class Loaded
    {
        public string Time { get; set; }
        public string Usage { get; set; }
    }
}