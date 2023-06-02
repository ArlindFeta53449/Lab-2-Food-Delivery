﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface INotificationDatabaseSettings
    {
        string CollectionName { get; set; } 
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }   
    }
}
