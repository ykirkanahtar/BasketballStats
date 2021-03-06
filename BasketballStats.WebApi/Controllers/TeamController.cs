﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BasketballStats.Contracts.Requests;
using BasketballStats.Contracts.Responses;
using BasketballStats.WebApi.ApplicationSettings;
using BasketballStats.WebApi.Business;
using BasketballStats.WebApi.Enums;
using BasketballStats.WebApi.Models;
using CustomFramework.Authorization.Attributes;
using CustomFramework.Authorization.Enums;
using CustomFramework.WebApiUtils.Authorization.Controllers;
using CustomFramework.WebApiUtils.Contracts;
using CustomFramework.WebApiUtils.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BasketballStats.WebApi.Controllers
{
    [Route(ApiConstants.DefaultRoute + "team")]
    public class TeamController : BaseControllerWithCrudAuthorization<Team, TeamRequest, TeamRequest, TeamResponse, ITeamManager, int>
    {
        public TeamController(ILocalizationService localizationService, ILogger<Controller> logger, IMapper mapper, ITeamManager manager)
            : base(localizationService, logger, mapper, manager)
        {

        }

        [Route("create")]
        [HttpPost]
        [Permission(nameof(WebApiEntities.Team), Crud.Create)]
        public async Task<IActionResult> Create([FromBody] TeamRequest request)
        {
            return await BaseCreateAsync(request);
        }

        [Route("{id:int}/update")]
        [HttpPut]
        [Permission(nameof(WebApiEntities.Team), Crud.Update)]
        public Task<IActionResult> UpdateName(int id, [FromBody] TeamRequest request)
        {
            return CommonOperationAsync<IActionResult>(async () =>
            {
                var result = await Manager.UpdateAsync(id, request);
                return Ok(new ApiResponse(LocalizationService, Logger).Ok(Mapper.Map<Team, TeamResponse>(result)));
            });
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        [Permission(nameof(WebApiEntities.Team), Crud.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            return await BaseDeleteAsync(id);
        }

        [Route("get/id/{id:int}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            return await BaseGetByIdAsync(id);
        }

        [Route("getall")]
        [HttpGet]
        [AllowAnonymous]
        public Task<IActionResult> GetAll(int skip, int take)
        {
            return CommonOperationAsync<IActionResult>(async () =>
            {
                var result = await Manager.GetAllAsync();

                return Ok(new ApiResponse(LocalizationService, Logger).Ok(
                    Mapper.Map<IEnumerable<Team>, IEnumerable<TeamResponse>>(result.ResultList), result.Count));
            });
        }
    }
}