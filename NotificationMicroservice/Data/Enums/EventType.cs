using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Enums
{
    public enum EventType
    {
        UserCreated,
        OrderStatusChanged,
        OrderCreated,
        OrderAccepted,
        Undetermined
    }
}
