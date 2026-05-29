using System;
using System.Collections.Generic;
using System.Linq;

namespace CentroAdopcion
{
    abstract class Animal
    {
        public string Nombre { get; }
        public string Raza { get; }

        protected Animal(string nombre, string raza)
        {
            Nombre = nombre;
            Raza = raza;
        }

        public abstract void HacerSonido();
    }
    class Perro : Animal
    {
        public Perro(string nombre, string raza) : base(nombre, raza) { }

        public override void HacerSonido()
        {
            Console.WriteLine("Ladrido");
        }
    }
    class Gato : Animal
    {
        public Gato(string nombre, string raza) : base(nombre, raza) { }

        public override void HacerSonido()
        {
            Console.WriteLine("Maullido");
        }
    }
    class Persona
    {
        public string Nombre { get; }
        public string DNI { get; }

        private List<Animal> mascotasAdoptadas;

        public Persona(string nombre, string dni)
        {
            Nombre = nombre;
            DNI = dni;
            mascotasAdoptadas = new List<Animal>();
        }

        public void AgregarMascota(Animal animal)
        {
            mascotasAdoptadas.Add(animal);
        }

        public List<Animal> ObtenerMascotas()
        {
            return mascotasAdoptadas;
        }
    }
    class Refugio
    {
        public List<Animal> AnimalesDisponibles { get; }
        public List<Persona> PersonasRegistradas { get; }

        public Refugio()
        {
            AnimalesDisponibles = new List<Animal>();
            PersonasRegistradas = new List<Persona>();
        }
        public void CargarDatosIniciales()
        {
            AnimalesDisponibles.Add(new Perro("Orion", "Marca perro"));
            AnimalesDisponibles.Add(new Gato("Ramses", "Siamés"));
            AnimalesDisponibles.Add(new Perro("Rango", "Breton español"));

            PersonasRegistradas.Add(new Persona("Facundo Montes", "1234"));
            PersonasRegistradas.Add(new Persona("Nehuen de la Cruz", "5678"));
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Refugio refugio = new Refugio();
            refugio.CargarDatosIniciales();

            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\nSISTEMA DE CENTRO DE ADOPCIÓN");
                Console.WriteLine("1. Mostra animales disponibles");
                Console.WriteLine("2. Registra nueva persona");
                Console.WriteLine("3. Adopta una mascota");
                Console.WriteLine("4. Mostra adoptantes y sus mascotas");
                Console.WriteLine("5. Salir");
                Console.Write("Ingrese una opción: ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        {
                            if (refugio.AnimalesDisponibles.Count == 0)
                            {
                                Console.WriteLine("No hay animales disponibles para adopción en este momento.");
                            }
                            else
                            {
                                Console.WriteLine("Animales disponibles para adopción:");
                                foreach (var animal in refugio.AnimalesDisponibles)
                                {
                                    string tipo = animal.GetType().Name;
                                    Console.Write($"- {tipo} | Nombre: {animal.Nombre} | Raza: {animal.Raza} | Sonido: ");
                                    animal.HacerSonido();
                                }
                            }
                            break;
                        }

                    case "2":
                        {
                            Console.Write("Ingrese el nombre de la persona: ");
                            string nombre = Console.ReadLine();
                            Console.Write("Ingrese el DNI: ");
                            string dni = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(dni))
                            {
                                Console.WriteLine("Error: El nombre y DNI no pueden estar vacíos.");
                            }
                            else if (refugio.PersonasRegistradas.Any(p => p.DNI == dni))
                            {
                                Console.WriteLine("Error: Ya existe una persona registrada con ese DNI.");
                            }
                            else
                            {
                                refugio.PersonasRegistradas.Add(new Persona(nombre, dni));
                                Console.WriteLine("Persona registrada con éxito.");
                            }
                            break;
                        }

                    case "3":
                        {
                            if (refugio.AnimalesDisponibles.Count == 0)
                            {
                                Console.WriteLine("No hay animales disponibles para adoptar.");
                                break;
                            }

                            if (refugio.PersonasRegistradas.Count == 0)
                            {
                                Console.WriteLine("No hay personas registradas. Registre una persona primero.");
                                break;
                            }

                            Console.Write("Ingrese el DNI del adoptante: ");
                            string dni = Console.ReadLine();

                            Persona adoptante = refugio.PersonasRegistradas.FirstOrDefault(p => p.DNI == dni);

                            if (adoptante == null)
                            {
                                Console.WriteLine("Error: No se encontró ninguna persona con ese DNI.");
                                break;
                            }

                            Console.Write("Ingrese el nombre del animal a adoptar: ");
                            string nombreAnimal = Console.ReadLine();

                            Animal animalAAdoptar = refugio.AnimalesDisponibles.FirstOrDefault(a => a.Nombre.Equals(nombreAnimal, StringComparison.OrdinalIgnoreCase));

                            if (animalAAdoptar == null)
                            {
                                Console.WriteLine("Error: No se encontró ningún animal con ese nombre en adopción.");
                            }
                            else
                            {
                                adoptante.AgregarMascota(animalAAdoptar);
                                refugio.AnimalesDisponibles.Remove(animalAAdoptar);
                                Console.WriteLine($"¡Éxito! {adoptante.Nombre} ha adoptado a {animalAAdoptar.Nombre}.");
                            }
                            break;
                        }

                    case "4":
                        {
                            if (refugio.PersonasRegistradas.Count == 0)
                            {
                                Console.WriteLine("No hay personas registradas en el sistema.");
                            }
                            else
                            {
                                Console.WriteLine("Lista de Adoptantes:");
                                foreach (var persona in refugio.PersonasRegistradas)
                                {
                                    Console.WriteLine($" {persona.Nombre} (DNI: {persona.DNI})");
                                    var mascotas = persona.ObtenerMascotas();

                                    if (mascotas.Count == 0)
                                    {
                                        Console.WriteLine("  - Todavia no ha adoptado ninguna mascota.");
                                    }
                                    else
                                    {
                                        foreach (var mascota in mascotas)
                                        {
                                            Console.WriteLine($"  - Adoptó un {mascota.GetType().Name} llamado {mascota.Nombre} ({mascota.Raza})");
                                        }
                                    }
                                }
                            }
                            break;
                        }

                    case "5":
                        {
                            salir = true;
                            Console.WriteLine("Saliendo del sistema");
                            break;
                        }

                    default:
                        {
                            Console.WriteLine("Opción no válida. Intente de nuevo.");
                            break;
                        }
                }
            }
        }
    }
}
