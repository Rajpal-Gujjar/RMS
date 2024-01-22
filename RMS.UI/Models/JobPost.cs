using System.ComponentModel.DataAnnotations;

namespace RMS.UI.Models
{
    public class JobPost
    {
        public int Id { get; set; }

        [Required, Range(typeof(DateTime), "01/02/2022", "01/31/2023", ErrorMessage = "Enter valid Date")]
        public DateTime RequiredDate { get; set; }= DateTime.Now.AddDays(3);

        [Required]
        public string Category { get; set; }

        [Required, Range(typeof(int),"1","2000", ErrorMessage = "Enter Valid Post No.")]
        public int NoOfPosts { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Qualification { get; set; }

        [Required, Range(typeof(int), "100000", "300000", ErrorMessage = "Choose any Salary")]
        public int Salary { get; set; }

        [Required, Range(typeof(int), "1", "4", ErrorMessage = "Choose any Experience")]
        public int Experience { get; set; }

        [Required,Range(typeof(int), "1", "5", ErrorMessage = "Choose any Company Name")]
        public int CompanyId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime? DeletedDate { get; set; }
    }
}
