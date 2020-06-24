using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransferData.BLL.Infrastructure
{
    /// <summary>
    /// Настройка автоматического преобразования типов
    /// </summary>
    public static class AutomapperConfig
    {
        /// <summary>
        /// Выполнить конфигурацию автоматического преобразования типов
        /// </summary>
        public static void Config()
        {
            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(AutomapperConfig)));
            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}
