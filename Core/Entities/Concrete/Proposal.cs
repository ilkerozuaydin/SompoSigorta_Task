using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Proposal : BaseEntity<int>, ISoftDelete
    {
        public ulong ProposalNo { get; set; }
        public int EndorsNo { get; set; }
        public int RenewalNo { get; set; }
        public string ProductNo { get; set; }
        public bool IsDeleted { get; set; }
        public string SerializedResponse { get; set; }
    }
}
