using QuizApp.Enities;
using QuizApp.Models;
using System.Net;

namespace QuizApp.Services
{
    public class AuthServices
    {
        public ApplicationDbContext DbContext;

        /// <summary>
        /// Construstor that takes application db context 
        /// </summary>
        public AuthServices(ApplicationDbContext _dbContext)
        {
            DbContext = _dbContext;
        }

        /// <summary>
        /// Authenticate UserLogin
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        public async Task<ResultModel> Authenticate(LoginModel loginModel)
        {
            try
            {
                /// try to look for user in the database 
                var user = DbContext.ApplicationUsers.FirstOrDefault(u => u.Email == loginModel.Email && u.Password == loginModel.Password);

                /// If no user is found 
                if (user == null)
                {
                    return new ResultModel()
                    {
                        IsSucessfull = false,
                        Message = "Email or Password is Invalid ",
                        Code = (int)HttpStatusCode.Unauthorized

                    };
                }
                else 
                {
                    return new ResultModel
                    {
                        IsSucessfull = true,
                        Data = user,
                        Message = "Login Sucessfull",
                        Code = (int)HttpStatusCode.OK
                    };

                }
            }
            catch (Exception)
            {

              
            }
            return new ResultModel()
            {
                IsSucessfull = false,
                Message = "Email or Password is Invalid ",
                Code = (int)HttpStatusCode.Unauthorized
            };


        }


        /// <summary>
        /// Authenticate Register
        /// </summary>
        /// <param name="RegisterModel"></param>
        /// <returns></returns>
        public async Task<ResultModel> AuthenticateRegister(RegisterModel RegisterModel)
        {
            try
            {
                /// try to look for user in the database 
                var user = DbContext.ApplicationUsers.FirstOrDefault(u => u.Email == RegisterModel.Email);

                /// If no user is found 
                if (user == null)
                {
                    return new ResultModel()
                    {
                        IsSucessfull = true,
                        Message = "Email is not register",
                        Code = (int)HttpStatusCode.OK

                    };
                }
                else
                {
                    return new ResultModel
                    {
                        IsSucessfull = false,
                        Data = user,
                        Message = "This Email is already registered",
                        Code = (int)HttpStatusCode.Unauthorized
                    };

                }
            }
            catch (Exception ex)
            {
                return new ResultModel
                {
                    IsSucessfull = false,
                    Message = ex.Message,
                    Code = (int)HttpStatusCode.Unauthorized
                };

            }

        }


    }
}