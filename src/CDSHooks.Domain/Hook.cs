﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CDSHooks.Domain
{
    public class Hook
    {
        [Required]
        public string Id { get; set; }
        public string Workflow { get; set; }
        [Required]
        public IList<HookContext> Context { get; set; }
    }
}
