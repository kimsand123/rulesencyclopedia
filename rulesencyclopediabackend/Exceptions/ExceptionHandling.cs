using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json;
using System.Web;

namespace rulesencyclopediabackend.Exceptions
{
    public class ExceptionHandling
    {
        public void exceptionHandlerMsSql(SqlException ex, string text)
        {
            Console.WriteLine("send to MsSql: " + text);
            Console.WriteLine("Exception message: " + ex.Message);
        }

        public void exceptionHandlerEntity(EntityException ex, string text)
        {
            Console.WriteLine("Controller problem: " + text);
            Console.WriteLine("Exception message: " + ex.Message);
        }

        public void exceptionHandlerJson(JsonSerializationException ex, string text)
        {
            Console.WriteLine("Json problem: " + text);
            Console.WriteLine("Exception message: " + ex.Message);
        }




    }
}