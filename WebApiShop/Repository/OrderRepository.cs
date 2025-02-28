using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata.Ecma335;
using WebApiShop.Interface;
using WebApiShop.Models;

namespace WebApiShop.Repository
{
    public class OrderRepository : IOrder
    {
        //Juan Carrasco: I decided not to use ORMs.

        private readonly ClsConnection _connectionString;

        public OrderRepository(IOptions<ClsConnection> option)
        {
            _connectionString = option.Value;
        }

        #region Crud Methods

        public async Task<ClsRequestOrderResponse> InsertOrder(ClsRequestCreateOrder orderInfo)
        {
            ClsRequestOrderResponse result = new();

            try
            {

                await using (var connection = new SqlConnection(_connectionString.Connection))
                {
                    var command = new SqlCommand("SP_CreateOrder", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CustomerId", orderInfo.Order!.Customer!.CustomerId);

                    await connection.OpenAsync();
                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            orderInfo.Order.OrderId = Convert.ToInt32(reader["orderId"]);
                        }
                    }

                }

                if (orderInfo.Order.OrderId <= 0)
                {
                    return new ClsRequestOrderResponse { Message = "Error Creating the Order" };
                }

                result = await InsertOrderDetail(orderInfo);
                if (!string.IsNullOrEmpty(result.Message)) return result;

                result = await UpdateOrderTotal(orderInfo);
                if (!string.IsNullOrEmpty(result.Message)) return result;

                result = await GetOrdersById(orderInfo.Order.OrderId ?? 0);
                result.Message = "OK";

                return result;
            }
            catch (Exception ex)
            {
                return new ClsRequestOrderResponse { Message = ex.Message };
            }
        }

        public async Task<ClsRequestOrderResponse> GetOrdersById(int orderId)
        {
            ClsRequestOrderResponse result = new();

            try
            {
                await using (var connection = new SqlConnection(_connectionString.Connection))
                {
                    var command = new SqlCommand("SP_GetOrderById", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrderId", orderId);

                    await connection.OpenAsync();
                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result = new ClsRequestOrderResponse
                            {
                                Order = new ClsOrder
                                {
                                    OrderId = Convert.ToInt32(reader["orderId"]),
                                    OrderDate = Convert.ToDateTime(reader["orderDate"]),
                                    OrderTotal = Convert.ToDecimal(reader["orderTotal"]),
                                    Customer = new ClsCustomer
                                    {
                                        CustomerId = Convert.ToInt32(reader["customerId"]),
                                        Name = reader["name"].ToString(),
                                        Address = reader["address"].ToString(),
                                        Email = reader["email"].ToString(),
                                        Phone = reader["phone"].ToString()
                                    },
                                },
                            };
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return new ClsRequestOrderResponse { Message = ex.Message };
            }
        }

        #endregion Crud Methods

        #region Methods Complementary

        private async Task<ClsRequestOrderResponse> InsertOrderDetail(ClsRequestCreateOrder orderInfo)
        {
            ClsRequestOrderResponse result = new();

            try
            {
                await using (var connection = new SqlConnection(_connectionString.Connection))
                {
                    var command = new SqlCommand("SP_AddOrderDetail", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    await connection.OpenAsync();
                    foreach (var item in orderInfo.OrderDetails!)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@OrderId", orderInfo.Order!.OrderId);
                        command.Parameters.AddWithValue("@ProductId", item.Product!.ProductId);
                        command.Parameters.AddWithValue("@Quantity", item.Quantity);
                        

                        await command.ExecuteNonQueryAsync();

                    }
                }


            }
            catch (SqlException ex)
            {
                result.Message = ex.Message;
            }
          
            return result;
        }

        private async Task<ClsRequestOrderResponse> UpdateOrderTotal(ClsRequestCreateOrder orderInfo)
        {
            ClsRequestOrderResponse result = new();

            try
            {
                await using (var connection = new SqlConnection(_connectionString.Connection))
                {
                    var command = new SqlCommand("SP_UpdateOrderTotal", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrderId", orderInfo.Order!.OrderId);
                    await connection.OpenAsync();
                    var reader = await command.ExecuteNonQueryAsync();
                    
                }

                return result;
            }
            catch (Exception ex)
            {
               return new ClsRequestOrderResponse { Message = ex.Message };
            }

        }

        #endregion Methods Complementary

    }
}
