﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Roles
    {
        public int RolesID { get; set; }

        public string RolesName { get; set; }

        public string RolesInstruction { get; set; }

        public int RolesIf { get; set; }

        public List<Tree> children { get; set; }
    }
}