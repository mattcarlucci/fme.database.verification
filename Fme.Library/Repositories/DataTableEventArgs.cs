using Fme.Library.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Fme.Library.Repositories
{
    public class DataTableEventArgs : EventArgs
    {
        public List<CompareMappingModel> Pairs { get; set; }
        public DataTable Table { get; set; }
        public string Message { get; set; }
    }
}