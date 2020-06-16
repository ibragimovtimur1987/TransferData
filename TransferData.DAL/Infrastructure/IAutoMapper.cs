using System;
using System.Collections.Generic;
using System.Text;

namespace TransferData.DAL.Infrastructure
{
    public interface IAutoMapper
    {
        /// <summary>
        /// Преобразовать объект objectToMap в тип T
        /// </summary>
        /// <typeparam name="T">Тип, к которому нужно привести объект</typeparam>
        /// <param name="objectToMap">Объект-источник</param>
        /// <returns>ОБъект типа Т, наполненный данными из objectToMap</returns>
        T Map<T>(object objectToMap);
    }
}
