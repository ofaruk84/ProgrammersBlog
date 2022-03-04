using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using ProgrammersBlog.Entities.Concrete;

namespace ProgrammersBlog.Entities.DTOs
{
    public class ArticleAddDto
    {
        [DisplayName("Title")]
        [Required(ErrorMessage = "{0} can not be empty")]
        [MaxLength(70, ErrorMessage = "{0} must be smaller than {1} character")]
        [MinLength(3, ErrorMessage = "{0} must be grater than {1} character")]
        public string Title { get; set; }

        [DisplayName("Content")]
        [Required(ErrorMessage = "{0} can not be empty")]
        [MinLength(20, ErrorMessage = "{0} must be grater than {1} character")]
        public string Content { get; set; }

        [DisplayName("Thumbnail")]
        [Required(ErrorMessage = "{0} can not be empty")]
        [MaxLength(70, ErrorMessage = "{0} must be smaller than {1} character")]
        [MinLength(3, ErrorMessage = "{0} must be grater than {1} character")]
        public string Thumbnail { get; set; }

        [DisplayName("Date")]
        [Required(ErrorMessage = "{0} can not be empty")]
        [DisplayFormat(ApplyFormatInEditMode = true,DataFormatString = "{0:dd:MM:yyyy}")]
        public DateTime Date { get; set; }

        [DisplayName("Seo Author")]
        [Required(ErrorMessage = "{0} can not be empty")]
        [MaxLength(50, ErrorMessage = "{0} must be smaller than {1} character")]
        [MinLength(3, ErrorMessage = "{0} must be grater than {1} character")]
        public string SeoAuthor { get; set; }

        [DisplayName("Seo Description")]
        [Required(ErrorMessage = "{0} can not be empty")]
        [MaxLength(150, ErrorMessage = "{0} must be smaller than {1} character")]
        [MinLength(3, ErrorMessage = "{0} must be grater than {1} character")]
        public string SeoDescription { get; set; }

        [DisplayName("Seo Tags")]
        [Required(ErrorMessage = "{0} can not be empty")]
        [MaxLength(70, ErrorMessage = "{0} must be smaller than {1} character")]
        [MinLength(3, ErrorMessage = "{0} must be grater than {1} character")]
        public string SeoTags { get; set; }

        [DisplayName("Category")]
        [Required(ErrorMessage = "{0} can not be empty")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [DisplayName("Is Active")]
        [Required(ErrorMessage = "{0} can not be empty")]
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}
