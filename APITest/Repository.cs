using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace APITest
{
    public class Repository : IFooRepository<WeatherForecast>
    {

        public Repository()
        {

        }


        public void AddEntity(WeatherForecast entity)
        {
            return ;
        }

        public bool DeleteEntity(int id)
        {
            return false;
        }

        public IList<WeatherForecast> GetAll()
        {
            return new List<WeatherForecast>();
        }

        public WeatherForecast GetById(int id)
        {
            return new WeatherForecast();
        }

        public bool UpdateEntity(WeatherForecast entity)
        {
            return true;
        }
    }
}
