using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;

namespace UserBaseAPI.Domain.Entities
{

    [Index(nameof(DocNumber), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public required Guid Id { get; set; }

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }  
        public required string Password { get; set; }

        public string Role { get; set; } = string.Empty;
        public Int64 DocNumber { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Phone2 { get; set; } = string.Empty;
     
        public required Guid ManagerID { get; set; } 
        public DateTime Birthday { get; set; }
        public Boolean Deleted { get; set; } = false;





        // firstName=""; 
        // lastName=""; 
        // password="";
        // email="";

        // docNumber unique 
        // phone 
        // phone2 
        // managerName
        // managerID 
        // birthday
    }
}
