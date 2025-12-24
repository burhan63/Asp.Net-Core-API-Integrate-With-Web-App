using System.ComponentModel.DataAnnotations;

namespace CoreWebAppWithCrud.Models
{
    public class Student
        {
            public int stdId { get; set; }
            [Required]
            public string stdName { get; set; }
            [Required]
            public string stdFatherName { get; set; }
            [Required]
            public string stdRollNo { get; set; }
            [Required]
            public string stdContact { get; set; }
            [Required]
            public string stdCnic { get; set; }
            }
}
