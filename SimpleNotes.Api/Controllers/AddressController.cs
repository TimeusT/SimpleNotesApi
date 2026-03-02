using Microsoft.AspNetCore.Mvc;
using SimpleNotes.Application.DTOs;
using SimpleNotes.Application.Interfaces;

namespace SimpleNotes.Api.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("{id}")]
        public ActionResult GetAddress(int id)
        {

        }

        [HttpPost]
        public IActionResult CreateAddress([FromBody] CreateUserRequest user)
        {

        }

        [HttpPut("{id}")]
        public ActionResult<UserResponse> UpdateAddress(int id, [FromBody] UpdateUserRequest user)
        {

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(int id)
        {

        }
    }
}
