using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ADOExample
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var connection = new SqlConnection("Server=(local);Database=Chinook;Trusted_Connection=True;")) // set up connection
            {
                connection.Open(); // opens connection

                var cmd = connection.CreateCommand(); // vehicle to set up our query

                cmd.CommandText = @"select x.invoiceid,BillingAddress
                                from invoice i
                                    join InvoiceLine x on x.InvoiceId = i.InvoiceId
                                where exists (select TrackId from Track where Name like 'a%' and TrackId = x.TrackId)";

                var reader = cmd.ExecuteReader(); // sql data reader, that reads the results from our sql query

                while (reader.Read()) // while there are rows, do this thing
                {
                    var invoiceId = reader.GetInt32(0);
                    var billingAddress = reader["BillingAddress"].ToString();

                    Console.WriteLine($"Invoice {invoiceId} is going to {billingAddress}");
                }
            }




            //var connection = new SqlConnection("Server=(local);Database=Chinook;Trusted_Connection=True;"); // set up connection
            //var cmd = connection.CreateCommand(); // vehicle to set up our query

            //cmd.CommandText = @"select x.invoiceid,BillingAddress
            //                    from invoice i
            //                        join InvoiceLine x on x.InvoiceId = i.InvoiceId
            //                    where exists (select TrackId from Track where Name like 'a%' and TrackId = x.TrackId)";

            //connection.Open(); // opens connection

            //try
            //{
            //    var reader = cmd.ExecuteReader(); // sql data reader, that reads the results from our sql query

            //    while (reader.Read()) // while there are rows, do this thing
            //    {
            //        var invoiceId = reader.GetInt32(0);
            //        var billingAddress = reader["BillingAddress"].ToString();

            //        Console.WriteLine($"Invoice {invoiceId} is going to {billingAddress}");
            //    }
            //}
            //catch (Exception ex)
            //{

            //    Console.WriteLine(ex.Message);
            //}
            //finally
            //{
            //    connection.Dispose();
            //}

            
            Console.ReadLine();

        }
    }
}
