using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

using Endpoints;

namespace Networks
{
  class Network
  {
    private TcpListener? listener;
    private Socket _socket;

    public Socket socket 
    {
      get => _socket;
    }

    private void Connexion(Int32 port)
    {
     this.listener = new TcpListener(IPAddress.Any, port);
     this.listener.Start();
    }

    public void Listen()
    {
      this.Connexion(4221);
      this._socket = this.listener.AcceptSocket();
    }

    public void Terminate()
    {
      this._socket.Close();
      this.listener.Stop();
    }

  }
}