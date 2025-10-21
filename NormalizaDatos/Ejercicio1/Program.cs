using System;
using System.Collections.Generic;

namespace NormalizaDatos.Ejercicio1
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

            List<Equipo> equipos = new();
            List<Competicion> competiciones = new();
            List<Partido> partidos = new();

            int idEquipo = 1, idCompeticion = 1, idPartido = 1;

            foreach (var linea in datos)
            {
                string[] partes = linea.Split(';');
                string nombreEq1 = partes[0];
                string nombreEq2 = partes[1];
                string resultado = partes[2];
                string nombreComp = partes[3];
                DateOnly fecha = DateOnly.Parse(partes[4]);

                var eq1 = equipos.Find(e => e.Nombre == nombreEq1);
                if (eq1 == null)
                {
                    eq1 = new Equipo { Id = idEquipo++, Nombre = nombreEq1 };
                    equipos.Add(eq1);
                }

                var eq2 = equipos.Find(e => e.Nombre == nombreEq2);
                if (eq2 == null)
                {
                    eq2 = new Equipo { Id = idEquipo++, Nombre = nombreEq2 };
                    equipos.Add(eq2);
                }

                var comp = competiciones.Find(c => c.Nombre == nombreComp);
                if (comp == null)
                {
                    comp = new Competicion { Id = idCompeticion++, Nombre = nombreComp };
                    competiciones.Add(comp);
                }

                partidos.Add(new Partido
                {
                    Id = idPartido++,
                    Equipo1Id = eq1.Id,
                    Equipo2Id = eq2.Id,
                    CompeticionId = comp.Id,
                    Resultado = resultado,
                    Fecha = fecha
                });
            }

            Console.WriteLine("EQUIPOS:");
            foreach (var e in equipos)
                Console.WriteLine($"{e.Id}: {e.Nombre}");

            Console.WriteLine("\nCOMPETICIONES:");
            foreach (var c in competiciones)
                Console.WriteLine($"{c.Id}: {c.Nombre}");

            Console.WriteLine("\nPARTIDOS:");
            foreach (var p in partidos)
                Console.WriteLine($"{p.Id}: {p.Equipo1Id}-{p.Equipo2Id} ({p.Resultado}) [{p.CompeticionId}] {p.Fecha}");
        }
    }
}

