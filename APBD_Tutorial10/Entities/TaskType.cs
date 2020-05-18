﻿using System;
using System.Collections.Generic;

namespace APBD_Tutorial10.Entities
{
    public partial class TaskType
    {
        public TaskType()
        {
            Task = new HashSet<Task>();
        }

        public int IdTaskType { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Task> Task { get; set; }
    }
}
