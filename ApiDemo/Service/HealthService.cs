using ApiDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiDemo.Service
{
    public class HealthService
    {
        public static double CalculateBMI(Person human)
        {
            return human.Weight / Math.Pow(human.Height, human.Height);
        }
        public static List<Person> GetAllPeople()
        {
            List<Person> p = new List<Person> {
                new Person("Kuan",18),
                new Person("Abby",20),
                new Person("Tom",21),
                new Person("Emily",30),
                new Person("Sally",16),
                new Person("Jacky",34),
                new Person("Tommy",38),
            };
            return p;
        }
    }
}