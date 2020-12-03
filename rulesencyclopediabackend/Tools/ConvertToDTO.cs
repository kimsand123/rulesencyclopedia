using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace rulesencyclopediabackend.Tools
{
    public class ConvertToDTO
    {
    //Takes all objects recieved from the MSSQL EntityFramwork call and transfers them into DTO object
    //finds the properties of the DTO object and compares them with the properties of the return object.
    //if the name of the property exists in both, the value from the return object is transfered
    //to the DTO object. There can be an unequal number of properties in the objects. only the ones with
    //the exact same names will transfer.
        public Object Converter(Object objDTO, Object obj2)
        {
            List<PropertyInfo> objDTOProperties = fillObjectPropertiesList(objDTO);
            List<PropertyInfo> obj2Properties = fillObjectPropertiesList(obj2);
           // Does  not work Type returnType = objDTO.GetType();
            for (int i = 0; i < objDTOProperties.Count; i++)
            {
                for (int j = 0; j < obj2Properties.Count; j++)
                {
                    if (objDTOProperties[i].Name == obj2Properties[j].Name)
                    {
                        objDTOProperties[i].SetValue(objDTO, obj2Properties[j].GetValue(obj2));
                        break;
                    }
                }
            }
            return objDTO;
        }

        private List<PropertyInfo> fillObjectPropertiesList (Object obj)
        {
            List<PropertyInfo> propertyInfo = new List<PropertyInfo>();
            foreach(PropertyInfo property in obj.GetType().GetProperties())
            {
                propertyInfo.Add(property);
            }
            return propertyInfo;
        }
    }
}