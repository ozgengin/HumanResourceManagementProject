using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class PasswordCode : BaseEntity
    {
        public string EmployeeId { get; set; }

        [StringLength(6)]
        public string Code { get; set; }
    }
}
