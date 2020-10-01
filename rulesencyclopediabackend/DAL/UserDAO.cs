using MySql.Data.MySqlClient;
using rulesencyclopedia;
using rulesencyclopediabackend.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace rulesencyclopediabackend
{
    public class UserDAO
    {
        List<UserDTO> userList = new List<UserDTO>();
        Connection conn = new Connection();
        public UserDAO()
        {
        }

        public List<rulesencyclopedia.UserDTO> getUserList()
        {
            return userList;
        }

        public UserDTO getUser(int id)
        {
            String sqlStatement = "Select JSON_OBJECT * FROM user WHERE ID="+id;
            UserDTO user = conn.executeSqlStatement(sqlStatement);
            return user;
        }
    }
}