using System;
using System.Collections.Generic;

namespace SistemaClinicoVeterinario
{
    // ==========================================
    // 1. MODELOS DE DOMINIO
    // ==========================================
    public class Propietario
    {
        public string Id { get; }
        public string Nombre { get; }

        public Propietario(string id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }

    public class Mascota
    {
        public string Id { get; }
        public string Nombre { get; }
        public string Especie { get; }
        public bool VacunasAlDia { get; set; }

        public Mascota(string id, string nombre, string especie, bool vacunasAlDia)
        {
            Id = id;
            Nombre = nombre;
            Especie = especie;
            VacunasAlDia = vacunasAlDia;
        }
    }

    // ==========================================
    // 2. ATENCIONES MEDICAS (POLIMORFISMO - RN-02)
    // ==========================================
    public abstract class Atencion
    {
        public string Descripcion { get; protected set; } = string.Empty;
        public abstract double CalcularTarifa();
    }

    public class ConsultaGeneral : Atencion
    {
        private readonly double _costoMedicamentos;

        public ConsultaGeneral(double costoMedicamentos)
        {
            Descripcion = "Consulta General + Receta Farmacia";
            _costoMedicamentos = costoMedicamentos;
        }

        public override double CalcularTarifa()
        {
            double tarifaBase = 35.00;
            return tarifaBase + _costoMedicamentos;
        }
    }

    public class Cirugia : Atencion
    {
        private readonly double _costoLaboratorio;

        public Cirugia(double costoLaboratorio)
        {
            Descripcion = "Cirugia Quirurgica + Examenes de Laboratorio";
            _costoLaboratorio = costoLaboratorio;
        }

        public override double CalcularTarifa()
        {
            double tarifaBase = 180.00;
            return tarifaBase + _costoLaboratorio;
        }
    }

    // ==========================================
    // 3. HOSPITALIZACION Y FACTURACION
    // ==========================================
    public class Hospitalizacion
    {
        public int DiasInternado { get; }
        public double TarifaDiaria { get; } = 45.00;

        public Hospitalizacion(int diasInternado)
        {
            DiasInternado = diasInternado;
        }

        public double CalcularCostoTotal() => DiasInternado * TarifaDiaria;
    }

    public class Factura
    {
        public Propietario Propietario { get; }
        public Mascota Mascota { get; }
        public Atencion Atencion { get; }
        public Hospitalizacion? Hospitalizacion { get; }
        public double CostoVacuna { get; }

        public Factura(Propietario propietario, Mascota mascota, Atencion atencion, Hospitalizacion? hospitalizacion, double costoVacuna)
        {
            Propietario = propietario;
            Mascota = mascota;
            Atencion = atencion;
            Hospitalizacion = hospitalizacion;
            CostoVacuna = costoVacuna;
        }

        public double CalcularTotal()
        {
            double total = Atencion.CalcularTarifa() + CostoVacuna;
            if (Hospitalizacion != null)
            {
                total += Hospitalizacion.CalcularCostoTotal();
            }
            return total;
        }

        public void Imprimir()
        {
            Console.Clear();
            Console.WriteLine("==========================================================");
            Console.WriteLine("             CENTRO VETERINARIO - ALTA MEDICA             ");
            Console.WriteLine("==========================================================");
            Console.WriteLine($"Cliente / Tutor : {Propietario.Nombre} (ID: {Propietario.Id})");
            Console.WriteLine($"Paciente        : {Mascota.Nombre} ({Mascota.Especie})");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine($"Servicio        : {Atencion.Descripcion}");
            Console.WriteLine($"Costo Servicio  : Q {Atencion.CalcularTarifa():N2}");

            if (Hospitalizacion != null)
            {
                Console.WriteLine($"Hospitalizacion : {Hospitalizacion.DiasInternado} dia(s) -> Q {Hospitalizacion.CalcularCostoTotal():N2}");
            }

            if (CostoVacuna > 0)
            {
                Console.WriteLine($"Vacunacion      : Control Esquema (RN-03) -> Q {CostoVacuna:N2}");
            }

            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine($"TOTAL A PAGAR   : Q {CalcularTotal():N2}");
            Console.WriteLine("==========================================================\n");
        }
    }

