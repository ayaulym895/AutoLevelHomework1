using System;
using System.Data;
using System.Data.SqlClient;

namespace ShopDB
{
    class Program
    {
        private static DataTable Orders;
        private static DataTable Customers;
        private static DataTable Employees;
        private static DataTable OrderDetails;
        private static DataTable Products;

        private string connectionString;
        //private readonly DbProviderFactory providerFactory;
        static void Main(string[] args)
        {
            var dataSet = new DataSet("shopDb");
            CreateOrderTable();
            CreateCustomerTable();
            CreateEmployeeTable();
            CreateOrderDetailTable();
            CreateProductTable();

            dataSet.Tables.AddRange(new DataTable[]
            {
               Orders,
               Customers,
               Employees,
               OrderDetails,
               Products
            });



            dataSet.Relations.Add(
                 Customers.Columns["customerid"],
                 Orders.Columns["customerid"]);
            dataSet.Relations.Add(
                 Employees.Columns["empid"],
                 Orders.Columns["empid"]);

            dataSet.Relations.Add(
                 Orders.Columns["orderid"],
                 OrderDetails.Columns["orderid"]);
            dataSet.Relations.Add(
                Products.Columns["productid"],
                OrderDetails.Columns["productid"]);

            FillOrders();
            FillCustomers();
            FillEmployees();
            FillOrderDetails();
            FillProducts();

            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            var connection = new SqlConnection("connectionString?");
            var selectCommand = new SqlCommand("select * from Таблица", connection);

            dataAdapter.SelectCommand = selectCommand;
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

            dataAdapter.Fill(dataSet); // получает данные из БД
            dataSet.AcceptChanges();
            dataAdapter.Update(dataSet); // обновляет БД исходя из локального DataSet
        }


        private static void FillProducts()
        {
            var idRow = Products.NewRow();
            idRow.ItemArray = new object[] { 1, "telephones", 230000};
            Products.Rows.Add(idRow);

            Products.Rows.Add(2, "computers", 340000);
        }

        private static void FillOrderDetails()
        {
            var idRow = OrderDetails.NewRow();
            idRow.ItemArray = new object[] { 1, 50000 };
            OrderDetails.Rows.Add(idRow);
        }

        private static void FillEmployees()
        {
            Employees.Rows.Add(1, "Denis", "Кунаева 13", "87012345678");
        }

        private static void FillCustomers()
        {
            Customers.Rows.Add(1, "Bolashak", "543210", "Сарыарка 56");
        }

        private static void FillOrders()
        {
            Orders.Rows.Add(1, "14-08-19", "phone", "Туркистан 3");
        }
        private static void CreateOrderTable()
        {
            Orders = new DataTable("order");
            Orders.Columns.Add(new DataColumn
            {
                ColumnName = "orderid",
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                AllowDBNull = false,
                Unique = true,
                DataType = typeof(int),
            });
            Orders.PrimaryKey = new DataColumn[]
            {
                Orders.Columns["orderid"]
            };
            Orders.Columns.Add(new DataColumn
            {
                ColumnName = "requireddate",
                AllowDBNull = false,
                Unique = true,
                DataType = typeof(string),
            });
            Orders.Columns.Add(new DataColumn
            {
                ColumnName = "shipname",
                AllowDBNull = false,
                Unique = true,
                DataType = typeof(string),
            });
            Orders.Columns.Add(new DataColumn
            {
                ColumnName = "shipadsress",
                AllowDBNull = false,
                Unique = true,
                DataType = typeof(string),
            });
        }

        private static void CreateProductTable()
        {
            Products = new DataTable("products");
            Products.Columns.Add(new DataColumn
            {
                ColumnName = "productid",
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                AllowDBNull = false,
                Unique = true,
                DataType = typeof(int),
            });
            Products.PrimaryKey = new DataColumn[]
            {
                Products.Columns["productid"]
            };
            Products.Columns.Add(new DataColumn
            {
                ColumnName = "productname",
                AllowDBNull = false,
                Unique = false,
                DataType = typeof(string),
            });
            Products.Columns.Add(new DataColumn
            {
                ColumnName = "unitpriceofproduct",
                AllowDBNull = true,
                Unique = false,
                DataType = typeof(long),
            });
        }

        private static void CreateOrderDetailTable()
        {
            OrderDetails = new DataTable("orderdetails");
            OrderDetails.Columns.Add(new DataColumn
            {
                ColumnName = "orderid",
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                AllowDBNull = false,
                Unique = true,
                DataType = typeof(int),
            });
            OrderDetails.Columns.Add(new DataColumn
            {
                ColumnName = "detailid",
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                AllowDBNull = false,
                Unique = true,
                DataType = typeof(int),
            });
            OrderDetails.PrimaryKey = new DataColumn[]
            {
                OrderDetails.Columns["orderid"]
            };
            OrderDetails.PrimaryKey = new DataColumn[]
            {
                OrderDetails.Columns["detailid"]
            };
            OrderDetails.Columns.Add(new DataColumn
            {
                ColumnName = "unitpriceoforderdetails",
                AllowDBNull = true,
                Unique = false,
                DataType = typeof(long),
            });
        }
        private static void CreateEmployeeTable()
        {
            Employees = new DataTable("employees");
            Employees.Columns.Add(new DataColumn
            {
                ColumnName = "empid",
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                AllowDBNull = false,
                Unique = true,
                DataType = typeof(int),
            });
            Employees.PrimaryKey = new DataColumn[]
            {
                Employees.Columns["empid"]
            };
            Employees.Columns.Add(new DataColumn
            {
                ColumnName = "fullname",
                AllowDBNull = false,
                Unique = false,
                DataType = typeof(string),
            });
            Employees.Columns.Add(new DataColumn
            {
                ColumnName = "address",
                AllowDBNull = true,
                Unique = false,
                DataType = typeof(string),
            });
            Employees.Columns.Add(new DataColumn
            {
                ColumnName = "phone",
                AllowDBNull = true,
                Unique = false,
                DataType = typeof(string),
            });
        }

        private static void CreateCustomerTable()
        {
            Customers = new DataTable("customers");
            Customers.Columns.Add(new DataColumn
            {
                ColumnName = "customerid",
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                AllowDBNull = false,
                Unique = true,
                DataType = typeof(int),
            });
            Customers.PrimaryKey = new DataColumn[]
            {
                Customers.Columns["customerid"]
            };
            Customers.Columns.Add(new DataColumn
            {
                ColumnName = "companyname",
                AllowDBNull = false,
                Unique = false,
                DataType = typeof(string),
            });
            Customers.Columns.Add(new DataColumn
            {
                ColumnName = "contactname",
                AllowDBNull = true,
                Unique = false,
                DataType = typeof(string),
            });
            Customers.Columns.Add(new DataColumn
            {
                ColumnName = "address",
                AllowDBNull = true,
                Unique = false,
                DataType = typeof(string),
            });    
            }
        }
    }
    
