using System;
using System.Reflection;
using System.Collections.Generic;

namespace rulesencyclopediabackend.Tools
{
    public class ConvertToDTO
    {
        //Takes all types of objects recieved from the MSSQL EntityFramwork call and transfers them into a passed in DTO object
        //finds the properties of the DTO object and compares them with the properties of the return object.
        //if the name of the property exists in both, the value from the return object is transfered
        //to the DTO object. There can be an unequal number of properties in the two objects. only the ones with
        //the exact same names will transfer.

        //When used a cast is necessary ex. UserDTO user = (UserDTO)DTOConverter.Converter(user, dbUser);
        public Object Converter(Object objDTO, Object objDB)
        {
            //Get the property info of the DTO object
            List<PropertyInfo> objDTOProperties = fillObjectPropertiesList(objDTO);
            //Get the property infor of the DB object
            List<PropertyInfo> obj2Properties = fillObjectPropertiesList(objDB);

            //for each name property in the DTO object, compare it to the name property of the DB object,
            //and if alike transfer the value.
            for (int i = 0; i < objDTOProperties.Count; i++)
            {
                for (int j = 0; j < obj2Properties.Count; j++)
                {
                    if (objDTOProperties[i].Name == obj2Properties[j].Name)
                    {
                        objDTOProperties[i].SetValue(objDTO, obj2Properties[j].GetValue(objDB));
                        break;
                    }
                }
            }
            return objDTO; 
        }

        private List<PropertyInfo> fillObjectPropertiesList (Object obj)
        {
            //Create a list of properties for an object
            List<PropertyInfo> propertyInfo = new List<PropertyInfo>();
            foreach(PropertyInfo property in obj.GetType().GetProperties())
            {
                propertyInfo.Add(property);
            }
            return propertyInfo;
        }
    }
}