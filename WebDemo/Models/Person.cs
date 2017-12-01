using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class Person
    {
        public Person() { }
        public string Name { get; set; }
        public int Age { get; set; }

        public double Height { get; set; }
        public int Weight { get; set; }
        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }
    }
}