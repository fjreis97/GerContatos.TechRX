﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities;

public class Regiao : EntityBase
{
    public string Name { get; set; }
    public ICollection<DDD> DDDs{ get; set; }
}
