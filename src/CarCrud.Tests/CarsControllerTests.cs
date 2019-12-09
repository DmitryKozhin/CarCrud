using System;
using System.Linq;

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
        public async void CreateOrUpdate_CreateSomeCar_NotExistException()
        {
            //Arrange
            var controller = new CarsController(_carRepository);
            var exceptedName = "car1";
            var exceptedDescription = "desc1";
            var carDto = new CarDto { Name = exceptedName, Description = exceptedDescription };

            //Act
            await controller.CreateOrUpdate(carDto);

            //Assert
            var allCars = await _carRepository.Get();
            allCars.Count().Should().Be(1);
            allCars.First().Id.Should().Be(1);
            allCars.First().Name.Should().Be(exceptedName);
            allCars.First().Description.Should().Be(exceptedDescription);
        }

        [Fact]
        public async void CreateOrUpdate_UpdateSomeCar_NotExistException()
        {
            //Arrange
            var controller = new CarsController(_carRepository);
            var carDto = new CarDto { Name = "car1", Description = "desc1" };
            await _carRepository.Add(carDto);

            var exceptedChangeName = "car1";
            var exceptedChangeDescription = "desc1";
            var updateCarDto = new CarDto() {Id = 1, Name = exceptedChangeName, Description = exceptedChangeDescription };

            //Act
            await controller.CreateOrUpdate(updateCarDto);

            //Assert
            var allCars = await _carRepository.Get();
            allCars.Count().Should().Be(1);
            allCars.First().Id.Should().Be(1);
            allCars.First().Name.Should().Be(exceptedChangeName);
            allCars.First().Description.Should().Be(exceptedChangeDescription);
        }

        [Fact]
        public async void CreateOrUpdate_TryToUpdateNotExistCar_ExistException()
        {
            //Arrange
            var controller = new CarsController(_carRepository);
            var updateCarDto = new CarDto() { Id = default(int), Name = "car1", Description = "desc1" };

            //Act/Assert
            await controller.Invoking(t => t.CreateOrUpdate(updateCarDto)).Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async void Get_GetCarWithNoEmptyData_GetSomeCars()
        {
            //Arrange
            var controller = new CarsController(_carRepository);
            var exceptedName = "car1";
            var exceptedDescription = "desc1";
            await _carRepository.Add(new CarDto { Name = exceptedName, Description = exceptedDescription });

            var exceptedName2 = "car2";
            var exceptedDescription2 = "desc2";
            await _carRepository.Add(new CarDto { Name = exceptedName2, Description = exceptedDescription2 });

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
            await _carRepository.Add(new CarDto { Name = exceptedName, Description = exceptedDescription });

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
            await _carRepository.Add(new CarDto { Name = exceptedName, Description = exceptedDescription });

            var exceptedName2 = "car2";
            var exceptedDescription2 = "desc2";
            await _carRepository.Add(new CarDto { Name = exceptedName2, Description = exceptedDescription2 });

            //Act
            await controller.Delete(1);

            //Assert
            var listOfCars = await _carRepository.Get();
            listOfCars.Count().Should().Be(1);
        }
    }
}
