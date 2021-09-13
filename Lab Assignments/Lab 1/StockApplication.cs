// Antonio Hughes, (Group Mate 2), (Group Mate 3), (Group Mate 4)
// Lab Assignment 1: Stock Application
// Due Date: 09-13-2021

// StockApplication.cs

using System;
using System.IO;

namespace Lab1
{
    class StockApplication
    {
        static void Main(string[] args)
        {
            string docPath = @"/Users/camwilson/Projects/Lab1/Lab1_output.txt";
            /*string docPath = @"C:\Users\Jether\Documents\CECS 475\output.txt";*/

            // overwrite file and write in titles
            string fileTitles = "Broker".PadRight(10) + "Stock".PadRight(15) + "Initial Value".PadRight(15) + "Current Value".PadRight(15) + "Date and Time\n";
            using (StreamWriter titleWriter = new StreamWriter(docPath))
            {
                titleWriter.WriteLine(fileTitles);
            }

            Console.WriteLine("Broker".PadRight(10) + "Stock".PadRight(15) + "Value".PadRight(10) + "Changes");

            Stock stock1 = new Stock("Technology", 160, 5, 15);
            Stock stock2 = new Stock("Retail", 30, 2, 6);
            Stock stock3 = new Stock("Banking", 90, 4, 10);
            Stock stock4 = new Stock("Commodity", 500, 20, 50);

            StockBroker b1 = new StockBroker("Broker 1");
            b1.AddStock(stock1);
            b1.AddStock(stock2);

            StockBroker b2 = new StockBroker("Broker 2");
            b2.AddStock(stock1);
            b2.AddStock(stock3);
            b2.AddStock(stock4);

            StockBroker b3 = new StockBroker("Broker 3");
            b3.AddStock(stock1);
            b3.AddStock(stock3);

            StockBroker b4 = new StockBroker("Broker 4");
            b4.AddStock(stock1);
            b4.AddStock(stock2);
            b4.AddStock(stock3);
            b4.AddStock(stock4);
        }
    }
}
