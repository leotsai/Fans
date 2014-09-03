using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fans.Context;
using Fans.Services;

namespace Fans.TestConsole
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("started");
            var service = new FansService();
            //var id1 = Guid.NewGuid();
            //var id2 = Guid.NewGuid();
            //var id3 = Guid.NewGuid();

            //var id1_1 = Guid.NewGuid();
            //var id1_2 = Guid.NewGuid();
            //var id1_3 = Guid.NewGuid();

            //service.CreateNew(id1);
            //service.CreateNew(id2);
            //service.CreateNew(id3);

            //service.AddNewFans(id1, id1_1);
            //service.AddNewFans(id1, id1_2);
            //service.AddNewFans(id1, id1_3);

            //service.AddNewFans(id1_2, Guid.NewGuid());
            //service.AddNewFans(id1_2, Guid.NewGuid());
            //service.AddNewFans(id1_2, Guid.NewGuid());

            //service.AddNewFans(id1_3, Guid.NewGuid());
            //service.AddNewFans(id1_3, Guid.NewGuid());
            //service.AddNewFans(id1_3, Guid.NewGuid());

            service.UpdateParent(Guid.Parse("8FF0065A-81D4-46A9-ADB7-5B060E905742"), null);

            Console.WriteLine("done");
            Console.Read();
        }
    }

    class User
    {
        public int Id1 { get; set; }
        public int Id2 { get; set; }
        public int Id3 { get; set; }

        public User(int id1, int id2, int id3)
        {
            this.Id1 = id1;
            this.Id2 = id2;
            this.Id3 = id3;
        }
    }
}
