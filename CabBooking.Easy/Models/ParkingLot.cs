using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CabBooking.Easy.Models
{
    public class ParkingLot
    {
        public int Capacity { get; }

        public List<Slot> Slots { get; private set;  }

        public ParkingLot(int capacity)
        {
            Capacity = capacity;
            Slots = new List<Slot>();
        }

        public Slot GetSlotDetails(int slotId)
        {
            var slot = Slots.FirstOrDefault(x => x.Id == slotId);
            if (slot == null)
            {
                var newSlot = new Slot(slotId);
                Slots.Add(newSlot);
                return newSlot;
            }

            return slot;
        }

        public Slot ParkCar(Car car, int slotId)
        {
            if (slotId > Capacity)
            {
                throw new Exception("Invalid Slot");
            }

            var slot = GetSlotDetails(slotId);
            if (slot != null)
            {
                slot.AssignCar(car);
            }

            return slot;
        }

        public Slot RemoveCar(int slotId)
        {
            if (slotId > Capacity)
            {
                throw new Exception("Invalid Slot");
            }

            var slot = GetSlotDetails(slotId);
            slot.RemoveCar();
            return slot;
        }
    }
}
