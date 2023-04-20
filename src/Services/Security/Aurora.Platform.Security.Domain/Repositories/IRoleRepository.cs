﻿using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;

namespace Aurora.Platform.Security.Domain.Repositories
{
    public interface IRoleRepository : IReadableRepository<Role>, IWriteableRepository<Role>
    {
    }
}