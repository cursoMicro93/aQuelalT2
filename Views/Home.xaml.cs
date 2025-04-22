namespace aQuelalT2.Views;

public partial class Home : ContentPage
{
	public Home()
	{
		InitializeComponent();
	}

    private async void OnCalcularClicked(object sender, EventArgs e)
    {
        // Validar que se seleccionó un estudiante
        if (pickerEstudiantes.SelectedIndex == -1)
        {
            await DisplayAlert("Error", "Seleccione un estudiante.", "OK");
            return;
        }

        // Validar campos y rangos
        if (!ValidarNota(entrySeguimiento1.Text, out double seguimiento1, "Seguimiento 1") ||
            !ValidarNota(entryExamen1.Text, out double examen1, "Examen 1") ||
            !ValidarNota(entrySeguimiento2.Text, out double seguimiento2, "Seguimiento 2") ||
            !ValidarNota(entryExamen2.Text, out double examen2, "Examen 2"))
        {
            return;
        }

        // Cálculos
        double parcial1 = seguimiento1 * 0.3 + examen1 * 0.2;
        double parcial2 = seguimiento2 * 0.3 + examen2 * 0.2;
        double notaFinal = parcial1 + parcial2;

        string estado;
        if (notaFinal >= 7)
            estado = "Aprobado";
        else if (notaFinal >= 5)
            estado = "Complementario";
        else
            estado = "Reprobado";

        string mensaje = $"Nombre: {pickerEstudiantes.SelectedItem}\n" +
                         $"Fecha: {dateFecha.Date:dd/MM/yyyy}\n" +
                         $"Nota Parcial 1: {parcial1:F2}\n" +
                         $"Nota Parcial 2: {parcial2:F2}\n" +
                         $"Nota Final: {notaFinal:F2}\n" +
                         $"Estado: {estado}";

        await DisplayAlert("Resultados", mensaje, "OK");
    }

    private bool ValidarNota(string? texto, out double nota, string campo)
    {
        if (string.IsNullOrWhiteSpace(texto) || !double.TryParse(texto, out nota))
        {
            DisplayAlert("Error", $"Ingrese un valor numérico válido para {campo}.", "OK");
            nota = 0;
            return false;
        }

        if (nota < 0 || nota > 10)
        {
            DisplayAlert("Error", $"{campo} debe estar entre 0 y 10.", "OK");
            return false;
        }

        return true;
    }

}