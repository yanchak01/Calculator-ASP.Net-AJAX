using Calculator.BLL.Interfaces.Managers;
using Calculator.DTO;
using Calculator.Operations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator.Controllers
{
    //[Route("[controller]/[action]")]
    public class HomeController:Controller
    {
        
        private readonly IExampleManager _exampleManager;
        private readonly IExampleActions _exampleActions;
        public HomeController(IExampleManager exampleManager,IExampleActions exampleActions)
        {
            _exampleManager = exampleManager;
            _exampleActions = exampleActions;
        }
        public async Task<ActionResult> Index()
        {
            return View();
        }
       [HttpGet]
       public async Task<IEnumerable<ExampleDTO>> GetAll()
        {
            try
            {
                var item = await _exampleManager.GetAll();
                return item;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<ExampleDTO> Insert([FromBody] ExampleDTO exampleDTO)
        {
            
            try
            {
                await _exampleManager.Insert(exampleDTO);

                return exampleDTO;

            }
            catch (Exception)
            {
                var c = new ExampleDTO { Text = "Error" };
                await _exampleManager.Insert(c);
                return c;

            }
        }
        [HttpDelete]
        public async Task DeleteAll()
        {
            var item = await _exampleManager.GetAll();
            List<ExampleDTO> items = item.ToList();
            for (int i = 0; i < items.Count; i++)
            {
                await _exampleManager.Delete(items[i]);
            }

        }
    }
}
