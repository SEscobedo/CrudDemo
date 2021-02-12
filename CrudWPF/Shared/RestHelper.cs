using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrudWPF.Shared
{
	class RestHelper
	{
		
		private static readonly string apiURL = "https://crudapisd.azurewebsites.net/api/";

		//Obtener registros
		public static async Task<string> GetALL()
		{
			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage res = await client.GetAsync(apiURL + "employees")) { 

					using(HttpContent content = res.Content)
					{
						string data = await content.ReadAsStringAsync();
						if (data != null)
						{
							return data;
						}
					}
				}
			}

			return string.Empty;
		}

		//Obtener registro especifico
		public static async Task<string> Get(Guid id)
		{
			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage res = await client.GetAsync(apiURL + "employees/" + id.ToString()))
				{

					using (HttpContent content = res.Content)
					{
						string data = await content.ReadAsStringAsync();
						if (data != null)
						{
							return data;
						}
					}
				}
			}

			return string.Empty;
		}

		//Adicionar novo registro
		public static async Task<string> Post(string nome, string sobrenome, string telefone)
		{
			
			var jsonString = $"{{\"nome\":\"{nome}\",\"sobrenome\":\"{sobrenome}\",\"telefone\":\"{telefone}\"}}";
			var jsonObject = JObject.Parse(jsonString);

			var input = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
		
			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage res = await client.PostAsync(apiURL + "employees", input))
				{

					using (HttpContent content = res.Content)
					{
						string data = await content.ReadAsStringAsync();
						if (data != null)
						{
							return data;
						}
					}
				}
			}

			return string.Empty;
		}

		//Editar
		public static async Task<string> Put(Guid id,string nome, string sobrenome, string telefone)
		{
			var jsonString = $"{{\"nome\":\"{nome}\",\"sobrenome\":\"{sobrenome}\",\"telefone\":\"{telefone}\"}}";
			var jsonObject = JObject.Parse(jsonString);

			var input = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");

			using (HttpClient client = new HttpClient())
			{	
				using (HttpResponseMessage res = await client.PutAsync(apiURL + "employees/" + id.ToString(), input))
				{
					using (HttpContent content = res.Content)
					{
						string data = await content.ReadAsStringAsync();
						if (data != null)
						{
							return data;
						}
					}
				}
			}

			return string.Empty;
		}

		//Apagar
		public static async Task<string> Delete(string id)
		{
			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage res = await client.DeleteAsync(apiURL + "employees/" + id))
				{

					using (HttpContent content = res.Content)
					{
						
						string data = await content.ReadAsStringAsync();
						if (data != null)
						{
							MessageBox.Show("Elemento apagado com suceso: " + ((int)res.StatusCode).ToString() + "-" + res.StatusCode.ToString());
							return data;
						}
						else
						{
							MessageBox.Show(((int)res.StatusCode).ToString() + "-" + res.StatusCode.ToString());
						}


					}
				}
			}

			return string.Empty;
		}
	}
}
