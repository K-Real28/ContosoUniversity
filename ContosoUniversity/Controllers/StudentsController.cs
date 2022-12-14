using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.ToListAsync();
            var result = new List<StudentViewModel>();

            foreach (var student in students)
            {
                var studentViewModel = new StudentViewModel();
                studentViewModel.Id = student.ID;
                studentViewModel.LastName = student.LastName;
                studentViewModel.FirstMidName = student.FirstMidName;
                studentViewModel.EnrollmentDate = student.EnrollmentDate;
                result.Add(studentViewModel);
            }

            return View(result);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            var studentViewModel = new StudentViewModel();
            studentViewModel.Id = student.ID;
            studentViewModel.LastName = student.LastName;
            studentViewModel.FirstMidName = student.FirstMidName;
            studentViewModel.EnrollmentDate = student.EnrollmentDate;

            return View(studentViewModel);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                var student = new Student()
                {
                    LastName = studentViewModel.LastName,
                    FirstMidName = studentViewModel.FirstMidName,
                    EnrollmentDate = studentViewModel.EnrollmentDate
                };

                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentViewModel);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            var studentViewModel = new StudentViewModel();
            studentViewModel.Id = student.ID;
            studentViewModel.LastName = student.LastName;
            studentViewModel.FirstMidName = student.FirstMidName;
            studentViewModel.EnrollmentDate = student.EnrollmentDate;

            return View(studentViewModel);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentViewModel studentViewModel)
        {
            var student = _context.Students.FirstOrDefault(_ => _.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    student.LastName = studentViewModel.LastName;
                    student.FirstMidName = studentViewModel.FirstMidName;
                    student.EnrollmentDate = studentViewModel.EnrollmentDate;

                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.ID))
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
            return View(studentViewModel);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }
            var studentViewModel = new StudentViewModel();
            studentViewModel.Id = student.ID;
            studentViewModel.LastName = student.LastName;
            studentViewModel.FirstMidName = student.FirstMidName;
            studentViewModel.EnrollmentDate = student.EnrollmentDate;
  
            return View(studentViewModel);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = _context.Students.FirstOrDefault(q => q.ID == id);
            if (student == null)
            {
                return NotFound();
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Students?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
