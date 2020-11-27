using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.DTOs;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private SadboisContext _context;
        private readonly IMapper _mapper;
        
        public CategoriesController(SadboisContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Categories
                .ToList()
                .Select(category => _mapper.Map<Category, CategoryDto>(category))
                .ToList()
            );
        }
    }
}