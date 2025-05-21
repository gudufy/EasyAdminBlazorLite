﻿using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace EasyAdminBlazor.Components
{
    public class AdminModuleComponentBase : BootstrapModuleComponentBase
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnLoadJSModule()
        {
            base.OnLoadJSModule();

            if (!string.IsNullOrEmpty(ModulePath))
            {
                ModulePath = $"/_content/EasyAdminBlazor/{ModulePath}";
            }
        }
    }
}