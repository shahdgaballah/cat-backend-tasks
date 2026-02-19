using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task01
{
    //the ITarget is what my system expects to work with, but LegacyGasStation is what i am forced to work with
    //so i have to incompatible objects (like wall socket and charger) ==> so i need an adapter which will make them compatible
    public interface ITarget
    {
        void GetFuel();
    }
}
