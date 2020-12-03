using System;

namespace PreDevIncubator3
{
    public enum Color
    {
        Red,
        Blue,
        White,
        Green,
        Gray,
        Yellow
    }
    public class VehicleType
    {
        public string TypeName { get; set; }

        public double TaxCoefficient { get; set; }

        public VehicleType(string TypeName, double TaxCoefficient)
        {
            this.TypeName = TypeName;
            this.TaxCoefficient = TaxCoefficient;
        }

        public void Display()
        {
            Console.WriteLine("TypeName = {0}", TypeName);
            Console.WriteLine("TaxCoefficient ={0}", TaxCoefficient);
        }

        public override string ToString()
        {
            return TypeName + "," + '"' + TaxCoefficient + '"';
        }
    }

    public class Vehicle : IComparable<Vehicle>
    {
        public VehicleType VehicleType { get; set; }

        public Engine Engine { get; set; }

        public readonly string ModelName;

        public string RegistrationNumber { get; set; }

        public int Weight { get; set; }

        public int ManufactureYear { get; set; }

        public int Mileage { get; set; }

        public Color Color { get; set; }

        public int TankVolume { get; set; }

        public Vehicle(VehicleType VehicleType, string ModelName, string RegistrationNumber, int Weight, int ManufactureYear, int Mileage, Color Color)
        {
            this.Color = Color;
            this.VehicleType = VehicleType;
            this.ModelName = ModelName;
            this.RegistrationNumber = RegistrationNumber;
            this.Weight = Weight;
            this.ManufactureYear = ManufactureYear;
            this.Mileage = Mileage;
            this.TankVolume = TankVolume;
        }

        public double GetCalcTaxPerMonth()
        {
            return (Weight * 0.0013) + (VehicleType.TaxCoefficient * Engine.TaxCoefficient * 30) + 5;
        }

        public int CompareTo(Vehicle other)
        {
            if (GetCalcTaxPerMonth() < other.GetCalcTaxPerMonth())
                return -1;
            else if (GetCalcTaxPerMonth() > other.GetCalcTaxPerMonth())
                return 1;
            else return 0;

        }
        public override string ToString()
        {
            return VehicleType.TypeName + "," + '"' + ModelName + '"' + "," + RegistrationNumber + "," + Weight + "," + ManufactureYear + "," + Mileage + "," + Color.ToString() + "," + TankVolume + "," + '"' + GetCalcTaxPerMonth().ToString("0.00") + '"';
        }

       

        public override bool Equals(object obj)
        {
            Vehicle vehicle = (Vehicle)obj;
            return vehicle.VehicleType.TypeName == VehicleType.TypeName && vehicle.ModelName == ModelName;
        }
    }

    public static class VehicleHelper
    {
        public static void WriteAll(Vehicle[] arr)
        {
            foreach (var a in arr)
            {
                Console.WriteLine(a.ToString());
            }
            Console.WriteLine();
        }

        public static void MinAndMaxMileage(Vehicle[] arr)
        {
            var min = 0;
            var max = 0;
            for (int i = 1; i < arr.Length; i++)
            {
                max = arr[i].Mileage > arr[max].Mileage ? i : max;
                min = arr[i].Mileage < arr[min].Mileage ? i : min;
            }
            Console.WriteLine("min: " + arr[min].ToString());
            Console.WriteLine("max: " + arr[max].ToString());
        }
    }
    public class Engine
    {
        public string EngineType { get; set; }

        public double TaxCoefficient { get; set; }

        public Engine(string EngineType,double TaxCoefficient)
        {
            this.EngineType = EngineType;
            this.TaxCoefficient = TaxCoefficient;
        }
    }

    public class ElectricalEngine : Engine
    {
        public double ElectricityConsumption { get; set; }

        public double BatterySize { get; set; }
        public ElectricalEngine(double ElectricityConsumption, double BatterySize)
            :base("Electrical", 0.1)
        {
            this.ElectricityConsumption = ElectricityConsumption;
            this.BatterySize = BatterySize;
        }

