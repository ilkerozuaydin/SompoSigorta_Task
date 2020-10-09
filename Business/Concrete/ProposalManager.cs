using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProposalManager : IProposalService
    {
        private readonly IProposalDal _proposalDal;

        public ProposalManager(IProposalDal proposalDal)
        {
            _proposalDal = proposalDal;
        }

        public async Task<IDataResult<Proposal>> Add(AddProposalLogRequest dto)
        {
            Proposal proposal = new Proposal
            {
                ProposalNo = dto.ProposalNo,
                ProductNo = dto.ProductNo,
                RenewalNo = dto.RenewalNo,
                EndorsNo = dto.EndorsNo,
            };
            await _proposalDal.Add(proposal);
            return new SuccessDataResult<Proposal>(proposal);
        }

        public async Task<IResult> Update(UpdateProposalLogRequest dto)
        {
            var isExists = await _proposalDal.Get(p => p.Id == dto.Id);
            if (isExists == null)
            {
                return new ErrorResult(Messages.EntityNotFound);
            }
            isExists.SerializedResponse = JsonConvert.SerializeObject(dto.Response);
            await _proposalDal.Update(isExists);
            return new SuccessResult(Messages.OperationSuccess);
        }
    }
}
