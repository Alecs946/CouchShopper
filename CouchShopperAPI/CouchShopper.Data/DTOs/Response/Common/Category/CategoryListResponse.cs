﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Common.Category
{
    public class CategoryListResponse
    {
        public int TotalEntities { get; set; }

        public int Offset { get; set; }

        public List<CategoryResponse> Categories { get; set; }
    }
}
