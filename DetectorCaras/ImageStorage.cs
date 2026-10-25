using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace DetectorCaras
{
    public static class ImageStorage
    {
        /// <summary>
        /// Obtiene o crea la carpeta "RostrosGuardados" en el directorio del ejecutable.
        /// </summary>
        private static string GetDefaultFolderPath()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string targetFolder = Path.Combine(baseDirectory, "RostrosGuardados");

            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }

            return targetFolder;
        }

        /// <summary>
        /// Obtiene la ruta del archivo JSON que almacena el catálogo de rostros.
        /// </summary>
        private static string GetJsonFilePath()
        {
            return Path.Combine(GetDefaultFolderPath(), "catalogo_rostros.json");
        }

        /// <summary>
        /// Guarda la imagen en formato PNG y añade el vector binario al catálogo JSON permanente.
        /// </summary>
        public static void GuardarNuevoRostro(Bitmap image, int[] binaryArray, string personName)
        {
            string folderPath = GetDefaultFolderPath();
            string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string baseFileName = $"{personName}_{timeStamp}";

            // 1. Guardar la imagen PNG individual
            string imagePath = Path.Combine(folderPath, baseFileName + ".png");
            image.Save(imagePath, ImageFormat.Png);

            // 2. Cargar el catálogo existente
            List<RostroData> catalogo = CargarTodosLosRostros();

            // 3. Añadir el nuevo rostro
            catalogo.Add(new RostroData
            {
                Nombre = personName,
                VectorBinario = binaryArray
            });

            // 4. Serializar la lista actualizada a JSON con Newtonsoft.Json
            string json = JsonConvert.SerializeObject(catalogo, Formatting.Indented);
            File.WriteAllText(GetJsonFilePath(), json);
        }

        /// <summary>
        /// Lee el archivo JSON y deserealiza todos los rostros almacenados.
        /// </summary>
        public static List<RostroData> CargarTodosLosRostros()
        {
            string jsonPath = GetJsonFilePath();

            if (!File.Exists(jsonPath))
            {
                return new List<RostroData>();
            }

            string json = File.ReadAllText(jsonPath);

            // Deserializar la lista con Newtonsoft.Json
            return JsonConvert.DeserializeObject<List<RostroData>>(json) ?? new List<RostroData>();
        }

        /// <summary>
        /// Obtiene la matriz int[][] con todos los patrones para entrenar la Red de Hopfield
        /// y retorna la lista ordenada de los nombres correspondientes.
        /// </summary>
        public static int[][] ObtenerMatrizParaHopfield(out List<string> nombres)
        {
            List<RostroData> catalogo = CargarTodosLosRostros();

            // Extraer la lista de nombres para relacionar los índices devueltos por la Red
            nombres = catalogo.Select(r => r.Nombre).ToList();

            // Convertir la lista de vectores a la matriz bidimensional requerida (int[][])
            return catalogo.Select(r => r.VectorBinario).ToArray();
        }
    }
}