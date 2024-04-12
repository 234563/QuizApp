using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuizApp;
using QuizApp.Enities;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext Dbcontext;

        public QuestionsController(ApplicationDbContext context)
        {
            Dbcontext = context;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            try
            {
                /// Check Session 
                var LoggedInUserName = HttpContext.Session.GetString("username");
                var LoggedInUser = HttpContext.Session.GetString("useremail");
                if (LoggedInUser == null)
                {
                    return RedirectToAction("Login", "Auth");
                }

                ViewBag.LoggedInUser = LoggedInUserName;

                /// Check if user is admin or not 
                var User = await Dbcontext.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == LoggedInUser);
                if (User != null)
                {
                    ViewBag.IsAdmin = (User.Id == 1);
                }
               
            }
            catch (Exception)
            {

               
            }
            return View(await Dbcontext.Questions.ToListAsync());
        }

        #region Details

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                // Check User Session 
                var LoggedInUser = HttpContext.Session.GetString("username");
                ViewBag.LoggedInUser = LoggedInUser;

                if (LoggedInUser == null)
                {
                    return RedirectToAction("Login", "Auth");
                }

                if (id == null)
                {
                    return NotFound();
                }

                // Get question from database 
                var question = await Dbcontext.Questions.FirstOrDefaultAsync(m => m.Id == id);
                /// if question is not fund then return NotFound
                if (question == null)
                {
                    return NotFound();
                }
                QuestionModel model = new QuestionModel() { Id = question.Id, QuestionText = question.QuestionText, Answer1 = question.Answer1, Answer2 = question.Answer2, Answer3 = question.Answer3, Answer4 = question.Answer4, CorrectAnswer = question.CorrectAnswer, UserAnswer = "" };

                return View(model);
            }
            catch (Exception ex)
            {

            }
            
            
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Details(QuestionModel questionmodel)
        {
            try
            {
                /// Check Session 
                var LoggedInUser = HttpContext.Session.GetString("useremail");
                if (LoggedInUser == null)
                {
                    return RedirectToAction("Login", "Auth");
                }

                ///Get question from database
                var question = await Dbcontext.Questions.FirstOrDefaultAsync(m => m.Id == questionmodel.Id);
              
                if(question != null)  /// if there is no record 
                {
                       
                    var User = await Dbcontext.ApplicationUsers.Include(u => u.QuestionsAnswered)
                                            .FirstOrDefaultAsync(u => u.Email == LoggedInUser);
                        
                    /// If User hasn't answer any question yet 
                    if(User.QuestionsAnswered == null)
                    {
                        User.QuestionsAnswered = new List<UserQuestion>();
                    }

                    //if user has not answered that question before 
                    if (!User.QuestionsAnswered.Any(qa => qa.QuestionId == questionmodel.Id))
                    {
                        /// Match user choice with correct option 
                        if (questionmodel.UserAnswer == question.CorrectAnswer.ToString())
                        {
                            // Add to Wins 
                            User.TotalWins = +1;
                            User.QuestionsAnswered.Add(new UserQuestion() { User = User, AnsweredDate = DateTime.Now, Question = question, IsCorrectAnswer = false });
                        }
                        else
                        {
                            User.TotalLosses = +1;
                            // Add to Losses 
                            User.QuestionsAnswered.Add(new UserQuestion() { User = User, AnsweredDate = DateTime.Now, Question = question, IsCorrectAnswer = false });
                        }

                        Dbcontext.SaveChanges();
                    }

                   
                }

            }
            catch(Exception ex)
            {

            }

            return RedirectToAction("Index");
        }
        #endregion
       
        #region Create

        // GET: Questions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuestionModel questionModel )
        {
            try
            {
                /// Check Session 
                var loggedinuser = HttpContext.Session.GetString("username");
                ViewBag.LoggedInUser = loggedinuser;

                /// Remove answer to avoid modelstate error 
                ModelState.Remove("UserAnswer");

                if (ModelState.IsValid)
                {
                    // Check if CorrectAnswer matches one of the provided options
                    if (questionModel.CorrectAnswer.ToLower() != questionModel.Answer1.ToLower() &&
                        questionModel.CorrectAnswer.ToLower() != questionModel.Answer2.ToLower() &&
                        questionModel.CorrectAnswer.ToLower() != questionModel.Answer3.ToLower() &&
                        questionModel.CorrectAnswer.ToLower() != questionModel.Answer4.ToLower())
                    {
                        ModelState.AddModelError(nameof(questionModel.CorrectAnswer), "Correct answer must be one of the provided options.");
                        return View(questionModel);
                    }

                    byte[] ImageFile = await ConvertImageToByteArray(questionModel.Image);
                    /// Create Question Object 
                    Question question = new Question()
                    {
                        QuestionText = questionModel.QuestionText
                                                        ,
                        Answer1 = questionModel.Answer1
                                                        ,
                        Answer2 = questionModel.Answer2
                                                        ,
                        Answer3 = questionModel.Answer3
                                                        ,
                        Answer4 = questionModel.Answer4
                                                        ,
                        CorrectAnswer = questionModel.CorrectAnswer
                                                        ,
                        Image = ImageFile
                    };

                    Dbcontext.Add(question);
                    await Dbcontext.SaveChangesAsync();
                    /// Redirect to list of questions 
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(Exception ex)
            {

            }

            
            return View(questionModel);
        }
        #endregion

       

        #region Edit

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            //Validate Session 
            var loggedinuser = HttpContext.Session.GetString("username");
            ViewBag.LoggedInUser = loggedinuser;

           

            var question = await Dbcontext.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            QuestionModel questionmodel = new QuestionModel()
            {
                Id = question.Id,
                
                QuestionText = question.QuestionText
                                                      ,
                Answer1 = question.Answer1
                                                      ,
                Answer2 = question.Answer2
                                                      ,
                Answer3 = question.Answer3
                                                      ,
                Answer4 = question.Answer4
                                                      ,
                CorrectAnswer = question.CorrectAnswer
                                                      
                
            };

            return View(questionmodel);
        }

        // POST: Questions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QuestionText,Answer1,Answer2,Answer3,Answer4,CorrectAnswer,Image")] QuestionModel questionmodel)
        {
            if (id != questionmodel.Id)
            {
                return NotFound();
            }
            ModelState.Remove("UserAnswer");
            if (ModelState.IsValid)
            {
                try
                {
                    // Check if CorrectAnswer matches one of the provided options
                    if (questionmodel.CorrectAnswer.ToLower() != questionmodel.Answer1.ToLower() &&
                        questionmodel.CorrectAnswer.ToLower() != questionmodel.Answer2.ToLower() &&
                        questionmodel.CorrectAnswer.ToLower() != questionmodel.Answer3.ToLower() &&
                        questionmodel.CorrectAnswer.ToLower() != questionmodel.Answer4.ToLower())
                    {
                        ModelState.AddModelError(nameof(questionmodel.CorrectAnswer), "Correct answer must be one of the provided options.");
                        return View(questionmodel);
                    }

                    Dbcontext.Update(questionmodel);
                    await Dbcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(questionmodel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(questionmodel);
        }
        #endregion

        #region Delete

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {   
                /// Check Session 
                var LoggedInUser = HttpContext.Session.GetString("username");
                ViewBag.LoggedInUser = LoggedInUser;

                /// Check ID 
                if (id == null)
                {
                    return NotFound();
                }
                /// FInd question from Db 
                var question = await Dbcontext.Questions .FirstOrDefaultAsync(m => m.Id == id);
                /// Question
                if (question == null)
                {
                    return NotFound();
                }

                return View(question);
            }
            catch (Exception)
            {

            }
            return NotFound();
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                /// Find Qustion to be deleted 
                var question = await Dbcontext.Questions.FindAsync(id);
                if (question != null)  // Delete question 
                {
                    Dbcontext.Questions.Remove(question);
                }

                await Dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

               
            }
           return RedirectToAction(nameof(Index));
           
        }

        #endregion


        #region Helper Methods

        private static async Task<byte[]> ConvertImageToByteArray(IFormFile Image)
        {
            try
            {
                byte[] ImageFile = null;
                // Check if an image file was uploaded
                if (Image != null && Image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        // Copy the file content to a memory stream
                        await Image.CopyToAsync(memoryStream);

                        // Convert the memory stream to a byte array
                        ImageFile = memoryStream.ToArray();
                    }
                }

                return ImageFile;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        private bool QuestionExists(int id)
        {
            return Dbcontext.Questions.Any(e => e.Id == id);
        }


        public async Task<IActionResult> GetImage(int ID)
        {
            var question = await Dbcontext.Questions.FindAsync(ID);
            
            if(question != null)
            {
                // Retrieve the image byte array from the database based on the given ID
                byte[] imageData = question.Image;

                ///Check if the image data is not null
                if (imageData != null)
                {
                    // Return the image data as a FileContentResult with the appropriate content type
                    return File(imageData, "image/jpeg"); // Adjust the content type as needed
                }
            }


            // If image data is null, return a placeholder image or handle the scenario as needed
            return NotFound(); // Or return a placeholder image
        }
        #endregion

    }
}
