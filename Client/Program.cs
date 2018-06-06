using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using NetworkCommsDotNet;

namespace ServerClient
{
    class Program
    {
        public static byte[] data;
        public static byte[] sizeOfData;
        static void Main(string[] args)
        {
            
            Console.WriteLine("Please enter the server IP and port in the format 192.168.0.1:10000 and press return:");
        
            string serverIP = "127.0.0.1";
            int serverPort = 5245;

            int loopCounter = 1;
            while (true)
            {
              
                string messageToSend = "This is message #" + loopCounter;
                Console.WriteLine("Sending message to server saying '" + messageToSend + "'");

            
                Console.WriteLine("\nPress q to quit or any other key to send another message.");
                if (Console.ReadKey(true).Key == ConsoleKey.Q) break;
                else
                {

                    Console.Read();

                    getData("TextFile1.txt");
                 NetworkComms.SendObject("FileData", serverIP, serverPort, data);
                

                }
            }
            
            NetworkComms.Shutdown();

        }
 

        public static void getData(String filename)
        {


            data = System.IO.File.ReadAllBytes("C:\\Users\\Dawid\\Desktop\\ServerClient\\Client\\" + filename);
            
         
        }

    }
    }