// Antonio Hughes, (Group Mate 2), (Group Mate 3), (Group Mate 4)
// Lab Assignment 1: Stock Application
// Due Date: 09-13-2021

// Stock.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stuff
{
    public class Stock
    {
        public event EventHandler<StockNotification> StockEvent;

        private readonly Thread _thread;

        public string StockName { get; set; }
        public int InitialValue { get; set; }
        public int CurrentValue { get; set; }
        public int MaxChange { get; set; }
        public int Threshold { get; set; }
        public int NumChanges { get; set; }

        /// <summary>
        /// Stock class that contains all the information and changes of the stock
        /// </summary>
        /// <param name="name"> Stock name </param>
        /// <param name="startingValue"> Starting stock value </param>
        /// <param name="maxchange"> The max value change of the stock </param>
        /// <param name="threshold"> The range for the stock </param>
        public Stock(string name, int startingValue, int maxchange, int threshold)
        {
            StockName = name;
            InitialValue = startingValue;
            CurrentValue = startingValue;
            MaxChange = maxchange;  
            Threshold = threshold;
            _thread = new Thread(() => Activate());
            _thread.Start();
        }

        /// <summary>
        /// Activates the threads synchronization
        /// </summary>
        public void Activate()
        {
            for (int i = 0; i < 25; i++)
            {
                // This thread causes the stock's value to be modified every 500 milliseconds.
                Thread.Sleep(500); // Equivalent to 1/2 second
                ChangeStockValue();
            }
        }

        /// <summary>
        /// Changes the stock value and also raising the event of stock value changes
        /// </summary>
        public void ChangeStockValue()
        {
            Random random = new Random();
            CurrentValue += random.Next(-1,MaxChange);
            NumChanges++;

            if (Math.Abs((CurrentValue - InitialValue)) > Threshold)
            {
                ThresholdReachedEventargs args = new ThresholdReachedEventargs();

                // Create event data
                args.stockName = StockName;
                args.currentValue = CurrentValue;
                args.numberChanges = NumChanges;

                onStockChangeReached(args);

            }
        }

        public delegate void StockNotification(String stockName, int currentValue, int numberChanges);


        public event StockNotification StockThresholdReachedEvent;

        protected virtual void onStockChangeReached(ThresholdReachedEventargs e)
        {
            /* StockNotification handler = StockThresholdReachedEvent;
             if (handler != null)
                 handler(this, e);*/
            StockThresholdReachedEvent?.DynamicInvoke(this, e);
        }

        //another event to notify saving the following information to a file
        //when the stock's threshold is reached: date and time, stock name, inital value and current value

        public delegate void FileDelegate(Object sender, EventArgs e);

        public event FileDelegate FileEventHandler;

        protected virtual void OnStockChangeFileCreated(FileReachedEventargs e)
        {
            FileDelegate filehandler = FileEventHandler;
            if (filehandler != null)
            {
                filehandler(this, e);
            }

        }

        public class ThresholdReachedEventargs : EventArgs
        {
            public string stockName { get; set; }
            public int currentValue { get; set; }
            public int numberChanges { get; set; }
        }

        public class FileReachedEventargs : EventArgs
        {
            public string stockName { get; set; }
            public int initialValue { get; set; }
            public int currentValue { get; set; }
        }
    }
}
