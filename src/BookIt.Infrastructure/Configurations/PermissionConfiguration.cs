﻿using BookIt.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookIt.Infrastructure.Configurations;

internal sealed class PermissionConfiguration: IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");
        
        builder.HasKey(permission => permission.Id);

        builder.HasData(Permission.UsersRead);
    }
}