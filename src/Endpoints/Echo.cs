using System;
using System.Net.Sockets;

using Endpoints;

class Echo : Endpoint 
{
  public override void Respond(Socket socket, string? argument)
  {
    Console.WriteLine("Accessing /Echo");

    if ( !socket.Connected ) throw new Exception("Socket not connected");

    string text = argument;
    string response = $"HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\nContent-Length: {text.Length}\r\n\r\n{text}\r\n\r\n";
    byte[] message = System.Text.Encoding.UTF8.GetBytes(response);
    socket.Send(message);
  }
}
