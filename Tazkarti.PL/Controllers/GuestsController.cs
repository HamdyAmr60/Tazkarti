using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Tazkarti.Core;
using Tazkarti.Core.Models;
using Tazkarti.Core.Services;
using Tazkarti.PL.DTOs;
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
        public async Task<ActionResult<IReadOnlyList<Guest>>> GetAllGuests()
        {
         var result =   await _unitOfWork.repository<Guest>().GetAllAsync();
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<Guest>> GetByID(int Id)
        {
            var result = await _unitOfWork.repository<Guest>().GetByIdAsync(Id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<ReturnedGuest>> Create(GuestsDTO guestsDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var party =await _unitOfWork.repository<Party>().GetByIdAsync(guestsDTO.PartyId);
            if (party == null) return NotFound(new {message = "Party Not Found"});
            var mapped = _mapper.Map<GuestsDTO , Guest>(guestsDTO);
            var qr = await _qRCodeService.GenerateQR($"Guest Name :  {guestsDTO.Name} Phone Number :{guestsDTO.PhoneNumber} Party : {party.Name}");
            mapped.QrCodeUrl = qr;
           await _unitOfWork.repository<Guest>().AddAsync(mapped);
          await  _unitOfWork.SavaAsync();
          var returned =   _mapper.Map<Guest, ReturnedGuest>(mapped);
            return Ok(returned);
        }
    }
}
