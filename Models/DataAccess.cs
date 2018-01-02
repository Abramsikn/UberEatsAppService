using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web;
using System.Linq;
using System.Net;
using System.Data;
using UberEatsAppService.Models;

namespace UberEatsAppService.Models
{
    public class DataAccess
    {
        static string connectString = "SERVER=localhost;UID=root;DATABASE=UberEATS_Portal;";
        static MySqlDataReader read;

        // Register user
        public string RegisterCustomer(Customer cust)
        {
            string x = "";

            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;
                string query = "INSERT INTO UberEATS_Portal.Customer(Firstname,Lastname,Contact,Email,Password) " +
                    "VALUES('" + cust.Firstname + "','" + cust.Lastname + "','" + cust.Contact + "','" + cust.Email + "','" + cust.Password + "');";
                using (MySqlCommand comma = new MySqlCommand(query, connect))
                {
                    try
                    {
                        comma.Connection.Open();

                        comma.Parameters.AddWithValue("@Firstname", cust.Firstname);
                        comma.Parameters.AddWithValue("@Lastname", cust.Lastname);
                        comma.Parameters.AddWithValue("@Contact", cust.Contact);
                        comma.Parameters.AddWithValue("@Email", cust.Email);
                        comma.Parameters.AddWithValue("@Password", cust.Password);
                        int y = comma.ExecuteNonQuery();

                        x = y.ToString();

                    }
                    catch (MySqlException ex)
                    {
                        ex.ToString();
                        comma.Connection.Close();
                    }
                }
                return null;
            }
        }

