using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.Request
{
   public class UpdateProposalLogRequest:IDto
    {
        public int Id { get; set; }
        public string Response { get; set; }
    }
}
