using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }

        public virtual int CustomerId { get; set; }//foreign key
        public virtual Customer customer { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
