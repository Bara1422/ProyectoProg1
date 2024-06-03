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

        // ------------------------------------------------------------ ALUMNOSSS ------------------------------------------------------
        // ------------------------------------------------------------ ALUMNOSSS ------------------------------------------------------
        // ------------------------------------------------------------ ALUMNOSSS ------------------------------------------------------
        // ------------------------------------------------------------ ALUMNOSSS ------------------------------------------------------
        // ------------------------------------------------------------ ALUMNOSSS ------------------------------------------------------
        // ------------------------------------------------------------ ALUMNOSSS ------------------------------------------------------

        // DAR DE ALTA UN ALUMNOOOOO
        static void AltaAlumno(ref Alumno nuevoAlumno)
        {
            List<Alumno> listita = new List<Alumno>();
            List<Alumno> listaVacia = new List<Alumno>();
            int indiceDeAlumno;

            if (TraerAlumnosDeArchivo(alumnosPath) != null && TraerAlumnosDeArchivo(alumnosPath).Count() > 0)
            {
                listita = TraerAlumnosDeArchivo(alumnosPath);
                indiceDeAlumno = listita.Count;
            }
            else
            {
                indiceDeAlumno = 0;
            }

            Alumno alumno = new Alumno();
            int dni;
            do
            {
                Console.WriteLine("Ingrese DNI");
            } while (!int.TryParse(Console.ReadLine(), out dni));

            if (listita.Exists(alumno => alumno.dni == dni))
            {
                for (int i = 0; i < listita.Count; i++)
                {
                    if (listita[i].dni == dni)
                    {
                        if (listita[i].estaActivo == false)
                        {
                            Console.WriteLine("El alumno con el dni ingresado se encuentra en la base de datos pero está desactivado");
                            char opcionElegida;
                            do
                            {
                                Console.WriteLine("Desea activarlo? s/n");
                            } while (!char.TryParse(Console.ReadLine(), out opcionElegida) || (opcionElegida != 's' && opcionElegida != 'n'));
                            if (opcionElegida == 's')
                            {
                                Alumno al = listita[i];
                                al.estaActivo = true;
                                listita[i] = al;
                                Console.WriteLine("Alumno activado correctamente");
                            }
                            EscribirAlumnoEnArchivo(listita, false);
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
                Console.WriteLine("Ingrese nombre del alumno");
                alumno.nombre = Console.ReadLine();
                Console.WriteLine("Ingrese apellido del alumno");
                alumno.apellido = Console.ReadLine();
                alumno.dni = dni;
                Console.WriteLine("Ingrese fecha de nacimiento en el formato dd/mm/yy");
                alumno.fechaNacimiento = Console.ReadLine();
                Console.WriteLine("Ingrese domicilio");
                alumno.domicilio = Console.ReadLine();
                alumno.estaActivo = true;
                nuevoAlumno = alumno;
                listaVacia.Add(nuevoAlumno);
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
        public static void BajaAlumno()
        {
            List<Alumno> listaAlumnos = TraerAlumnosDeArchivo(alumnosPath);
            int dniAlumno;
            do
            {
                Console.WriteLine("Ingrese el dni del alumno a dar de baja");
            } while (!int.TryParse(Console.ReadLine(), out dniAlumno));

            if (listaAlumnos.Any(elemento => elemento.dni == dniAlumno))
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
            int dniIngresado;
            do
            {
                Console.WriteLine("Ingrese el dni del alumno que quiere modificar");
            } while (!int.TryParse(Console.ReadLine(), out dniIngresado));
            if (listaAlumnos.Exists(alumno => alumno.dni == dniIngresado))
            {
                for (int i = 0; i < listaAlumnos.Count; i++)
                {
                    if (listaAlumnos[i].dni == dniIngresado)
                    {
                        Alumno alumnoModificado = listaAlumnos[i];
                        Console.WriteLine("Ingrese nombre del alumno");
                        alumnoModificado.nombre = Console.ReadLine();
                        Console.WriteLine("Ingrese apellido del alumno");
                        alumnoModificado.apellido = Console.ReadLine();
                        int dni;
                        do
                        {
                            Console.WriteLine("Ingrese DNI");
                        } while (!int.TryParse(Console.ReadLine(), out dni));
                        // TODO ingresa dni de otro alumno
                        // for(int j = 0;i < listaAlumnos.Count; i++)
                        // {
                        //     if (listaAlumnos[j].dni)
                        // }
                        alumnoModificado.dni = dni;
                        Console.WriteLine("Ingrese fecha de nacimiento en el formato dd/mm/yy");
                        alumnoModificado.fechaNacimiento = Console.ReadLine();
                        Console.WriteLine("Ingrese domicilio");
                        alumnoModificado.domicilio = Console.ReadLine();

                        listaAlumnos[i] = alumnoModificado;
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
                Console.WriteLine("*          0 - Salir                *");
                Console.WriteLine("*************************************");
                opcion = Console.ReadLine();
                Console.Clear();
                string linea = "INDICE".PadRight(10) + "NOMBRE".PadRight(20) + "APELLIDO".PadRight(15) + "DNI".PadRight(10) + "F.NAC".PadRight(15) + "DOMICILIO".PadRight(15) + "ACTIVO";
                List<Alumno> listaAlumnos = TraerAlumnosDeArchivo(alumnosPath);

                if (opcion == "1") //Alta alumno
                {
                    Alumno nuevoAlumno = new Alumno();
                    AltaAlumno(ref nuevoAlumno);
                }
                else if (opcion == "2") // Baja alumno
                {
                    BajaAlumno();
                }
                else if (opcion == "3") // Modificacion alumno
                {
                    //List<Alumno> listaAlumnos = TraerAlumnosDeArchivo(alumnosPath);
                    ModificarAlumno(listaAlumnos);
                }
                else if (opcion == "4") // Alumnos activos
                {
                    Console.WriteLine("------------- ALUMNOS ACTIVOS --------------");
                    //List<Alumno> listaAlumnos = TraerAlumnosDeArchivo(alumnosPath);
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
                else if (opcion == "5") // Alumnos inactivos
                {
                    Console.WriteLine("------------- ALUMNOS INACTIVOS ------------");
                    //List<Alumno> listaAlumnos = TraerAlumnosDeArchivo(alumnosPath);
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
            } while (opcion != "0");
        }

        // ---------------------------------------------------- MATERIASSSS -------------------------------------------------------
        // ---------------------------------------------------- MATERIASSSS -------------------------------------------------------
        // ---------------------------------------------------- MATERIASSSS -------------------------------------------------------
        // ---------------------------------------------------- MATERIASSSS -------------------------------------------------------



        // ALTA MATERIAAA
        public static void AltaMateria(ref Materia nuevaMateria)
        {
            List<Materia> listaMaterias = new List<Materia>();
            List<Materia> listaMateriasVacia = new List<Materia>();
            int indiceMateria;

            if (TraerMateriasDeArchivo(materiasPath) != null && TraerMateriasDeArchivo(materiasPath).Count() > 0)
            {
                listaMaterias = TraerMateriasDeArchivo(materiasPath);
                indiceMateria = listaMaterias.Count;
            }
            else
            {
                indiceMateria = 0;
            }
            Console.WriteLine("Ingrese nombre de la materia");
            string? nombreMateria = Console.ReadLine();
            if (listaMaterias.Exists(materia => materia.nombreMateria.ToLower() == nombreMateria.ToLower()))
            {
                for (int i = 0; i < listaMaterias.Count; i++)
                {
                    if (listaMaterias[i].nombreMateria == nombreMateria)
                    {
                        if (listaMaterias[i].estaActiva == false)
                        {
                            Console.WriteLine("La materia se encuentra en la base de datos pero está desactivada");
                            char opcionActivar;
                            do
                            {
                                Console.WriteLine("Desea activarla? s/n");
                            } while (!char.TryParse(Console.ReadLine(), out opcionActivar) || (opcionActivar != 's' && opcionActivar != 'n'));
                            if (opcionActivar == 's')
                            {
                                Materia materiaAModificar = new Materia();
                                materiaAModificar = listaMaterias[i];
                                materiaAModificar.estaActiva = true;
                                listaMaterias[i] = materiaAModificar;
                                Console.WriteLine("Materia activada correctamente");
                            }
                            EscribirMateriaEnArchivo(listaMaterias, false);
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
                Console.WriteLine("Materia ingresada correctamente");
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

        public static void BajaMateria()
        {
            List<Materia> materias = TraerMateriasDeArchivo(materiasPath);
            int indiceMateria;
            do
            {
                Console.WriteLine("Ingrese el indice de la materia que quiere dar de baja");
            } while (!int.TryParse(Console.ReadLine(), out indiceMateria));
            if (materias.Exists(materia => materia.indice == indiceMateria))
            {
                for (int i = 0; i < materias.Count; i++)
                {
                    if (materias[i].indice == indiceMateria)
                    {
                        Materia materia = materias[i];
                        materia.estaActiva = false;
                        Console.WriteLine("La materia fue dada de baja correctamente");
                        materias[i] = materia;
                    }
                }
                EscribirMateriaEnArchivo(materias, false);
            }
            else
            {
                Console.WriteLine("No existe una materia con ese indice");
            }
        }

        // MODIFICAR MATERIAAAAA
        public static void ModificarMateria(List<Materia> listaMaterias)
        {
            int opcionAModificar;
            do
            {
                Console.WriteLine("Ingrese el indice de la materia a modificar");
            } while (!int.TryParse(Console.ReadLine(), out opcionAModificar));

            if (listaMaterias.Exists(materia => materia.indice == opcionAModificar))
            {
                for (int i = 0; i < listaMaterias.Count; i++)
                {
                    if (listaMaterias[i].indice == opcionAModificar)
                    {
                        Materia materia = new Materia();
                        materia = listaMaterias[i];
                        Console.WriteLine("Ingrese el nombre de la materia");
                        materia.nombreMateria = Console.ReadLine();
                        // TODO NOMBRE MATERIA NO DEBE ESTAR DUPLICADO
                        Console.WriteLine("Materia modificada correctamente");
                        listaMaterias[i] = materia;
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
                Console.WriteLine("*          0 - Salir                *");
                Console.WriteLine("*************************************");
                opcion = Console.ReadLine();
                Console.Clear();
                string linea = "INDICE".PadRight(10) + "NOMBRE".PadRight(20) + "ACTIVA";
                List<Materia> listaMaterias = TraerMateriasDeArchivo(materiasPath);
                if (opcion == "1")
                {
                    Materia nuevaMateria = new Materia();
                    AltaMateria(ref nuevaMateria);
                }
                else if (opcion == "2")
                {
                    BajaMateria();
                }
                else if (opcion == "3")
                {
                    Console.WriteLine("Modificar materia");
                    ModificarMateria(listaMaterias);
                }
                else if (opcion == "4")
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

        static void InscribirAlumno()
        {
            int dniIngresado;
            do
            {
                Console.WriteLine("Ingrese el dni del alumno");
            } while (!int.TryParse(Console.ReadLine(), out dniIngresado));
            List<Alumno> listaAlumnos = TraerAlumnosDeArchivo(alumnosPath);
            List<Materia> listaMaterias = TraerMateriasDeArchivo(materiasPath);
            List<Inscripcion> nuevaInscripcion = new List<Inscripcion>();
            Alumno alumno = new Alumno();
            Materia materia = new Materia();
            bool existeAlumno = false;
            bool existeMateria = false;
            int indiceInscripcion = 0;

            if (listaAlumnos.Exists(alumno => alumno.dni == dniIngresado))
            {
                for (int i = 0; i < listaAlumnos.Count; i++)
                {
                    if (listaAlumnos[i].dni == dniIngresado)
                    {
                        existeAlumno = true;
                        alumno = listaAlumnos[i];
                    }
                }
            }
            else
            {
                Console.WriteLine($"No existe un alumno con el dni {dniIngresado}");
            }

            if (existeAlumno)
            {
                int indiceMateria;
                do
                {
                    Console.WriteLine("Ingrese indice de la materia a inscribirse"); // o ya inscripto?
                } while (!int.TryParse(Console.ReadLine(), out indiceMateria));

                if (listaMaterias.Exists(materia => materia.indice == indiceMateria))
                {
                    for (int i = 0; i < listaMaterias.Count; i++)
                    {
                        if (listaMaterias[i].indice == indiceMateria)
                        {
                            existeMateria = true;
                            materia = listaMaterias[i];
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
                Inscripcion inscripcion = new Inscripcion();
                inscripcion.indice = ++indiceInscripcion;
                inscripcion.indice_alumno = alumno.indice;
                inscripcion.indice_materia = materia.indice;
                inscripcion.estado = "Anotado";
                nuevaInscripcion.Add(inscripcion);
                Console.WriteLine($"{inscripcion.indice},{inscripcion.indice_alumno}, {inscripcion.indice_materia}, {inscripcion.estado}, {inscripcion.nota}, {inscripcion.fecha}");
                Console.WriteLine("Alumno inscripto correctamente");
                EscribirInscripcionEnArchivo(nuevaInscripcion, true);
            }

        }

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

        static void EstadoInscripcionAlumno()
        {
            List<Inscripcion> listaInscripcion = TraerInscripcionDeArchivo(inscripcionPath);
            int indiceAlumno;
            do
            {
                Console.WriteLine("Ingrese indice del alumno para ver su estado");
            } while (!int.TryParse(Console.ReadLine(), out indiceAlumno));

            if(listaInscripcion.Exists(inscr => inscr.indice_alumno == indiceAlumno))
            {
                for(int i = 0; i < listaInscripcion.Count; i++)
                {
                    if (listaInscripcion[i].indice_alumno == indiceAlumno)
                    {
                        Inscripcion inscripcion = listaInscripcion[i];
                        Console.WriteLine("Ingrese ");
                    }
                }
            }else
            {
                Console.WriteLine("No se encontro ningun alumno con ese indice");
            }
            
        }

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
                Console.WriteLine("*          0 - Salir                *");
                Console.WriteLine("*************************************");
                opcion = Console.ReadLine();
                Console.Clear();
                List<Inscripcion> listaInscripcion = TraerInscripcionDeArchivo(inscripcionPath);
                if (opcion == "1")
                {
                    InscribirAlumno();
                }
                else if (opcion == "2")
                {
                    EstadoInscripcionAlumno();
                }
                else if (opcion == "3")
                {
                    Console.WriteLine("Modificar estado");
                }
                else if (opcion == "4")
                {
                    string linea = "INDICE".PadRight(10) + "ALUMNO".PadRight(10) + "MATERIA".PadRight(10) + "ESTADO".PadRight(15) + "NOTA".PadRight(10) + "FECHA";
                    Console.WriteLine(linea);
                    foreach (Inscripcion inscripcion in listaInscripcion)
                    {
                        Console.WriteLine($"{inscripcion.indice.ToString().PadRight(10)}{inscripcion.indice_alumno.ToString().PadRight(10)}{inscripcion.indice_materia.ToString().PadRight(10)}{inscripcion.estado.PadRight(15)}{inscripcion.nota.ToString().PadRight(10)}{inscripcion.fecha}");
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

// Que tiene que hacer la seccion inscripcion?
// tiene que hacer un crud o solo ingresar y leer
// tengo que ingresar la fecha 1x1? o formato dd/mm/yyyy
// nota al estar anotado me aparece en 0
// si modifica, al modificarlo como se a cual materia apunta?