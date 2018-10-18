using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //momenteel string om te testen
        public string Rol { get; set; }
    }
}
