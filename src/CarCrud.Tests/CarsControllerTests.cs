using System;
using System.Linq;
using System.Threading.Tasks;
using CarCrud.Controllers;
using CarCrud.Models;
using CarCrud.Repositories;

using FluentAssertions;

using Xunit;

namespace CarCrud.Tests
{
    public class CarsControllerTests
    {
        private readonly ICarRepository _carRepository = new FakeRepository();

        [Fact]
        public async void Create_CreateSomeCar_NotExistException()
        {
            //Arrange
            var controller = new CarsController(_carRepository);
            var exceptedName = "car1";
            var exceptedDescription = "desc1";
            var carDto = new CreateCarDto { Name = exceptedName, Description = exceptedDescription };

            //Act
            await controller.Create(carDto);

            //Assert
            var allCars = await _carRepository.Get();
            allCars.Count().Should().Be(1);
            allCars.First().Id.Should().Be(1);
            allCars.First().Name.Should().Be(exceptedName);
            allCars.First().Description.Should().Be(exceptedDescription);
        }

        [Fact]
        public async void Update_UpdateSomeCar_NotExistException()
        {
            //Arrange
            var controller = new CarsController(_carRepository);
            await CreateCar("car1", "desc1");

            var exceptedChangeName = "car1";
            var exceptedChangeDescription = "desc1";
            var updateCarDto = new UpdateCarDto() {Id = 1, Name = exceptedChangeName, Description = exceptedChangeDescription };

            //Act
            await controller.Update(updateCarDto);

            //Assert
            var allCars = await _carRepository.Get();
            allCars.Count().Should().Be(1);
            allCars.First().Id.Should().Be(1);
            allCars.First().Name.Should().Be(exceptedChangeName);
            allCars.First().Description.Should().Be(exceptedChangeDescription);
        }

        [Fact]
        public async void Update_TryToUpdateNotExistCar_ExistException()
        {
            //Arrange
            var controller = new CarsController(_carRepository);
            var updateCarDto = new UpdateCarDto() { Id = default(int), Name = "car1", Description = "desc1" };

            //Act/Assert
            await controller.Invoking(t => t.Update(updateCarDto)).Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async void Get_GetCarWithNoEmptyData_GetSomeCars()
        {
            //Arrange
            var controller = new CarsController(_carRepository);
            var exceptedName = "car1";
            var exceptedDescription = "desc1";
            await CreateCar(exceptedName, exceptedDescription);

            var exceptedName2 = "car2";
            var exceptedDescription2 = "desc2";
            await CreateCar(exceptedName2, exceptedDescription2);

            //Act
            var result = await controller.Get();
            var listOfCars = result.ToList();

            //Assert
            listOfCars.Count.Should().Be(2);
            listOfCars[0].Id.Should().Be(1);
            listOfCars[1].Id.Should().Be(2);

            listOfCars[0].Name.Should().Be(exceptedName);
            listOfCars[1].Name.Should().Be(exceptedName2);

            listOfCars[0].Description.Should().Be(exceptedDescription);
            listOfCars[1].Description.Should().Be(exceptedDescription2);
        }

        [Fact]
        public async void GetById_GetCarWithNoEmptyData_GetTheCar()
        {
            //Arrange
            var controller = new CarsController(_carRepository);
            var exceptedName = "car1";
            var exceptedDescription = "desc1";
            await CreateCar(exceptedName, exceptedDescription);

            var findingId = 1;

            //Act
            var result = await controller.GetById(findingId);

            //Assert
            result.Id.Should().Be(findingId);
            result.Name.Should().Be(exceptedName);
            result.Description.Should().Be(exceptedDescription);
        }

        [Fact]
        public async void Delete_DeleteExistCar_CarHasBeenDeleted()
        {
            //Arrange
            var controller = new CarsController(_carRepository);
            var exceptedName = "car1";
            var exceptedDescription = "desc1";
            await CreateCar(exceptedName, exceptedDescription);

            var exceptedName2 = "car2";
            var exceptedDescription2 = "desc2";
            await CreateCar(exceptedName2, exceptedDescription2);

            //Act
            await controller.Delete(1);

            //Assert
            var listOfCars = await _carRepository.Get();
            listOfCars.Count().Should().Be(1);
        }

        private async Task CreateCar(string name, string description)
        {
            var car = new CreateCarDto {Name = name, Description = description};
            await _carRepository.Add(car);
        }
    }
}