    // ==========================================
    // 4. CONTROLADOR DEL SISTEMA (KPIS - RN-05)
    // ==========================================
    public class SistemaVeterinario
    {
        private readonly Dictionary<string, Propietario> _propietarios = new();
        private readonly Dictionary<string, Mascota> _mascotas = new();

        public double IngresosTotales { get; private set; }
        public int TotalAtenciones { get; private set; }

        public Propietario? BuscarPropietario(string id)
        {
            _propietarios.TryGetValue(id, out var propietario);
            return propietario;
        }

        public Mascota? BuscarMascota(string id)
        {
            _mascotas.TryGetValue(id, out var mascota);
            return mascota;
        }

        public void RegistrarCliente(Propietario p, Mascota m)
        {
            _propietarios[p.Id] = p;
            _mascotas[m.Id] = m;
        }

        public void RegistrarFactura(Factura factura)
        {
            IngresosTotales += factura.CalcularTotal();
            TotalAtenciones++;
        }

        public void MostrarKPIs()
        {
            Console.WriteLine("==========================================================");
            Console.WriteLine("             METRICAS Y KPIS DEL SISTEMA (RN-05)          ");
            Console.WriteLine("==========================================================");
            Console.WriteLine($"Atenciones Realizadas : {TotalAtenciones}");
            Console.WriteLine($"Ingresos Acumulados   : Q {IngresosTotales:N2}");
            Console.WriteLine("==========================================================\n");
        }
    }

    // ==========================================
    // 5. INTERFAZ DE USUARIO (CONSOLA INTERACTIVA)
    // ==========================================
    public class Program
    {
        private static readonly SistemaVeterinario _sistema = new();

        public static void Main(string[] args)
        {
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("==========================================================");
                Console.WriteLine("            SISTEMA DE GESTION VETERINARIA                ");
                Console.WriteLine("==========================================================");
                Console.WriteLine("1. Atender Paciente / Generar Expediente");
                Console.WriteLine("2. Ver KPIs e Ingresos Totales");
                Console.WriteLine("3. Salir");
                Console.WriteLine("==========================================================");

                int opcion = LeerEnteroEnRango("Seleccione una opcion: ", 1, 3);

                switch (opcion)
                {
                    case 1:
                        ProcesarAtencion();
                        break;
                    case 2:
                        Console.Clear();
                        _sistema.MostrarKPIs();
                        Pausar();
                        break;
                    case 3:
                        continuar = false;
                        break;
                }
            }
        }

