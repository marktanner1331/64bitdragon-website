using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace udp_proxy
{
    class Program
    {
       static void Main(string[] args)
        {
            UdpProxy UdpRedirect = new UdpProxy() { address = "0.0.0.0", Port = 12222 };
            Thread _Thread = new Thread(UdpRedirect.Connect);
            _Thread.Name = "UDP";
            _Thread.Start();
            TcpProxy _TcpRedirect = new TcpProxy("0.0.0.0", 12222);            
        }
    }
    class UdpProxy
    {
        public string address;
        public int Port;

        public void Connect()
        {
            UdpClient UdpClient = new UdpClient(Port);
            int? LocalPort = 372;
            while (true)
            {
                IPEndPoint _IPEndPoint = null;
                byte[] bytes = UdpClient.Receive(ref _IPEndPoint);
                bool Local = IPAddress.IsLoopback(_IPEndPoint.Address);
                string AddressToSend = null;
                int PortToSend = 0;
                if (Local)
                {
                    AddressToSend = address;
                    PortToSend = Port;
                }
                else
                {
                    AddressToSend = "127.0.0.1";
                    PortToSend = LocalPort.Value;
                }
                UdpClient.Send(bytes, bytes.Length, AddressToSend, PortToSend);
            }
        }
    }
    class TcpProxy
    {
        public TcpProxy(string address, int Port)
        {
            TcpListener TcpListener = new TcpListener(IPAddress.Any, 372);
            TcpListener.Start();
            int i = 0;
            while (true)
            {
                i++;
                TcpClient LocalSocket = TcpListener.AcceptTcpClient();
                NetworkStream NetworkStreamLocal = LocalSocket.GetStream();

                TcpClient RemoteSocket = new TcpClient(address, Port);
                NetworkStream NetworkStreamRemote = RemoteSocket.GetStream();
                Console.WriteLine("connected");
                Client RemoteClient = new Client("remote" + i)
                {
                    SendingNetworkStream = NetworkStreamLocal,
                    ListenNetworkStream = NetworkStreamRemote,
                    ListenSocket = RemoteSocket
                };
                Client _LocalClient = new Client("local" + i)
                {
                    SendingNetworkStream = NetworkStreamRemote,
                    ListenNetworkStream = NetworkStreamLocal,
                    ListenSocket = LocalSocket
                };
            }
        }
        public class Client
        {
            public TcpClient ListenSocket;
            public NetworkStream SendingNetworkStream;
            public NetworkStream ListenNetworkStream;
            Thread thread;
            public Client(string Name)
            {
                thread = new Thread(new ThreadStart(ThreadStartHander));
                thread.Name = Name;
                thread.Start();
            }
            public void ThreadStartHander()
            {
                Byte[] data = new byte[99999];
                while (true)
                {
                    if (ListenSocket.Available > 0)
                    {
                        int bytesReaded = ListenNetworkStream.Read(data, 0, ListenSocket.Available);
                        SendingNetworkStream.Write(data, 0, bytesReaded);
                    }
                    Thread.Sleep(10);
                }
            }
        }
    }
}
