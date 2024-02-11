using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentManagementSystem.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentManagementController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Get()
        {
            return Ok("Apartment management");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Save()
        {
            return Ok("Save management");
        }

        [HttpPut]
        [Authorize(Roles = "Manager")]
        public ActionResult Update()
        {
            return Ok("Update management");
        }

        [HttpDelete]
        [Authorize(Policy = "ResidentOwnerAuthorizationHandler")]
        public ActionResult Delete()
        {
            return Ok("Delete management");
        }
    }
}

