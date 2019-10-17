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
    public class FoodStorageController : ControllerBase
    {
        private readonly IPackService packService;

        public FoodStorageController(IPackService packService)
        {
            this.packService = packService;
        }

        [HttpGet("GetAllPack")]
        public List<Pack> Get()
        {
            return packService.GetPackList();
        }
    }
}
