using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using UnitTest;
using UnitTest.Models;
using Xunit;

namespace XUnitTest
{
    public class PersonTest
    {

        private readonly string _name = "Jorge";
        private readonly int _age = 18;



        //[Fact]
        //public void Person_WithConstructorValues_ShouldBeNameAsExpected()
        //{
        //    // Arrange
        //    var person = new Person(this._name, this._age);
        //    // Act
        //    var personName = person.GetName();
        //    // Assert
        //    personName.Should().Be(this._name);

        //}

        //[Theory]
        //[InlineData("Jorge", 18)]
        //public void Person_WithConstructorValues_ShouldBeNameAsExpected(string name, int age)
        //{
        //    // Arrange
        //    var person = new Person(name, age);
        //    // Act
        //    var personName = person.GetName();
        //    // Assert
        //    personName.Should().Be(name);
        //}


        [Theory]
        [MemberData(nameof(Data))]
        public void Person_WithConstructorValues_ShouldBeNameAsExpected(string name, int age)
        {
            // Arrange
            var person = new Person(name, age);

            // Act
            var personName = person.GetName();
            // Assert
            personName.Should().Be(name);
        }


        public static List<object[]> Data =>
            new List<object[]>
            {
                new object[] { "Jorge", 18 },
                new object[] { "Juan", 19 }
            };


        //[Fact]
        //public void Person_WithConstructorValues_ShouldBeAWrongPersonAgeException()
        //{
        //    // Arrange & Act
        //    Action action = () => new Person("Jorge", 6);

        //    // Assert
        //    action.Should().Throw<WrongPersonAgeException>().WithMessage("Age 6 is invalid. Should be greater than 18.");
        //}


        //[Fact]
        //public void Person_WithConstructorValues_ShouldBeAWrongPersonAgeException()
        //{
        //    // Arrange & Act
        //    Action action = () => new Person("Jorge", 6);

        //    // Assert
        //    var exception = Assert.Throws<WrongPersonAgeException>(action);
        //    Assert.Equal("Age 6 is invalid. Should be greater than 18.", exception.Message);
        //}


        [Fact]
        public void AddYearsToPeople_WithGetAllWithData_ShouldAddYearPersons()
        {
            //Arrange
            //Mocking
            var mockPersons = new List<Person>() { new Person("Jorge", 20), new Person("Lucas", 30) };
            var mockDependency = new Mock<ICommonData<Person>>();
            mockDependency.Setup(z => z.GetAll()).Returns(new List<Person>() { new Person("Jorge", 20), new Person("Lucas", 30) });
            var person = new Person(mockDependency.Object);


            //Act

            var persons = person.AddYearsToPeople();

            // Assert -old school
            foreach (var p in persons)
            {
                if (p.GetName() == "Lucas")
                {
                    Assert.Equal(31, p.Age);
                }
                else if (p.GetName() == "Jorge")
                {
                    Assert.Equal(21, p.Age);
                }
            }


            // with fluent Assertions & more complete

            foreach (var mock in mockPersons)
            {
                mock.Age++;
            }
            persons.Should().BeEquivalentTo(mockPersons);
        }

        //[Fact]
        //public void AddYearsToPeople_WithGetAllWithData_ShouldAddYearPersons()
        //{
        //    //Arrange
        //    //Mocking
        //    var mockPersons = new List<Person>() { new Person("Jorge", 20), new Person("Lucas", 30) };
        //    var mockDependency = new Mock<ICommonData<Person>>();
        //    mockDependency.Setup(z => z.GetAll()).Returns(new List<Person>() { new Person("Jorge", 20), new Person("Lucas", 30) });
        //    var person = new Person(mockDependency.Object);


        //    //Act

        //    var persons = person.AddYearsToPeople();

        //    // Assert -old school
        //    foreach (var p in persons)
        //    {
        //        if (p.GetName() == "Lucas")
        //        {
        //            Assert.Equal(31, p.Age);
        //        }
        //        else if (p.GetName() == "Jorge")
        //        {
        //            Assert.Equal(21, p.Age);
        //        }
        //    }


        //    // with fluent Assertions & more complete

        //    foreach (var mock in mockPersons)
        //    {
        //        mock.Age++;
        //    }
        //    persons.Should().BeEquivalentTo(mockPersons);
        //}



        [Fact]
        public void AddYearsToPeople_GetAllAndAgeProperty_ShouldCallOnce()
        {
            //Arrange
            //Mocking
            var mockPersons = new List<Person>() { new Person("Jorge", 20), new Person("Lucas", 30) };
            var mockDependency = new Mock<ICommonData<Person>>();
            mockDependency.Setup(z => z.GetAll()).Returns(new List<Person>() { new Person("Jorge", 20), new Person("Lucas", 30) });
            var person = new Person(mockDependency.Object);
            
            //Act
             person.AddYearsToPeople();

            // Assert
            mockDependency.Verify(x => x.GetAll(), Times.Once);

        }
    }
}
