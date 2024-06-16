using System.Net;
using System.Net.Sockets;
using Networks;
using Requests;

namespace Program
{
  class Program
  {
    public static void Main()
    {
      Network network = new Network();
      network.Listen();
      Request request = new Request(network.socket);
      request.Send();
      network.Terminate();
    }
  }
}
