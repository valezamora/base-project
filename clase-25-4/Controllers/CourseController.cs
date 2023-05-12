using System.Net;
using clase_25_4.Models;
using clase_25_4.Services;
using Microsoft.AspNetCore.Mvc;

namespace clase_25_4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;
        private readonly IEntityService<Course> courseService;

        public CourseController(ILogger<CourseController> logger, IEntityService<Course> courseSrv)
        {
            _logger = logger;
            courseService = courseSrv;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<Course>> GetCourses()
        {
            return  courseService.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            Course course =  courseService.GetById((int)id);
            // todo handle if id doesn't exist, return error/404
            return course;
        }
    }
}