using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Tazkarti.Core;
using Tazkarti.Core.Models;
using Tazkarti.Core.Services;
using Tazkarti.Core.Specifications.GuestSpecs;
using Tazkarti.PL.DTOs;
using Tazkarti.PL.Helpers;
using Tazkarti.Service;

namespace Tazkarti.PL.Controllers
{
   
    public class GuestsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IQRCodeService _qRCodeService;

        public GuestsController(IUnitOfWork unitOfWork , IMapper mapper , IQRCodeService qRCodeService)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._qRCodeService = qRCodeService;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ReturnedGuest>>> GetAllGuests([FromQuery] GuestParam guestParam)
        {
            var spec = new GuestWithPartySpec(guestParam);
         var result =   await _unitOfWork.repository<Guest>().GetAllAsync(spec);
            if (result == null) return NotFound();
            var mapped = _mapper.Map<IReadOnlyList<Guest>, IReadOnlyList<ReturnedGuest>>(result);
            return Ok(mapped);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ReturnedGuest>> GetByID(int Id)
        {
            var spec = new GuestWithPartySpec(Id);
            var result = await _unitOfWork.repository<Guest>().GetByIdAsync(spec);
            if (result == null) return NotFound();
            var mapped = _mapper.Map<ReturnedGuest>(result);
            return Ok(mapped);
        }
        [HttpPost]
        public async Task<ActionResult<ReturnedGuest>> Create(GuestsDTO guestsDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var party =await _unitOfWork.repository<Party>().GetByIdOnlyAsync(guestsDTO.PartyId);
            if (party == null) return NotFound(new {message = "Party Not Found"});
            var mapped = _mapper.Map<GuestsDTO , Guest>(guestsDTO);
            var qr = await _qRCodeService.GenerateQR($"Guest Name :  {guestsDTO.Name} Phone Number :{guestsDTO.PhoneNumber} Party : {party.Name}");
            mapped.QrCodeUrl = qr;
           await _unitOfWork.repository<Guest>().AddAsync(mapped);
          await  _unitOfWork.SavaAsync();
          var returned =   _mapper.Map<Guest, ReturnedGuest>(mapped);
            return Ok(returned);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReturnedGuest>> Update(int id , GuestsDTO guestsDTO)
        {
            var guest = await _unitOfWork.repository<Guest>().GetByIdOnlyAsync(id);
            if (guest == null) return NotFound();
            if (!ModelState.IsValid) return BadRequest();
            var party = await _unitOfWork.repository<Party>().GetByIdOnlyAsync(guestsDTO.PartyId);
           var qr =  await _qRCodeService.GenerateQR($"name :{guestsDTO.Name} : phone Number :{guestsDTO.PhoneNumber} :party :{party.Name}");
            ImageFunctions.DeleteFile(guest.QrCodeUrl);
            guest.PhoneNumber = guestsDTO.PhoneNumber;
            guest.Name = guestsDTO.Name;
            guest.PartyId = guestsDTO.PartyId;
            guest.QrCodeUrl = qr;
            await _unitOfWork.SavaAsync();
           var mapped =  _mapper.Map<Guest , ReturnedGuest>(guest);
            return Ok(mapped);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var guest = await _unitOfWork.repository<Guest>().GetByIdOnlyAsync(id);
            if(guest == null) return NotFound();
            ImageFunctions.DeleteFile(guest.QrCodeUrl);
            _unitOfWork.repository<Guest>().Delete(guest);
            await _unitOfWork.SavaAsync();
            return Ok(new { message = "Deleted Successfully" });
        }
    }
}
