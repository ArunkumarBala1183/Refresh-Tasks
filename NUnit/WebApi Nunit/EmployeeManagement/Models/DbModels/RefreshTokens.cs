using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models.DbModels
{
    public class RefreshTokens
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(name:"Username")]
        public string UserName { get; set; }

        [Column(name:"Refresh_Token")]
        public string RefreshToken { get; set; }
    }
}
