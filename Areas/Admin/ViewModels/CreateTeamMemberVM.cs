//using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebFrontToBack.Areas.Admin.ViewModels
{
    public class CreateTeamMemberVM
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        //[MaxLength(50, ErrorMessage = "Uzunluq maximum 50 simvol olmalıdır")]
        public string FullName { get; set; }
        [Required]
        public string Profession { get; set; }//ixtisas
    
        /*[required,notmapped]//*//*/---miq olma*/
        [Required,NotMapped]
        public IFormFile Photo { get; set; }
    }
}
