using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Controllers.DTOs;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SizesController : ControllerBase
    {
        private readonly SadboisContext _context;

        public SizesController(SadboisContext context)
        {
            _context = context;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Size> GetAll()
        {
            return _context.Sizes;
        }
    }
}