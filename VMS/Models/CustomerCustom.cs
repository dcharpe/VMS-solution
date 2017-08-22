using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VMS.Models
{
    public class CustomerCustom : SortAndPage
    {
        public IEnumerable<Odb> cust { get; set; }
        public IEnumerable<Odb> custDDL { get; set; }
        public string SelectedFirstName { get; set; }
        public string SelectedLastName { get; set; }
    }
}