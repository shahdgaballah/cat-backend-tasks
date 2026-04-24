using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.Data;
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Controllers
{
    [ApiController]
    [Route("controller")]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<Student>> GetAll()
        { 
            var students =  _context.Students.ToList();
            return Ok(students);

        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Student> GetById(int id)
        { 
            var student =  _context.Students.Find(id);
            if (student == null) return NotFound();

            return Ok(student);

        }
        [HttpPost]
        [Route("")]
        public ActionResult<Student> CreateStudent(Student student)
        {
            student.Id = 0;
            _context.Students.Add(student);
            _context.SaveChanges();
            return Ok(student.Id);

        }
        [HttpPut]
        [Route("")]
        public ActionResult<Student> UpdateStudent(Student student)
        {
            var existingStudent = _context.Students.Find(student.Id);
            if (existingStudent == null) return NotFound();

            existingStudent.Name = student.Name;
            existingStudent.PhoneNumber = student.PhoneNumber;
            _context.Students.Update(existingStudent);
            _context.SaveChanges();
            return Ok();

        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult<Student> DeleteStudent(int id)
        {
            var existingStudent = _context.Students.Find(id);
            if (existingStudent == null) return NotFound();

            _context.Students.Remove(existingStudent);
            _context.SaveChanges();
            return Ok();

        }
    }
}
