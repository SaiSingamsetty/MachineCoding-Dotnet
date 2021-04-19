using System;
using System.Collections.Generic;
using System.Text;

namespace CabBooking.Easy.Models
{
    public class Car
    {
        public string Color { get; }

        public string RegistrationNumber { get; }

        public Car(string color, string registrationNumber)
        {
            Color = color;
            RegistrationNumber = registrationNumber;
        }
    }
}
