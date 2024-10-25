using System.ComponentModel.DataAnnotations;

namespace dotnet_console_sample.Entities
{
    public class Customer
    {
        [Key]
        public int CusID { get; set; }
        public string? CusName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public DateTime? LastLoginTime { get; set; }
        
    }
}