using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.Core
{
    public class Items
    {
            public string Name { get; set; } = string.Empty;
            public string pickupMessage { get; set; } = string.Empty;

            public Items(string Name, string pickupMessage)
            {
                this.Name = Name;
                this.pickupMessage = pickupMessage;
            }

    }
}
