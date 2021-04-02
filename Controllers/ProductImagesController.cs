using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductImagesController : ControllerBase
    {
        private readonly SadboisContext _context;
        private readonly IMapper _mapper;

        public ProductImagesController(SadboisContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        [HttpGet("{productId}")]
        public IActionResult GetImagesForProduct(string productId)
        {
            return Ok(_context.ProductImages.Select(pi => pi.ProductId.ToString().Equals(productId)).FirstOrDefault());
        } 
    }
}