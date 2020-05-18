﻿using System;
using System.Collections.Generic;

namespace APBD_Tutorial10.Entities
{
    public partial class TeamMember
    {
        public TeamMember()
        {
            TaskIdAssignedToNavigation = new HashSet<Task>();
            TaskIdCreatorNavigation = new HashSet<Task>();
        }

        public int IdTeamMember { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Task> TaskIdAssignedToNavigation { get; set; }
        public virtual ICollection<Task> TaskIdCreatorNavigation { get; set; }
    }
}
