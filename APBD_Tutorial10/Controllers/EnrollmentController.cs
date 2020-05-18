using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBD_Tutorial10.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Tutorial10.Controllers
{
    [Route("api/enrollment")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly StudentContext _enrollcontext;

        public EnrollmentController(StudentContext studentContext) {
            _enrollcontext = studentContext;
        }

        [HttpPost("{enroll}")]
        public IActionResult enroll() {


            string fname = "Jogn";
            string lname = "Doe";
            string index = "s12345";
            string studies = "IT";
            int semester = 2020;
            DateTime bdate = DateTime.Parse("30.03.1993");

            var studs = _enrollcontext.Student.Select(f => f.IndexNumber == index).ToList();
            if (studs is null) {
                return NotFound("This student has already been assigned");
            }

            var dubs = _enrollcontext.Studies.Select(g => g.Name == studies).ToList();
            if (dubs is null)
            {
                return NotFound("This student has already been assigned");
            }

            var give = new Student
            {
                IndexNumber = index,
                FirstName = fname,
                LastName = lname,
                BirthDate = bdate,
                IdEnrollment = 1

            };

            _enrollcontext.Student.Add(give);

            _enrollcontext.Enrollment.Add(new Enrollment
            {
                IdEnrollment = 3,
                Semester = semester,
                IdStudy = 1,
                StartDate = DateTime.Parse("2022/01/03")
            });

            _enrollcontext.SaveChanges();

            return CreatedAtAction(nameof(enroll), give);
        }

        [HttpPost("{promote}")]
        public IActionResult promote() {


            var prom = new Student
            {
                IdEnrollment = 2
            };
            _enrollcontext.Attach(prom);
            _enrollcontext.Entry(prom).Property("IdEnrollment").IsModified = true;
            _enrollcontext.SaveChanges();

            return CreatedAtAction(nameof(promote), "Students have been updated");

        }
    }
}