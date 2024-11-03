using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Project.Models;

namespace Project.Data.EF
{
    public class UserDbContext : IdentityDbContext<User>
    {

        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options) { }

    }
}
