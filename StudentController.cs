using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using StudentManagement.Models;
using StudentManagement.Services;


namespace StudentManagement.Controllers
{
    public class StudentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IStudentServices _studentServices;

        public StudentController(IConfiguration configuration, IStudentServices studentServices)
        {
            _configuration = configuration;
            _studentServices = studentServices;
        }

  

        public IActionResult Index()
        {
            StudentVM model = new StudentVM();
            model.StudentList = _studentServices.GetStudentList().ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddUpdateStudent(Student student)
        {
            StudentVM model=new StudentVM();

            student.CreatedBy = 1;
            string url = Request.Headers["Referer"].ToString();

            string result = string.Empty;

            if(student.StudentId>0)
            {
                result = _studentServices.UpdateStudent(student);
            }
            else
            {
                result = _studentServices.InsertStudent(student);
            }
            if(result=="Save Successfully")
            {
                TempData["SuccessMsg"] = "Save Successfully";
                return Redirect(url);
            }
            else
            {
                TempData["ErrorMsg"] = result;
                return Redirect(url);
            }
            
        }
        [HttpPost] 
        public IActionResult DeleteStudent(int StudentId)
        {
            if(StudentId != 0)
           
            return Json(new { Message = "Delete Successfully ...." + StudentId });
            else
                return Json(new { Message = "fail ---" });
            //string url = Request.Headers["Referer"].ToString();
            //string result = _studentServices.DeleteStudent(StudentId);
            //if (result == "Delete Successfully")
            //{
            //    return Json(new { Message = "Delete Successfully" });
            //}
            //else
            //{
            //    return Json(new { Message = "Error Occurred"});
            //}
        }
    }
}    
