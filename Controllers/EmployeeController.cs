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
       
       
        public EmployeeController(Interfaceemp inter,IMemoryCache memory)
        {
            _inter = inter;
            _memory = memory;
            
           
        }

        [Route("Index")]
        public IActionResult Index()
        {
            List<Employee> result = new List<Employee>();
           if(_memory.TryGetValue("empmem",out List<Employee> employ))
            {
                
                return View(new MyView() { Employees = employ });
            }
            else
            {
                
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
