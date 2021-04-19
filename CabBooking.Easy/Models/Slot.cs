using System;
using System.Collections.Generic;
using System.Text;

namespace CabBooking.Easy.Models
{
    public class Slot
    {
        public int Id { get; }

        public Car ParkedCar { get; private set; }

        public Slot(int id)
        {
            Id = id;
            ParkedCar = null;
        }

        public void AssignCar(Car car)
        {
            ParkedCar = car;
        }

        public void RemoveCar()
        {
            ParkedCar = null;
        }
    }
}
