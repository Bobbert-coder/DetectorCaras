using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DetectorCaras
{
    public partial class Form1 : Form
    {
        Bitmap img;
        int size = 64;
        int limite = 75;
        int[] vectorBinarioActual; // Almacena el vector 1D (1 y -1) de la imagen cargada

        public Form1()
        {
            InitializeComponent();
        }

        private void MostrarMatrizEnConsole(int[,] matrix, int width, int height)
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    sb.Append(matrix[y, x] == 1 ? "1 " : "0 ");
                }
                sb.AppendLine();
            }

            txtMatrizOutput.Text = sb.ToString();
        }

        /// <summary>
        /// Convierte una matriz 2D (0 y 1) a un vector 1D bipolar (1 y -1) necesario para Hopfield.
        /// </summary>
        private int[] MatrizAVectorBipolar(int[,] matrix, int width, int height)
        {
            int[] vector = new int[width * height];
            int index = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Convertir 1 -> 1, 0 -> -1
                    vector[index++] = (matrix[y, x] == 1) ? 1 : -1;
                }
            }

            return vector;
        }

        private void btnCargarImagen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Imágenes (*.jpg; *.png; *.bmp; *.jpeg)|*.jpg;*.png;*.bmp;*.jpeg";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    img = new Bitmap(ofd.FileName);

                    // 1. Obtener la matriz binarizada (2D)
                    int[,] matrizRostro = ImageProcessor.ConvertToBinaryMatrix(img, size, size, limite);

                    // 2. Convertir a vector 1D bipolar (1 y -1) para la Red de Hopfield
                    vectorBinarioActual = MatrizAVectorBipolar(matrizRostro, size, size);

                    // 3. Mostrar la matriz en pantalla
                    MostrarMatrizEnConsole(matrizRostro, size, size);
                }
            }
        }

        private void btnRecargar_Click_1(object sender, EventArgs e)
        {
            if (img == null)
            {
                MessageBox.Show("Primero debes cargar una imagen.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int nuevoLimite;

            if (int.TryParse(tbLimite.Text, out nuevoLimite))
            {
                limite = nuevoLimite;

                // Recalcular la binarización con el nuevo umbral
                int[,] matrizRostro = ImageProcessor.ConvertToBinaryMatrix(img, size, size, limite);
                vectorBinarioActual = MatrizAVectorBipolar(matrizRostro, size, size);

                MostrarMatrizEnConsole(matrizRostro, size, size);
            }
            else
            {
                MessageBox.Show("Por favor ingresa un valor numérico válido para el umbral/límite. Por defecto: 80", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Botón para GUARDAR un nuevo rostro en el catálogo permanente.
        /// </summary>
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (img == null || vectorBinarioActual == null)
            {
                MessageBox.Show("Carga primero una imagen para poder guardarla.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Asumiendo que tienes un TextBox llamado txtNombrePersona para ingresar el nombre
            string nombrePersona = txtNombre.Text.Trim();

            if (string.IsNullOrEmpty(nombrePersona))
            {
                MessageBox.Show("Por favor ingresa el nombre de la persona.", "Nombre Requerido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Guardar la imagen PNG y registrar el vector en catalogo_rostros.json
                ImageStorage.GuardarNuevoRostro(img, vectorBinarioActual, nombrePersona);

                MessageBox.Show($"¡El rostro de '{nombrePersona}' ha sido guardado exitosamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNombre.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el rostro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Botón para RECONOCER a qué rostro del catálogo se parece la imagen actual.
        /// </summary
        private void btnReconocer_Click_1(object sender, EventArgs e)
        {
            if (vectorBinarioActual == null)
            {
                MessageBox.Show("Carga primero una imagen para clasificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> nombresRostros;
            int[][] patronesGuardados = ImageStorage.ObtenerMatrizParaHopfield(out nombresRostros);

            if (patronesGuardados.Length == 0)
            {
                MessageBox.Show("No hay rostros guardados en el catálogo.", "Catálogo Vacío", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Llamada directa al método dinámico
            int indiceCoincidencia = RedNeuronal.ReconocerPatron(patronesGuardados, vectorBinarioActual, 30);

            if (indiceCoincidencia >= 0 && indiceCoincidencia < nombresRostros.Count)
            {
                MessageBox.Show($"El rostro detectado corresponde a: {nombresRostros[indiceCoincidencia]}",
                                "Reconocimiento Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (indiceCoincidencia == -1) MessageBox.Show("El rostro no ha podido ser identificado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}