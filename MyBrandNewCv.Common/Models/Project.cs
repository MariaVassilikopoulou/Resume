﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MyBrandNewCv.Common.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string UserId { get; set; }
    }
}