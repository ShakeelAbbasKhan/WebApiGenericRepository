using CourseProject.Business.Validation;
using CourseProject.Common.Dtos.Address;
using CourseProject.Common.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAddress(AddressCreate addressCreate)
        {
            var id = await _addressService.CreateAddressAsync(addressCreate);
            return Ok(id);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAddress(AddressUpdate addressUpdate)
        {
            await _addressService.UpdateAddressAsync(addressUpdate);
            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAddress(AddressDelete addressDelete)
        {
            await _addressService.DeleteAddressAsync(addressDelete);
            return Ok();
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetAddress(int id)
        {
            var address = await _addressService.GetAddressAsync(id);
            return Ok(address);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAddresses()
        {
         //   throw new Exception("Test");
            var addresses = await _addressService.GetAddressesAsync();
            return Ok(addresses);
        }
    }
}
