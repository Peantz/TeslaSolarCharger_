﻿using TeslaSolarCharger.Shared.Dtos.RestValueConfiguration;

namespace TeslaSolarCharger.Services.Services.Contracts;

public interface IRestValueConfigurationService
{
    Task<List<DtoRestValueConfiguration>> GetAllRestValueConfigurations();
    Task<List<DtoRestValueConfigurationHeader>> GetHeadersByConfigurationId(int parentId);
    Task<int> SaveHeader(int parentId, DtoRestValueConfigurationHeader dtoData);
    Task<int> SaveRestValueConfiguration(DtoRestValueConfiguration dtoData);
    Task<List<DtoRestValueResultConfiguration>> GetResultConfigurationsByConfigurationId(int parentId);
    Task<int> SaveResultConfiguration(int parentId, DtoRestValueResultConfiguration dtoData);
}