        public double GetMaxKilometres(double BatterySize)
        {
            return BatterySize / ElectricityConsumption;
        }
    }

    public class CombustionEngine : Engine
    {
        public double EngineCapacity { get; set; }

        public double FuelConsumptionPer100 { get; set; }

        public int FuelTankCapacity { get; set; }

        public CombustionEngine(string TypeName, double TaxCoefficient)
            :base (TypeName, TaxCoefficient)
        {
            
        }
        public double GetMaxKilometres()
        {
            return FuelTankCapacity * 100 / FuelConsumptionPer100;
        }
    }

    public class GasolineEngine : CombustionEngine
    {
        public GasolineEngine(double EngineCapacity, double FuelConsumptionPer100, int FuelTankCapacity)
            :base("Gasoline", 1)
        {
            this.EngineCapacity = EngineCapacity;
            this.FuelConsumptionPer100 = FuelConsumptionPer100;
            this.FuelTankCapacity = FuelTankCapacity;
        }
    }

    public class DieselEngine : CombustionEngine
    {
        public DieselEngine(double EngineCapacity, double FuelConsumptionPer100, int FuelTankCapacity)
            :base("Diesel", 1.2)
        {
            this.EngineCapacity = EngineCapacity;
            this.FuelConsumptionPer100 = FuelConsumptionPer100;
            this.FuelTankCapacity = FuelTankCapacity;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var types = new VehicleType[4];
            types[0] = new VehicleType("Bus", 1.2);
            types[1] = new VehicleType("Car", 1.0);
            types[2] = new VehicleType("Rink", 1.5);
            types[3] = new VehicleType("Tractor", 1.2);

            var vehicles = new Vehicle[7];
            vehicles[0] = new Vehicle(types[0], "Volkswagen Crafter", "5427 AX-7", 2022, 2015, 376000, Color.Blue);
            vehicles[0].Engine = new GasolineEngine(2, 8.1, 75);
            vehicles[1] = new Vehicle(types[0], "Volkswagen Crafter", "6427 AA-7", 2500, 2014, 227010, Color.White);
            vehicles[1].Engine = new GasolineEngine(2.18, 8.5, 75);
            vehicles[2] = new Vehicle(types[0], "Electric Bus E321", "6785 BA-7", 12080, 2019, 20451, Color.Green);
            vehicles[2].Engine = new ElectricalEngine(50, 150);
            vehicles[3] = new Vehicle(types[1], "Golf 5", "8682 AX-7", 1200, 2006, 230451, Color.Gray);
            vehicles[3].Engine = new DieselEngine(1.6, 7.2, 55);
            vehicles[4] = new Vehicle(types[1], "Tesla Model S 70D", "E001 AA-7", 2200, 2019, 10454, Color.White);
            vehicles[4].Engine = new ElectricalEngine(25, 70);
            vehicles[5] = new Vehicle(types[2], "Hamm HD 12 VV", null, 3000, 2016, 122, Color.Yellow);
            vehicles[5].Engine = new DieselEngine(3.2, 25, 20);
            vehicles[6] = new Vehicle(types[3], "МТЗ Беларус-1025.4", "1145 AВ-7", 1200, 2020, 109, Color.Red);
            vehicles[6].Engine = new DieselEngine(4.75, 20.1, 135);
            VehicleHelper.WriteAll(vehicles);
            var flags = new bool[7] { true, true, true, true, true, true, true };
            for (int i=0; i<vehicles.Length-1;i++) // for 3 or more same vehicles
            {  
                for(int j = i+1;j<vehicles.Length;j++)
                {
                    if(vehicles[i].Equals(vehicles[j]))
                    {
                        if (flags[i]) 
                        {
                            Console.WriteLine(vehicles[i].ToString());
                            flags[i] = false;
                        }
                        if(flags[j])
                        {
                            Console.WriteLine(vehicles[j].ToString());
                            flags[j] = false;
                        }
                    }
                }
            }
        }
    }
}
