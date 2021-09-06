// Antonio Hughes, (Group Mate 2), (Group Mate 3), (Group Mate 4)
// Lab Assignment 1: Stock Application
// Due Date: 09-13-2021

// StockBroker.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;



namespace stuff
{
    public class StockBroker
    {
        public string BrokerName { get; set; }

        public List<Stock> stocks = new List<Stock>();

        public static ReaderWriterLockSlim myLock = new ReaderWriterLockSlim();

        public static SemaphoreSlim sem = new SemaphoreSlim(1);

        // Make sure to use your actual path 
        readonly string docPath = @"/Users/camwilson/Desktop/CECS475/Lab1_output.txt";

        public string titles = "Broker".PadRight(10) + "Stock".PadRight(15) +
       "Value".PadRight(10) + "Changes".PadRight(10) + "Date and Time";

        /// <summary>
        /// The stock broker object 
        /// </summary>
        /// <param name="brokerName"> The stockbroker's name </param>
        public StockBroker(string brokerName)
        {
            BrokerName = brokerName;
        }

        public event BrokerDelegateHandler BrokerEventHandler;

        public virtual void onBrokerEventChanged()
        {
            BrokerDelegateHandler handler = BrokerEventHandler;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public void Notify()
        {
            Console.WriteLine("{0}\t\t {1}\t\t {2}\t\t {3}", BrokerName);
        }
        public void AddStock(Stock s)
        {
            //add stocks to the list
            stocks.Add(s);

            //call stock event
            s.StockThresholdReachedEvent += StockNotification();
            Console.WriteLine("Broker \t\t Stock \t\t Value \t\t Changes ");
        }

        private Stock.StockNotification StockNotification()
        {
            throw new NotImplementedException();
        }

        public delegate void BrokerDelegateHandler(Object sender, EventArgs e);
    }


}
