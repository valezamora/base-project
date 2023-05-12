using System;
using clase_25_4.Data;
using Microsoft.AspNetCore.Mvc;

namespace clase_25_4.Controllers
{
    public class StudentsController : Controller
    {
        private readonly DatabaseContext _context;

        public StudentsController(DatabaseContext context)
        {
            _context = context;
        }
    }
}
