﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Customer
{
    public class CustomerUpdateDto : CustomerBaseDto
    {
        public Guid CustomerId { get; set; }
    }
}
