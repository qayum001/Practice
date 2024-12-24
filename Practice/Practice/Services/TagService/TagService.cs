using Practice.Data;
using Practice.Data.Dto;
using Practice.Data.Model;

namespace Practice.Services.TagService
{
    public class TagService : ITagService
    {
        private readonly AppDbContext _context;

        public TagService(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<TagDto>> GetTagDtoList()
        {
            var list = _context.Tag.ToList();

            var dtoList = new List<TagDto>();

            foreach (var item in list)
            {
                dtoList.Add(new TagDto() { Id = item.Id, Name = item.Name, CreateTime = item.CreatedTime });
            }

            return Task.FromResult(dtoList);
        }

        public async Task<List<Tag>> GetTagListByIdList(List<Guid> guides)
        {
            var tagList = new List<Tag>();

            foreach(var guide in guides) 
            {
                var tag = await _context.Tag.FindAsync(guide);
                if(tag == null) { continue; }
                tagList.Add(tag);
            }

            return tagList;
        }

        public Task AddTagsToPost(Post post, List<Guid> tagGuidList)
        {
            var postTaglist = post.Tags.ToList();

            tagGuidList = tagGuidList.Distinct().ToList();

            foreach (var tag in postTaglist)
            {
                foreach(var id in tagGuidList)
                {
                    if(tag.Id == id) continue;
                    var currentTag = _context.Tag.First(x => x.Id == id);
                    post.Tags.Add(currentTag);
                }
            }

            return Task.CompletedTask;
        }

        public Task DeleteTagsFromPost(Post post, List<Guid> tagGuidList)
        {
            var postTaglist = post.Tags.ToList();

            tagGuidList = tagGuidList.Distinct().ToList();

            foreach (var tag in postTaglist)
            {
                foreach (var id in tagGuidList)
                {
                    if (tag.Id != id) continue;
                    var currentTag = _context.Tag.First(x => x.Id == id);
                    post.Tags.Remove(currentTag);
                }
            }

            return Task.CompletedTask;
        }
    }
}