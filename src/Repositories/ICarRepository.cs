using CarCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarCrud.Repositories
{
    public interface ICarRepository : IDisposable
    {
        Task<IEnumerable<Car>> Get();
        Task<Car> Get(int id);
        Task Add(CarDto carDto);
        Task Update(CarDto carDto);
        Task Delete(int id);
    }
}
