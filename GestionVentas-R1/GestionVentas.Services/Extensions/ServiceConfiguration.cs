﻿using GestionVentas.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionVentas.Services.Extensions
{
    public static class ServiceConfiguration
    {

        public static void AddCustomServiceConfiguration(this IServiceCollection services) {
            services.AddTransient<IModeloService, ModeloService>();
        }
    }
}