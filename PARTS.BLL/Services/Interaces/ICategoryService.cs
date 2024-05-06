﻿using PARTS.BLL.DTOs.Requests;
using PARTS.BLL.DTOs.Responses;
using PARTS.DAL.Entities.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARTS.BLL.Services.Interaces
{
    public interface ICategoryService : IGenericService<Category, CategoryRequest, CategoryResponse>
    {
    }
}