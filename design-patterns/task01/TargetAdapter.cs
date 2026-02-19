using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task01
{
    //the adapter that makes the LegacyGasStation implement the ITarget interface, so we make a TargetAdapter that implements the ITarget
    public class TargetAdapter : ITarget
    {
        //1.we make a variable of type LegacyGasStation
        private LegacyGasStation _legacyGasStation;

        //2.we intialize a ctor that takes legacyGasSation as a param of type LegacyGasStation
        public TargetAdapter(LegacyGasStation legacyGasStation)
        {
            //3. we assign that param to the variable of type LegacyGasStation so we can use it later
            _legacyGasStation = legacyGasStation;
        }
        public void GetFuel()  //the implemented method from ITarget
        {
            //4. we finally made the legacy class call the implemented method from ITarget
            _legacyGasStation.ProvideGasoline();
        }
    }
}
