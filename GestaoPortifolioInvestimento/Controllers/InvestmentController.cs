using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPortifolioInvestimento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentController : ControllerBase
    {
        private readonly Application.ICustomerService _customerService;

        public InvestmentController(Application.ICustomerService customerService)
        {
            this._customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Domain.Customer teste)
        {
            return Ok(new
            {
                data = await _customerService.Gravar(teste)
            });
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _customerService.Listar());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var product = await _customerService.Registro(id);
            return Ok(new
            {
                data = product
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(new
            {
                data = await _customerService.Excluir(id)
            });
        }
    }
}
