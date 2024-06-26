using static ProyectoProg1.Program;

namespace ProyectoProg1
{
    internal class Program
    {
        public struct Alumno
        {
            public int indice;
            public string nombre;
            public string apellido;
            public int dni;
            public string fechaNacimiento;
            public string domicilio;
            public bool estaActivo;
        }

        public struct Materia
        {
            public int indice;
            public string nombreMateria;
            public bool estaActiva;
        }

        public struct Inscripcion
        {
            public int indice;
            public int indice_alumno;
            public int indice_materia;
            public string estado;
            public double nota;
            public string fecha;
        }

        public static string alumnosPath = "alumnos.txt";
        public static string materiasPath = "materias.txt";
        public static string inscripcionPath = "inscripcion.txt";

        // ------------------------------------------------------------ UTILIDADES -----------------------------------------------------
        // ------------------------------------------------------------ UTILIDADES -----------------------------------------------------
        // ------------------------------------------------------------ UTILIDADES -----------------------------------------------------
        // ------------------------------------------------------------ UTILIDADES -----------------------------------------------------

        // VALIDAR CHAR

        static char ValidarCharSON(string mensaje)
        {
            char opcionChar;
            do
            {
                Console.WriteLine(mensaje);
            } while (!char.TryParse(Console.ReadLine(), out opcionChar) || (opcionChar != 's' && opcionChar != 'n' && opcionChar != 'S' && opcionChar != 'N'));
            return opcionChar;
        }

        // VALIDAR NUMERO

        static int ValidarNumero(string mensaje)
        {
            int numero;
            do
            {
                Console.WriteLine(mensaje);
            } while (!int.TryParse(Console.ReadLine(), out numero) && numero >= 0);
            return numero;
        }

        // PEDIR DIA

        static int PedirDia(string mensaje)
        {
            int dia;
            string diaIngresado;
            do
            {
                Console.WriteLine($"Por favor ingrese el dia {mensaje} (01-31), asegurese de que el dia tenga 2 dígitos");
                diaIngresado = Console.ReadLine();
            } while (!(diaIngresado.Length == 2 && int.TryParse(diaIngresado, out dia) && dia >= 1 && dia <= 31));
            return dia;
        }

        // PEDIR MES

        static int PedirMes(string mensaje)
        {
            int mes;
            string mesIngresado;
            do
            {
                Console.WriteLine($"Por favor ingrese el mes {mensaje} (01-12), asegurece de que el mes tenga 2 dígitos");
                mesIngresado = Console.ReadLine();
            } while (!(mesIngresado.Length == 2 && int.TryParse(mesIngresado, out mes) && mes >= 1 && mes <= 12));
            return mes;
        }

        // PEDIR ANIO
        static int PedirAnio(string mensaje)
        {
            int anio;
            do
            {
                Console.WriteLine($"Por favor ingrese el año {mensaje} completo");
            } while (!(int.TryParse(Console.ReadLine(), out anio) && anio >= 1900 && anio <= 2100));
            return anio;
        }

        // ES BISIESTO
        static bool EsBisiesto(int anio)
        {
            return (anio % 4 == 0 && (anio % 100 != 0 || anio % 400 == 0));
        }

        // PEDIR FECHA VALIDA
        static string fechaValida(string mensaje)
        {
            int dia, mes, anio;
            dia = PedirDia(mensaje);
            mes = PedirMes(mensaje);
            anio = PedirAnio(mensaje);
            string fechaValida = "";
            bool esFechaValida = true;

            if (mes == 4 || mes == 6 || mes == 9 || mes == 11)
            {
                if (dia > 31)
                {
                    esFechaValida = false;
                }
            }
            else if (mes == 2)
            {
                if (EsBisiesto(anio))
                {
                    if (dia > 29)
                    {
                        esFechaValida = false;
                    }
                }
                else
                {
                    if (dia > 28)
                    {
                        esFechaValida = false;
                    }
                }
            }
            if (!esFechaValida)
            {
                Console.WriteLine("La fecha ingresada no es válida");
            }
            else
            {
                fechaValida = $"{dia}/{mes}/{anio}";
            }
            return fechaValida;
        }

        // PEDIR STRING NO NULL SIN ,

        static string StringValido(string mensaje)
        {
            string cadena;
            do
            {
                Console.WriteLine(mensaje);
                cadena = Console.ReadLine();
            } while (cadena.Length < 1 || cadena.Contains(','));
            return cadena;
        }

        // ------------------------------------------------------------ ALUMNOSSS ------------------------------------------------------
        // ------------------------------------------------------------ ALUMNOSSS ------------------------------------------------------
        // ------------------------------------------------------------ ALUMNOSSS ------------------------------------------------------
        // ------------------------------------------------------------ ALUMNOSSS ------------------------------------------------------
        // ------------------------------------------------------------ ALUMNOSSS ------------------------------------------------------
        // ------------------------------------------------------------ ALUMNOSSS ------------------------------------------------------

