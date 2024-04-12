using System.IO;
namespace QuizApp.Models
{
    /// <summary>
    /// Model for Question class
    /// </summary>
    public class QuestionModel
    {
        public int Id { get; set; }

        public string QuestionText { get; set; }
        
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public string CorrectAnswer { get; set; }
        public IFormFile Image { get; set; }
        public string UserAnswer { get; set; }


       

       public byte[] ConvertImageToByteArray(string imagePath)
       {
             byte[] imageBytes = File.ReadAllBytes(imagePath);
             return imageBytes;
       }

}
}
