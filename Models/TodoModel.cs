using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.Models {
    public class TodoModel {
        public Guid? Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Title { get; set; } = "";
        public bool Done { get; set; } = false;
    }
}