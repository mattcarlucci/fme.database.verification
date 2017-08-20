using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fme.Data.Models
{
    [Serializable]
    public class VerificationSetModel
    {
        public string Name { get; set; }
        public List<FieldSetModel> Fields { get; set; }
    }
}
