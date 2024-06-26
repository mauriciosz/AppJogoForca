using AppJogoForca.Libraries.Text;
using AppJogoForca.Models;
using AppJogoForca.Repositories;

namespace AppJogoForca;

public partial class MainPage : ContentPage
{
	private Palavras _palavra;
	private int _errors;
	public MainPage()
	{
		InitializeComponent();
        ResetGame();
    }

    private async void OnButtonClicked(object sender, EventArgs e)
    {
        Button button = ((Button)sender);
        button.IsEnabled = false; // desabilita o botão que possui o caracter que já foi utilizado.

        String letter = button.Text;
        var positions = _palavra.Text.GetPositions(letter);

        if (positions.Count == 0)
        {
            ErrorHandle(button);

            await IsGameOver();
            return;
        }

        ReplaceLetter(letter, positions);
        // Pega a instância atual do aplicativo, acessa os recursos e os dicionários unidos para pegar o primeiro elemento passando o stilo definido "Success" e converte isso em Style
        button.Style = App.Current.Resources.MergedDictionaries.ElementAt(1)["Success"] as Style;

        await HasWinner();
    }

    #region Eventos de Sucesso
    private async Task HasWinner()
    {
        if (!LblText.Text.Contains("_"))
        {
            await DisplayAlert("Parabéns!", "Você ganhou o jogo!", "Novo Jogo");
            ResetGame();
        }
    }
    #endregion

    #region Eventos de Reset do Game
    private void ResetGame()
    {
        ResetVirtualKeyBoard();
        ResetErrors();
        GeneratedNewPalavra();
    }
    private void GeneratedNewPalavra()
    {
        var repository = new PalavrasRepository();
        _palavra = repository.GetPalavrasAleatorias();

        LblTips.Text = _palavra.Tips;
        LblText.Text = new string('_', _palavra.Text.Length);
    }
    private void ResetErrors()
    {
        _errors = 0;
        ImgMain.Source = ImageSource.FromFile("forca1.png");
    }
    private void ResetVirtualKeyBoard()
    {
        // Passa para o método ResetVirtualLines() qual o filho do tipo HorizontalStackLayout que o KeyboardContainer (VerticalStackLayout) contem. 
        ResetVirtualLines((HorizontalStackLayout)KeyboardContainer.Children[0]);
        ResetVirtualLines((HorizontalStackLayout)KeyboardContainer.Children[1]);
        ResetVirtualLines((HorizontalStackLayout)KeyboardContainer.Children[2]);
    }
    private void ResetVirtualLines(HorizontalStackLayout horizontal)
    {
        // itera os filhos do HorizontalStackLayout para verificar se algum deles´é um "Button". Se for, passa True para o Enable
        foreach (Button button in horizontal.Children)
        {
            button.IsEnabled = true;
            button.Style = null;
        }
    }
    #endregion

    private void ReplaceLetter(string letter, List<int> positions)
    {
        foreach (int position in positions)
        {
            // Remove o caracter da posição encontrada e ao mesmo tempo insere um novo caracter. Basicamente troca um "_ _ _ _ _" por "_ A _ _ A"
            LblText.Text = LblText.Text.Remove(position, 1).Insert(position, letter);
        }
    }

    private void ErrorHandle(Button button)
    {
        _errors++;

        ImgMain.Source = ImageSource.FromFile($"forca{_errors + 1}.png");
        button.Style = App.Current.Resources.MergedDictionaries.ElementAt(1)["Fail"] as Style;
    }

    private async Task IsGameOver()
    {
        if (_errors == 6)
        {
            await DisplayAlert("Perdeu!", "Você foi enforcado!", "Novo Jogo");
            ResetGame();
        }
    }

    
    private void OnButtonResetGame(object sender, EventArgs e)
    {
		ResetGame();
    }
}