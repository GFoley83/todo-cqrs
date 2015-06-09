using System.ComponentModel.DataAnnotations;

namespace TodoCqrs.Web.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Resolved { get; set; }
    }
}