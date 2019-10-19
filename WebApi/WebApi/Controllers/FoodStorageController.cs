using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<ActionResult<List<Pack>>> GetPackList()
        {
            return await packService.GetPackList();
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<Pack>> GetPack(long id)
        {
            var item = await packService.GetPack(id);
            if (item != null)
                return item;

            return NotFound(id);
        }

        [HttpGet("IsComplete/{id}")]
        public async Task<ActionResult<bool>> IsCompletePack(long id)
        {
            var pack = await packService.GetPack(id);
            if (pack != null)
                return await packService.IsPackComplete(pack);

            return NotFound(id);
        }

        [HttpPut("UpdatePack/{id}")]
        public async Task<ActionResult> UpdatePack(long id, Pack pack)
        {
            var item = await packService.GetPack(id);
            if (item != null)
            {
                await packService.UpdatePack(id, pack);
                return Ok(id);
            }
            return NotFound(id);
        }

        [HttpPost("RemovePack/{id}")]
        public async Task<ActionResult> RemovePack(long id)
        {
            var pack = await packService.GetPack(id);
            if (pack != null)
            {
                await packService.DeletePack(id);
                return CreatedAtAction(nameof(RemovePack), new { id }, pack);
            }
            return NotFound(id);
        }

        [HttpPost("AddPack")]
        public async Task<ActionResult> AddPack(Pack pack)
        {
            await packService.AddPack(pack);
            return CreatedAtAction(nameof(AddPack), new { id = pack.Id }, pack);
        }

        /// <summary>
        /// Return updated pack minus one item if there are more than one item.
        /// Otherwise return null (pack deleted from food storage)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("RemoveItemFromPack/{id}")]
        public async Task<ActionResult<Pack>> RemoveItemFromPack(long id)
        {
            var pack = await packService.GetPack(id);
            if (pack != null)
            {
                pack = await packService.RemoveItemFromPack(pack);
                return CreatedAtAction(nameof(RemoveItemFromPack), new { id }, pack);
            }
            return NotFound(id);
        }
    }
}
