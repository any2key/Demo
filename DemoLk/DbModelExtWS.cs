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
                            Info = JsonConvert.SerializeObject(new WorkStationInfo() {Cpu=new List<Loaded>(),Ram=new List<Loaded>() }),
                            Ip = req.Address,
                            Number = number,
                            State = WorkStationState.Wait
                        });
                    else
                        ws.State = WorkStationState.Wait;
                        
                    
                    db.SaveChanges();
                });
            }

            internal static IEnumerable<Models.WorkStation.WorkStation> Fetch()
            {
                return Run(db =>
                {
                    return db.WorkStations.ToArray();
                });
            }

            internal static Models.WorkStation.WorkStation Fetch(string Id)
            {
                return Run(db => 
                {
                    return db.WorkStations.First(e => e.Id == Id);
                });
            }

            internal static void ChangeState(string Id,WorkStationState State)
            {
                Run(db =>
                {
                    var ws = db.WorkStations.First(e => e.Id == Id);
                    ws.State = State;
                    db.SaveChanges();
                });
            }
        }
    }
}