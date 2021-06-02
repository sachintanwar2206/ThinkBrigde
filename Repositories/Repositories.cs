using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Repositories;
using ThinkBridge.Models;

namespace ThinkBridge.Repository
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(Context context) : base(context) { }
    }
}
