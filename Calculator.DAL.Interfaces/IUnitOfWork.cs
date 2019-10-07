using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.DAL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IBaseRepository<Example> Examples { get; }

        Task Save();
    }
}
