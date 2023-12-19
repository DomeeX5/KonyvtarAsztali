using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonyvtarAsztali
{
	public class Konyv
	{
		public Konyv(int id, string title, string author, int publish_Year, int page_Count)
		{
			Id = id;
			Title = title;
			Author = author;
			Publish_Year = publish_Year;
			Page_Count = page_Count;
		}

		public int Id { get;  }
		public string Title { get;  }
		public string Author { get; }
		public int Publish_Year { get; }
		public int Page_Count { get; }

	}
}
