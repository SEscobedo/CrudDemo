using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CrudWPF.Shared;
using Newtonsoft.Json.Linq;

namespace CrudWPF
{
	/// <summary>
	/// Interação lógica para Formulario.xam
	/// </summary>
	public partial class Formulario : Page
	{
		public Guid Id { get; set; }
	
		public Formulario( Guid Id )
		{
			InitializeComponent();
		}

		public async void Button_Click(object sender, RoutedEventArgs e)
		{
			if (Id == Guid.Empty) {
				//Novo registro
				var Nome = txtNome.Text;

				if (Nome != "")
				{
					var response = await RestHelper.Post(Nome, txtSobrenome.Text, txtTelefone.Text);
				}
				else
				{
					MessageBox.Show("O Nome é requerido. O registro não sera salvo");
				}
				

				MainWindow.StaticMainFrame.Content = new MenuLista();

			}
			else
			{
				//Editando o registro existente


				using (Model.EmployeeDbEntities db = new Model.EmployeeDbEntities())
				{
					var oEmployee = db.Employees.Find(Id);
					oEmployee.Nome = txtNome.Text;
					oEmployee.Sobrenome = txtSobrenome.Text;
					oEmployee.Telefone = txtTelefone.Text;

					db.Entry(oEmployee).State = System.Data.Entity.EntityState.Modified;
					db.SaveChanges();

					MainWindow.StaticMainFrame.Content = new MenuLista();
				}
			}
		}
	}
}
