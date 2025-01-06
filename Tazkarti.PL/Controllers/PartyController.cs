using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tazkarti.Core;
using Tazkarti.Core.Models;
using Tazkarti.Core.Specifications.PartySpecs;
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
        public async Task<ActionResult<IReadOnlyList<ReturnedParty>>> AllParties()
        {
            var specs = new PartyWithGuests();
          var result =    await  _unitOfWork.repository<Party>().GetAllAsync(specs);
            if (result == null) return NotFound();
            var mappedParties = _mapper.Map<IReadOnlyList<ReturnedParty>>(result);
            return Ok(mappedParties);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReturnedParty>> GetPartyById(int id)
        {
            var specs = new PartyWithGuests(id);
            var result = await _unitOfWork.repository<Party>().GetByIdAsync(specs);
            if (result == null) return NotFound();
            var mappedParties = _mapper.Map<ReturnedParty>(result);
            return Ok(mappedParties);
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
        [HttpPut("{id}")]
        public async Task<ActionResult<ReturnedParty>> updateParty(PartyDTO partyDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var party = await _unitOfWork.repository<Party>().GetByIdOnlyAsync(id);
            if (party == null)
            {
                return NotFound();
            }

            if (partyDTO.PictureUrl == null)
            {
                party.Name = partyDTO.Name;
                party.Address = partyDTO.Address;
                party.Time = partyDTO.Time;
            }
            else
            {
                var newPictureUrl = ImageFunctions.Upload(partyDTO.PictureUrl, "Parties");

                if (!string.IsNullOrEmpty(party.invitationUrl))
                {
                    ImageFunctions.DeleteFile(party.invitationUrl);
                }

                party.invitationUrl = newPictureUrl;
                party.Name = partyDTO.Name;
                party.Address = partyDTO.Address;
                party.Time = partyDTO.Time;
            }

            _unitOfWork.repository<Party>().Update(party);

            await _unitOfWork.SavaAsync();

            var returnedParty = _mapper.Map<Party, ReturnedParty>(party);

            return Ok(returnedParty);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var party =await _unitOfWork.repository<Party>().GetByIdOnlyAsync(id);
            if (party == null) return NotFound();
            _unitOfWork.repository<Party>().Delete(party);
            ImageFunctions.DeleteFile(party.invitationUrl);
           await _unitOfWork.SavaAsync();
            return Ok(new { message = "Deleted Successfully" });
        }
    }
}
