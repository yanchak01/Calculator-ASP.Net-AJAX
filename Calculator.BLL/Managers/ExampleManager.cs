using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Calculator.BLL.Interfaces.Managers;
using Calculator.DAL.Interfaces;
using Calculator.DTO;
using Calculator.Models;
using Calculator.Operations;

namespace Calculator.BLL.Managers
{
    public class ExampleManager : BaseManager,IExampleManager
    {
        private readonly IExampleActions _exampleActions;
        public ExampleManager(IUnitOfWork unitOfWork,IMapper mapper,IExampleActions exampleActions) : base(unitOfWork,mapper)
        {
            _exampleActions = exampleActions;
        }

        public async Task Delete(ExampleDTO example)
        {
            Example ex = await unitOfWork.Examples.Get(example.Id);
            await unitOfWork.Examples.Delete(ex);
            await unitOfWork.Save();
        }

        public Task Get()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ExampleDTO>> GetAll()
        {
            var b = await unitOfWork.Examples.GetAll();
            return mapper.Map<IEnumerable<Example>, IEnumerable<ExampleDTO>>(b);
        }

        public async Task Insert(ExampleDTO example)
        {
            var b = _exampleActions.Calculate(example.Text);
            example.Result = b;
            example.Text += "=" + b.ToString();
            await unitOfWork.Examples.Insert(mapper.Map<ExampleDTO, Example>(example));
            await unitOfWork.Save();
        }

        public async Task Update(ExampleDTO example)
        {
            await unitOfWork.Examples.Update(mapper.Map<ExampleDTO, Example>(example));
            await unitOfWork.Save();
        }
    }
}
