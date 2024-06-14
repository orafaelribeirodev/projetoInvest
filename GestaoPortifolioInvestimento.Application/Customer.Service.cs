namespace GestaoPortifolioInvestimento.Application
{
    //INJETAR: services.AddScoped<GestaoPortifolioInvestimento.Service.ICustomer, GestaoPortifolioInvestimento.Service.Customer>();

    public interface ICustomerService
    {
        Task<string?> Gravar(GestaoPortifolioInvestimento.Domain.Customer obj);
        Task<string?> Excluir(long cust_id);
        Task<List<GestaoPortifolioInvestimento.Domain.Customer>?> Listar();
        Task<GestaoPortifolioInvestimento.Domain.Customer?> Registro(long cust_id);
    }

    public class CustomerService: ICustomerService
    {
        private GestaoPortifolioInvestimento.Repositories.ICustomerData _customer;

        public CustomerService(GestaoPortifolioInvestimento.Repositories.ICustomerData customer)
        {
            _customer = customer;
        }

        public async Task<string?> Gravar (GestaoPortifolioInvestimento.Domain.Customer obj)
        {
            var _validar = Validar(obj);
            if(!string.IsNullOrEmpty(_validar)) return  _validar;

            return await _customer.Gravar(obj) ? "" : "";
        }

        public async Task<string?> Excluir (long cust_id)
        {
            return await _customer.Excluir(cust_id) ? "" : "Não foi possível excluir a informação.";
        }

        public async Task<GestaoPortifolioInvestimento.Domain.Customer?> Registro (long cust_id)
        {
            return await _customer.Registro(cust_id);
        }

        public async Task<List<GestaoPortifolioInvestimento.Domain.Customer>?> Listar()
        {
            return await _customer.Listar();
        }

        public string? Validar (GestaoPortifolioInvestimento.Domain.Customer obj)
        {
            var msg = "";
            
            if(string.IsNullOrEmpty(obj.cust_name))
            {
                msg += "Campo  é obrigatório.\n";
            }
            if(string.IsNullOrEmpty(obj.cust_email))
            {
                msg += "Campo  é obrigatório.\n";
            }

            return msg;
        }

    }
}