        // DAR DE ALTA UN ALUMNOOOOO
        static void AltaAlumno()
        {
            List<Alumno> listaExistente = new List<Alumno>();
            List<Alumno> listaVacia = new List<Alumno>();
            int indiceDeAlumno;

            if (TraerAlumnosDeArchivo(alumnosPath) != null && TraerAlumnosDeArchivo(alumnosPath).Count() > 0)
            {
                listaExistente = TraerAlumnosDeArchivo(alumnosPath);
                indiceDeAlumno = listaExistente.Count;
            }
            else
            {
                indiceDeAlumno = 0;
            }

            Alumno alumno = new Alumno();
            int dni = ValidarNumero("Ingrese DNI");

            if (listaExistente.Exists(alumno => alumno.dni == dni))
            {
                for (int i = 0; i < listaExistente.Count; i++)
                {
                    if (listaExistente[i].dni == dni)
                    {
                        if (listaExistente[i].estaActivo == false)
                        {
                            Console.WriteLine("El alumno con el dni ingresado se encuentra en la base de datos pero está desactivado");
                            char opcionElegida = ValidarCharSON("Desea activarlo? s/n");
                            if (opcionElegida == 's' || opcionElegida == 'S')
                            {
                                Alumno al = listaExistente[i];
                                al.estaActivo = true;
                                listaExistente[i] = al;
                                Console.Clear();
                                Console.WriteLine("Alumno activado correctamente");
                            }
                            EscribirAlumnoEnArchivo(listaExistente, false);
                        }
                        else
                        {
                            Console.WriteLine("Error: el dni ya esta ingresado");
                        }
                    }
                }
            }
            else
            {
                alumno.indice = ++indiceDeAlumno;
                alumno.nombre = StringValido("Ingrese nombre del alumno");
                alumno.apellido = StringValido("Ingrese apellido del alumno");
                alumno.dni = dni;
                string fechaIngresada = "";
                do
                {
                    fechaIngresada = fechaValida("de nacimiento");
                } while (fechaIngresada == "");

                alumno.fechaNacimiento = fechaIngresada;
                alumno.domicilio = StringValido("Ingrese domicilio");
                alumno.estaActivo = true;
                listaVacia.Add(alumno);
                Console.Clear();
                Console.WriteLine("Alumno ingresado correctamente");
                Console.WriteLine();
                EscribirAlumnoEnArchivo(listaVacia, true);
            }
        }

        // ESCRIBIR ALUMNO EN EL ARCHIVOOOO
        public static void EscribirAlumnoEnArchivo(List<Alumno> listaAlumnos, bool concatenar)
        {
            using (StreamWriter sw = new StreamWriter(alumnosPath, concatenar))
            {
                foreach (Alumno alumno in listaAlumnos)
                {
                    sw.WriteLine($"{alumno.indice},{alumno.nombre},{alumno.apellido},{alumno.dni},{alumno.fechaNacimiento},{alumno.domicilio},{alumno.estaActivo}");
                }
            }
        }

        // TRAER ALUMNOOOOO DE ARCHIVOOOO
        public static List<Alumno> TraerAlumnosDeArchivo(string archivo)
        {
            List<Alumno> listaAlumnos = new List<Alumno>();

            if (!File.Exists(archivo))
            {
                using (StreamWriter sw = File.CreateText(archivo)) ;
            }

            using (StreamReader sr = new StreamReader(archivo))
            {
                string? linea = sr.ReadLine();
                while (linea != null)
                {
                    string[] alumnoCSV = linea.Split(',');
                    Alumno alumnoStruct = new Alumno();
                    alumnoStruct.indice = int.Parse(alumnoCSV[0]);
                    alumnoStruct.nombre = alumnoCSV[1];
                    alumnoStruct.apellido = alumnoCSV[2];
                    alumnoStruct.dni = int.Parse(alumnoCSV[3]);
                    alumnoStruct.fechaNacimiento = alumnoCSV[4];
                    alumnoStruct.domicilio = alumnoCSV[5];
                    alumnoStruct.estaActivo = bool.Parse(alumnoCSV[6]);
                    listaAlumnos.Add(alumnoStruct);
                    linea = sr.ReadLine();
                }
            }
            return listaAlumnos;
        }

        // BAJA ALUMNOOOOO
        public static void BajaAlumno(List<Alumno> listaAlumnos)
        {
            int dniAlumno = ValidarNumero("Ingrese el dni del alumno a dar de baja");

            if (listaAlumnos.Exists(elemento => elemento.dni == dniAlumno))
            {
                for (int i = 0; i < listaAlumnos.Count; i++)
                {
                    if (listaAlumnos[i].dni == dniAlumno)
                    {
                        Alumno alumno = listaAlumnos[i];
                        alumno.estaActivo = false;
                        Console.WriteLine("El alumno fue dado de baja correctamente");
                        listaAlumnos[i] = alumno;
                    }
                }
                EscribirAlumnoEnArchivo(listaAlumnos, false);
            }
            else
            {
                Console.WriteLine($"El alumno con el dni {dniAlumno} no existe");
            }
        }

