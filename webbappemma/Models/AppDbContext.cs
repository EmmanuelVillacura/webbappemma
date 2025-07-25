﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class AppDbContext : DbContext


{
    public DbSet<usuario> TblUsuario { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=proyecto001EV;Integrated Security=True;");
        }
    }
}
