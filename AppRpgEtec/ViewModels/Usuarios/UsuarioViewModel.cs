using AppRpgEtec.Models;
using AppRpgEtec.Services.Usuarios;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppRpgEtec.ViewModels.Usuarios
{
    public class UsuarioViewModel : BaseViewModel
    {
        private UsuarioService _uService;
        public ICommand AutenticarCommand { get; set; }


        // CONSTRUTOR 
        public UsuarioViewModel()
        {
            _uService = new UsuarioService();
            InicializarCommands();
        }

        public void InicializarCommands()
        {
            AutenticarCommand = new Command(async() => await AutenticarUsuario());
        }


        #region AtributosPropriedades
        private string login = ""; 
        public string Login
        {
            get { return login;}
            set
            {
                login = value;
                OnPropertyChanged();
            }
        }
      
        private string senha = string.Empty;
        public string Senha
        {
            get { return senha; }
            set
            {
                senha = value;
                OnPropertyChanged();
            }
        }
        
        #endregion

        public async Task AutenticarUsuario()
        {
            try
            {
                Usuario u = new Usuario();
                u.Username = login;
                u.PasswordString = senha;
                Usuario uAutenticado = await _uService.PostAutenticarAsync(u);

                if (!string.IsNullOrEmpty(uAutenticado.Token))
                {
                    string mensagem = $"Bem vindo { u.Username}";
                    Preferences.Set("UsuarioToken", uAutenticado.Token) ;
                    Preferences.Set("UsuarioId", uAutenticado.Id);
                    Preferences.Set("UsuarioUsername", uAutenticado.Username);
                    Preferences.Set("UsuarioPerfil", uAutenticado.Perfil);

                    await Application.Current.MainPage
                        .DisplayAlert("Informação", mensagem, "Ok");

                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Informação", "Dados incorretos", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + "Detalhes: " + ex.InnerException, "OK");
            }
        }

        public async Task RegistrarUsuario()
        {
            try
            {
                Usuario u = new Usuario();
                u.Username = login;
                u.PasswordString = senha;

                Usuario uRegistrado = await _uService.PostRegistrarUsuarioAsync(u);

                if (uRegistrado.Id != 0) 
                {
                    string mensagem = $"Usuário Id {uRegistrado.Id} registrado com sucesso!";
                    await Application.Current.MainPage.DisplayAlert("Informação", mensagem, "Ok");
                }
            }
            catch (Exception ex) 
            { 
            
            }




        }
    }
}
