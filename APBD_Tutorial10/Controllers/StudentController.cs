using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBD_Tutorial10.Entities;
using APBD_Tutorial10.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APBD_Tutorial10.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentContext _studentContext;

        public StudentController(StudentContext studentContext) {
            _studentContext = studentContext;
        }

        [HttpGet]
        public IActionResult GetStudent() {

            var students = _studentContext.Student.Include(s => s.IdEnrollmentNavigation).
                ThenInclude(f => f.IdStudyNavigation).Select(g => new MyStudent
                {
                    IndexNumber = g.IndexNumber,
                    FirstName = g.FirstName,
                    Lastname = g.LastName,
                    Birthdate = g.BirthDate.ToShortDateString(),
                    Semester = g.IdEnrollmentNavigation.Semester,
                    Studies = g.IdEnrollmentNavigation.IdStudyNavigation.Name

                }).ToList();

            return Ok(students);
        }

        [HttpPut]
        public IActionResult NewStudent() {


            MyStudent enter = new MyStudent
            {
                IndexNumber = "s456",
                FirstName = "Ntokozo",
                Lastname = "Ndabandaba",
                Birthdate = "1999/15/07",
                Semester = 2022,
                Studies = "Computer Science"
            };

            var addi = new Student
            {
                IndexNumber = enter.IndexNumber,
                FirstName = enter.FirstName,
                LastName = enter.Lastname,
                BirthDate = DateTime.Parse(enter.Birthdate),
                IdEnrollment = 1

        };

            _studentContext.Student.Add(addi);

          

            _studentContext.SaveChanges();


            return Ok("New Student Added");

        }

        [HttpPut]
        public IActionResult UpdateStudents() {


            var change = new Student {
                IdEnrollment = 2
            };
            _studentContext.Attach(change);
            _studentContext.Entry(change).Property("IdEnrollment").IsModified = true;
            _studentContext.SaveChanges();


            return Ok("Student has been updated");


        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id) {

            var exc = _studentContext.Student.Find(id);
            if (exc is null) {
                return NotFound("Student does not exist");
            }
            _studentContext.Student.Remove(exc);
            _studentContext.SaveChanges();

            return Ok("Student Deleted");
        }
    }
}