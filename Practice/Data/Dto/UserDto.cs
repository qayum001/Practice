using Practice.Data.Model;

namespace Practice.Data.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
    }


}