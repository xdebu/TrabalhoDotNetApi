using AppCliente.Logic;
using AppCliente.Model;
using System;
using System.Collections.Generic;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AppCliente.Paginas
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TarefasPage : Page
    {

        private List<Tarefa> lista = new List<Tarefa>();
        public TarefasPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            try
            {
                lista = await TarefaRequestApi.ListarAsync();

                lstDados.ItemsSource = lista;
            }
            catch (Exception ex)
            {
                var msg = TratarException.ErrorMessage(ex);

                var dialog = new MessageDialog(msg, "Oooooppssss");

                await dialog.ShowAsync();
            }
            
        }

        private void lstDados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;

            var tarefa = listBox.SelectedItem as Tarefa;

            Frame.Navigate(typeof(NovaTarefaPage), tarefa);
        }
    }
}
