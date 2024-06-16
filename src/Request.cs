using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

using Endpoints;

namespace Requests
{
  class Request
  {

    private Socket socket;

    public Request(Socket CurrentSocket)
    {
      this.socket = CurrentSocket;
    }

    private string[] ParseRequest()
    {
      const string CRLF = "\r\n";
      byte[] buffer = new byte[1024];
      this.socket.Receive(buffer);
      string request = Encoding.UTF8.GetString(buffer);
      Console.WriteLine(request);
      string[] parsedRequest = request.Split(CRLF);

      return parsedRequest;
    }

    private string GetPath()
    {
      string[] parsedRequest = ParseRequest();
      return parsedRequest[0].Split(" ")[1];
    }

    public bool DoesExists(string[] availableEndpoints)
    {
      string destinationPath = this.GetPath();
      bool exists = false;

      if ( availableEndpoints.Contains(destinationPath) ) exists = true;

      return exists;
    }

    public void Send()
    {
      string endpointKey = this.GetPath();
      string argument = null;
      Int32 slashPosition = endpointKey.LastIndexOf("/");
      if ( slashPosition > 0 ) 
      {
        argument = endpointKey.Substring(slashPosition + 1);
        endpointKey = endpointKey.Substring(0, slashPosition);
      }
      Dictionary<string, Endpoint> EndpointMap = new Dictionary<string, Endpoint>
      {
        {"/", new Root()},
        {"/echo", new Echo()},
        {"/user-agent", new UserAgent()}
      };

      if (!EndpointMap.TryGetValue(endpointKey, out Endpoint endpoint))
      {
        string response = "HTTP/1.1 404 Not Found\r\n\r\n";
        byte[] message = System.Text.Encoding.UTF8.GetBytes(response);
        socket.Send(message);
      } else endpoint.Respond(this.socket, argument);

    }

  }
}