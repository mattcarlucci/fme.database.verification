using Fme.Library.Models;
using System;
using System.Collections.Generic;

namespace Fme.Library.Comparison
{
    public class CompareHelperEventArgs : EventArgs
    {
        public string Source { get; set; }
        public int RowCount { get; set; }
        public int Current { get; set; }
        public int ErrorCount { get; set; }
        public string Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int CurrentRow { get; set; }

        public List<CompareResultModel> Results { get; set; }
        public TimeSpan Duration
        {
            get { return new TimeSpan(EndTime.Ticks - StartTime.Ticks); }
        }
        public CompareHelperEventArgs()
        {
            Results = new List<CompareResultModel>();
        }

    }
}
