using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Web.Common.Constants;

namespace Web.Models
{
    public class ProposalStatusModel:IDto
    {
        public ProposalStatusModel()
        {
            TableData = new List<ProposalStatusTableModel>();
        }
        public PROPOSAL_STATUS_TYPES StatusType { get; set; }
        public List<ProposalStatusTableModel> TableData { get; set; }
    }
}
