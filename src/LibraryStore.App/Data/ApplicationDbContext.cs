﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LibraryStore.App.ViewModels;

namespace LibraryStore.App.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //public DbSet<LibraryStore.App.ViewModels.ProductViewModel> ProductViewModel { get; set; }
        //public DbSet<LibraryStore.App.ViewModels.AddressViewModel> AddressViewModel { get; set; }
    }
}