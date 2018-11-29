using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace LWTech.CameronPadron.Assignment8
{
	class Program
	{
		static void Main(string[] args)
		{

			Console.WriteLine("Boeing and Airbus Models Still in Flight");
			Console.WriteLine("=========================================================================================================");


			string line = "";
			string[] tokens= null;

			List<string> planeTypes = new List<string>();
			List<string> AirBusAndBoeing = new List<string>();

			

			try
			{
				WebClient client = new WebClient();
				Stream stream = client.OpenRead("https://public-api.adsbexchange.com/VirtualRadar/AircraftList.json?fTypQN=");

				using (StreamReader reader = new StreamReader(stream))
				{
					while (!reader.EndOfStream)
					{
					    line = reader.ReadLine();
						line = line.Replace("\"Type\":","|");
					    tokens = line.Split('|');
						//Console.WriteLine(line); // For Step 2 of assignment
						
						
					}

					
				}
			}
			catch (IOException exception)
			{
				Console.WriteLine("Something Happened... " + exception.Message);
				Console.WriteLine("Terminating Program...");
				return;
			}


			
			
			foreach (string s in tokens)
			{
				
				planeTypes.Add(s.Substring(0,5).Replace("\"", "")); // Adding our plane models to list. 
				
			}


			foreach (string s in planeTypes)
			{

				if (s.Contains("B7")) // adding our boeing and airbus models to their own list. 
				{
					string boeing = s.Remove(s.Length - 1, 1) + "7";
					AirBusAndBoeing.Add(boeing);
					//Console.WriteLine(boeing);

				}
				if (s.Contains("A3"))
				{
					string airbus = s.Remove(s.Length - 1, 1) + "0";
					AirBusAndBoeing.Add(airbus);
					//Console.WriteLine(airbus);

				}

				//Console.WriteLine(s);
				//Console.ReadLine(); //Testing that it's splitting at the right spot
			}

			Histogram PlaneTypeHistogram = new Histogram(AirBusAndBoeing, width: 100, maxLabelWidth: 5);
			PlaneTypeHistogram.Sort((x, y) => y.Value.CompareTo(x.Value));
			Console.WriteLine(PlaneTypeHistogram);


			Console.ReadLine();

		}
	}
}
