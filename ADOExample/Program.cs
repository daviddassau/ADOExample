using System;
using System.Data;
using System.Data.SqlClient;

namespace ADOExample
{
    class Program
    {
        static void Main(string[] args)
        {

            var firstLetter = Console.ReadLine();

            using (var connection = new SqlConnection("Server=(local);Database=Chinook;Trusted_Connection=True;")) // set up connection
            {
                connection.Open(); // opens connection
                var cmd = connection.CreateCommand(); // vehicle to set up our query
                cmd.CommandText = @"select x.invoiceid,BillingAddress
                                from invoice i
                                    join InvoiceLine x 
                                        on x.InvoiceId = i.InvoiceId
                                where exists (select TrackId from Track 
                                              where Name like @FirstLetter + '%' and TrackId = x.TrackId)";

                var firstLetterParam = new SqlParameter("@FirstLetter", SqlDbType.NVarChar);
                firstLetterParam.Value = firstLetter;
                cmd.Parameters.Add(firstLetterParam);

                var reader = cmd.ExecuteReader(); // sql data reader, that reads the results from our sql query

                while (reader.Read()) // while there are rows, do this thing
                {
                    var invoiceId = reader.GetInt32(0);
                    var billingAddress = reader["BillingAddress"].ToString();

                    Console.WriteLine($"Invoice {invoiceId} is going to {billingAddress}");
                }
            }

            Console.ReadLine();

        }
    }
}
