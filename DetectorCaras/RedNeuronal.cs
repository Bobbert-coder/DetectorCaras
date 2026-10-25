using System;
using System.Linq;

namespace DetectorCaras
{
    public class RedNeuronal
    {
        /// <summary>
        /// Método dinámico y genérico para reconocer cualquier conjunto de patrones bipolares (1 y -1).
        /// </summary>
        /// <param name="patrones">Matriz de entrenamiento (M patrones, cada uno de tamaño N)</param>
        /// <param name="patronRuidoso">Vector de entrada a clasificar (tamaño N)</param>
        /// <param name="maxIteraciones">Número de iteraciones para la convergencia (por defecto 15)</param>
        /// <returns>Índice del patrón almacenado con menor distancia (0 a M-1)</returns>
        public static int ReconocerPatron(int[][] patrones, int[] patronRuidoso, int maxIteraciones = 15)
        {
            if (patrones == null || patrones.Length == 0)
                throw new ArgumentException("La matriz de patrones no puede estar vacía.");

            int M = patrones.Length;          // Número de patrones/clases almacenados
            int N = patrones[0].Length;       // Número de neuronas/elementos por vector

            if (patronRuidoso.Length != N)
                throw new ArgumentException($"El vector de entrada ({patronRuidoso.Length}) debe coincidir con el tamaño de los patrones entrenados ({N}).");

            // 1. Matriz de Pesos (Usamos int[,] para optimizar la memoria)
            int[,] W = new int[N, N];

            for (int k = 0; k < M; k++)
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        if (i != j) // La diagonal principal se mantiene en 0 (sin auto-conexiones)
                        {
                            W[i, j] += patrones[k][i] * patrones[k][j];
                        }
                    }
                }
            }

            // 2. Iteración y convergencia del patrón de entrada
            int[] estadoActual = (int[])patronRuidoso.Clone();
            int[] estadoSiguiente = new int[N];

            for (int iter = 0; iter < maxIteraciones; iter++)
            {
                for (int i = 0; i < N; i++)
                {
                    int suma = 0;
                    for (int j = 0; j < N; j++)
                    {
                        suma += W[i, j] * estadoActual[j];
                    }
                    // Función de activación Bipolar
                    if (suma > 0)
                        estadoSiguiente[i] = 1;
                    else if (suma < 0)
                        estadoSiguiente[i] = -1;
                    else
                        estadoSiguiente[i] = estadoActual[i];
                }

                Array.Copy(estadoSiguiente, estadoActual, N);
            }

            // 3. Comparación con los patrones originales (Distancia Euclídea / Norma L2)
            // 1. Define un umbral adecuado según tu extractor de características
            // (por ejemplo: para embeddings de 128D normalizados, suele ser entre 0.4 y 0.6)
            const double UMBRAL_DISTANCIA = 0.4;

            double menorDistancia = double.MaxValue;
            int patronMasCercano = -1;

            for (int k = 0; k < M; k++)
            {
                double sumaCuadrados = 0;
                for (int i = 0; i < N; i++)
                {
                    double diff = estadoActual[i] - patrones[k][i];
                    sumaCuadrados += diff * diff;
                }
                double distancia = Math.Sqrt(sumaCuadrados);

                if (distancia < menorDistancia)
                {
                    menorDistancia = distancia;
                    patronMasCercano = k;
                }
            }

            // 2. Validar si la distancia encontrada realmente corresponde al mismo rostro
            if (menorDistancia > UMBRAL_DISTANCIA)
            {
                return -1; // -1 indica "Rostro Desconocido / No registrado"
            }

            return patronMasCercano;
        }
    }
}