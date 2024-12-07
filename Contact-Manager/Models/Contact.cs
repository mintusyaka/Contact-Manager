using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contact_Manager.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Phone { get; set; }
		[Required]
		public OperatorType Operator { get; set; }

		public string Description { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }

    public enum OperatorType
    {
        Kyivstar,
        Vodafone,
        Lycamobile,
        Lifecell
    }
}
