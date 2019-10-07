using Calculator.DAL.Interfaces;
using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.DAL.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CalculatorDbContext _calculatorDbContext;
        private IBaseRepository<Example> exampleRepo;
        private bool isDisposed = false;
        public UnitOfWork(CalculatorDbContext calculatorDbContext)
        {
            _calculatorDbContext = calculatorDbContext;
        }
        public IBaseRepository<Example> Examples
        {
            get
            {
                if (exampleRepo == null) { exampleRepo = new BaseRepository<Example>(_calculatorDbContext); }
                return exampleRepo;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                _calculatorDbContext.Dispose();
            }
            isDisposed = true;
        }

        public async Task Save()
        {
            await _calculatorDbContext.SaveChangesAsync();
        }
    }
}
