using CrudWPF.Shared;
using Newtonsoft.Json.Linq;
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
	
		public Formulario(Guid Id)
		{
			InitializeComponent();
			this.Id = Id;

			if(this.Id != Guid.Empty)
			{
				FillData(this.Id);
			}
		}

		private async void FillData(Guid Id)
		{
			string jsonString = await RestHelper.Get(Id);
			var jsonObject = JObject.Parse(jsonString);

			txtNome.Text = jsonObject["nome"].ToString();
			txtSobrenome.Text = jsonObject["sobrenome"].ToString();
			txtTelefone.Text = jsonObject["telefone"].ToString();
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
					MessageBox.Show("O nome é requerido. O registro não será salvo");
				}
				

				MainWindow.StaticMainFrame.Content = new MenuLista();

			}
			else
			{
				//Editando o registro existente
				var Nome = txtNome.Text;

				if (Nome != "")
				{
					var response = await RestHelper.Put(Id, Nome, txtSobrenome.Text, txtTelefone.Text);
				}
				else
				{
					MessageBox.Show("O nome é requerido. O registro não será modificado");
				}


				MainWindow.StaticMainFrame.Content = new MenuLista();
			}
		}
	}
}
