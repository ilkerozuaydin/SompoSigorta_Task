using Core.Entities.Abstract;
using Entities.Dtos.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.Response
{
    public class GetProposalResponse:IDto
    {
        public GetProposalResponse()
        {
            Results = new List<GetProposalResponseItemModel>();
        }
        public List<GetProposalResponseItemModel> Results { get; set; }
    }
}
