using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIT.Models;

namespace SIT.Web.Services.Interfaces
{
    public interface ITransitionSchemeService
    {
        TransitionScheme AddDefaultTransitionScheme();
    }
}
