using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.DTOs
{
    public class CategoryAddDto
    {
        [DisplayName("Category Name")]
        [Required(ErrorMessage = "{0} can not be empty")]
        [MaxLength(70,ErrorMessage = "{0} must be smaller than {1} character")]
        [MinLength(3, ErrorMessage = "{0} must be grater than {1} character")]
        public string Name { get; set; }
        [DisplayName("Description")]
        [MaxLength(500, ErrorMessage = "{0} must be smaller than {1} character")]
        [MinLength(3, ErrorMessage = "{0} must be grater than {1} character")]
        public string Description { get; set; }

        [DisplayName("Category Note")]
        [MaxLength(500, ErrorMessage = "{0} must be smaller than {1} character")]
        [MinLength(3, ErrorMessage = "{0} must be grater than {1} character")]
        public string Note { get; set; }
        
        [DisplayName("Status")]
        [Required(ErrorMessage = "{0} can not be empty")]
        public bool IsActive  { get; set; }

    }
}
