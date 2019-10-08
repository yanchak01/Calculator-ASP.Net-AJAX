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
          //var item = await _exampleManager.GetAll();
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
                var b = _exampleActions.Calculate(exampleDTO.Text);
                var c = new ExampleDTO { Text = exampleDTO.Text + "=" + b.ToString() };
                await _exampleManager.Insert(c);

                return c;

            }
            catch (Exception)
            {
                var c = new ExampleDTO { Text = "Error" };
                await _exampleManager.Insert(new ExampleDTO { Text = "Error" });
                return c;

            }
        }

        public string Calculate(string Text)
        {
            var b = _exampleActions.Calculate(Text);
            return Text + "=" + b.ToString();
        }

    }
}
