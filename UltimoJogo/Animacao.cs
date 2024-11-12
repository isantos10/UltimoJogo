namespace UltimoJogo;

public class Animacao
{
    protected List<String> Animacao01 = new List<String>();
    protected List<String> Animacao02 = new List<String>();
    protected List<String> Animacao03 = new List<String>();

    protected bool loop = true;
    protected int AnimacaoAtiva = 1;
    bool parado = true;
    int frameAtual = 1;
    protected Image compImage;
    public Animacao(Image a)
    {
        compImage = a;
    }
    public void Stop()
    {
        parado = true;
    }
    public void Play()
    {
        parado = false;
    }
    public void SetAnimacaoAtiva(int a)
    {
        AnimacaoAtiva = a;
    }
    public void Desenha()
    {
        if (parado)
            return;
        string NomeArquivo = "";
        int TamanhoAnimacao = 0;
        if (AnimacaoAtiva == 1)
        {
            NomeArquivo = Animacao01[frameAtual];
            TamanhoAnimacao = Animacao01.Count;
        }
        else if (AnimacaoAtiva == 2)
        {
            NomeArquivo = Animacao02[frameAtual];
            TamanhoAnimacao = Animacao02.Count;
        }
        else if (AnimacaoAtiva == 3)
        {
            NomeArquivo = Animacao03[frameAtual];
            TamanhoAnimacao = Animacao03.Count;
        }
        compImage.Source = ImageSource.FromFile(NomeArquivo);
        frameAtual++;
        if (frameAtual >= TamanhoAnimacao)
        {
            if (loop)
                frameAtual = 0;
            else
            {
                parado = true;
                QuandoParar();
            }
        }

    }
    public virtual void QuandoParar()
    {

    }

}