using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FriendList.Models;

    public class FriendListContext : DbContext
    {
        public FriendListContext (DbContextOptions<FriendListContext> options)
            : base(options)
        {
        }

        public DbSet<FriendList.Models.Friend> Friend { get; set; } = default!;
    }
