﻿using Barebone.Controllers;
using Barebone.Services;
using Checkins.Data.Abstractions;
using Checkins.Data.Entities;
using Checkins.ViewModels.Checkin;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkins.Controllers.Api
{
    //[Authorize]
    [Route("api/checkin")]
    public class CheckinsController : Barebone.Controllers.ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IConfiguration _configuration;

        public CheckinsController(IStorage storage, IImageService imageService, IConfiguration configuration) : base(storage)
        {
            _imageService = imageService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get(int page = 0, int size = 25)
        {
            IEnumerable<CheckinDto> data = new CheckinModelFactory().LoadAll(this.Storage, page, size)?.Checkins;
            int count = data.Count();

            return Ok(new
            {
                success = true,
                data,
                count,
                totalPage = ((int)count / size) + 1
            });
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var repo = this.Storage.GetRepository<ICheckinRepository>();

            Checkin checkin = repo.WithKey(id);

            if (checkin == null)
                return this.NotFound(new { success = false });
            return Ok(new { success = true, data = checkin });
        }

        [HttpPost]
        public async Task<IActionResult> Post(CheckinCreateViewModels viewModel)
        {
            if (this.ModelState.IsValid)
            {
                Checkin entity = viewModel.ToEntity();
                var repo = this.Storage.GetRepository<ICheckinRepository>();

                entity.VerifyLate(_configuration.GetValue<string>("LateTime"));

                var imageUrl = await _imageService.UploadImageAsync(viewModel.Image);

                entity.ImageUrl = imageUrl.ToString();
                repo.Create(entity, GetCurrentUserName());
                this.Storage.Save();
                return Ok(new { success = true });
            }

            return BadRequest(new { success = false });
        }
    }
}
