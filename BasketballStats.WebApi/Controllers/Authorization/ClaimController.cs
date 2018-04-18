﻿using AutoMapper;
using BasketballStats.WebApi.ApplicationSettings;
using CustomFramework.WebApiUtils.Authorization.Business.Contracts;
using CustomFramework.WebApiUtils.Authorization.Controllers;
using CustomFramework.WebApiUtils.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BasketballStats.WebApi.Controllers.Authorization
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route(ApiConstants.AdminRoute + "claim")]
    public class ClaimController : BaseClaimController
    {
        public ClaimController(IClaimManager claimManager, ILocalizationService localizationService, ILogger<ClaimController> logger, IMapper mapper)
        : base(claimManager, localizationService, logger, mapper)
        {

        }
    }
}
