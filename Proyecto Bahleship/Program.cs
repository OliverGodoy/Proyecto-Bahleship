using System;
using System.Threading;

// Diseño simple en la consola
Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\t\t\t\t╔══════════════════════════════════════════════════════╗");
Console.WriteLine("\t\t\t\t║                 Bienvenido al juego                  ║");
Console.WriteLine("\t\t\t\t║                     Battleship                       ║");
Console.WriteLine("\t\t\t\t╚══════════════════════════════════════════════════════╝");
Thread.Sleep(3500);
Console.Clear();

Console.WriteLine("\n\n\n\n\n\n\n\n\n\t\t\t\t\t¡CREANDO CAMPO DE BATALLA.........!");
Thread.Sleep(3000);
Console.Clear();

int tirosAcertados = 0;
int fila, columna;
char[,] Mapa = new char[20, 20];
char[,] Mapa2 = new char[20, 20];   
LlenarMapa(Mapa);
LlenarMapa(Mapa2);
ColocarBarcos(Mapa2);
MostrarMapa(Mapa);

int intentos = 5;
do
{
    Console.WriteLine("\t\t\t¡Ingresa -1 en la fila si quieres salir del programa....!");
    Console.WriteLine($"Tienes {intentos} intentos restantes");
    Console.Write("Ingrese la fila: ");
    if (!int.TryParse(Console.ReadLine(), out fila))
    {
        Console.WriteLine("¡Entrada inválida. Por favor ingresa un número válido.!");
        continue;
    }

    if (fila == -1)
    {
        Console.Write("\t\t\t¡Saliendo del programa.......!");
        Console.ReadKey ();
        break; // Salir del bucle si se ingresa -1
    }

    Console.Write("Ingrese la columna: ");
    if (!int.TryParse(Console.ReadLine(), out columna))
    {
        Console.WriteLine("¡Entrada inválida. Por favor ingresa un número válido.!");
        continue;
    }

    if (Mapa[fila, columna] == 'X' || Mapa[fila, columna] == 'W')
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("¡Ya has disparado en esta posición!");
        Console.WriteLine("¡No se restaran intentos, vuelve a ingresar la posición.......!");
        Console.ResetColor();
        Console.ReadKey();
        continue; // Evitar restar un intento si la posición ya ha sido disparada
    }

    Disparo(Mapa, Mapa2, fila, columna);
    if (Mapa[fila, columna] != 'W')
    {
        intentos -= 1;
    }
    else
    {
        intentos++;
        tirosAcertados++;
    }
    
    Console.Write("¡DISPARANDO!");
    Console.ReadKey();
    Console.Clear();
    MostrarMapa(Mapa);
        
} while (intentos >0);
// Mostrar resumen al finalizar los intentos
Console.WriteLine("\nResumen del Juego:");
Console.WriteLine($"Número total de disparos acertados: {tirosAcertados }");


static void LlenarMapa(char[,] Mapa)
{
    for (int i = 0; i < 20; i++)
    {
        for (int j = 0; j < 20; j++)
        {
            Mapa[i, j] = 'O';
        }
    }
}

static void MostrarMapa(char[,] Mapa)
{
    // Cambiar el color de los encabezados
    Console.ForegroundColor = ConsoleColor.Red;

    // Encabezado horizontal
    Console.Write("    ");
    for (int i = 0; i < 20; i++)
    {
        Console.Write("{0,3}", i);
    }
    Console.WriteLine("\n_________________________________________________________________");
    Console.ResetColor();

    
    // Encabezado vertical y contenido de la matriz
    for (int i = 0; i < 20; i++)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("{0,3}|", i);
        Console.ResetColor();
        for (int j = 0; j < 20; j++)
        {
            if (Mapa[i, j] == 'X')
            {
                Console.ForegroundColor = ConsoleColor.Blue; // Cambiar el color de las celdas con 'X' a azul
                Console.Write("{0,3}", Mapa[i, j]);
                Console.ResetColor(); // Restablecer el color después de mostrar la celda

            }
            else if (Mapa[i, j] == 'W')
            {
                Console.ForegroundColor = ConsoleColor.Green; // Cambiar el color de las celdas con 'X' a verde
                Console.Write("{0,3}", Mapa[i, j]);
                Console.ResetColor(); // Restablecer el color después de mostrar la celda

            }
            else
            {
                Console.Write("{0,3}", Mapa[i, j]);// Mostrar 'O' en lugar de 'M' para ocultar los barcos
            }
        }
        Console.WriteLine(); // Salto de línea al final de cada fila
    }
    Console.ResetColor();
}



static void Disparo(char[,] Mapa, char[,]Mapa2, int fila, int columna)
{
    if (fila >= 0 && fila < 20 && columna >= 0 && columna < 20)
    {
        if (Mapa2[fila, columna] == 'P' || Mapa2[fila, columna] == 'F' || Mapa2[fila, columna] == 'N')
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("¡Felicidades, acertaste al barco!");
            Console.WriteLine("¡Se agregará un disparo más!");
            Mapa[fila, columna] = 'W';
            Console.ResetColor();
        }
        else
        {
            Mapa[fila, columna] = 'X'; // Marcar en Mapa aunque no haya acertado
        }

    }
    else
    {
        Console.WriteLine("\tPosición fuera de rango.");
        Console.WriteLine("\tVuelve a ingresar el valor");
        Console.ReadKey();
        Console.Clear();
    }

}

static void ColocarBarcos(char[,]Mapa2)
{
    Random rnd = new Random();

    // Colocar 10 fragatas de 1 espacio
    for (int fragata = 0; fragata < 10; fragata++)
    {
        int fila = rnd.Next(0, 20);
        int columna = rnd.Next(0, 20);

        if (Mapa2[fila, columna] == 'O')
        {
            Mapa2[fila, columna] = 'F'; // 'F' representa una fragata
        }
        else
        {
            fragata--;
        }
    }

    // Colocar 1 portaviones de 3 espacios
    for (int portaviones = 0; portaviones < 5; portaviones++)
    {
        int fila = rnd.Next(0, 20);
        int columna = rnd.Next(0, 18); // Asegurar espacio suficiente para el portaviones

        if (Mapa2[fila, columna] == 'O' && Mapa2[fila, columna + 1] == 'O' && Mapa2[fila, columna + 2] == 'O')
        {
            Mapa2[fila, columna] = 'P'; // 'P' representa un portaviones
            Mapa2[fila, columna + 1] = 'P';
            Mapa2[fila, columna + 2] = 'P';
        }
        else
        {
            portaviones--;
        }
    }

    // Colocar 3 navíos de 2 espacios
    for (int navio = 0; navio < 8; navio++)
    {
        int fila = rnd.Next(0, 20);
        int columna = rnd.Next(0, 19); // Asegurar espacio suficiente para el navío

        if (Mapa2[fila, columna] == 'O' && Mapa2[fila, columna + 1] == 'O')
        {
            Mapa2[fila, columna] = 'N'; // 'N' representa un navío
            Mapa2[fila, columna + 1] = 'N';
        }
        else
        {
            navio--;
        }
    }
    
}

