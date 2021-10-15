using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Task_Managment_System.Core.Models;

namespace Domain.Entities
{
   public class RefreshToken
    {

        [Key]
        public string Token { get; set; }
        public string Id { get; set; }
        public DateTime  CreationDate { get; set; }
        public DateTime  ExpireDate { get; set; }
        public bool IsUsed { get; set; }
        public bool Invaliddated { get; set; }
        public string Userid { get; set; }
        [ForeignKey(nameof(Userid))]
        public AppUser User { get; set; }
    }
}
