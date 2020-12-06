using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace PreDevIncubator5
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
        public int VehicleTypeId { get; set; }

        public string TypeName { get; set; }

        public double TaxCoefficient { get; set; }

        public VehicleType(string TypeName, double TaxCoefficient)
        {
            this.TypeName = TypeName;
            this.TaxCoefficient = TaxCoefficient;
        }

        public VehicleType()
        {

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
        public int VehicleId { get; set; }

        public VehicleType VehicleType { get; set; }

        public AbstractEngine Engine { get; set; }

        public List<Rent> Rents { get; set; }

        public string ModelName { get; set; }

        public string RegistrationNumber { get; set; }

        public int Weight { get; set; }

        public int ManufactureYear { get; set; }

        public int Mileage { get; set; }

        public Color Color { get; set; }

        public int TankVolume { get; set; }

        public Vehicle()
        {

        }

        public Vehicle(VehicleType VehicleType, string ModelName, string RegistrationNumber, int Weight, int ManufactureYear, int Mileage, Color Color,AbstractEngine Engine ,int TankVolume)
        {
            this.Color = Color;
            this.VehicleType = VehicleType;
            this.ModelName = ModelName;
            this.RegistrationNumber = RegistrationNumber;
            this.Weight = Weight;
            this.ManufactureYear = ManufactureYear;
            this.Mileage = Mileage;
            this.Engine = Engine;
            this.TankVolume = TankVolume;
            Rents = new List<Rent>();
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

        public double GetTotalIncome()
        {
            double sum = 0;
            foreach(var a in Rents)
            {
                sum += a.RentPrice;
            }
            return sum;
        }

        public double GetTotalProfit()
        {
            return GetTotalIncome() - GetCalcTaxPerMonth();
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
    public abstract class AbstractEngine
    {
        public string EngineType { get; set; }

        public double TaxCoefficient { get; set; }

        public AbstractEngine(string EngineType, double TaxCoefficient)
        {
            this.EngineType = EngineType;
            this.TaxCoefficient = TaxCoefficient;
        }

        public abstract double GetMaxKilometres(double FuelTank);

    }

    public class ElectricalEngine : AbstractEngine
    {
        public double ElectricityConsumption { get; set; }


        public ElectricalEngine(double ElectricityConsumption)
            : base("Electrical", 0.1)
        {
            this.ElectricityConsumption = ElectricityConsumption;
        }

        public override double GetMaxKilometres(double BatterySize)
        {
            return BatterySize / ElectricityConsumption * 100;
        }
    }

    public abstract class AbstractCombustionEngine : AbstractEngine
    {
        public double EngineCapacity { get; set; }

        public double FuelConsumptionPer100 { get; set; }



        public AbstractCombustionEngine(string TypeName, double TaxCoefficient)
            : base(TypeName, TaxCoefficient)
        {

        }
        public override double GetMaxKilometres(double FuelTankCapacity)
        {
            return FuelTankCapacity * 100 / FuelConsumptionPer100;
        }
    }

    public class GasolineEngine : AbstractCombustionEngine
    {
        public GasolineEngine(double EngineCapacity, double FuelConsumptionPer100)
            : base("Gasoline", 1)
        {
            this.EngineCapacity = EngineCapacity;
            this.FuelConsumptionPer100 = FuelConsumptionPer100;

        }
    }

    public class DieselEngine : AbstractCombustionEngine
    {
        public DieselEngine(double EngineCapacity, double FuelConsumptionPer100)
            : base("Diesel", 1.2)
        {
            this.EngineCapacity = EngineCapacity;
            this.FuelConsumptionPer100 = FuelConsumptionPer100;

        }
    }

    public class Rent
    {
        public DateTime RentDate { get; set; }

        public double RentPrice { get; set; }

        public Rent()
        {

        }

        public Rent(DateTime RentDate, double RentPrice)
        {
            this.RentDate = RentDate;
            this.RentPrice = RentPrice;
        }
    }

    public class Collections
    {
        public List<VehicleType> types { get; set; }

        public List<Vehicle> vehicles { get; set; }

        public Collections(string types, string vehicles, string rents)
        {
            this.types = LoadTypes(types);
            this.vehicles = LoadVehicles(vehicles);
            LoadRents(rents);
        }

        public List<VehicleType> LoadTypes(string inFile)
        {
            var vehicletypes = new List<VehicleType>();
            using (StreamReader sr = new StreamReader($"../../../{inFile}.csv"))
            {
                string line;
                while((line = sr.ReadLine()) !=null)
                {
                    
                    vehicletypes.Add(CreateType(line));
                }               
            }
            return vehicletypes;
        }

        public List<Vehicle> LoadVehicles(string inFile)
        {
            var vehicles = new List<Vehicle>();
            using (StreamReader sr = new StreamReader($"../../../{inFile}.csv"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    
                    vehicles.Add(CreateVehicle(line));
                }
            }
                return vehicles;
        }

        public VehicleType CreateType(string csvString)
        {
            string[] arr;
            var type = new VehicleType();
            TextFieldParser parser = new TextFieldParser(new StringReader(csvString));
            parser.HasFieldsEnclosedInQuotes = true;
            parser.SetDelimiters(",");
            arr = parser.ReadFields();
            type.TaxCoefficient = Convert.ToDouble(arr[2]);
            type.VehicleTypeId = Convert.ToInt32(arr[0]);
            type.TypeName = arr[1];

            return type;
        }

        public Vehicle CreateVehicle(string csvString)
        {
            var vehicle = new Vehicle();
            string[] arr;
            TextFieldParser parser = new TextFieldParser(new StringReader(csvString));
            parser.HasFieldsEnclosedInQuotes = true;
            parser.SetDelimiters(",");
            arr = parser.ReadFields();
            vehicle.VehicleId = Convert.ToInt32(arr[0]);
            vehicle.VehicleType = types[Convert.ToInt32(arr[1]) - 1];
            vehicle.ModelName = arr[2];
            vehicle.RegistrationNumber = arr[3];
            vehicle.Weight = Convert.ToInt32(arr[4]);
            vehicle.ManufactureYear = Convert.ToInt32(arr[5]);
            vehicle.Mileage = Convert.ToInt32(arr[6]);
            vehicle.Color = (Color)Enum.Parse(typeof(Color), arr[7], true);
            if (arr[8] == "Gasoline")
            {
                vehicle.Engine = new GasolineEngine(Convert.ToDouble(arr[9]), Convert.ToDouble(arr[10]));
                vehicle.TankVolume = Convert.ToInt32(arr[11]);
            }
            if (arr[8] == "Diesel")
            {
                vehicle.Engine = new GasolineEngine(Convert.ToDouble(arr[9]), Convert.ToDouble(arr[10]));
                vehicle.TankVolume = Convert.ToInt32(arr[11]);
            }
            if (arr[8] == "Electrical")
            {
                vehicle.Engine = new ElectricalEngine(Convert.ToDouble(arr[9]));
                vehicle.TankVolume = Convert.ToInt32(arr[10]);
            }
            vehicle.Rents = new List<Rent>();
            return vehicle;
        }

        public void Insert(int index, Vehicle v)
        {
            if(index<vehicles.Capacity&&index>=0)
            {
                vehicles.Insert(index, v);
            }
            else
            {
                vehicles.Add(v);
            }
        }

        public int Delete(int index)
        {
            if(index>=0&&index<vehicles.Capacity)
            {
                vehicles.RemoveAt(index);
                return index;
            }
            return -1;
        }

        public double SumTotalProfit()
        {
            double sum = 0;
            foreach(var a in vehicles)
            {
                sum += a.GetTotalProfit();
            }
            return sum;
        }

        public void Print()
        {
            Console.WriteLine(string.Format("{0,-5}{1,-10}{2,-25}{3,-15}{4,-15}{5,-10}{6,-10}{7,-10}{8,-10}{9,-10}{10,-10}",
                "Id", "Type", "ModelName", "Number", "Weight(kg)", "Year", "Mileage", "Color", "Income", "Tax", "Profit"
                ));
            foreach (var a in vehicles) 
            {
                Console.WriteLine(string.Format("{0,-5}{1,-10}{2,-25}{3,-15}{4,-15}{5,-10}{6,-10}{7,-10}{8,-10}{9,-10}{10,-10}",

                    a.VehicleId,
                    a.VehicleType.TypeName,
                    a.ModelName,
                    a.RegistrationNumber,
                   a.Weight,
                    a.ManufactureYear,
                    a.Mileage,
                    a.Color,
                    a.GetTotalIncome().ToString("0.00"),
                    a.GetCalcTaxPerMonth().ToString("0.00"),
                   a.GetTotalProfit().ToString("0.00"))                    
                );
            }
            Console.WriteLine(string.Format("{0,-120}{1,-10}","Total", SumTotalProfit().ToString("0.00")));
        }
        public void LoadRents(string inFile)
        {

            using (StreamReader sr = new StreamReader($"../../../{inFile}.csv"))
            {
                string line;
                string[] arr;
                while ((line = sr.ReadLine()) != null)
                {
                    TextFieldParser parser = new TextFieldParser(new StringReader(line));
                    parser.HasFieldsEnclosedInQuotes = true;
                    parser.SetDelimiters(",");
                    var rent = new Rent();
                    arr = parser.ReadFields();
                    rent.RentDate = Convert.ToDateTime(arr[1]);
                    rent.RentPrice = Convert.ToDouble(arr[2]);
                    vehicles[vehicles.FindIndex(a => a.VehicleId == Convert.ToInt32(arr[0]))].Rents.Add(rent);
                }
            }
        }
        private class SortVehiclesHelper : IComparer<Vehicle>
        {
            int IComparer<Vehicle>.Compare(Vehicle x, Vehicle y)
            {
                return string.Compare(x.ModelName, y.ModelName);
            }
        }
        public static IComparer<Vehicle> Comparator()
        {
            return new SortVehiclesHelper();
        }

        public void Sort(IComparer<Vehicle> comparator)
        {
            vehicles.Sort(comparator);
        }
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            var collection = new Collections("types","vehicles","rents");
            collection.Print();
           var v = collection.CreateVehicle("8,2,Model Name,9876 AA-5,1300,2021,1,Red,Electrical,50,500");
            collection.Insert(-1, v);
            collection.Delete(1);
            collection.Delete(4);
            collection.Print();
            collection.Sort(Collections.Comparator());
            collection.Print();
        }
    }
}
