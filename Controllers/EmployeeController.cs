using Inmemoryu.DI;
using Inmemoryu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Inmemoryu.Controllers
{
    [Route("[Controller]")]
    public class EmployeeController : Controller
    {
        private readonly Interfaceemp _inter;
        private readonly IMemoryCache _memory;
        private readonly ILogger<EmployeeController> _logger;
       
        public EmployeeController(Interfaceemp inter,IMemoryCache memory,ILogger<EmployeeController> logger)
        {
            _inter = inter;
            _memory = memory;
            _logger = logger;
           
        }

        [Route("Index")]
        public IActionResult Index()
        {
            List<Employee> result = new List<Employee>();
           if(_memory.TryGetValue("empmem",out List<Employee> employ))
            {
                _logger.LogInformation("from memory");
                return View(new MyView() { Employees = employ });
            }
            else
            {
                _logger.LogInformation("from db");
                result = _inter.getAll();
                var cacheentryoptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(60))
                    .SetPriority(CacheItemPriority.Normal);
                _memory.Set("empmem", result, cacheentryoptions);
            }
            return View(new MyView() { Employees = result });

        }
    }
}
