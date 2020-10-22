using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace rulesencyclopediabackend.Tools
{
    public class ConvertToDTO
    {
        public Object Converter(Object objDTO, Object obj2)
        {
            List<PropertyInfo> objDTOProperties = fillObjectPropertiesList(objDTO);
            List<PropertyInfo> obj2Properties = fillObjectPropertiesList(obj2);

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