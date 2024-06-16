using System.Net.Sockets;
using Networks;

namespace Endpoints
{
  abstract class Endpoint
  {
    public abstract void Respond(Socket socket, string? argument);
  }
}