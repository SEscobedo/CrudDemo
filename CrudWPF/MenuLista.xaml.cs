using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using Newtonsoft.Json;
using CrudWPF.Shared;
using System.ComponentModel;

namespace CrudWPF
{
	/// <summary>
	/// Interação lógica para MenuLista.xam
	/// </summary>
	public partial class MenuLista : Page
	{
		public MenuLista()
		{
			InitializeComponent();
			Refresh();
		}

		private async void Refresh()
		{
		
			string jsonString = await RestHelper.GetALL();
		
			DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonString);
			DG.ItemsSource = dt.DefaultView;

		}


		

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			
			MainWindow.StaticMainFrame.Content = new Formulario(Guid.Empty);
		}

		private async void Button_Click_1(object sender, RoutedEventArgs e)
		{
			string id = ((Button)sender).CommandParameter.ToString();
				
			await RestHelper.Delete(id);

			Refresh();
		}

		private void Button_Edit(object sender, RoutedEventArgs e)
		{
			Guid id = (Guid)((Button)sender).CommandParameter;

			Formulario pFormulario = new Formulario(id);

			MainWindow.StaticMainFrame.Content = pFormulario;

		}

	}


	public class EmployeeViewModel
	{
		public Guid Id { get; set; }
		public string Nome { get; set; }
		public string Sobrenome { get; set; }
		public string Telefone { get; set; }
	}
}
