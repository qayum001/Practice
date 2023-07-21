using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Data.Dto;

namespace Practice.Services.TagService
{
    public class TagService : ITagService
    {
        private readonly AppDbContext _context;

        public TagService(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<TagDto>> GetTagList()
        {
            var list = _context.Tag.ToList();

            var dtoList = new List<TagDto>();

            foreach (var item in list)
            {
                dtoList.Add(new TagDto() { Id = item.Id, Name = item.Name, CreateTime = item.CreatedTime });
            }

            return Task.FromResult(dtoList);
        }
    }
}
