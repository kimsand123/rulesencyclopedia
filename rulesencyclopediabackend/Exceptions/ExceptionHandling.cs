﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rulesencyclopediabackend.Exceptions
{
    public class ExceptionHandling
    {
        public void exceptionHandlerMySql(MySqlException ex, string text)
        {
            Console.WriteLine("send to MySql: " + text);
            Console.WriteLine("Exception message: " + ex.Message);
        }




    }
}