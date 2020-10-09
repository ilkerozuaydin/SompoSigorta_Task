using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class ProposalStatusTableModel
    {
        public ProposalStatusTableModel()
        {
            Code = "";
            Description = "";
        }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
