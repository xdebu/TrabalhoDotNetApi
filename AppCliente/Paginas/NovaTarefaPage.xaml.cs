using AppCliente.Logic;
using AppCliente.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AppCliente.Paginas
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NovaTarefaPage : Page
    {
        private Tarefa model;

        public NovaTarefaPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            model = e.Parameter as Tarefa;
            base.OnNavigatedTo(e);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (model == null || model.Id == 0)
            {
                txtCodigo.Visibility = Visibility.Collapsed;
                lblCodigo.Visibility = Visibility.Collapsed;
                var json = MyLocalStorage.GetFromLocalStorage("usuario");
                var usuario = JsonConvert.DeserializeObject<Usuario>(json.ToString());
                txtUsername.Text = usuario.Login;
            }
            else
            {
                txtCodigo.Text         = model.Id.ToString();
                txtTitulo.Text         = model.Titulo;
                txtDescricao.Text      = model.Descricao;
                dtpDataLimite.Date     = model.DataLimite;
                ckbConcluido.IsChecked = model.Concluido;
                txtUsername.Text       = model.Username;
            }
        }

        private async void btnGravar_Click(object sender, RoutedEventArgs e)
        {
            model = new Tarefa();

            model.Id         = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text);
            model.Titulo     = txtTitulo.Text;
            model.Descricao  = txtDescricao.Text;
            model.DataLimite = dtpDataLimite.Date.DateTime;
            model.Concluido  = ckbConcluido.IsChecked.Value;
            model.Username   = txtUsername.Text;

            try
            {
                if (model.Id > 0)
                {
                    await TarefaRequestApi.AlterarTarefa(model);

                    var dialog = new MessageDialog("Sua tarefa foi alterada com sucesso!", "Sucesso");
                    await dialog.ShowAsync();

                    Frame.Navigate(typeof(TarefasPage));
                }
                else
                {
                    await TarefaRequestApi.GravarTarefa(model);

                    txtCodigo.Text = "";
                    txtTitulo.Text = "";
                    txtDescricao.Text = "";
                    ckbConcluido.IsChecked = false;

                    var dialog = new MessageDialog("Sua tarefa foi criada com sucesso!", "Sucesso");
                    await dialog.ShowAsync();

                    txtTitulo.Focus(FocusState.Keyboard);
                }
                
            }

            catch (Exception ex)
            {
                var msg = TratarException.ErrorMessage(ex);
                var dialog = new MessageDialog(msg, "Oooooopssssss..!");
                await dialog.ShowAsync();
            }
        }

        
    }
}
