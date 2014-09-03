using System;
using System.Collections.Generic;
using System.Linq;
using Fans.Context;
using Fans.Entities;

namespace Fans.Services
{
    public class FansService : IFansService
    {
        public void CreateNew(Guid id)
        {
            using (var db = new FansDbContext())
            {
                var fans = new FansUser();
                fans.Id = id;
                db.FansUsers.Add(fans);
                db.SaveChanges();
            }
        }

        public void AddNewFans(Guid parentId, Guid fansId)
        {
            using (var db = new FansDbContext())
            {
                var parent = db.FansUsers.FirstOrDefault(x => x.Id == parentId);
                if (parent == null)
                {
                    throw new Exception("The current user doesn't exist.");
                }
                var fans = new FansUser();
                fans.Id = fansId;
                fans.UpdateFromParent(parent);
                db.FansUsers.Add(fans);
                db.SaveChanges();
            }
        }

        public void UpdateParent(Guid id, Guid? parentId)
        {
            using (var db = new FansDbContext())
            {
                var fans = db.FansUsers.FirstOrDefault(x => x.Id == id);
                if (fans == null)
                {
                    throw new Exception("The current user doesn't exist.");
                }
                var parent = parentId == null ? null : db.FansUsers.FirstOrDefault(x => x.Id == parentId);
                if (parentId.HasValue && parent == null)
                {
                    throw new Exception("The parent doesn't exist.");
                }
                var where = string.Format("Id = '{1}' or ParentPath like '{0}%'", fans.GetPath(), id);
                if (parent == null)
                {
                    var path = string.Format("SUBSTRING(ParentPath, {0}, 10000)", fans.ParentPath.Length);
                    var level = 1 - fans.Level;
                    var sql = string.Format("update [FansUsers] set ParentPath = {0}, Level = Level {1} where {2}", path, level, where);
                    db.Database.ExecuteSqlCommand(sql);
                }
                else
                {
                    var parentPath = parent.GetPath();
                    var path = string.Format("'{0}' + SUBSTRING(ParentPath, {1}, 10000)", parentPath, fans.ParentPath.Length);
                    var level = parent.Level - fans.Level;
                    var sql = string.Format("update [FansUsers] set ParentPath={0}, Level=Level {1},[LastUpdatedTime]=GETDATE() where {2}", path, level, where);
                    db.Database.ExecuteSqlCommand(sql);
                }
                var parentIdSql = string.Format("update [FansUsers] set [ParentId]={0},[LastUpdatedTime]=GETDATE() where Id='{1}'", parentId == null ? "NULL" : "'" + parentId + "'", id);
                db.Database.ExecuteSqlCommand(parentIdSql);
                db.Database.ExecuteSqlCommand("update [FansUsers] set [ParentPath]=NULL,[LastUpdatedTime]=GETDATE() where [ParentPath]=','");
            }
        }

        public List<Guid> Get6LevelParents(Guid id)
        {
            using (var db = new FansDbContext())
            {
                var current = db.FansUsers.FirstOrDefault(x => x.Id == id);
                if (current == null)
                {
                    throw new Exception("The current user doesn't exist.");
                }
                return current.Get6ParentIds();
            }
        }
    }
}
