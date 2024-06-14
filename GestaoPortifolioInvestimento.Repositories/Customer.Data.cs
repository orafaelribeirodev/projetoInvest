using Dapper;
using Npgsql;

namespace GestaoPortifolioInvestimento.Repositories
{
    //INJETAR: services.AddScoped<GestaoPortifolioInvestimento.Repositories.ICustomer, GestaoPortifolioInvestimento.Repositories.Customer>();

    public interface ICustomerData
    {
        Task<bool> Gravar(GestaoPortifolioInvestimento.Domain.Customer obj);
        Task<bool> Excluir(long cust_id);
        Task<List<GestaoPortifolioInvestimento.Domain.Customer>?> Listar();
        Task<GestaoPortifolioInvestimento.Domain.Customer?> Registro(long cust_id);
    }

    public class CustomerData: ICustomerData
    {
        private NpgsqlConnection _conn;

        public CustomerData()
        {
            _conn = new NpgsqlConnection(GestaoPortifolioInvestimento.Repositories.Settings.StrConnection);
        }

        public  async Task<bool> Gravar(GestaoPortifolioInvestimento.Domain.Customer obj)
        {
            return obj.cust_id == 0 ? await Inserir(obj) : await Editar(obj);
        }

        public  async Task<bool> Inserir(GestaoPortifolioInvestimento.Domain.Customer obj)
        {
            try
            {
                var sql = "insert into customer (cust_name, cust_email)values(@cust_name, @cust_email1)";
                return await _conn.ExecuteAsync(sql, obj) > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public  async Task<bool> Editar(GestaoPortifolioInvestimento.Domain.Customer obj)
        {
            try
            {
                var sql = "update customer set cust_name = @cust_name, cust_email = @cust_email where cust_id = @cust_id";
                return await _conn.ExecuteAsync(sql, obj) > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public  async Task<bool> Excluir(long cust_id)
        {
            try
            {
                var sql = "delete from customer where cust_id = @cust_id";
                return await _conn.ExecuteAsync(sql, new {cust_id}) > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public  async Task<List<GestaoPortifolioInvestimento.Domain.Customer>?> Listar()
        {
            try
            {
                var sql = $"select * from customer";
                var rsp = await _conn.QueryAsync<GestaoPortifolioInvestimento.Domain.Customer>(sql);
                return rsp?.ToList();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public  async Task<GestaoPortifolioInvestimento.Domain.Customer?> Registro(long cust_id)
        {
            try
            {
                var sql = "select * from customer where cust_id = @cust_id";
                var rsp = await _conn.QueryAsync<GestaoPortifolioInvestimento.Domain.Customer>(sql, new {cust_id});
                return rsp?.FirstOrDefault();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public  async Task<bool> Existe(GestaoPortifolioInvestimento.Domain.Customer obj)
        {
            try
            {
                var sql = "select * from customer where cust_id = @cust_id";
                var rsp = await _conn.QueryAsync<GestaoPortifolioInvestimento.Domain.Customer>(sql, new {obj.cust_id});
                return rsp?.Count() > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

    }
}
