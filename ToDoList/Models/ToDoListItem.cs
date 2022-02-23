using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class ToDoListItem
    {
        public int Id { get; set; }

        public DateTime AddDate { get; set; }
        public DateTime SetDate { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Title must contain a maximum of 250 characters!")]
        public string TITLE { get; set; }

        public bool STATUS { get; set; }
    }
}
