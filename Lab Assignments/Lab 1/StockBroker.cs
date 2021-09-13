// Antonio Hughes, (Group Mate 2), (Group Mate 3), (Group Mate 4)
// Lab Assignment 1: Stock Application
// Due Date: 09-13-2021

// StockBroker.cs

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace Lab1
{
    public class StockBroker
    {
        public string BrokerName { get; set; }
        public List<Stock> stocks = new List<Stock>();
        public static ReaderWriterLockSlim myLock = new ReaderWriterLockSlim();
        /*readonly string docPath = @"/Users/camwilson/Projects/Lab1/Lab1_output.txt";*/
        readonly string docPath = @"C:\Users\Jether\Documents\CECS 475\output.txt";
        public string titles = "Broker".PadRight(10) + "Stock".PadRight(15) + "Value".PadRight(10) + "Changes".PadRight(10);

        /// <summary>
        /// The stockbroker object
        /// </summary>
        /// <param name="brokerName"> The stockbroker's name </param>
        public StockBroker(string brokerName)
        {
            BrokerName = brokerName;
        }

        /// <summary>
        /// Adds stock objects to the stock list
        /// </summary>
        /// <param name="stock">Stock object</param>
        public void AddStock(Stock stock)
        {
            stocks.Add(stock);
            stock.StockEvent += EventHandler;
        }

        /// <summary>
        /// The eventhandler that raises the event of a change
        /// </summary>
        /// <param name="sender">The sender that indicated a change</param>
        /// <param name="e">Event arguments</param>
        void EventHandler(Object sender, EventArgs e)
        {
            myLock.EnterWriteLock();
            try
            {
                Stock newStock = (Stock)sender;

                string displayCurrentValue = newStock.CurrentValue.ToString();
                string displayNumChanges = newStock.NumChanges.ToString();
                string statement = BrokerName.PadRight(10) + newStock.StockName.PadRight(15)
                    + displayCurrentValue.PadRight(10) + displayNumChanges.PadRight(10);

                Console.WriteLine(statement);

                WriteStockToFile(newStock, displayCurrentValue, displayNumChanges);
            }
            catch (Exception ex)
            {
                Console.WriteLine("EventHandler failed! Message: {0}", ex.Message);
            }
            finally
            {
                myLock.ExitWriteLock();
            }
        }

        private void WriteStockToFile(Stock newStock, string displayCurrentValue, string displayNumChanges)
        {
            DateTime now = DateTime.Now;
            string dateTimeStr = now.ToString("F");

            using (StreamWriter outputFile = new StreamWriter(docPath, true))
            {
                outputFile.WriteLine(this.BrokerName.PadRight(10) + newStock.StockName.PadRight(15) +
                        newStock.InitialValue.ToString().PadRight(15) + newStock.CurrentValue.ToString().PadRight(15) + dateTimeStr);
            }
        }
    }
}
