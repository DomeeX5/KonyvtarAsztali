﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonyvtarAsztali
{
	internal class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			if (args.Contains("--stat"))
			{
				new Statisztika();
			}
		}
	}
}
