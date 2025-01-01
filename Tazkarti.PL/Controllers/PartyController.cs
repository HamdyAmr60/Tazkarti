using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tazkarti.Core;
using Tazkarti.Core.Models;
using Tazkarti.PL.DTOs;
using Tazkarti.PL.Helpers;

namespace Tazkarti.PL.Controllers
{
   
    public class PartyController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PartyController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Party>>> AllParties()
        {
          var result =    await  _unitOfWork.repository<Party>().GetAllAsync();
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Party>> GetPartyById(int id)
        {
            var result = await _unitOfWork.repository<Party>().GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PartyDTO>> AddParty(PartyDTO party)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var PictureUrl = ImageFunctions.Upload(party.PictureUrl, "Parties");

            var mappedresult = _mapper.Map<PartyDTO,Party>(party);
            mappedresult.invitationUrl = PictureUrl;
            await  _unitOfWork.repository<Party>().AddAsync(mappedresult);
            await _unitOfWork.SavaAsync();
            return Ok(mappedresult);
        }

    }
}
