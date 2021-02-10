using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

		private void Refresh()
		{
			List<EmployeeViewModel> lst = new List<EmployeeViewModel>();
			using (Model.EmployeeDbEntities db = new Model.EmployeeDbEntities())
			{
				lst = (from d in db.Employees
					   select new EmployeeViewModel
					   {
						   Nome = d.Nome,
						   Sobrenome = d.Sobrenome,
						   Telefone = d.Telefone,
						   Id = d.Id
					   }).ToList();

			}
			DG.ItemsSource = lst;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			
			MainWindow.StaticMainFrame.Content = new Formulario(Guid.Empty);
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			Guid id = (Guid)((Button)sender).CommandParameter;

			using (Model.EmployeeDbEntities db = new Model.EmployeeDbEntities())
			{
				var oEmployee = db.Employees.Find(id);
				db.Employees.Remove(oEmployee);
				db.SaveChanges();
			}

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
