using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("Messages")]
    public class Message
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string text { get; set; }
    }
}