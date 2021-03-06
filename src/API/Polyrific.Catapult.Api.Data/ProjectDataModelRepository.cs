﻿// Copyright (c) Polyrific, Inc 2018. All rights reserved.

using Polyrific.Catapult.Api.Core.Entities;
using Polyrific.Catapult.Api.Core.Repositories;

namespace Polyrific.Catapult.Api.Data
{
    public class ProjectDataModelRepository : BaseRepository<ProjectDataModel>, IProjectDataModelRepository
    {
        public ProjectDataModelRepository(CatapultDbContext dbContext) : base(dbContext)
        {
        }

        public ProjectDataModelRepository(CatapultSqliteDbContext dbContext) : base(dbContext)
        {
        }
    }
}
