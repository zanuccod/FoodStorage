using System;
using FoodStorage.Entities;
using FoodStorage.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoodStorageController : Controller
    {
        private readonly IPackService packService;

        public FoodStorageController(IPackService packService)
        {
            this.packService = packService;
        }

        [HttpGet("GetPackList")]
        public ActionResult<JsonResult> GetPackList()
        {
            return Json(packService.GetPackList());
        }

        [HttpGet("Get/{id}")]
        public ActionResult<JsonResult> GetPack(long id)
        {
            var item = packService.GetPack(id);
            if (item != null)
                return Json(item);

            return NotFound();
        }

        [HttpPost("AddPack")]
        public ActionResult AddPack(Pack pack)
        {
            packService.AddPack(pack);
            return Accepted();
        }

        [HttpPut("RemoveItemFromPack/{id}")]
        public ActionResult RemoveItemFromPack(long id)
        {
            packService.RemoveItemFromPack(id);
            return Accepted();
        }

        [HttpGet("IsComplete/{id}")]
        public ActionResult<JsonResult> IsCompletePack(long id)
        {
            return Json(packService.IsPackComplete(id));
        }
    }
}
