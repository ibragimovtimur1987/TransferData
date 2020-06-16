using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransferData.BLL.Infrastructure
{
    /// <summary>
    /// Реализация маппинга на базе AutoMapping
    /// </summary>
    public class AutoMapperAdapter : IAutoMapper
    {
        /// <summary>
        /// Преобразовать объект objectToMap в тип T
        /// </summary>
        /// <typeparam name="T">Тип, к которому нужно привести объект</typeparam>
        /// <param name="objectToMap">Объект-источник</param>
        /// <returns>ОБъект типа Т, наполненный данными из objectToMap</returns>
        public T Map<T>(object objectToMap)
        {
            return Mapper.Map<T>(objectToMap);
        }
    }
}
