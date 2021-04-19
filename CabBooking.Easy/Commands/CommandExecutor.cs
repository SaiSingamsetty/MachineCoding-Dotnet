using System;
using System.Collections.Generic;
using System.Text;
using CabBooking.Easy.Models;

namespace CabBooking.Easy.Commands
{
    public abstract class CommandExecutor
    {
        public abstract bool Validate(Command command);
        public abstract void Execute(Command command);
    }
}
