using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProposalService
    {
        Task<IDataResult<Proposal>> Add(AddProposalLogRequest dto);

        Task<IResult> Update(UpdateProposalLogRequest dto);
    }
}