        private static void ProcesarAtencion()
        {
            Console.Clear();
            Console.WriteLine("--- REGISTRO DE PACIENTE ---");
            string idProp = LeerCadenaNoVacia("Ingrese ID de Identificacion del Propietario: ");

            Propietario? propietario = _sistema.BuscarPropietario(idProp);
            Mascota? mascota = _sistema.BuscarMascota(idProp);

            if (propietario == null)
            {
                Console.WriteLine("\n[+] Cliente nuevo detectado. Registrando expediente...");
                string nombreProp = LeerCadenaNoVacia("Nombre completo del propietario: ");
                propietario = new Propietario(idProp, nombreProp);

                string nombreMascota = LeerCadenaNoVacia("Nombre de la mascota: ");
                string especie = SeleccionarEspecie();
                bool vacunasAlDia = LeerConfirmacion("¿Tiene el esquema de vacunas al dia? (s/n): ");

                mascota = new Mascota(idProp, nombreMascota, especie, vacunasAlDia);
                _sistema.RegistrarCliente(propietario, mascota);
            }
            else
            {
                Console.WriteLine($"\n[✓] Cliente existente encontrado: {propietario.Nombre}");
                Console.WriteLine($"[✓] Mascota registrada: {mascota!.Nombre} ({mascota.Especie})");
            }

            // Seleccionar tipo de atencion medica
            Console.WriteLine("\n--- TIPO DE ATENCION MEDICA ---");
            Console.WriteLine("1. Consulta General");
            Console.WriteLine("2. Cirugia");
            int tipoAtencion = LeerEnteroEnRango("Seleccione el servicio: ", 1, 2);

            Atencion atencion;
            if (tipoAtencion == 1)
            {
                double costoMed = LeerDecimalPositivo("Costo acumulado en medicamentos/farmacia (Q): ");
                atencion = new ConsultaGeneral(costoMed);
            }
            else
            {
                double costoLab = LeerDecimalPositivo("Costo total de analisis clinicos/laboratorio (Q): ");
                atencion = new Cirugia(costoLab);
            }

            // Hospitalizacion
            Hospitalizacion? hosp = null;
            if (LeerConfirmacion("\n¿El paciente requiere hospitalizacion? (s/n): "))
            {
                int dias = LeerEnteroEnRango("Ingrese cantidad de dias internado: ", 1, 90);
                hosp = new Hospitalizacion(dias);
            }

            // Evaluacion de esquema de vacunacion (RN-03 / CU-09)
            double costoVacuna = 0.0;
            if (!mascota!.VacunasAlDia)
            {
                Console.WriteLine("\n[!] Alerta: Esquema de vacunacion incompleto.");
                Console.WriteLine("[+] Se aplica dosis obligatoria en el alta medica (+Q 25.00).");
                costoVacuna = 25.00;
                mascota.VacunasAlDia = true;
            }

            // Emision de factura y registro de KPIs
            Factura factura = new Factura(propietario, mascota, atencion, hosp, costoVacuna);
            factura.Imprimir();
            _sistema.RegistrarFactura(factura);

            Pausar();
        }

        // ==========================================
        // METODOS AUXILIARES DE ENTRADA / VALIDACION
        // ==========================================
        private static string SeleccionarEspecie()
        {
            Console.WriteLine("\nSeleccione la especie de la mascota:");
            Console.WriteLine("1. Perro");
            Console.WriteLine("2. Gato");
            Console.WriteLine("3. Ave");
            Console.WriteLine("4. Reptil / Exotico");
            Console.WriteLine("5. Otro");

            int opcion = LeerEnteroEnRango("Opcion: ", 1, 5);
            return opcion switch
            {
                1 => "Perro",
                2 => "Gato",
                3 => "Ave",
                4 => "Reptil/Exotico",
                _ => LeerCadenaNoVacia("Especifique la especie: ")
            };
        }

        private static string LeerCadenaNoVacia(string mensaje)
        {
            string? valor;
            do
            {
                Console.Write(mensaje);
                valor = Console.ReadLine()?.Trim();
            } while (string.IsNullOrWhiteSpace(valor));

            return valor;
        }

        private static int LeerEnteroEnRango(string mensaje, int min, int max)
        {
            int valor;
            while (true)
            {
                Console.Write(mensaje);
                if (int.TryParse(Console.ReadLine(), out valor) && valor >= min && valor <= max)
                {
                    return valor;
                }
                Console.WriteLine($"Entrada invalida. Ingrese un valor entre {min} y {max}.");
            }
        }

        private static double LeerDecimalPositivo(string mensaje)
        {
            double valor;
            while (true)
            {
                Console.Write(mensaje);
                if (double.TryParse(Console.ReadLine(), out valor) && valor >= 0)
                {
                    return valor;
                }
                Console.WriteLine("Entrada invalida. Ingrese un monto numerico igual o mayor a 0.");
            }
        }

        private static bool LeerConfirmacion(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                string respuesta = Console.ReadLine()?.Trim().ToLower() ?? "";
                if (respuesta == "s" || respuesta == "si") return true;
                if (respuesta == "n" || respuesta == "no") return false;

                Console.WriteLine("Responda con 's' (Si) o 'n' (No).");
            }
        }

        private static void Pausar()
        {
            Console.WriteLine("Presione cualquier tecla para volver al menu principal...");
            Console.ReadKey();
        }
    }
}