        //Login
        public Customer CustomerLogin(string email, string password)
        {
            string sql = "SELECT Customer_Id,Firstname,Lastname,Contact,Email,Password FROM UberEATS_Portal.Customer WHERE Email='" + email + "'AND Password='" + password + "';";

            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;
                MySqlCommand comma = new MySqlCommand(sql, connect);
                comma.Connection = connect;

                try
                {
                    comma.Connection.Open();
                    comma.Parameters.Add(new MySqlParameter("@Email", email));
                    comma.Parameters.Add(new MySqlParameter("@Password", password));

                    read = comma.ExecuteReader();

                    while (read.Read())
                    {
                        return new Customer(Convert.ToInt32(read["Customer_id"]), Convert.ToString(read["Firstname"]), Convert.ToString(read["Lastname"]), Convert.ToString(read["Contact"]), Convert.ToString(read["Email"]), Convert.ToString(read["Password"]));
                    }
                    read.Close();
                }
                catch (MySqlException exception)
                {
                    comma.Connection.Close();
                    exception.ToString();
                }
                return null;
            }
        }

        //Update customer information
        public Customer CustomerUpdate(Customer cust, int id)
        {
            string sql = "UPDATE UberEATS_Portal.Customer SET Firstname='" + cust.Firstname + "',Lastname='" + cust.Lastname + "',Contact='" + cust.Contact + "',Email='" + cust.Email + "',Password='" + cust.Password + "' WHERE Customer_Id=" + id + ";";
            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;
                using (MySqlCommand comma = new MySqlCommand(sql, connect))
                {

                    comma.Connection = connect;
                    try
                    {
                        comma.Connection.Open();

                        comma.Parameters.Add(new MySqlParameter("@_Firstname", cust.Firstname));
                        comma.Parameters.Add(new MySqlParameter("@_Lastname", cust.Lastname));
                        comma.Parameters.Add(new MySqlParameter("@_Contact", cust.Contact));
                        comma.Parameters.Add(new MySqlParameter("@_Email", cust.Email));
                        comma.Parameters.Add(new MySqlParameter("@_Password", cust.Password));

                        read = comma.ExecuteReader();
                        while (read.Read())
                        {
                            return cust = new Customer(Convert.ToString(read["Firstname"]),
                                                Convert.ToString(read["Lastname"]),
                                                Convert.ToString(read["Contact"]),
                                                Convert.ToString(read["Email"]),
                                                Convert.ToString(read["Password"])
                                           );
                        }
                        read.Close();

                    }
                    catch (MySqlException exception)
                    {
                        exception.ToString();

                    }
                    finally
                    {
                        comma.Connection.Close();
                    }
                }
                return cust;
            }
        }

        //Get All The Customers
        public Customer[] GetAllCust()
        {
            string sql = "SELECT * FROM UberEATS_Portal.Customer.Customers;";
            List<Customer> clients = new List<Customer>();
            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;

                MySqlCommand comma = new MySqlCommand(sql, connect);
                comma.Connection = connect;
                try
                {
                    comma.Connection.Open();
                    Customer cust = new Customer();
                    read = comma.ExecuteReader();
                    while (read.Read())
                    {
                        cust = new Customer(Convert.ToInt32(read["Customer_Id"]),
                                            Convert.ToString(read["Firstname"]),
                                            Convert.ToString(read["Lastname"]),
                                            Convert.ToString(read["Contact"]),
                                            Convert.ToString(read["Email"]),
                                            Convert.ToString(read["Password"])
                                            );
                        clients.Add(cust);
                    }
                    read.Close();

                    MySqlDataReader reader = comma.ExecuteReader(System.Data.CommandBehavior.SingleRow);
                    reader.Read();
                    reader.Close();
                }
                catch (MySqlException exception)
                {
                    comma.Connection.Close();
                    exception.ToString();
                }
                return clients.ToArray();
            }
        }


        // Get All Restuarant Information
        public Restaurant[] GetAllRest()
        {
            List<Restaurant> restaurants = new List<Restaurant>();

            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;

                string querys = "SELECT * FROM UberEATS_Portal.Restaurant;";
                using (MySqlCommand comma = new MySqlCommand(querys, connect))
                {
                    try
                    {
                        comma.Connection.Open();
                        Restaurant rest = new Restaurant();

                        read = comma.ExecuteReader();
                        while (read.Read())
                        {
                            rest = new Restaurant(Convert.ToInt32(read["Rest_Id"]),
                                                  Convert.ToString(read["Rest_Name"]),
                                                  Convert.ToString(read["Rest_Type"]),
                                                  Convert.ToString(read["Rest_Desc"]),
                                                  Convert.ToString(read["Rest_Address"]),
                                                  Convert.ToString(read["Rest_City"]),
                                                  Convert.ToString(read["Rest_Contact"]),
                                                  (byte[])(read["Image"])
                                                 );
                            restaurants.Add(rest);

                        }
                        read.Close();
                        MySqlDataReader reader = comma.ExecuteReader(System.Data.CommandBehavior.SingleRow);
                        reader.Read();
                        reader.Close();
                    }
                    catch (MySqlException ex)
                    {
                        ex.ToString();
                    }
                    finally
                    {
                        comma.Connection.Close();
                    }
                }
                return restaurants.ToArray();
            }

        }

        //Gettuing all products from the database
        public Product[] GetProduct()
        {
            List<Product> restaurants = new List<Product>();

            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;

                string querys = "SELECT * FROM UberEATS_Portal.Product;";
                using (MySqlCommand comma = new MySqlCommand(querys, connect))
                {
                    try
                    {
                        comma.Connection.Open();
                        Product prod = new Product();

                        read = comma.ExecuteReader();
                        while (read.Read())
                        {
                            prod = new Product(Convert.ToInt32(read["idProduct"]),
                                                Convert.ToString(read["ProductName"]),
                                                Convert.ToString(read["ProductPrice"]),
                                                (byte[])(read["Image"])

                                               );
                            restaurants.Add(prod);

                        }
                        read.Close();
                        MySqlDataReader reader = comma.ExecuteReader(System.Data.CommandBehavior.SingleRow);
                        reader.Read();
                        reader.Close();
                    }
                    catch (MySqlException ex)
                    {
                        ex.ToString();
                    }
                    finally
                    {
                        comma.Connection.Close();
                    }
                }
                return restaurants.ToArray();
            }

        }

        //Place an order and make payment
        public string Orders(Order ord)
        {
            string x = "";
            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;
                string query = "INSERT INTO UberEATS_Portal.Order(OrderAmount,OrderQuantity,OrderAddress,CardName,CardNumber,CardCCV,Customer_Id) " +
                    "VALUES('" + ord.OrderAmount + "','" + ord.OrderQuantity + "','" + ord.OrderAddress + "','" + ord.CardName + "','" + ord.CardNumber + "','" + ord.CardCCV + "','" + ord.Customer_Id + "');";
                using (MySqlCommand comma = new MySqlCommand(query, connect))
                {
                    try
                    {
                        comma.Connection.Open();

                        comma.Parameters.AddWithValue("@OrderAmount", ord.OrderAmount);
                        comma.Parameters.AddWithValue("@OrderQuantity", ord.OrderQuantity);
                        comma.Parameters.AddWithValue("@OrderAddress", ord.OrderAddress);
                        comma.Parameters.AddWithValue("@CardName", ord.CardName);
                        comma.Parameters.AddWithValue("@CardNumber", ord.CardNumber);
                        comma.Parameters.AddWithValue("@CardCCV", ord.CardCCV);
                        comma.Parameters.AddWithValue("@Customer_Id", ord.Customer_Id);
                        int y = comma.ExecuteNonQuery();

                        x = y.ToString();

                    }
                    catch (MySqlException ex)
                    {
                        ex.ToString();
                        comma.Connection.Close();
                    }
                }
                return x;
            }
        }
    }
}
