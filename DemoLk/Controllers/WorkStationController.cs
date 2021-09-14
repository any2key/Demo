using DemoLk.Models.Request;
using DemoLk.Models.Response;
using DemoLk.Models.WorkStation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DemoLk.Controllers
{
    public class WorkStationController:ControllerBaseEx
    {
        [HttpPost]
        public HttpResponseMessage Register(RegisterRequest req) 
        {
            return SafeRun(_=> 
            {
                DbModelExt.DbModelExtWS.WorkStation.Register(req);
                return Response.OK;
            });
        }


        [HttpGet]
        public HttpResponseMessage Fetch()
        {
            return SafeRun(_=> 
            {
                return new DataResponse<IEnumerable<Models.WorkStation.WorkStation>>() {IsOk=true,Data= DbModelExt.DbModelExtWS.WorkStation.Fetch() };
            });
        }

        [HttpGet]
        public HttpResponseMessage Lock(string Id)
        {
            return SafeRun(_=>
            {
                var ws = DbModelExt.DbModelExtWS.WorkStation.Fetch(Id);
                TcpClient client = new TcpClient(ws.Ip);
                var result = client.SendData<TcpRequest>(new TcpRequest() { Method = "lock",Seconds=60 });
                return Response.OK;
            });
        }

        [HttpGet]
        public HttpResponseMessage Unlock(string Id)
        {
            return SafeRun(_ =>
            {
                var ws = DbModelExt.DbModelExtWS.WorkStation.Fetch(Id);
                TcpClient client = new TcpClient(ws.Ip);
                var result = client.SendData<TcpRequest>(new TcpRequest() {Method="unlock" });
                return Response.OK;
            });
        }

        [HttpGet]
        public HttpResponseMessage ChangeState(string Id,int State)
        {
            return SafeRun(_ =>
            {
                DbModelExt.DbModelExtWS.WorkStation.ChangeState(Id, (WorkStationState)State);
                return Response.OK;
            });
        }
    }
}