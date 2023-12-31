﻿using Microsoft.AspNetCore.Mvc;
using Practice.Data;
using Practice.Data.Dto;
using Practice.Data.Model;
using Practice.Services.TagService;

namespace Practice.Controllers
{
    [Route("api/tag")]
    [ApiController]

    public class TagController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITagService _tagService;
        public TagController(AppDbContext context, 
            ITagService tagService)
        {
            _context = context;
            _tagService = tagService;
        }
        /// <summary>
        ///Get tag list
        /// </summary>
        /// <response  code="200">Success</response>
        /// <response  code="400">Bad Request</response>
        /// <response  code="500">Internal Server Error</response>
        [HttpGet]
        public async Task<List<TagDto>> GetTag()
        {
            return await _tagService.GetTagDtoList();
        }
        [HttpPost]
        public async Task CreateTags(List<string> tags)
        {
            foreach (var item in tags)
            {
                _context.Tag.Add(new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = item,
                    CreatedTime = DateTime.Now,
                });
            }
            await _context.SaveChangesAsync();
        }
    }
}