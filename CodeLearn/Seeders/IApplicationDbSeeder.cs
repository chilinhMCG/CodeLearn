using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Seeders
{
    public interface IApplicationDbSeeder
    {
        Task SeedPostManagerData();
    }
}
