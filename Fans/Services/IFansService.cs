using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fans.Services
{
    public interface IFansService
    {
        void CreateNew(Guid id);
        void AddNewFans(Guid parentId, Guid fansId);
        void UpdateParent(Guid id, Guid? parentId);
        List<Guid> Get6LevelParents(Guid id);
    }
}
