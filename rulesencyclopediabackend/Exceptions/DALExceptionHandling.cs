﻿using Newtonsoft.Json;
using System;
using System.Data.Entity.Core;
using System.Data.SqlClient;

namespace rulesencyclopediabackend.Exceptions
{
    public class DALExceptionHandling
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

        public void exceptionHandlerInvalidOperation(System.InvalidOperationException ex, string text)
        {
            Console.WriteLine("No record found: " + text);
            Console.WriteLine("Exception message: " + ex.Message);
        }




    }
}