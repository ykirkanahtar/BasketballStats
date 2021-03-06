﻿using AutoMapper;
using BasketballStats.Contracts.Requests;
using BasketballStats.WebApi.Constants;
using BasketballStats.WebApi.Data;
using BasketballStats.WebApi.Models;
using CustomFramework.Data.Contracts;
using CustomFramework.WebApiUtils.Business;
using CustomFramework.WebApiUtils.Enums;
using CustomFramework.WebApiUtils.Utils;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Threading.Tasks;
using CustomFramework.WebApiUtils.Contracts;

namespace BasketballStats.WebApi.Business
{
    public class TeamManager : BaseBusinessManagerWithApiRequest<ApiRequest>, ITeamManager
    {
        private readonly IUnitOfWorkWebApi _uow;

        public TeamManager(IUnitOfWorkWebApi uow, ILogger<TeamManager> logger, IMapper mapper, IApiRequestAccessor apiRequestAccessor)
            : base(logger, mapper, apiRequestAccessor)
        {
            _uow = uow;
        }

        public Task<Team> CreateAsync(TeamRequest request)
        {
            return CommonOperationAsync(async () =>
            {
                var result = Mapper.Map<Team>(request);

                /**************Name is unique*****************/
                /*********************************************/
                var teamNameUniqueResult =
                    await _uow.Teams.GetByNameAsync(result.Name);

                teamNameUniqueResult.CheckUniqueValue(WebApiResourceConstants.Name);
                /**************Name is unique*****************/
                /*********************************************/

                _uow.Teams.Add(result, GetLoggedInUserId());
                await _uow.SaveChangesAsync();
                return result;
            }, new BusinessBaseRequest() { MethodBase = MethodBase.GetCurrentMethod() });
        }

        public Task<Team> UpdateAsync(int id, TeamRequest request)
        {
            return CommonOperationAsync(async () =>
            {
                var result = await GetByIdAsync(id);
                Mapper.Map(request, result);

                /**************Name is unique*****************/
                /*********************************************/
                var teamNameUniqueResult =
                    await _uow.Teams.GetByNameAsync(result.Name);

                teamNameUniqueResult.CheckUniqueValueForUpdate(result.Id, WebApiResourceConstants.Name);
                /**************Name is unique*****************/
                /*********************************************/

                _uow.Teams.Update(result, GetLoggedInUserId());
                await _uow.SaveChangesAsync();
                return result;
            }, new BusinessBaseRequest() { MethodBase = MethodBase.GetCurrentMethod() });
        }

        public Task DeleteAsync(int id)
        {
            return CommonOperationAsync(async () =>
            {
                var result = await GetByIdAsync(id);

                _uow.Teams.Delete(result, GetLoggedInUserId());
                await _uow.SaveChangesAsync();
            }, new BusinessBaseRequest() { MethodBase = MethodBase.GetCurrentMethod() });
        }

        public Task<Team> GetByIdAsync(int id)
        {
            return CommonOperationAsync(async () => await _uow.Teams.GetByIdAsync(id), new BusinessBaseRequest { MethodBase = MethodBase.GetCurrentMethod() },
                BusinessUtilMethod.CheckRecordIsExist, GetType().Name);
        }

        public Task<ICustomList<Team>> GetAllAsync()
        {
            return CommonOperationAsync(async () => await _uow.Teams.GetAllAsync(), new BusinessBaseRequest { MethodBase = MethodBase.GetCurrentMethod() }, BusinessUtilMethod.CheckNothing, GetType().Name);
        }

    }
}
