using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ServerClient
{
    class Server
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(7777);
            listener.Start();
            
            bool End = false;
            bool reading = false;
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream ns = client.GetStream();

            BinaryReader br = new BinaryReader(ns);

            BinaryWriter bw = new BinaryWriter(ns);

            while (End == false)
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
                catch(Exception e)
                {
                    End = true;
                }                          
            }
                listener.Stop();
        }
    }
}


