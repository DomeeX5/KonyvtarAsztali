using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonyvtarAsztali
{
	public class Statisztika
	{
		MySqlConnection connection;
		List<Konyv> konyvek = new List<Konyv>();
		public Statisztika() 
		{
			MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
			builder.Server = "localhost";
			builder.Port = 3306;
			builder.UserID = "root";
			builder.Password = "";
			builder.Database = "konyvtar";

			try
			{
				connection = new MySqlConnection(builder.ConnectionString);
				OpenConnection();
				GetAll();
				PageMoreThan500();
				OlderThan1950Book();
				LongestBook();
				MostBooksAuthor();
				InputTitle();
				CloseConnection();
			} catch (MySqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			
		}

		private void PageMoreThan500()
		{
			Console.WriteLine($"500 oldalnál hosszabb könyvek száma: {(konyvek.Where(e => e.Page_Count > 500)).Count()}");
		}

		private void OlderThan1950Book()
		{
			if(konyvek.Any(konyv => konyv.Publish_Year < 1950) == true)
				Console.WriteLine("Van 1950-nél régebbi könyv");
			else
				Console.WriteLine("Nincs 1950-nél régebbi könyv");
		}

		private void LongestBook()
		{
			Konyv leghosszabbKonyv = konyvek.MaxBy(konyv => konyv.Page_Count);
			Console.WriteLine("A leghosszabb könyv:");
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine($"\tSzerző: {leghosszabbKonyv!.Author}");
			stringBuilder.AppendLine($"\tCím: {leghosszabbKonyv.Title}");
			stringBuilder.AppendLine($"\tKiadás éve: {leghosszabbKonyv.Publish_Year}");
			stringBuilder.AppendLine($"\tOldalszám: {leghosszabbKonyv.Page_Count}");
			Console.Write(stringBuilder);
		}

		private void MostBooksAuthor()
		{
			Console.WriteLine($"{konyvek.GroupBy(konyv => konyv.Author).MaxBy(group => group.Count()).Key}");
        }

		private void InputTitle()
		{
            Console.Write("Adjon meg egy könyv címet: ");
			string input = Console.ReadLine();
			int i = 0;
			while (i< konyvek.Count && input != konyvek[i].Title)
			{
				i++;
			}
			if (i < konyvek.Count)
				Console.WriteLine($"Sikeres találat! A könyv szerzője: {konyvek[i].Author}");
			else
				Console.WriteLine("A keresett könyv nem létezik!");
		}

		private List<Konyv> GetAll()
		{
			OpenConnection();
			string sql = "SELECT id, title, author, publish_year, page_count from books";
			MySqlCommand command = new MySqlCommand(sql, connection);
			using(MySqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					int id = reader.GetInt32("id");
					string title = reader.GetString("title");
					string author = reader.GetString("author");
					int publish_year = reader.GetInt32("publish_year");
					int page_count = reader.GetInt32("page_count");
					Konyv konyv = new Konyv(id, title, author, publish_year, page_count);
					konyvek.Add(konyv);
				}
			}
			CloseConnection();
			return konyvek;
		}

		private void OpenConnection()
		{
			if (connection.State != System.Data.ConnectionState.Open) 
				connection.Open();
		}

		private void CloseConnection()
		{
			if (connection.State == System.Data.ConnectionState.Open)
				connection.Close();
		}
	}
}
