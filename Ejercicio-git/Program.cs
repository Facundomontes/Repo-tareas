using System;

namespace EjercicioEncapsulamiento
{

    public class CuentaBancaria
    {
        private readonly string _id; 
        private decimal _saldo;      

        public CuentaBancaria(string idInicial, decimal saldoInicial)
        {
            _id = idInicial;
            _saldo = saldoInicial;
        }

        public string Id => _id;
        public decimal Saldo => _saldo; 

        public void Depositar(decimal monto)
        {
            if (monto > 0) _saldo += monto;
        }

        public void Retirar(decimal monto)
        {
       
            if (monto > 0 && monto <= _saldo)
            {
                _saldo -= monto;
                Console.WriteLine($"[Banco] Retiro de {monto:C} exitoso.");
            }
            else
            {
                Console.WriteLine("[Banco] Error: Fondos insuficientes.");
            }
        }
    }

    
    public class Temperatura
    {
        private double _grados;
        public double Grados
        {
            get => _grados;
            set
            {
                if (value < -273.15)
                    throw new ArgumentException("Error: La temperatura no puede ser menor al cero absoluto.");
                _grados = value;
            }
        }
    }

    public class Rectangulo
    {
        public double Ancho { get; set; }
        public double Alto { get; set; }

      
        public double Perimetro => 2 * (Ancho + Alto);
    }

    public class Persona
    {
        private int _edad;
        private string _codigoSecreto; 

        public string Nombre { get; set; }

   
        public int Edad
        {
            get => _edad;
            set
            {
                if (value >= 0 && value <= 150)
                    _edad = value;
                else
                    Console.WriteLine("[Persona] Edad inválida (debe ser 0-150).");
            }
        }

        public string CodigoSecreto
        {
            set { _codigoSecreto = value; }
        }
    }

    class Program
    {
        
        const decimal IVA = 0.21m;

        static void Main(string[] args)
        {
            Console.WriteLine("--- Demostración de C# y Encapsulamiento ---\n");

           
            CuentaBancaria cuenta = new CuentaBancaria("CTA-001", 500m);
       
            Console.WriteLine($"Cuenta ID: {cuenta.Id} | Saldo inicial: {cuenta.Saldo:C}");
            cuenta.Retirar(600m); 
            cuenta.Retirar(200m); 

        
            Temperatura temp = new Temperatura();
            try { temp.Grados = -300; }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

         
            Rectangulo rect = new Rectangulo { Ancho = 10, Alto = 5 };
            Console.WriteLine($"\nRectángulo (10x5) - Perímetro: {rect.Perimetro}");

            decimal precioBase = 100m;
            Console.WriteLine($"Precio: {precioBase:C} | Con IVA ({(IVA * 100)}%): {precioBase * (1 + IVA):C}");

      
            Persona p = new Persona();
            p.Nombre = "Juan";
            p.Edad = 25;
            p.Edad = 200; 
            p.CodigoSecreto = "123456";

            Console.WriteLine($"\nPersona: {p.Nombre} | Edad asignada: {p.Edad}");

            Console.WriteLine("\nPresiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
} 