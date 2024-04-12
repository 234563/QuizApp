using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using QuizApp.Enities;
using System.Diagnostics.CodeAnalysis;


namespace QuizApp.Enities
{

    public class User
    {
        /// <summary>
        /// Unique identifier for the user.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Username of the user.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        /// <summary>
        /// Email address of the user.
        /// </summary>
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// Email address of the user.
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }


        /// <summary>
        /// Date of birth of the user.
        /// </summary>
        [Column(TypeName = "Date")]
        [AllowNull]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Address of the user.
        /// </summary>
        [MaxLength(200)]
        [AllowNull]
        public string Address { get; set; } = "";

        /// <summary>
        /// Phone number of the user.
        /// </summary>

        [Phone]
        [MaxLength(11)]
        [AllowNull]
        public string PhoneNumber { get; set; } = "";

        /// <summary>
        /// Indicates whether the user's account is deleted or not.
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Date when the user registered.
        /// </summary>
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Total number of wins for user 
        /// </summary>
        public int TotalWins { get; set; } = 0;

        /// <summary>
        /// Total number of wins for user 
        /// </summary>
        public int TotalLosses { get; set; } = 0;


        // Navigation property for the many-to-many relationship
        public virtual ICollection<UserQuestion> QuestionsAnswered { get; set; }
    }

}



