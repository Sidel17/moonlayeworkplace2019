﻿using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Data.Entities
{
    public class Education :Entity, ISoftDelete
    {
        public string LastEducation { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? Deleted { get; set; }
        public string DeleteBy { get; set; }
    }

}
