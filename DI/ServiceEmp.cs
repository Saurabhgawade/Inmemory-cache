using Inmemoryu.Models;

namespace Inmemoryu.DI
{
    public class ServiceEmp : Interfaceemp
    {
        List<Employee> employees = new List<Employee>() { new Employee() { Id = 1, Name = "saurabh", Salary = 50000 }, new Employee() { Id = 2, Name = "gawade", Salary = 100000 } };
        public List<Employee> getAll()
        {
            return employees.Where(x => true).ToList();
        }
    }
}
