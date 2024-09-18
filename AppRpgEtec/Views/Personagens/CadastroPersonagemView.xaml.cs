using AppRpgEtec.ViewModels.Personagens;
using System.Windows.Input;

namespace AppRpgEtec.Views.Personagens;

public partial class CadastroPersonagemView : ContentPage
{
	private CadastroPersonagemViewModel cadviewModel;

    public CadastroPersonagemView()
	{
		InitializeComponent();

		cadviewModel = new CadastroPersonagemViewModel();
		BindingContext = cadviewModel;
		Title = "Novo Personagem";
	}
}