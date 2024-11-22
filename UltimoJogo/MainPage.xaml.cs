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
	const int forcaGravidade = 10;
	bool estanoChao = true;
	bool estanoAr = false;
	int tempoPulando = 0;
	int temponoAr = 0;
	const int forcaPulo = 10;
	const int maxTempoPulando = 10;
	const int maxTemponoAr = 5;
    Player player;


	public MainPage()
	{
		InitializeComponent();
		player = new Player(imgpPlayer);
		player.Run();
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
		velocidade2 = (int)(w * 0.005);
		velocidade3 = (int)(w * 0.002);
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
		
		HSLayer1.WidthRequest = w;
		HSLayer2.WidthRequest = w;
		HSLayer3.WidthRequest = w;
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
		while (!estaMorto)
		{
			GerenciaCenarios();
			if (!estaPulando && !estanoAr)

			{
				AplicaGravidade();
				player.Desenha();
			}

			else
				AplicaPulo();
			await Task.Delay(tempoEntreFrames);
		}
	}
	protected override void OnAppearing()
		{
			base.OnAppearing();
			Desenha();
		}
void AplicaGravidade()
	{
		if (player.GetY() < 0)
			player.MoveY(forcaGravidade);
		else if (player.GetY() >= 0)
		{
			player.SetY(0);
			estanoChao = true;
		}
	}
	void AplicaPulo()
	{
		estanoChao = false;
		if (estaPulando && tempoPulando >= maxTempoPulando)
		{
			estaPulando = false;
			estanoAr = true;
			temponoAr = 0;
		}
		else if (estanoAr && temponoAr >= maxTemponoAr)
		{
			estaPulando = false;
			estanoAr = false;
			tempoPulando = 0;
			temponoAr = 0;
		}
		else if (estaPulando && tempoPulando < maxTempoPulando)
		{
			player.MoveY(-forcaPulo);
			tempoPulando++;
		}
		else if (estanoAr)
			temponoAr++;
	}


	void OnGridClicked(object o, TappedEventArgs a)
	{
		if (estanoChao)
			estaPulando = true;
	}
}