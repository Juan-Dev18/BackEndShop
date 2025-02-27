using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Security.Cryptography;
using WebApiShop.Interface;
using WebApiShop.Models;

namespace WebApiShop.Repository
{
    public class ProductRepository : IProduct
    {

        //Juan Carrasco: I decided not to use ORMs.

        private readonly ClsConnection _connectionString;

        public ProductRepository(IOptions<ClsConnection> option)
        {
            _connectionString = option.Value;
        }

        public async Task<IEnumerable<ClsProduct>> GetProducts(int offset, int pageSize)
        {
            List<ClsProduct> lstProduct = new List<ClsProduct>();

            try
            {

                await using (var connection = new SqlConnection(_connectionString.Connection))
                {
                    var command = new SqlCommand("SP_GetProducts", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@pageSize", pageSize);

                    await connection.OpenAsync();
                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lstProduct.Add(new ClsProduct
                            {
                                ProductId = Convert.ToInt16(reader["productId"]),
                                Name = reader["productName"].ToString(),
                                Code = reader["code"].ToString(),
                                Description = reader["description"].ToString(),
                                UnitPrice = Convert.ToDecimal(reader["unitprice"]),
                                Stock = Convert.ToInt16(reader["stock"]),
                                Category = new ClsCategory
                                {
                                    CategoryId = Convert.ToInt32(reader["categoryId"]),
                                    CategoryName = reader["categoryName"].ToString()
                                },

                            });
                        }
                    }

                }

                return lstProduct;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Getting products", ex);
            }          
        }
    }

}
