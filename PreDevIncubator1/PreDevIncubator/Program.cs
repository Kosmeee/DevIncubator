using System;

namespace PreDevIncubator
{
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
            Console.WriteLine("TaxCoefficient ={0}",TaxCoefficient);
        }

        public override string ToString()
        {
            return TypeName + "," + '"' + TaxCoefficient + '"';
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new VehicleType[4];
            arr[0] = new VehicleType("Bus", 1.2);
            arr[1] = new VehicleType("Car", 1.0);
            arr[2] = new VehicleType("Rink", 1.5);
            arr[3] = new VehicleType("Tractor", 1.2);

            var max = arr[0].TaxCoefficient;
            double mid = 0;
            for(int i = 0; i<arr.Length;i++)
            {
            
                max = max < arr[i].TaxCoefficient ? arr[i].TaxCoefficient : max;
                mid += arr[i].TaxCoefficient;
                arr[i].Display();
                Console.WriteLine();
                if (i == arr.Length-1)
                {
                    mid = mid / arr.Length;
                    arr[i].TaxCoefficient = 1.3;
                }
            }
            Console.WriteLine("average tax = {0}", mid);
            Console.WriteLine("max tax = {0}", max);

            foreach (var a in arr)
            {
                Console.WriteLine(a.ToString());
            }

        }
    }
}
