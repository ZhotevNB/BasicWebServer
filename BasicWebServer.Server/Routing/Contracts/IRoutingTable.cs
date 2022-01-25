﻿using BasicWebServer.Server.HTTP;


namespace BasicWebServer.Server.Routing.Contracts
{
    public interface IRoutingTable
    {
        IRoutingTable Map(Method method, string path,Func<Request,Response>  responseFunction);

        IRoutingTable MapGet(string path, Func<Request, Response> responseFunction);

        IRoutingTable MapPost(string path, Func<Request, Response> responseFunction);

    }
}
