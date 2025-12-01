public struct Quadrado
{
    public bool foiInteragido = false;
    public bool Bomba = false;
    public bool Bandeira = false;
    public int? bombasAoRedor;
    public char? simbolo;
    public Quadrado(){}
}
public class Program
{
    public static Quadrado[,] criarCampoMinado(int numeroDeBombas,int tamanhoX,int tamanhoY)
    {
        Quadrado[,] campo = new Quadrado[tamanhoX,tamanhoY];
        Random random = new Random();
        numeroDeBombas = tamanhoX * tamanhoY / tamanhoX + tamanhoY;
        for (int i = 0; i < numeroDeBombas; i++)
        {
            campo[random.Next(tamanhoX), random.Next(tamanhoY)].Bomba = true ;
        }
        for (int x = 0; x < campo.GetLength(0); x++)
        {
            for (int y = 0; y < campo.GetLength(1); y++)
            {
                campo[x, y].bombasAoRedor = contarBombasAoRedor(campo, x, y);
            }
        }
        return campo;
    }

    public static int contarBombasAoRedor(Quadrado[,] CampoMinado,int x,int y)
    {
        int xAoRedorMin = Math.Max(0, x - 1);
        int yAoRedorMin = Math.Max(0, y - 1);
        int xAoRedorMax = Math.Min(CampoMinado.GetLength(0) - 1, x+1);
        int yAoRedorMax = Math.Min(CampoMinado.GetLength(1) - 1, y+1);
        int numeroDeBombasAoRedor = 0;
        for(int xAoRedor = xAoRedorMin; xAoRedor <= xAoRedorMax;xAoRedor++)
        {
            for(int yAoRedor = yAoRedorMin; yAoRedor <= yAoRedorMax;yAoRedor++)
            {
                if(CampoMinado[xAoRedor,yAoRedor].Bomba == true){
                    numeroDeBombasAoRedor++;
                }
            }
        }
        return numeroDeBombasAoRedor;
    }
    public static void mostrarCampo(Quadrado[,] campo)
    {
        for (int y = 0; y < campo.GetLength(1); y++)
        {
            for (int x = 0; x < campo.GetLength(0); x++)
            {
                if (campo[x, y].Bomba)
                {
                    Console.Write("!");
                }else
                {
                    Console.Write(campo[x,y].bombasAoRedor);
                }
            }
            Console.Write("\n");
        }
    }
    public static void interagir(Quadrado[,] campo, int x,int y )
    {
        if (campo[x,y].Bomba == true)
        {
            morte();
        }else
        {
            campo[x,y].simbolo = Convert.ToChar(campo[x,y].bombasAoRedor);   
        }
        if (campo[x,y].bombasAoRedor == 0)
        {
        int xAoRedorMin = Math.Max(0, x - 1);
        int yAoRedorMin = Math.Max(0, y - 1);
        int xAoRedorMax = Math.Min(campo.GetLength(0) - 1, x+1);
        int yAoRedorMax = Math.Min(campo.GetLength(1) - 1, y+1);
        for(int xAoRedor = xAoRedorMin; xAoRedor <= xAoRedorMax;xAoRedor++)
        {
            for(int yAoRedor = yAoRedorMin; yAoRedor <= yAoRedorMax;yAoRedor++)
            {
                interagir(campo,xAoRedor,yAoRedor);
            }
        }
        }
    }
    public static void morte()
    {
        Console.Clear();
        Console.WriteLine("Você perdeu!");

    }
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var corOriginalFundo = Console.BackgroundColor;
        var corOriginalTexto = Console.ForegroundColor;

        /*for (int i = 0; i < 30; i++)
        {
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write("█");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            System.Console.Write("B");
            Console.BackgroundColor = corOriginalFundo;
            Console.ForegroundColor = corOriginalTexto;
            if (i % 10 == 0)
            {
                System.Console.Write("\n");
            }
        }*/
        /*Espaco[,] campo = new Espaco[5,5];
        Console.WriteLine(campo[0,0].foiInteragido);
        campo[0,0].foiInteragido = true;
        Console.WriteLine(campo[0,0].foiInteragido);
        Espaco[,] Campo = criarCampo(10,8,8);*/
        bool inicioDoPrograma = true;
        bool partida = false;
        bool ligado = true;
        Quadrado[,]? CampoMinado;
        while (ligado)
        {
            while (inicioDoPrograma)
            {
                Console.WriteLine("Bem vindo!");
                Console.WriteLine("Qual largura você deseja para o campo?");
                int xEscolhido = Convert.ToInt32(Console.ReadLine())-1 ;
                Console.WriteLine("Qual altura você deseja para o campo?") ;
                int yEscolhido = Convert.ToInt32(Console.ReadLine())-1 ;
                CampoMinado = criarCampoMinado(0,xEscolhido,yEscolhido);
                partida = true;
                inicioDoPrograma = false;
            }
            while (partida)
            {
                
            }
        }
        mostrarCampo(CampoMinado);


    }
}