using System.Data.Entity.Migrations;
using EpisodeService.Data;
using EpisodeService.Data.Model;
using EpisodeService.Features.Users;

namespace EpisodeService.Migrations
{
    public class RoleConfiguration
    {
        public static void Seed(EpisodeServiceContext context) {

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.SYSTEM
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.ACCOUNT_HOLDER
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.DEVELOPMENT
            });

            context.SaveChanges();
        }
    }
}
