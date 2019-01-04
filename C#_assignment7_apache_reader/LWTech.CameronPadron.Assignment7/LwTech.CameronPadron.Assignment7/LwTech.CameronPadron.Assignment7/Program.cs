using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LwTech.CameronPadron.Assignment7
{
	class Program
	{
		static void Main(string[] args)
		{
			int lineNumber = 0;
			int uniqueCount = 0;

			Dictionary<string, int> IPDictionary = new Dictionary<string, int>();
			Dictionary<string, int> URLDictionary = new Dictionary<string, int>();
			Dictionary<string, int> StatusCodeDictionary = new Dictionary<string, int>();

			using (StreamReader sr = new StreamReader("access-log.txt"))
				while (!sr.EndOfStream)
				{
					string line = sr.ReadLine();

					int indexOfIp = line.IndexOf("- -");
					int indexOfURL = line.IndexOf("GET");
					int indexOfStatusCode = line.IndexOf("\" ");

					


					string ipAddress = line.Remove(indexOfIp);
					string statusCode = line.Substring(indexOfStatusCode + 2, 3);
					string url = line.Remove(indexOfStatusCode);
					url = url.Substring(indexOfURL + 1);

					bool questionMarkFound = false;
					foreach (char c in url)
					{
						if (c == '?')
							questionMarkFound = true;

					}

					if (questionMarkFound)
					{
						int indexOfQuestionDeleter = url.IndexOf("?");
						url = url.Remove(indexOfQuestionDeleter);

						IncrementDictionary(IPDictionary, ipAddress);
						IncrementDictionary(URLDictionary, url );
						IncrementDictionary(StatusCodeDictionary, statusCode );


						lineNumber++;
						//Console.WriteLine(lineNumber + ": " + line);
						//Console.WriteLine(lineNumber + " IP Address: " + ipAddress);
						//Console.WriteLine(lineNumber + " URL: " + url);
						//Console.WriteLine(lineNumber + " Status Code: " + statusCode);

					}
					else
					{


						IncrementDictionary(IPDictionary, ipAddress);
						IncrementDictionary(URLDictionary, url);
						IncrementDictionary(StatusCodeDictionary, statusCode);

						lineNumber++;
						//Console.WriteLine(lineNumber + ": " + line);
						//Console.WriteLine(lineNumber + " IP Address: " + ipAddress);
						//Console.WriteLine(lineNumber + " URL: " + url);
						//Console.WriteLine(lineNumber + " Status Code: " + statusCode);
					}
				}

			//foreach (KeyValuePair<string, int> x in IPDictionary)
			//{
				//if(x.Value >= 10)
					//Console.WriteLine("IP Adresss:{0} , Visits: {1} ", x.Key, x.Value);
			//}

			//foreach (KeyValuePair<string, int> x in URLDictionary)
			//{
				//if (x.Value >= 10)
					//Console.WriteLine("URL:{0} , Visits: {1} ", x.Key, x.Value);
			//}

			//foreach (KeyValuePair<string, int> x in StatusCodeDictionary)
			//{
				
					//Console.WriteLine("Status Code:{0} , Visits: {1} ", x.Key, x.Value);
			//}

			//Changing the dictionaries to lists

			List<KeyValuePair<String, int>> myIPList = IPDictionary.ToList();

			myIPList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
			myIPList.Reverse();

			foreach (KeyValuePair<string, int> x in myIPList)
			{
				//if (x.Value >= 10) //This was for testing purposes
					Console.WriteLine("IP Adresss:{0} , Visits: {1} ", x.Key, x.Value);
			}

			List<KeyValuePair<String, int>> myUrlList = URLDictionary.ToList();

			myUrlList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
			myUrlList.Reverse();

			foreach (KeyValuePair<string, int> x in myUrlList)
			{
				//if (x.Value >= 10) //Testing purposes again.
					Console.WriteLine("URL:{0} ,  URL Visits: {1} ", x.Key, x.Value);
			}

			List<KeyValuePair<String, int>> myStatusCodeList = StatusCodeDictionary.ToList();

			
			myStatusCodeList.Sort((pair1, pair2) => pair1.Key.CompareTo(pair2.Key));

			foreach (KeyValuePair<string, int> x in myStatusCodeList)
			{
				Console.WriteLine("Status Code:{0} , Total Count: {1} ", x.Key, x.Value);
			}


			Console.ReadLine();
		}

		private static void IncrementDictionary(Dictionary<string, int> dictionary, string key)
		{
			if (!dictionary.ContainsKey(key))
			{
				dictionary.Add(key, 1);

			}
			else
			{
				dictionary[key]++;

			}
		}
	}
}
