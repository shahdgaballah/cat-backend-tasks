namespace task01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //we make an instance of legacy class
            LegacyGasStation legacyGasStation = new LegacyGasStation();

            //wrap that instance in the adapter
            //so ctor of TargetAdapter is called taking legacyGasStation as an argument
            //then it assigns it to _legacyGasStation
            ITarget target = new TargetAdapter(legacyGasStation); //now this line is holding the _legacyGasStation instance
                                                                  //so this its only function to make the targetadapter recognise the _legacyGasStation inside it
                                                                  //so the forced-to-implement method getfuel() could access the legacy class (interface accessed/called the legacy method)
            //make the adapter call its method
            //getfuel() is invoked via target (which is an interface that has this method)
            //whatever inherits that interface (TargetAdapter) will have to implement that getfuel()
            //so getfuel() gets called which has the legacy class calling the providegasoline()
            target.GetFuel();
        }
    }
}
