using Practice.Data.Model;

namespace Practice.Data.Dto
{
    public class EditUserDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
    }
}
