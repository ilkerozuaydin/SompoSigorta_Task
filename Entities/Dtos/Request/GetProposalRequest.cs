using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.Request
{
    public class GetProposalRequest : IDto
    {
        public ulong ProposalNo { get; set; }
        public int EndorsNo { get; set; }
        public int RenewalNo { get; set; }
        public string ProductNo { get; set; }
    }
}
