// Antonio Hughes, (Group Mate 2), (Group Mate 3), (Group Mate 4)
// Lab Assignment 1: Stock Application
// Due Date: 09-13-2021

// StockNotification.cs

using System;

namespace Lab1
{
    public class StockNotification : EventArgs
    {
        public string StockName { get; set; }
        public int CurrentValue { get; set; }
        public int NumChanges { get; set; }
        /// <summary>
        /// Stock notification attributes that are set and changed
        /// </summary>
        /// <param name="stockName">Name of stock</param>
        /// <param name="currentValue">Current vallue of the stock</param>
        /// <param name="numChanges">Number of changes the stock goes through</param>

        public StockNotification(string stockName, int currentValue, int numChanges)
        {
            StockName = stockName;
            CurrentValue = currentValue;
            NumChanges = numChanges;
        }
    }
}
