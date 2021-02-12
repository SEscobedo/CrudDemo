using System.Windows;
using System.Windows.Controls;

namespace CrudWPF
{
	/// <summary>
	/// Interação lógica para MainWindow.xam
	/// </summary>
	public partial class MainWindow : Window
	{
		public static Frame StaticMainFrame;
		public MainWindow()
		{

			InitializeComponent();
			StaticMainFrame = MainFrame;

		}
	}
}
