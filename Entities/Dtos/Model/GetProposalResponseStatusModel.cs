using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.Model
{
    public class GetProposalResponseStatusModel:IDto
    {
        public string Value { get; set; }
        public string Name { get; set; }
    }
}
