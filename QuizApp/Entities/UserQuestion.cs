using Microsoft.EntityFrameworkCore;
using QuizApp.Enities;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Enities
{
    [Keyless]
    public class UserQuestion
    {
        // Foreign key for user 
        public int UserId { get; set; }
        // Foreign key for question 
        public int QuestionId { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Question Question { get; set; }

        // Was the answer right  or  wrong
        public bool IsCorrectAnswer { get; set; }
        // Date when user answered the question 
        public DateTime AnsweredDate { get; set; } = DateTime.Now;
        
    }

}


