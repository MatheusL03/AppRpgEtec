using AppRpgEtec.ViewModels.Usuarios;

namespace AppRpgEtec.Views.Usuarios;

public partial class CadastroView2 : ContentPage
{
    public CadastroView2()
    {
        UsuarioViewModel _viewModel;
        InitializeComponent();

        _viewModel = new UsuarioViewModel();
        BindingContext = _viewModel;
    }
}