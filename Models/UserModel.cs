using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.Models {
    public class UserModel {
        public Guid? Id { get; set; }
        [Required]
        public string Username { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";

        public UserModel ToDTO() {
            UserModel dto = this;
            dto.Password = "";
            return dto;
        }
    }
}