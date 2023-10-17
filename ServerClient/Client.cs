/*using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ServerClient
{
    class Client
    {
        static void Main(string[] args)
        {
            bool readingWriting = true;
            TcpClient client = new TcpClient();

            client.Connect("127.0.0.1", 7777);
            NetworkStream ns = client.GetStream();
            BinaryReader br = new BinaryReader(ns);
            BinaryWriter bw = new BinaryWriter(ns);
            string message = Console.ReadLine();
            Console.WriteLine("Out: " + message);

        }
    }
}*/
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ServerClient
{
    class Client
    {
        static void Main(string[] args)
        {
            bool reading = true;
            bool end = false;
            TcpClient client = new TcpClient();
            client.Connect("127.0.0.1", 7777);

            NetworkStream ns = client.GetStream();
            BinaryReader br = new BinaryReader(ns);
            BinaryWriter bw = new BinaryWriter(ns);

           
            while (!end)
            {
                try
                {
                    if (reading)
                    {
                        byte b = br.ReadByte();
                        if (b == 4)
                        {
                            int msg = br.ReadInt32();
                            Console.WriteLine("int " + msg);
                        }
                        else if (b == 1)
                        {
                            byte msg = br.ReadByte();
                            Console.WriteLine("byte " + msg);
                        }
                        else if (b == 2)
                        {
                            bool msg = br.ReadBoolean();
                            Console.WriteLine("Bool " + msg);
                        }
                        reading = false;
                    }
                    else
                    {
                        string text = Console.ReadLine();
                        if (text == "int")
                        {
                            byte size = 4;
                            bw.Write(size);
                            int msg = 69;
                            bw.Write(msg);
                        }
                        else if (text == "byte")
                        {
                            byte size = 1;
                            bw.Write(size);
                            byte msg = 5;
                            bw.Write(msg);
                        }
                        else if (text == "boolean")
                        {
                            byte size = 2;
                            bw.Write(size);
                            int msg = 1;
                            bw.Write(msg);
                        }
                        bw.Flush();
                        reading = true;
                    }
                }
                catch (Exception e)
                {
                    end = true;
                    throw;
                }
            }
        }
    }
}

