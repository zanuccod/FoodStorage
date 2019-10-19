using System;
using System.Collections.Generic;
using FoodStorage.Entities;
using FoodStorage.Models;
using FoodStorage.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class FoodStorageController : Controller
    {
        private readonly IPackService packService;

        public FoodStorageController(IPackService packService)
        {
            this.packService = packService;
        }

        [HttpGet("GetPackList")]
        public ActionResult<List<Pack>> GetPackList()
        {
            return packService.GetPackList();
        }

        [HttpGet("Get/{id}")]
        public ActionResult<Pack> GetPack(long id)
        {
            var item = packService.GetPack(id);
            if (item != null)
                return item;

            return NotFound(id);
        }

        [HttpGet("IsComplete/{id}")]
        public ActionResult<bool> IsCompletePack(long id)
        {
            var pack = packService.GetPack(id);
            if (pack != null)
                return packService.IsPackComplete(pack);

            return NotFound(id);
        }

        [HttpPut("UpdatePack/{id}")]
        public ActionResult UpdatePack(long id, Pack pack)
        {
            var item = packService.GetPack(id);
            if (item != null)
            {
                packService.UpdatePack(id, pack);
                return Ok(id);
            }
            return NotFound(id);
        }

        [HttpPost("RemovePack/{id}")]
        public ActionResult RemovePack(long id)
        {
            var pack = packService.GetPack(id);
            if (pack != null)
            {
                packService.DeletePack(id);
                return CreatedAtAction(nameof(RemovePack), new { id }, pack);
            }
            return NotFound(id);
        }

        [HttpPost("AddPack")]
        public ActionResult AddPack(Pack pack)
        {
            packService.AddPack(pack);
            return CreatedAtAction(nameof(AddPack), new { id = pack.Id }, pack);
        }

        /// <summary>
        /// Return updated pack minus one item if there are more than one item.
        /// Otherwise return null (pack deleted from food storage)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("RemoveItemFromPack/{id}")]
        public ActionResult<Pack> RemoveItemFromPack(long id)
        {
            var pack = packService.GetPack(id);
            if (pack != null)
            {
                pack = packService.RemoveItemFromPack(pack);
                return CreatedAtAction(nameof(RemoveItemFromPack), new { id }, pack);
            }
            return NotFound(id);
        }
    }
}
