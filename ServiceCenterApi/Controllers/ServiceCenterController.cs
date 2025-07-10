using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceCenterApi.Data;
using ServiceCenterApi.Models;

namespace ServiceCenterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCentersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServiceCentersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceCenter>> CreateServiceCenter(ServiceCenter serviceCenter)
        {
            _context.ServiceCenters.Add(serviceCenter);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetServiceCenter), new { id = serviceCenter.Id }, serviceCenter);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceCenter>> GetServiceCenter(int id)
        {
            var serviceCenter = await _context.ServiceCenters.FindAsync(id);
            if (serviceCenter == null)
            {
                return NotFound();
            }
            return serviceCenter;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceCenter>>> GetServiceCenters()
        {
            return await _context.ServiceCenters.ToListAsync();
        }
    }
}