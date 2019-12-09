using System.Collections.Generic;
using System.Threading.Tasks;
using CarCrud.Models;
using CarCrud.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CarCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarRepository _carRepository;

        public CarsController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Car>> Get()
        {
            return await _carRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<Car> GetById(int id)
        {
            return await _carRepository.Get(id);
        }

        [HttpPost]
        public async Task Create([FromBody] CreateCarDto carDto)
        {
            await _carRepository.Add(carDto);
        }

        [HttpPut]
        public async Task Update([FromBody] UpdateCarDto carDto)
        {
            await _carRepository.Update(carDto);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _carRepository.Delete(id);
        }
    }
}