        // MODIFICAR ALUMNOOOO
        static void ModificarAlumno(List<Alumno> listaAlumnos)
        {
            int dniIngresado = ValidarNumero("Ingrese el dni del alumno que quiere modifiar");
            if (listaAlumnos.Exists(alumno => alumno.dni == dniIngresado))
            {
                for (int i = 0; i < listaAlumnos.Count; i++)
                {
                    if (listaAlumnos[i].dni == dniIngresado)
                    {
                        Alumno alumnoModificado = listaAlumnos[i];
                        alumnoModificado.nombre = StringValido("Ingrese nombre del alumno");
                        alumnoModificado.apellido = StringValido("Ingrese apellido del alumno");
                        alumnoModificado.fechaNacimiento = fechaValida("de nacimiento");
                        alumnoModificado.domicilio = StringValido("Ingrese domicilio");
                        listaAlumnos[i] = alumnoModificado;
                        Console.Clear();
                        Console.WriteLine("Alumno modificado correctamente");
                        Console.WriteLine();
                    }
                }
                EscribirAlumnoEnArchivo(listaAlumnos, false);
            }
            else
            {
                Console.WriteLine("No se encontro ningun alumno con ese dni");
            }
        }


        // MENU DE ALUMNOSSSS
        static void MenuAlumnos()
        {
            string opcion;
            do
            {
                Console.WriteLine();
                Console.WriteLine("*************************************");
                Console.WriteLine("*                                   *");
                Console.WriteLine("*            MENU ALUMNOS           *");
                Console.WriteLine("*-----------------------------------*");
                Console.WriteLine("*        Ingrese una opcion         *");
                Console.WriteLine("*          1 - Alta alumno          *");
                Console.WriteLine("*          2 - Baja alumno          *");
                Console.WriteLine("*          3 - Modificacion alumno  *");
                Console.WriteLine("*          4 - Alumnos activos      *");
                Console.WriteLine("*          5 - Alumnos inactivos    *");
                Console.WriteLine("*                                   *");
                Console.WriteLine("*          0 - Volver               *");
                Console.WriteLine("*************************************");
                opcion = Console.ReadLine();
                Console.Clear();
                string linea = "INDICE".PadRight(10) + "NOMBRE".PadRight(20) + "APELLIDO".PadRight(15) + "DNI".PadRight(10) + "F.NAC".PadRight(15) + "DOMICILIO".PadRight(15) + "ACTIVO";
                List<Alumno> listaAlumnos = TraerAlumnosDeArchivo(alumnosPath);

                if (opcion == "1") //Alta alumno
                {
                    AltaAlumno();
                }
                else if (opcion == "2") // Baja alumno
                {
                    BajaAlumno(listaAlumnos);
                }
                else if (opcion == "3") // Modificacion alumno
                {
                    ModificarAlumno(listaAlumnos);
                }
                else if (opcion == "4") // Alumnos activos
                {
                    if (listaAlumnos.Count > 0)
                    {
                        Console.WriteLine("------------- ALUMNOS ACTIVOS --------------");
                        Console.WriteLine(linea);
                        foreach (Alumno alumno in listaAlumnos)
                        {
                            if (alumno.estaActivo == true)
                            {
                                Console.Write($"{alumno.indice.ToString().PadRight(10)}{alumno.nombre.PadRight(20)}{alumno.apellido.PadRight(15)}{alumno.dni.ToString().PadRight(10)}{alumno.fechaNacimiento.PadRight(15)}{alumno.domicilio.PadRight(15)}{alumno.estaActivo} ");
                                Console.WriteLine();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No hay alumnos ingresados");
                    }
                }
                else if (opcion == "5") // Alumnos inactivos
                {
                    if (listaAlumnos.Count > 0)
                    {

                        Console.WriteLine("------------- ALUMNOS INACTIVOS ------------");
                        Console.WriteLine(linea);
                        foreach (Alumno alumno in listaAlumnos)
                        {
                            if (alumno.estaActivo == false)
                            {
                                Console.Write($"{alumno.indice.ToString().PadRight(10)}{alumno.nombre.PadRight(20)}{alumno.apellido.PadRight(15)}{alumno.dni.ToString().PadRight(10)}{alumno.fechaNacimiento.PadRight(15)}{alumno.domicilio.PadRight(15)}{alumno.estaActivo} ");
                                Console.WriteLine();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No hay alumnos ingresados");
                    }
                }
            } while (opcion != "0");
        }

        // ---------------------------------------------------- MATERIASSSS -------------------------------------------------------
        // ---------------------------------------------------- MATERIASSSS -------------------------------------------------------
        // ---------------------------------------------------- MATERIASSSS -------------------------------------------------------
        // ---------------------------------------------------- MATERIASSSS -------------------------------------------------------



        // ALTA MATERIAAA
        public static void AltaMateria()
        {
            List<Materia> listaMateriasExistentes = new List<Materia>();
            List<Materia> listaMateriasVacia = new List<Materia>();
            int indiceMateria;

            if (TraerMateriasDeArchivo(materiasPath) != null && TraerMateriasDeArchivo(materiasPath).Count() > 0)
            {
                listaMateriasExistentes = TraerMateriasDeArchivo(materiasPath);
                indiceMateria = listaMateriasExistentes.Count;
            }
            else
            {
                indiceMateria = 0;
            }

            string nombreMateria = StringValido("Ingrese nombre de la materia");
            if (listaMateriasExistentes.Exists(materia => materia.nombreMateria.ToLower() == nombreMateria.ToLower()))
            {
                for (int i = 0; i < listaMateriasExistentes.Count; i++)
                {
                    if (listaMateriasExistentes[i].nombreMateria == nombreMateria)
                    {
                        if (listaMateriasExistentes[i].estaActiva == false)
                        {
                            Console.WriteLine("La materia se encuentra en la base de datos pero está desactivada");
                            char opcionActivar = ValidarCharSON("Desea activarla? s/n");
                            if (opcionActivar == 's' || opcionActivar == 'S')
                            {
                                Materia materiaAModificar = new Materia();
                                materiaAModificar = listaMateriasExistentes[i];
                                materiaAModificar.estaActiva = true;
                                listaMateriasExistentes[i] = materiaAModificar;
                                Console.Clear();
                                Console.WriteLine("Materia activada correctamente");
                            }
                            EscribirMateriaEnArchivo(listaMateriasExistentes, false);
                        }
                        else
                        {
                            Console.WriteLine($"ERROR: La materia {nombreMateria} ya existe");
                        }
                    }
                }
            }
            else
            {
                Materia materia = new Materia();
                materia.indice = ++indiceMateria;
                materia.nombreMateria = nombreMateria;
                materia.estaActiva = true;
                listaMateriasVacia.Add(materia);
                Console.Clear();
                Console.WriteLine($"Materia ingresada correctamente con el inidice {materia.indice}");
                Console.WriteLine();
                EscribirMateriaEnArchivo(listaMateriasVacia, true);
            }
        }

        // ESCRIBIR MATERIA EN ARCHIVOO
        public static void EscribirMateriaEnArchivo(List<Materia> listaMaterias, bool concat)
        {
            using (StreamWriter sw = new StreamWriter(materiasPath, concat))
            {
                foreach (Materia materia in listaMaterias)
                {
                    sw.WriteLine($"{materia.indice},{materia.nombreMateria},{materia.estaActiva}");
                }
            }
        }

        // TRAER MATERIA DE ARCHIVOOO
        public static List<Materia> TraerMateriasDeArchivo(string archivo)
        {
            List<Materia> listaMaterias = new List<Materia>();

            if (!File.Exists(archivo))
            {
                using (StreamWriter sw = File.CreateText(archivo)) ;
            }

            using (StreamReader sr = new StreamReader(archivo))
            {
                string? linea = sr.ReadLine();
                while (linea != null)
                {
                    string[] materiaCSV = linea.Split(',');
                    Materia materiaStruct = new Materia();
                    materiaStruct.indice = int.Parse(materiaCSV[0]);
                    materiaStruct.nombreMateria = materiaCSV[1];
                    materiaStruct.estaActiva = bool.Parse(materiaCSV[2]);
                    listaMaterias.Add(materiaStruct);
                    linea = sr.ReadLine();
                }
            }
            return listaMaterias;
        }

        // BAJA MATERIAAAA

        public static void BajaMateria(List<Materia> listaMaterias)
        {

            int indiceMateria = ValidarNumero("Ingrese el indice de la materia que quiere dar de baja");
            if (listaMaterias.Exists(materia => materia.indice == indiceMateria))
            {
                for (int i = 0; i < listaMaterias.Count; i++)
                {
                    if (listaMaterias[i].indice == indiceMateria)
                    {
                        Materia materia = listaMaterias[i];
                        materia.estaActiva = false;
                        Console.WriteLine("La materia fue dada de baja correctamente");
                        listaMaterias[i] = materia;
                    }
                }
                EscribirMateriaEnArchivo(listaMaterias, false);
            }
            else
            {
                Console.WriteLine("No existe una materia con ese indice");
            }
        }

        // MODIFICAR MATERIAAAAA
        public static void ModificarMateria(List<Materia> listaMaterias)
        {
            int opcionAModificar = ValidarNumero("Ingrese el indice de la materia a modifiar");

            if (listaMaterias.Exists(materia => materia.indice == opcionAModificar))
            {
                for (int i = 0; i < listaMaterias.Count; i++)
                {
                    if (listaMaterias[i].indice == opcionAModificar)
                    {
                        Materia materia = new Materia();
                        materia = listaMaterias[i];
                        string nuevoNombreMateria = StringValido("Ingrese el nombre de la materia");
                        if (listaMaterias.Exists(materia => materia.nombreMateria == nuevoNombreMateria))
                        {
                            Console.WriteLine("El nombre de la materia ya existe");
                            return;
                        }
                        else
                        {
                            materia.nombreMateria = nuevoNombreMateria;
                            Console.WriteLine("Materia modificada correctamente");
                            listaMaterias[i] = materia;
                        }
                    }
                }
                EscribirMateriaEnArchivo(listaMaterias, false);
            }
            else
            {
                Console.WriteLine("No se encontro una materia con ese indice");
            }
        }

        // MENU MATERIASSS
        static void MenuMaterias()
        {
            string? opcion;
            do
            {
                Console.WriteLine();
                Console.WriteLine("*************************************");
                Console.WriteLine("*                                   *");
                Console.WriteLine("*            MENU MATERIAS          *");
                Console.WriteLine("*-----------------------------------*");
                Console.WriteLine("*        Ingrese una opcion         *");
                Console.WriteLine("*          1 - Alta materia         *");
                Console.WriteLine("*          2 - Baja materia         *");
                Console.WriteLine("*          3 - Modificacion materia *");
                Console.WriteLine("*          4 - Mostrar materias     *");
                Console.WriteLine("*                                   *");
                Console.WriteLine("*          0 - Volver               *");
                Console.WriteLine("*************************************");
                opcion = Console.ReadLine();
                Console.Clear();
                string linea = "INDICE".PadRight(10) + "NOMBRE".PadRight(20) + "ACTIVA";
                List<Materia> listaMaterias = TraerMateriasDeArchivo(materiasPath);

                if (opcion == "1") // Alta materia
                {
                    AltaMateria();
                }
                else if (opcion == "2") // Baja materia
                {
                    BajaMateria(listaMaterias);
                }
                else if (opcion == "3") // Modificar materia
                {
                    ModificarMateria(listaMaterias);
                }
                else if (opcion == "4") // Mostrar materias
                {
                    if (listaMaterias.Count() != 0)
                    {

                        Console.WriteLine(linea);
                        foreach (Materia materia in listaMaterias)
                        {
                            Console.WriteLine($"{materia.indice.ToString().PadRight(10)}{materia.nombreMateria.PadRight(20)}{materia.estaActiva}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No hay materias ingresadas");
                    }
                }
            } while (opcion != "0");
        }


        // ---------------------------------------------------------- INSCRIPCIONES ------------------------------------------------------
        // ---------------------------------------------------------- INSCRIPCIONES ------------------------------------------------------
        // ---------------------------------------------------------- INSCRIPCIONES ------------------------------------------------------
        // ---------------------------------------------------------- INSCRIPCIONES ------------------------------------------------------
        // ---------------------------------------------------------- INSCRIPCIONES ------------------------------------------------------
        // ---------------------------------------------------------- INSCRIPCIONES ------------------------------------------------------

        // INSCRIBIR ALUMNO

        static void InscribirAlumno(List<Inscripcion> listaInscripcionExistente, List<Materia> listaMateriasExistente)
        {
            int dniIngresado = ValidarNumero("Ingrese el dni del alumno a inscribir");
            List<Alumno> listaAlumnosExistente = TraerAlumnosDeArchivo(alumnosPath);
            List<Inscripcion> nuevaInscripcion = new List<Inscripcion>();
            Alumno alumno = new Alumno();
            Materia materia = new Materia();
            bool existeAlumno = false;
            bool existeMateria = false;
            int indiceInscripcion = 0;

            if (TraerInscripcionDeArchivo(inscripcionPath) != null && TraerInscripcionDeArchivo(inscripcionPath).Count() > 0)
            {
                listaInscripcionExistente = TraerInscripcionDeArchivo(inscripcionPath);
                indiceInscripcion = listaInscripcionExistente.Count;
            }
            else
            {
                indiceInscripcion = 0;
            }


            if (listaAlumnosExistente.Exists(alumno => alumno.dni == dniIngresado))
            {
                for (int i = 0; i < listaAlumnosExistente.Count; i++)
                {
                    if (listaAlumnosExistente[i].dni == dniIngresado)
                    {
                        if (listaAlumnosExistente[i].estaActivo == true)
                        {
                            existeAlumno = true;
                            alumno = listaAlumnosExistente[i];
                        }
                        else
                        {
                            Console.WriteLine("El alumno está desactivado, por favor activelo o elija otro alumno");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"No existe un alumno con el dni {dniIngresado}");
            }

            if (existeAlumno)
            {

                int indiceMateria = ValidarNumero("Ingrese indice de la materia a inscribir al alumno");

                if (listaMateriasExistente.Exists(materia => materia.indice == indiceMateria))
                {
                    for (int i = 0; i < listaMateriasExistente.Count; i++)
                    {
                        if (listaMateriasExistente[i].indice == indiceMateria)
                        {
                            if (listaMateriasExistente[i].estaActiva == true)
                            {
                                existeMateria = true;
                                materia = listaMateriasExistente[i];
                            }
                            else
                            {
                                Console.WriteLine("La materia está desactivada por favor activela o elija otra materia");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"No existe una materia con el indice {indiceMateria}");
                }
            }

            if (existeMateria)
            {
                bool alumnoInscripto = listaInscripcionExistente.Exists(inscripcion => inscripcion.indice_alumno == alumno.indice && inscripcion.indice_materia == materia.indice);
                if (alumnoInscripto)
                {
                    Console.WriteLine("El alumno ya esta inscripto a esta materia, vaya a la seccion de modificacion");
                    return;
                    // o es mejor poner !alumnoInscripto y dejar alumnoInscipto true para el else?
                }

                Inscripcion inscripcion = new Inscripcion();
                inscripcion.indice = ++indiceInscripcion;
                inscripcion.indice_alumno = alumno.indice;
                inscripcion.indice_materia = materia.indice;
                char cursoChar = ValidarCharSON("El alumno cursó la materia? s/n");
                inscripcion.estado = "Anotado";
                if (cursoChar == 's' || cursoChar == 'S')
                {
                    char rindioChar = ValidarCharSON("El alumno rindió el final? s/n");
                    if (rindioChar == 's' || rindioChar == 'S')
                    {
                        double notaIngresada;
                        do
                        {
                            Console.WriteLine("Ingrese la nota del alumno entre 1 y 10");
                        } while (!double.TryParse(Console.ReadLine(), out notaIngresada) || (notaIngresada < 1 && notaIngresada > 10));

                        inscripcion.nota = notaIngresada;
                        if (notaIngresada >= 6 && notaIngresada <= 10)
                        {
                            inscripcion.estado = "Aprobado";
                        }
                        else
                        {
                            inscripcion.estado = "Desaprobado";
                        }
                        inscripcion.fecha = fechaValida("del parcial");
                    }
                    else
                    {
                        inscripcion.estado = "Cursado";
                    }
                }
                nuevaInscripcion.Add(inscripcion);
                Console.Clear();
                Console.WriteLine("Alumno inscripto correctamente");
                EscribirInscripcionEnArchivo(nuevaInscripcion, true);

            }

        }

        // ESCRIBIR INSCRIPCION EN ARCHIVO

        static void EscribirInscripcionEnArchivo(List<Inscripcion> listaInscripcion, bool concat)
        {
            using (StreamWriter sw = new StreamWriter(inscripcionPath, concat))
            {
                foreach (Inscripcion inscripcion in listaInscripcion)
                {
                    sw.WriteLine($"{inscripcion.indice},{inscripcion.indice_alumno},{inscripcion.indice_materia},{inscripcion.estado},{inscripcion.nota},{inscripcion.fecha}");
                }
            }
        }

        // LEER INSCRIPCION DE ARCHIVO

        static List<Inscripcion> TraerInscripcionDeArchivo(string archivo)
        {
            List<Inscripcion> listaInscripcion = new List<Inscripcion>();

            if (!File.Exists(archivo))
            {
                using (StreamWriter sw = File.CreateText(archivo)) ;
            }

            using (StreamReader sr = new StreamReader(archivo))
            {
                string? linea = sr.ReadLine();
                while (linea != null)
                {
                    string[] inscripcionCSV = linea.Split(',');
                    Inscripcion inscripcionStruct = new Inscripcion();
                    inscripcionStruct.indice = int.Parse(inscripcionCSV[0]);
                    inscripcionStruct.indice_alumno = int.Parse(inscripcionCSV[1]);
                    inscripcionStruct.indice_materia = int.Parse(inscripcionCSV[2]);
                    inscripcionStruct.estado = inscripcionCSV[3];
                    inscripcionStruct.nota = int.Parse(inscripcionCSV[4]);
                    inscripcionStruct.fecha = inscripcionCSV[5];
                    listaInscripcion.Add(inscripcionStruct);
                    linea = sr.ReadLine();
                }
            }
            return listaInscripcion;
        }

        // VER ESTADO DE INSCRIPCION DE UN ALUMNO

        static void EstadoInscripcionAlumno()
        {
            List<Materia> listaMaterias = TraerMateriasDeArchivo(materiasPath);
            List<Inscripcion> listaInscripcion = TraerInscripcionDeArchivo(inscripcionPath);
            int indiceAlumno = ValidarNumero("Ingrese indice del alumno para ver su estado");
            Console.Clear();
            if (listaInscripcion.Exists(inscr => inscr.indice_alumno == indiceAlumno))
            {
                string linea = "INDICE".PadRight(10) + "ALUMNO".PadRight(10) + "MATERIA".PadRight(20) + "ESTADO".PadRight(15) + "NOTA".PadRight(10) + "FECHA";
                Console.WriteLine(linea);
                string nombreMateria;
                
                List<Inscripcion> inscripcionesAlumno = new List<Inscripcion>();
                foreach (Inscripcion inscripcion in listaInscripcion)
                {
                    if (inscripcion.indice_alumno == indiceAlumno)
                    {
                        nombreMateria = "";
                        int i = 0;
                        bool encontre = false;
                        while (i < listaMaterias.Count && !encontre)
                        {
                            if (inscripcion.indice_materia == listaMaterias[i].indice)
                            {
                                encontre = true;
                                nombreMateria = listaMaterias[i].nombreMateria;
                            }
                            i++;
                        }
                        string notaCorregida = inscripcion.nota == 0 ? "-" : inscripcion.nota.ToString();
                        Console.WriteLine($"{inscripcion.indice.ToString().PadRight(10)}{inscripcion.indice_alumno.ToString().PadRight(10)}{nombreMateria.PadRight(20)}{inscripcion.estado.PadRight(15)}{notaCorregida.PadRight(10)}{inscripcion.fecha}");
                        inscripcionesAlumno.Add(inscripcion);
                    }
                }
                Console.WriteLine();
                char opcionBubble = ValidarCharSON("Desea ordenar las notas del alumno en forma decreciente? Elija una opción s/n");
                if (opcionBubble == 's' || opcionBubble == 'S')
                {
                    Console.Clear();
                    Console.WriteLine(linea);
                    for (int i = inscripcionesAlumno.Count; i > 0; i--)
                    {
                        for (int j = inscripcionesAlumno.Count - 1; j > 0; j--)
                        {
                            if (inscripcionesAlumno[j].nota > inscripcionesAlumno[j - 1].nota)
                            {
                                Inscripcion inscripcionTemp = inscripcionesAlumno[j];
                                inscripcionesAlumno[j] = inscripcionesAlumno[j - 1];
                                inscripcionesAlumno[j - 1] = inscripcionTemp;
                            }
                        }
                    }
                }
                foreach (Inscripcion inscripto in inscripcionesAlumno)
                {
                    nombreMateria = "";
                    int i = 0;
                    bool encontre = false;
                    while (i < listaMaterias.Count && !encontre)
                    {
                        if (inscripto.indice_materia == listaMaterias[i].indice)
                        {
                            encontre = true;
                            nombreMateria = listaMaterias[i].nombreMateria;
                        }
                        i++;
                    }
                    string notaCorregida = inscripto.nota == 0 ? "-" : inscripto.nota.ToString();
                    Console.WriteLine($"{inscripto.indice.ToString().PadRight(10)}{inscripto.indice_alumno.ToString().PadRight(10)}{nombreMateria.PadRight(20)}{inscripto.estado.PadRight(15)}{notaCorregida.PadRight(10)}{inscripto.fecha}");
                }
            }
            else
            {
                Console.WriteLine("No se encontro ningun alumno inscripto con ese indice");
            }
        }

        // MODIFICAR INSCRIPCION

        static void ModificarInscripcion(List<Inscripcion> listaInscripcion, string linea)
        {
            int indiceAlumno = ValidarNumero("Ingrese el indice del alumno a modificar");

            if (listaInscripcion.Exists(inscr => inscr.indice_alumno == indiceAlumno))
            {
                Console.Clear();
                Console.WriteLine(linea);
                foreach (Inscripcion inscripcion in listaInscripcion)
                {
                    if (inscripcion.indice_alumno == indiceAlumno)
                    {
                        string notaCorregida = inscripcion.nota == 0 ? "-" : inscripcion.nota.ToString();
                        Console.WriteLine($"{inscripcion.indice.ToString().PadRight(10)}{inscripcion.indice_alumno.ToString().PadRight(10)}{inscripcion.indice_materia.ToString().PadRight(20)}{inscripcion.estado.PadRight(15)}{notaCorregida.PadRight(10)}{inscripcion.fecha}");
                    }
                }
                Console.WriteLine();
                int indiceMateria = ValidarNumero("Ingrese el indice de la materia a modificar");

                if (listaInscripcion.Exists(materia => materia.indice_materia == indiceMateria))
                {
                    for (int i = 0; i < listaInscripcion.Count; i++)
                    {
                        if (listaInscripcion[i].indice_materia == indiceMateria && listaInscripcion[i].indice_alumno == indiceAlumno)
                        {
                            Inscripcion inscripcionMod = listaInscripcion[i];
                            char cursoChar = ValidarCharSON("El alumno cursó la materia? s/n");
                            inscripcionMod.estado = "Anotado";
                            inscripcionMod.nota = 0;
                            inscripcionMod.fecha = "";
                            if (cursoChar == 's' || cursoChar == 'S')
                            {
                                char rindioChar = ValidarCharSON("El alumno rindió el final? s/n");
                                if (rindioChar == 's' || rindioChar == 'S')
                                {
                                    double notaIngresada;
                                    do
                                    {
                                        Console.WriteLine("Ingrese la nota del alumno entre 1 y 10");
                                    } while (!double.TryParse(Console.ReadLine(), out notaIngresada) || (notaIngresada < 1 && notaIngresada > 10));

                                    inscripcionMod.nota = notaIngresada;
                                    if (notaIngresada >= 6 && notaIngresada <= 10)
                                    {
                                        inscripcionMod.estado = "Aprobado";
                                    }
                                    else
                                    {
                                        inscripcionMod.estado = "Desaprobado";
                                    }
                                    inscripcionMod.fecha = fechaValida("del parcial");
                                }
                                else
                                {
                                    inscripcionMod.estado = "Cursado";
                                }
                            }
                            listaInscripcion[i] = inscripcionMod;
                            Console.Clear();
                            Console.WriteLine("Alumno modificado con exito");
                            EscribirInscripcionEnArchivo(listaInscripcion, false);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No existe la materia con el indice ingresado");
                }

            }
            else
            {
                Console.WriteLine("No se encontro ningun alumno inscripto con ese indice");
            }
        }

        // MENU INSCRIPCIONES

        static void MenuInscripciones()
        {
            string? opcion;
            do
            {
                Console.WriteLine();
                Console.WriteLine("*************************************");
                Console.WriteLine("*                                   *");
                Console.WriteLine("*         Menu Inscripciones        *");
                Console.WriteLine("*-----------------------------------*");
                Console.WriteLine("*        Ingrese una opcion         *");
                Console.WriteLine("*          1 - Inscribir alumno     *");
                Console.WriteLine("*          2 - Estado alunno        *");
                Console.WriteLine("*          3 - Modificar estado     *");
                Console.WriteLine("*          4 - Mostar todo          *");
                Console.WriteLine("*                                   *");
                Console.WriteLine("*          0 - Volver               *");
                Console.WriteLine("*************************************");
                opcion = Console.ReadLine();
                Console.Clear();
                List<Inscripcion> listaInscripcion = TraerInscripcionDeArchivo(inscripcionPath);
                List<Materia> listaMaterias = TraerMateriasDeArchivo(materiasPath);
                string linea = "INDICE".PadRight(10) + "ALUMNO".PadRight(10) + "MATERIA".PadRight(20) + "ESTADO".PadRight(15) + "NOTA".PadRight(10) + "FECHA";
                if (opcion == "1") // INSCRIBIR ALUMNO
                {
                    InscribirAlumno(listaInscripcion, listaMaterias);
                }
                else if (opcion == "2") // ESTADO INSCRIPCION DE UN ALUMNO
                {
                    EstadoInscripcionAlumno();

                }
                else if (opcion == "3") // MODIFICAR INSCRIPCION
                {
                    ModificarInscripcion(listaInscripcion, linea);
                }
                else if (opcion == "4") // MOSTRAR TODAS LAS INSCRIPCIONES
                {
                    Console.WriteLine(linea);
                    string nombreMateria;
                    foreach (Inscripcion inscripcion in listaInscripcion)
                    {
                        nombreMateria = "";
                        int i = 0;
                        bool encontre = false;
                        while (i < listaMaterias.Count && !encontre)
                        {
                            if (inscripcion.indice_materia == listaMaterias[i].indice)
                            {
                                encontre = true;
                                nombreMateria = listaMaterias[i].nombreMateria;
                            }
                            i++;
                        }

                        string notaCorregida = inscripcion.nota == 0 ? "-" : inscripcion.nota.ToString();
                        Console.WriteLine($"{inscripcion.indice.ToString().PadRight(10)}{inscripcion.indice_alumno.ToString().PadRight(10)}{nombreMateria.PadRight(20)}{inscripcion.estado.PadRight(15)}{notaCorregida.PadRight(10)}{inscripcion.fecha}");
                    }
                }
            } while (opcion != "0");
        }


        // ---------------------------------------------------------- MAIN ------------------------------------------------------
        // ---------------------------------------------------------- MAIN ------------------------------------------------------
        // ---------------------------------------------------------- MAIN ------------------------------------------------------
        // ---------------------------------------------------------- MAIN ------------------------------------------------------
        // ---------------------------------------------------------- MAIN ------------------------------------------------------
        // ---------------------------------------------------------- MAIN ------------------------------------------------------

        // MAINNNNN
        static void Main(string[] args)
        {
            string? opcion;
            do
            {
                Console.WriteLine();
                Console.WriteLine("*************************************");
                Console.WriteLine("*                                   *");
                Console.WriteLine("*             OPCIONES              *");
                Console.WriteLine("*-----------------------------------*");
                Console.WriteLine("*        Ingrese una opcion         *");
                Console.WriteLine("*          1 - Alumnos              *");
                Console.WriteLine("*          2 - Materias             *");
                Console.WriteLine("*          3 - Inscripcion          *");
                Console.WriteLine("*                                   *");
                Console.WriteLine("*          0 - Salir                *");
                Console.WriteLine("*************************************");
                opcion = Console.ReadLine();
                Console.Clear();
                if (opcion == "1")
                {
                    MenuAlumnos();
                }
                else if (opcion == "2")
                {
                    MenuMaterias();
                }
                else if (opcion == "3")
                {
                    MenuInscripciones();
                }
            } while (opcion != "0");

        }
    }
}