using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;

namespace ServerClient
{
    class Program
    {
        static Boolean flags = false;
        static int dataFileLengths;
        static byte[] fileDatas;
        static int dataFileSizeLengths;
        static byte[] fileSizeDatas;
        static void Main(string[] args)
        {
           

            NetworkComms.AppendGlobalIncomingPacketHandler<byte[]>("FileData", readFileData);
            NetworkComms.AppendGlobalIncomingPacketHandler<int>("FileDataLength", readFileDataLength);
            NetworkComms.AppendGlobalIncomingPacketHandler<byte[]>("FileSizeData", readFileSizeData);
            NetworkComms.AppendGlobalIncomingPacketHandler<int>("FileSizeDataLength", readFileSizeDataLength);
          
            String ipAddress = "127.0.0.1";
            IPAddress address = IPAddress.Parse(ipAddress);
            IPEndPoint endpoint = new IPEndPoint(address, 5245);
          
            Connection.StartListening(ConnectionType.TCP, endpoint);
           
        
            Console.WriteLine("Server listening for TCP connection on:");
         
           
            foreach (System.Net.IPEndPoint localEndPoint in Connection.ExistingLocalListenEndPoints(ConnectionType.TCP))
            {
                Console.WriteLine("{0}:{1}", localEndPoint.Address, localEndPoint.Port);
               
                System.Console.WriteLine("lol3");
            }
        
            Console.WriteLine("\nPress any key to close server.");
       
            Console.ReadKey(true);
            
          
            NetworkComms.Shutdown();
        }

        private static void readFileDataLength(PacketHeader packetHeader, Connection connection, int dataFileLength)
        {
            Console.WriteLine(connection.ToString() + dataFileLength);
            dataFileLengths = dataFileLength;
            flags = true;

        }

        private static void readFileData(PacketHeader packetHeader, Connection connection, byte[] fileData)
        {
            Console.WriteLine( connection.ToString() +fileData);
            fileDatas = fileData;
            ReadMessage();
        }
        private static void readFileSizeDataLength(PacketHeader packetHeader, Connection connection, int dataFileSizeLength)
        {
            Console.WriteLine(connection.ToString() + dataFileSizeLength);
            dataFileSizeLengths = dataFileSizeLength;
        }

        private static void readFileSizeData(PacketHeader packetHeader, Connection connection, byte[] fileSizeData)
        {
            Console.WriteLine(connection.ToString() + fileSizeData);
            fileSizeDatas = fileSizeData;
        }
        private static void  ReadMessage( )
        {
            string result = System.Text.Encoding.UTF8.GetString(fileDatas);
            using (Stream stream = File.Open(@"C:\\Users\\Dawid\\Desktop\\ServerClient\\ServerClient\\" + "NewFile.txt", FileMode.Create))
            {
                stream.Write(fileDatas, 0, fileDatas.Length);
                Console.WriteLine(">> File send.");
            }
                
            Console.WriteLine(result);
            Console.WriteLine(dataFileLengths);
        }

   
    }
}