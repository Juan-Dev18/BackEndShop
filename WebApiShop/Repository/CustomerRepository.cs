using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using WebApiShop.Interface;
using WebApiShop.Models;

namespace WebApiShop.Repository
{
    public class CustomerRepository : ICustomer
    {
        //Juan Carrasco: I decided not to use ORMs.

        private readonly ClsConnection _connectionString;

        public CustomerRepository(IOptions<ClsConnection> option)
        {
            _connectionString = option.Value;
        }

        public async Task<IEnumerable<ClsCustomer>> GetCustomers()
        {
            List<ClsCustomer> lstCustomer = new List<ClsCustomer>();

            try
            {

                await using (var connection = new SqlConnection(_connectionString.Connection))
                {
                    var command = new SqlCommand("SP_GetCustomers", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lstCustomer.Add(new ClsCustomer
                            {
                                CustomerId = Convert.ToInt32(reader["customerId"]),
                                Name = reader["name"].ToString(),
                                Address = reader["address"].ToString(),
                                Email = reader["email"].ToString(),
                                Phone = reader["phone"].ToString()
                            });
                        }
                    }

                }

                return lstCustomer;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Getting products", ex);
            }
        }
    }
}
