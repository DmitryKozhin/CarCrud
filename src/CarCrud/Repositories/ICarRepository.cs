using CarCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarCrud.Repositories
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> Get();
        Task<Car> Get(int id);
        Task Add(CreateCarDto carDto);
        Task Update(UpdateCarDto carDto);
        Task Delete(int id);
    }
}
