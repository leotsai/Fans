using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Fans.Entities
{
    public class FansUser : EntityBase
    {
        /// <summary>
        /// comma seperated guids string, e.g. ,BC6D19DE-198B-4766-AA80-EE432ACC8774,285DFE57-8A05-4BDF-AD57-4AE7183F6258,
        /// </summary>
        [MaxLength(5000)]
        public string ParentPath { get; set; }

        public Guid? ParentId { get; set; }

        /// <summary>
        /// starts from 1
        /// </summary>
        public int Level { get; set; }

        public virtual FansUser Parent { get; set; }

        public FansUser()
        {
            this.Level = 1;
        }

        public void UpdateFromParent(FansUser parent)
        {
            if (this.Id == Guid.Empty)
            {
                throw new Exception("Please set Id first.");
            }
            this.ParentId = parent.Id;
            this.Level = parent.Level + 1;
            this.ParentPath = parent.GetPath();
        }

        public string GetPath()
        {
            if (string.IsNullOrEmpty(ParentPath))
            {
                return "," + this.Id + ",";
            }
            return ParentPath + this.Id + ",";
        }

        public List<Guid> Get6ParentIds()
        {
            if (string.IsNullOrEmpty(this.ParentPath) && this.ParentPath != ",")
            {
                return new List<Guid>();
            }
            var strIds = this.ParentPath.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            var skip = strIds.Length > 6 ? strIds.Length - 6 : 0;
            return strIds.Skip(skip).Take(6).Select(Guid.Parse).ToList();
        }
    }
}
