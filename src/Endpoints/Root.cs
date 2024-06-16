using System;
using System.Net.Sockets;

using Endpoints;

class Root : Endpoint 
{
  public override void Respond(Socket socket, string? argument)
  {
    Console.WriteLine("Accessing /");
    if ( !socket.Connected ) throw new Exception("Socket not connected");

    string response = "HTTP/1.1 200 OK\r\n\r\n";
    byte[] message = System.Text.Encoding.UTF8.GetBytes(response);
    socket.Send(message);
  }
}
