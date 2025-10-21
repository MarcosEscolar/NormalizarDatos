using System;
using System.Collections.Generic;
using System.Linq;

namespace NormalizaDatos.Ejercicio2
{
    class Equipo
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
    }

    class Competicion
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
    }

    class Partido
    {
        public int Id { get; set; }
        public int Equipo1Id { get; set; }
        public int Equipo2Id { get; set; }
        public int CompeticionId { get; set; }
        public string? Resultado { get; set; }
        public DateOnly Fecha { get; set; }
    }

    internal class Program
    {
        static void Main()
        {
            List<string> datos = new()
            {
                "Real Madrid;Barcelona;2-1;Liga;2025-10-12",
                "Atlético de Madrid;Sevilla;1-0;Liga;2025-10-13",
                "Barcelona;Valencia;3-2;Copa del Rey;2025-10-14",
                "Sevilla;Real Madrid;0-2;Liga;2025-10-15",
                "Valencia;Atlético de Madrid;1-1;Copa del Rey;2025-10-16",
                "Real Madrid;Atlético de Madrid;2-2;Liga;2025-10-17",
                "Barcelona;Sevilla;4-0;Liga;2025-10-18",
                "Valencia;Real Madrid;0-1;Copa del Rey;2025-10-19",
                "Atlético de Madrid;Barcelona;1-3;Liga;2025-10-20",
                "Sevilla;Valencia;2-2;Copa del Rey;2025-10-21"
            };

            var equipos = datos
                .SelectMany(d => d.Split(';').Take(2))
                .Distinct()
                .Select((nombre, i) => new Equipo { Id = i + 1, Nombre = nombre })
                .ToList();

            var competiciones = datos
                .Select(d => d.Split(';')[3])
                .Distinct()
                .Select((nombre, i) => new Competicion { Id = i + 1, Nombre = nombre })
                .ToList();

            var partidos = datos
                .Select((d, i) =>
                {
                    var partes = d.Split(';');
                    return new Partido
                    {
                        Id = i + 1,
                        Equipo1Id = equipos.First(e => e.Nombre == partes[0]).Id,
                        Equipo2Id = equipos.First(e => e.Nombre == partes[1]).Id,
                        CompeticionId = competiciones.First(c => c.Nombre == partes[3]).Id,
                        Resultado = partes[2],
                        Fecha = DateOnly.Parse(partes[4])
                    };
                })
                .ToList();

            Console.WriteLine("EQUIPOS:");
            equipos.ForEach(e => Console.WriteLine($"{e.Id}: {e.Nombre}"));

            Console.WriteLine("\nCOMPETICIONES:");
            competiciones.ForEach(c => Console.WriteLine($"{c.Id}: {c.Nombre}"));

            Console.WriteLine("\nPARTIDOS:");
            partidos.ForEach(p => Console.WriteLine($"{p.Id}: {p.Equipo1Id}-{p.Equipo2Id} ({p.Resultado}) [{p.CompeticionId}] {p.Fecha}"));
        }
    }
}
}
