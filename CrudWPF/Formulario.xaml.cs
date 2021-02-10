using System;
using System.Windows;
using System.Windows.Controls;

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

			this.Id = Id;
			if(this.Id != Guid.Empty)
			{
				using(Model.EmployeeDbEntities db = new Model.EmployeeDbEntities())
				{
					var oEmployee = db.Employees.Find(this.Id);
					txtNome.Text = oEmployee.Nome;
					txtSobrenome.Text = oEmployee.Sobrenome;
					txtTelefone.Text = oEmployee.Telefone;
				}
			}
		}

		public void Button_Click(object sender, RoutedEventArgs e)
		{
			if (Id == Guid.Empty) { 
				//Novo registro
				using(Model.EmployeeDbEntities db= new Model.EmployeeDbEntities())
				{
					var oEmployee = new Model.Employees();
					oEmployee.Id = Guid.NewGuid();
					oEmployee.Nome = txtNome.Text;
					oEmployee.Sobrenome = txtSobrenome.Text;
					oEmployee.Telefone = txtTelefone.Text;

					db.Employees.Add(oEmployee);
					db.SaveChanges();

					MainWindow.StaticMainFrame.Content = new MenuLista();
				}
			}
			else
			{
				//Editando registro existente
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
