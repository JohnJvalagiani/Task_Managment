using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration.Abstraction
{
    public class TaskEntityConfiguration: EntityConfiguration<task>
    {

        public override void Map(EntityTypeBuilder<task> builder)
        {
            builder.ToTable("Tasks");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Title).HasMaxLength(50).IsRequired();
            builder.Property(u => u.AssignedUser).IsRequired();


        }


    }
}
