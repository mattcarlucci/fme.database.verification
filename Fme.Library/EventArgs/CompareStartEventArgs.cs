using Fme.Library.Models;
using System;
using System.Collections.Generic;

namespace Fme.Library.Repositories
{
    public class CompareStartEventArgs : EventArgs
    {
        public List<CompareMappingModel> Pairs { get; set; }
        public string Message { get; set; }
    }
}