using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebFrontToBack.Models
{
    public class TeamMembers
    {

        public int Id { get; set; }
        //[Required(ErrorMessage = "Boş ola bilməz"), MaxLength(50, ErrorMessage = "Uzunluq maximum 50 simvol olmalıdır")]
        public string FullName { get; set; }
        //[Required]
        public string Profession { get; set; }//ixtisas
        //[Required]
        public string ImagePath { get; set; }

        //[Required,NotMapped] //---Miq olma
        //public IFormFile Photo { get; set; }


    }
}
