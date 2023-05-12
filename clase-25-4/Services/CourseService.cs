using clase_25_4.Data;
using clase_25_4.Models;
using Microsoft.EntityFrameworkCore;

namespace clase_25_4.Services
{
    public class CourseService: IEntityService<Course>
    {
        private DatabaseContext _context;
        public CourseService(DatabaseContext context) : base()
        {
            _context = context;
        }

        public List<Course> GetAll()
        {
            return _context.Courses.ToList();
        }

        public Course GetById(int id)
        {
            return _context.Courses.Single(c => c.CourseID == id);
        }


    }
}