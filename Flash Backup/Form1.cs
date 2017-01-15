using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flash_Backup
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		List<string> paths1 = new List<string>();
		List<string> paths2 = new List<string>();

		private void button1_Click(object sender, EventArgs e)
		{
			paths1 = Directory.GetFiles(textBox1.Text, "*.*", SearchOption.AllDirectories).ToList();
			paths2 = Directory.GetFiles(textBox2.Text, "*.*", SearchOption.AllDirectories).ToList();

			foreach (var item1 in paths1)
			{
				foreach (var item2 in paths2)
				{
					if (Path.GetFileName(item1) == Path.GetFileName(item2))
						if (!GetFileHash(item1).SequenceEqual(GetFileHash(item2)))
						{
							Console.WriteLine("\n{0} \nNOT SAME AS\n {1}\n", item1, item2);
						}
						else
						{
							Console.WriteLine("\n{0} \n SAME AS\n {1}\n", item1, item2);
						}
				}
			}
		}

		private byte[] GetFileHash(string filePath)
		{
			HashAlgorithm sha1 = HashAlgorithm.Create();
			
			using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
				return sha1.ComputeHash(stream);
		}
	}
}
