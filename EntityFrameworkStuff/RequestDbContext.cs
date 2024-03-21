using WebCheckerAPI.DataModels;
using Microsoft.EntityFrameworkCore;
using WebCheckerAPI.Models;

namespace WebCheckerAPI.EntityFrameworkStuff
{
    //This represents Database
    public class RequestDbContext : DbContext
    {
        public RequestDbContext(DbContextOptions options) : base(options)
        {  //the Actual point out place for the Conection String is Program.cs file
           //thats where we configure WebAPI and pass the parameter as options here

        }
        //this represent table inside of database
        public DbSet<RequestModel> Request { get; set; } = null!;
        public DbSet<RequestSettings> Commands { get; set; } = null!;
        public DbSet<RequestStatus> RequestResults { get; set; } = null!;
        public DbSet<CommandObjectsModel> CommandObjects { get; set; } = null!;


        public DbSet<WebCheckerAPI.Models.Endpoint> Endpoints { get; set; } = null!;
        public DbSet<EndpointResult> EndpointResults { get; set; } = null!;



    }
}