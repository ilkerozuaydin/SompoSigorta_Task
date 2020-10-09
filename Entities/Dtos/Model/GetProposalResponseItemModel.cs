using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.Model
{
    public class GetProposalResponseItemModel:IDto
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public GetProposalResponseStatusModel Status { get; set; }
    }
}
