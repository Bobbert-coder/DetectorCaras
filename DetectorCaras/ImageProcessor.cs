using System;
using System.Drawing;

public static class ImageProcessor
{
    /// <summary>
    /// Convierte una imagen a una matriz binaria (0 y 1).
    /// </summary>
    /// <param name="original">Imagen de origen</param>
    /// <param name="width">Ancho de salida para la matriz</param>
    /// <param name="height">Alto de salida para la matriz</param>
    /// <param name="threshold">Umbral de brillo (0 a 255). Por defecto 128</param>
    /// <returns>Matriz de enteros con valores 0 (claro) y 1 (oscuro)</returns>
    public static int[,] ConvertToBinaryMatrix(Bitmap original, int width, int height, int threshold = 128)
    {
        int[,] matrix = new int[height, width];

        // 1. Redimensionar la imagen para estandarizar las entradas
        using (Bitmap resized = new Bitmap(original, new Size(width, height)))
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixelColor = resized.GetPixel(x, y);

                    // 2. Calcular la luminancia en escala de grises
                    int gray = (int)(0.299 * pixelColor.R + 0.587 * pixelColor.G + 0.114 * pixelColor.B);

                    // 3. Binarización: 1 para píxeles oscuros, 0 para claros
                    matrix[y, x] = (gray < threshold) ? 1 : 0;
                }
            }
        }

        return matrix;
    }
}