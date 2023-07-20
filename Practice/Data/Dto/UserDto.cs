﻿using Practice.Data.Model;

namespace Practice.Data.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }
    }
}