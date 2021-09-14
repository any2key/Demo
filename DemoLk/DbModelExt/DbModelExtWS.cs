using DemoLk.Context;
using DemoLk.Models.Request;
using DemoLk.Models.WorkStation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoLk.DbModelExt
{
    public class DbModelExtWS : AppDbContext
    {
        internal static class WorkStation
        {
            internal static void Register(RegisterRequest req)
            {
                Run(db =>
                {
                    var ws = db.WorkStations.FirstOrDefault(e => e.Id == req.Hwid);
                    var number = db.WorkStations.Any() ? db.WorkStations.OrderByDescending(e => e.Number).First().Number + 1 : 1;
                    if (ws == null)
                        db.WorkStations.Add(new Models.WorkStation.WorkStation()
                        {
                            BusyDateTime = null,
                            Id = req.Hwid,
                            Info = JsonConvert.SerializeObject(new WorkStationInfo()),
                            Ip = req.Address,
                            Number = number,
                            State = WorkStationState.Wait
                        });
                    else
                        db.Entry(ws).CurrentValues.SetValues(new Models.WorkStation.WorkStation()
                        {
                            Ip = req.Address,
                            State = WorkStationState.Wait
                        });
                    db.SaveChanges();
                });
            }
        }
    }
}