using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodStorage.Entities;
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

        [HttpGet("get-pack-list")]
        public async Task<ActionResult<List<Pack>>> GetPackList()
        {
            try
            {
                return await packService.GetPackList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<Pack>> GetPack(long id)
        {
            try
            {
                var item = await packService.GetPack(id);
                if (item != null)
                    return item;

                return NotFound(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("is-complete/{id}")]
        public async Task<ActionResult<bool>> IsCompletePack(long id)
        {
            try
            {
                var pack = await packService.GetPack(id);
                if (pack != null)
                    return await packService.IsPackComplete(pack);

                return NotFound(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("update-pack/{id}")]
        public async Task<ActionResult> UpdatePack(long id, Pack pack)
        {
            try
            {
                var item = await packService.GetPack(id);
                if (item != null)
                {
                    await packService.UpdatePack(id, pack);
                    return Ok(id);
                }
                return NotFound(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("remove-pack/{id}")]
        public async Task<ActionResult> RemovePack(long id)
        {
            try
            {
                var pack = await packService.GetPack(id);
                if (pack != null)
                {
                    await packService.DeletePack(id);
                    return CreatedAtAction(nameof(RemovePack), new { id }, pack);
                }
                return NotFound(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("add-pack")]
        public async Task<ActionResult> AddPack(Pack pack)
        {
            try
            {
                await packService.AddPack(pack);
                return CreatedAtAction(nameof(AddPack), new { id = pack.Id }, pack);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Return updated pack minus one item if there are more than one item.
        /// Otherwise return null (pack deleted from food storage)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("remove-item-from-pack/{id}")]
        public async Task<ActionResult<Pack>> RemoveItemFromPack(long id)
        {
            try
            {
                var pack = await packService.GetPack(id);
                if (pack != null)
                {
                    pack = await packService.RemoveItemFromPack(pack);
                    return CreatedAtAction(nameof(RemoveItemFromPack), new { id }, pack);
                }
                return NotFound(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
