using Calculator.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.BLL.Interfaces.Managers
{
    public interface IExampleManager
    {
        Task Insert(ExampleDTO example);
        Task Update(ExampleDTO example);
        Task Delete(ExampleDTO example);
        Task<IEnumerable<ExampleDTO>> GetAll();
        Task Get();
    }
}
