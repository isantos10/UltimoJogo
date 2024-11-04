namespace UltimoJogo;

public partial class MainPage : ContentPage
{
	bool estaMorto = false;
	bool estaPulando = false;
	const int tempoEntreFrames = 25;
	int velocidade1 = 0;
	int velocidade2 = 0;
	int velocidade3 = 0;
	int velocidade = 0;
	int larguraJanela = 0;
	int alturaJanela = 0;



	public MainPage()
	{
		InitializeComponent();
	}

	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		CorrigeTamanhoCenario(w, h);
		CalculaVelocidade(w);
	}
	void CalculaVelocidade(double w)
	{
		velocidade1 = (int)(w * 0.001);
		velocidade2 = (int)(w * 0.004);
		velocidade3 = (int)(w * 0.008);
		velocidade = (int)(w * 0.01);

	}
	void CorrigeTamanhoCenario (double w, double h)
	{
		foreach (var A in HSLayer1.Children)
		(A as Image).WidthRequest = w;
		foreach (var A in HSLayer2.Children)
		(A as Image).WidthRequest = w;
		foreach (var A in HSLayer3.Children)
		(A as Image).WidthRequest = w;
		
		HSLayer1.WidthRequest = w * 1.5;
		HSLayer2.WidthRequest = w * 1.5;
		HSLayer3.WidthRequest = w * 1.5;
	}
	void GerenciaCenarios()
	{
		MoveCenario();
		GerenciaCenario(HSLayer1);
		GerenciaCenario(HSLayer2);
		GerenciaCenario(HSLayer3);
	    
	}
	void MoveCenario()
	{
		HSLayer1.TranslationX-=velocidade2;
		HSLayer2.TranslationX-=velocidade1;
		HSLayer3.TranslationX-=velocidade;
		
	}
	void GerenciaCenario(HorizontalStackLayout hsl)
	{
		var view = (hsl.Children.First() as Image);
		if (view.WidthRequest+hsl.TranslationX<0)
		{
			hsl.Children.Remove(view);
            hsl.Children.Add(view);
			hsl.TranslationX=view.TranslationX;
		}
	}
	async Task Desenha()
	{
		while(! estaMorto)
		{
			GerenciaCenarios();
			await Task.Delay(tempoEntreFrames);
		}
	}
	protected override void OnAppearing()
		{
			base.OnAppearing();
			Desenha();
		}

}