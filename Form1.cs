using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace matrix
{
    public partial class Form1 : Form
    {
				public static int R, C;
		List<string> lineaMatrizes = new List<string>();
		List<Tuple<Int64, string>> repetidos = new List<Tuple<Int64, string>>();

		public Form1()
        {
            InitializeComponent();
        }

        


		static int N ;
		static void transpose(string[,] A, string[,] B)
		{
			int i, j;
			for (i = 0; i < N; i++)
				for (j = 0; j < N; j++)
					B[i, j] = A[j, i];
		}


		public void LoadData(string path)
		{
			
             string direccion;
			direccion = path;

			List<List<string>> matriz = new List<List<string>>();
			//direccion = "C:\\cadena.txt";

			string[] records = File.ReadAllLines(direccion);
			string[] strLineas = File.ReadAllLines(direccion);
			string[] campos;

						
			string[,] arr2D = new string[records.Count(), records.Count()];


			foreach (string linea in strLineas)
			{

				List<string> lineaMatriz = new List<string>();
				campos = linea.Split(";".ToCharArray());
				lineaMatriz.AddRange(campos.ToList());
				matriz.Add(lineaMatriz);

				for (int i = 0; i <= matriz.Count - 1; i++)
				{
					foreach (var item in matriz[i])
					{
						var FILA = item.Split(',');
						for (int j = 0; j <= FILA.Length - 1; j++)
						{
							var x = FILA[j].Trim();

							arr2D[i, j] = x;
							lineaMatrizes.Add(arr2D[i, j]);
							
						}
						lineaMatrizes.Add(",");
					}
				}

			}
			string[,] A =  arr2D ;
			N = records.Count();
			string[,] B = new string[N, N];

			//INVIRTE COLUMNAS AFILAS
			
			transpose(A, B);



			for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < N; j++)
					lineaMatrizes.Add(B[i, j]);
			    //	Console.Write(B[i, j] + " ");

				Console.Write("\n");
				lineaMatrizes.Add(",");
			}
			string[,] arr22 = arr2D;

			R = arr2D.GetLength(0);
			C = arr2D.GetLength(1);


			diagonalOrder(arr2D);

			string[,] p = arr2D;
			int maxIndex = p.GetUpperBound(1);

			//List<string> mm = new List<string>();
			foreach (var index in DiagonalIndices(maxIndex))
			{
				lineaMatrizes.Add(p[index.Row, index.Col]);
				Console.Write(p[index.Row, index.Col] + " ");

			}


			var data = new List<Tuple<Int64, string>>();

			string[] s = lineaMatrizes.ToArray();
			int contador = 0;
			foreach (var item in lineaMatrizes)
            {
				

				if (item == ",")
				{
					contador++;
				}


				if (item != ",")
				{
					data.Add(Tuple.Create(Convert.ToInt64(contador), item.ToString()));
				}

		   }


			var datos = (from ra in data group ra.Item2 by ra.Item1 into g select g.ToList()).ToList();
			
			foreach (var item in datos)
            {
				if(item.Count>1)
                {
					findRepeat(item.ToArray(), item.Count-1);
				}
            }

			var buscarMayor = (from x in repetidos orderby x.Item1 descending select x).FirstOrDefault();
			string cadena = "";
			for (int Y = 0; Y<= Convert.ToInt32(buscarMayor.Item1); Y++ )
            {
				cadena = cadena + " " +buscarMayor.Item2;

			}
			MessageBox.Show("La Cadena Mas larga es: " + " " + cadena   );
            
	
        }

		public sealed class Index
		{
			public Index(int row, int col)
			{
				Row = row;
				Col = col;
			}

			public readonly int Row;
			public readonly int Col;
		}


		public  IEnumerable<Index> DiagonalIndices(int maxIndex)
		{

			for (int i = 0; i <= maxIndex; ++i)
			{
				for (int j = 0; j <= i; ++j)
				{



					yield return new Index(maxIndex - i + j, j);
				}

				lineaMatrizes.Add(",");
				Console.Write(",");
			}

			for (int i = 0; i < maxIndex; ++i)
			{
				{
					for (int j = 0; j < maxIndex - i; ++j)


					yield return new Index(j, i + j + 1);

					lineaMatrizes.Add(",");
					Console.Write(",");

				}

			}

			//Console.Write("\n");
		}
		public string findRepeat(string[] arr, int n)
		{
			int count = 0; //count of repeated element
			string value = ""; //to store repeated element
			for (int i = 0; i < n; i++)
			{
				if (arr[i] == arr[i + 1])
				{
					count++;
					value = arr[i];
				}
			}
			count++; //for last element
			repetidos.Add(Tuple.Create(Convert.ToInt64(count), value.ToString()));
			return "";
		}

		private void diagonalOrder(string[,] arr)
		{
			try
			{
				for (int k = 0; k < R; k++)
				{
					Console.Write(arr[k, 0] + " ");

					lineaMatrizes.Add(arr[k, 0]);
					// set row index for next
					// point in diagonal
					int i = k - 1;

					// set column index for
					// next point in diagonal
					int j = 1;

					/* Print Diagonally upward */
					while (isValid(i, j))
					{

						Console.Write(arr[i, j] + " ");
						lineaMatrizes.Add(arr[i, j]);
						i--;
						// move in upright direction
						j++;

					}

					lineaMatrizes.Add(",");
					Console.Write("\n");

				}

				for (int k = 1; k < C; k++)
				{
					Console.Write(arr[R - 1, k] + " ");
					lineaMatrizes.Add(arr[R - 1, k]);
					// set row index for next
					// point in diagonal
					int i = R - 2;

					// set column index for
					// next point in diagonal
					int j = k + 1;

					/* Print Diagonally upward */
					while (isValid(i, j))
					{
						Console.Write(arr[i, j] + " ");
						lineaMatrizes.Add(arr[i, j]);
						i--;
						j++; // move in upright direction
					}

					//var name = Console.ReadLine();
					//	lineaMatriz.Add(name);
					lineaMatrizes.Add(",");
					Console.Write("\n");
				}


			}
			catch (Exception ex)
			{

			}
		}

        private void Form1_Load_1(object sender, EventArgs e)
        {
			
        }

		private void button1_Click(object sender, EventArgs e)
		{
			OpenFileDialog fdlg = new OpenFileDialog();
			fdlg.Filter = "Text|*.txt|All|*.*";
			if (fdlg.ShowDialog() == DialogResult.OK)
			{
				string strfilename = fdlg.InitialDirectory + fdlg.FileName;
				LoadData(strfilename);
			}
		}
        public static bool isValid(int i, int j)
		{
			if (i < 0 || i >= R || j >= C || j < 0)
				return false;
			return true;
		}



	}




}
