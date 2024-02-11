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
        public ActionResult Get()
        {
            return Ok("apartment management");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Save()
        {
            return Ok("save management");
        }

        [Authorize(Roles = "Manager")]
        [HttpPut]
        public ActionResult Update()
        {
            return Ok("update management");
        }

        [Authorize(Policy = "BirthDateOver18Check")]
        [HttpDelete]
        public ActionResult Delete()
        {
            return Ok("delete management");
        }
    }
}
