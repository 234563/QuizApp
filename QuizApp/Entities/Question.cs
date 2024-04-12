using QuizApp.Enities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Enities
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// This is actal question 
        /// </summary>
        [Required]
        [StringLength(500)]
        public string QuestionText { get; set; }

        /// <summary>
        /// First Option for Question
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Answer1 { get; set; }
        /// <summary>
        /// Second Option for question 
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Answer2 { get; set; }

        /// <summary>
        /// Third Option for question 
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Answer3 { get; set; }

        /// <summary>
        /// Forth Option for question 
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Answer4 { get; set; }
        /// <summary>
        /// Correct Option  1 for Answer1, 2 for Answer2, etc.
        /// </summary>
        [Required]
        public string CorrectAnswer { get; set; } 

        /// <summary>
        /// Image of object 
        /// </summary>
        [Required]
        public byte[] Image { get; set; }


        public virtual ICollection<UserQuestion> UsersWhoAnswered { get; set; }


    }

}