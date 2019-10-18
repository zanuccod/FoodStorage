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
        public JsonResult GetPackList()
        {
            return Json(packService.GetPackList());
        }

        [HttpGet("Get/{id}")]
        public JsonResult GetPack(long id)
        {
            return Json(packService.GetPack(id));
        }

        [HttpPost("AddPack")]
        public void AddPack(Pack pack)
        {
            packService.AddPack(pack);
        }

        [HttpPut("RemoveItem/{id}")]
        public void RemoveItemFromPack(long id)
        {
            packService.RemoveItemFromPack(id);
        }

        [HttpGet("IsComplete/{id}")]
        public JsonResult IsCompletePack(long id)
        {
            return Json(packService.IsPackComplete(id));
        }
    }
}
