using System.ComponentModel.DataAnnotations;

namespace RMS.UI.Models
{
    public class JobSeeker
    {
        public int Id { get; set; }

        [Required, RegularExpression(@"^[A-Za-z\s]{1,}[\.]{0,1}[A-Za-z\s]{0,}$", ErrorMessage = "Enter valid Name")]
        public string Name { get; set; }

        [Required, RegularExpression(@"^(91[\-\s]?)?[6-9]\d{9}$", ErrorMessage = "Enter valid phone number")]
        public long Phone { get; set; }

        [Required, EmailAddress, RegularExpression(@"^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$", ErrorMessage = "Enter Valid Email")]
        public string Email { get; set; }

        [Required, MinLength(8), MaxLength(15)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required, Range(typeof(DateTime), "01/01/1980", "01/01/2005", ErrorMessage = "Enter valid age")]
        public DateTime DateOfBirth { get; set; }= DateTime.Now.AddYears(-18);

        [Required]
        public string Language { get; set; }

        [Required]
        public string Qualification { get; set; }

        [Required,Range(typeof(int),"2019","2024",ErrorMessage ="Enter vaild Year")]
        public int PassingYear { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string State { get; set; }

        [Required, MinLength(3), MaxLength(20), RegularExpression(@"^[A-Za-z\s]{0,}$", ErrorMessage = "Invalid City number.")]
        public string City { get; set; }

        [Required]
        public int Exprience { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}
