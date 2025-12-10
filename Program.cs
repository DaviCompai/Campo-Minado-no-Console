using System;
using System.Text.RegularExpressions;
public struct Quadrado
{
    public bool foiInteragido = false;
    public bool Bomba = false;
    public bool Bandeira = false;
    public int? bombasAoRedor;
    public char simbolo = '█';
    public Quadrado(){}
}
public class Program
{
    public static string titulo =
    @"                                                                                                   
       ▄▄▄  ▄    ▄          ▄▄▄                                                         
     ▄▀   ▀ ██  ██ ▄ ▄▄   ▄▀   ▀                                                        
     █      █ ██ █ █▀  █  █        █                                                    
     █      █ ▀▀ █ █   █  █                                                             
      ▀▄▄▄▀ █    █ █   █   ▀▄▄▄▀   █                                                    
                                                                                    
                                                                                    
                                                                                    
       ▄▄▄                                     ▄    ▄   ▀                      █        
     ▄▀   ▀  ▄▄▄   ▄▄▄▄▄  ▄▄▄▄    ▄▄▄          ██  ██ ▄▄▄    ▄ ▄▄    ▄▄▄    ▄▄▄█   ▄▄▄  
     █      ▀   █  █ █ █  █▀ ▀█  █▀ ▀█         █ ██ █   █    █▀  █  ▀   █  █▀ ▀█  █▀ ▀█ 
     █      ▄▀▀▀█  █ █ █  █   █  █   █         █ ▀▀ █   █    █   █  ▄▀▀▀█  █   █  █   █ 
      ▀▄▄▄▀ ▀▄▄▀█  █ █ █  ██▄█▀  ▀█▄█▀         █    █ ▄▄█▄▄  █   █  ▀▄▄▀█  ▀█▄██  ▀█▄█▀ 
                          █                                                             
                          ▀                                                             
                                                                                    
                            ▄▄▄                              ▀▀█                        
     ▄ ▄▄    ▄▄▄          ▄▀   ▀  ▄▄▄   ▄ ▄▄    ▄▄▄    ▄▄▄     █     ▄▄▄                
     █▀  █  █▀ ▀█         █      █▀ ▀█  █▀  █  █   ▀  █▀ ▀█    █    █▀  █               
     █   █  █   █         █      █   █  █   █   ▀▀▀▄  █   █    █    █▀▀▀▀               
     █   █  ▀█▄█▀          ▀▄▄▄▀ ▀█▄█▀  █   █  ▀▄▄▄▀  ▀█▄█▀    ▀▄▄  ▀█▄▄▀";
    public static int dificuldade = 1;
    public static Quadrado[,] criarCampoMinado(int numeroDeBombas, int tamanhoX, int tamanhoY)
    {
        Quadrado[,] campo = new Quadrado[tamanhoX, tamanhoY];
        Random random = new Random();
        numeroDeBombas = tamanhoX * tamanhoY / 10 * dificuldade;
        for (int i = 0; i < numeroDeBombas; i++)
        {
            campo[random.Next(tamanhoX), random.Next(tamanhoY)].Bomba = true;
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

    public static int contarBombasAoRedor(Quadrado[,] CampoMinado, int x, int y)
    {
        int xAoRedorMin = Math.Max(0, x - 1);
        int yAoRedorMin = Math.Max(0, y - 1);
        int xAoRedorMax = Math.Min(CampoMinado.GetLength(0) - 1, x + 1);
        int yAoRedorMax = Math.Min(CampoMinado.GetLength(1) - 1, y + 1);
        int numeroDeBombasAoRedor = 0;
        for (int xAoRedor = xAoRedorMin; xAoRedor <= xAoRedorMax; xAoRedor++)
        {
            for (int yAoRedor = yAoRedorMin; yAoRedor <= yAoRedorMax; yAoRedor++)
            {
                if (CampoMinado[xAoRedor, yAoRedor].Bomba == true)
                {
                    numeroDeBombasAoRedor++;
                }
            }
        }
        return numeroDeBombasAoRedor;
    }
    public static int contarBandeirasAoRedor(Quadrado[,] CampoMinado, int x, int y)
    {
        int xAoRedorMin = Math.Max(0, x - 1);
        int yAoRedorMin = Math.Max(0, y - 1);
        int xAoRedorMax = Math.Min(CampoMinado.GetLength(0) - 1, x + 1);
        int yAoRedorMax = Math.Min(CampoMinado.GetLength(1) - 1, y + 1);
        int numeroDeBandeirasAoRedor = 0;
        for (int xAoRedor = xAoRedorMin; xAoRedor <= xAoRedorMax; xAoRedor++)
        {
            for (int yAoRedor = yAoRedorMin; yAoRedor <= yAoRedorMax; yAoRedor++)
            {
                if (CampoMinado[xAoRedor, yAoRedor].Bandeira == true)
                {
                    numeroDeBandeirasAoRedor++;
                }
            }
        }
        return numeroDeBandeirasAoRedor;
    }
    public static void mostrarCampo(Quadrado[,] campo)
    {
        for (int y = 0; y < campo.GetLength(1); y++)
        {
            for (int x = 0; x < campo.GetLength(0); x++)
            {
                if (x == 0 && y == 0) //mostra os valores para X
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(" ");
                    printarPosicoes(campo.GetLength(0));
                    Console.ResetColor();
                    Console.Write("\n");
                }
                if (x == 0)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(numeroParaLocal(y+1));
                    Console.ResetColor();
                }
                if (campo[x, y].simbolo == '?')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write(campo[x, y].simbolo);
                    Console.ResetColor();
                }
                else if (campo[x,y].simbolo == '*')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write(campo[x, y].simbolo);
                    Console.ResetColor();
                }
                else if (Char.IsDigit(campo[x, y].simbolo) == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.BackgroundColor = ConsoleColor.Gray;

                    Console.Write(campo[x, y].simbolo);

                    Console.ResetColor();
                }
                else
                {
                    Console.Write(campo[x, y].simbolo == '\0' ? '█' : campo[x, y].simbolo);
                }
            }
            Console.Write("\n");
        }
    }
    public static void printarPosicoes(int quantidade)
    {
        for (int i = 1; i < quantidade + 1; i++)
        {
            Console.Write(numeroParaLocal(i));
        }

    }
    public static bool morto = false;
    public static void interagir(Quadrado[,] campo, int x, int y)
    {
        campo[x, y].Bandeira = false;
        if (campo[x, y].foiInteragido == true)
        {
            int numeroDeBandeirasAoRedor = contarBandeirasAoRedor(campo, x, y);
            // Corrigido: comparar corretamente o dígito visível com o número de bandeiras
            if (char.IsDigit(campo[x, y].simbolo) && (campo[x, y].simbolo - '0') == numeroDeBandeirasAoRedor)
            {
                espalharInteragir(campo, x, y);
            }
            return;
        }
        campo[x, y].foiInteragido = true;
        if (campo[x, y].Bomba == true)
        {
            morto = true;
        }
        else
        {
            campo[x, y].simbolo = campo[x, y].bombasAoRedor.ToString()[0];
        }
        if (campo[x, y].bombasAoRedor == 0)
        {
            campo[x, y].simbolo = '░';
            espalharInteragir(campo,x,y);
        }
    }
    public static void espalharInteragir(Quadrado[,] campo, int x, int y)
    {
        int xAoRedorMin = Math.Max(0, x - 1);
        int yAoRedorMin = Math.Max(0, y - 1);
        int xAoRedorMax = Math.Min(campo.GetLength(0) - 1, x + 1);
        int yAoRedorMax = Math.Min(campo.GetLength(1) - 1, y + 1);
        for (int xAoRedor = xAoRedorMin; xAoRedor <= xAoRedorMax; xAoRedor++)
        {
            for (int yAoRedor = yAoRedorMin; yAoRedor <= yAoRedorMax; yAoRedor++)
            {
                if (campo[xAoRedor, yAoRedor].Bandeira == false && campo[xAoRedor,yAoRedor].foiInteragido == false)
                {
                    interagir(campo, xAoRedor, yAoRedor);
                }
            }
        }
    }
    public static void ColocarBandeira(Quadrado[,] campo, int x, int y)
    {
        if (campo[x, y].foiInteragido == false)
        {
            if (campo[x, y].Bandeira == false)
            {
                campo[x, y].Bandeira = true;
                campo[x, y].simbolo = '?';
            }
            else
            {
                campo[x, y].Bandeira = false;
                campo[x, y].simbolo = '█';
            }
        }
    }
    public static void printarLinhas(int numero_de_linhas)
    {
        for (int i = 0; i < numero_de_linhas; i++)
        {
            Console.WriteLine();
        }
    }
    public static void morte(Quadrado[,] campo)
    {
        printarLinhas(2);
        Console.WriteLine("Você perdeu!");
        for (int y = 0; y < campo.GetLength(1); y++)
        {
            for (int x = 0; x < campo.GetLength(0); x++)
            {
                if (campo[x, y].Bomba == true)
                {
                    campo[x, y].simbolo = '*';
                }
            }
        }
        mostrarCampo(campo);
        Console.WriteLine("Começar de novo? (Sim[s] Não[n])");
        bool naoRespondido = true;
        while (naoRespondido)
        {
            switch (Console.ReadLine().ToLower())
            {
                case "s":
                    morto = false;
                    naoRespondido = false;
                    break;
                case "n":
                    ligado = false;
                    naoRespondido = false;
                    break;
                default:
                    EscreverEstiloAviso("Entrada inválida, escolha S ou N");
                    break;
            }
        }
    }
    public static bool ligado = true;
    public static char numeroParaLocal(int numero)
    {

        if (numero >= 0 && numero <= 9)
            return (char)('0' + numero);

        if (numero >= 10 && numero <= 35)
            return (char)('a' + (numero - 10));

        if (numero >= 36 && numero <= 61)
            return (char)('A' + (numero - 36));
        return 'X';
    }

    public static int localParaNumero(char simbolo)
    {
        if (char.IsDigit(simbolo))
            return simbolo - '0';

        if (simbolo >= 'a' && simbolo <= 'z')
            return 10 + (simbolo - 'a');

        if (simbolo >= 'A' && simbolo <= 'Z')
            return 36 + (simbolo - 'A');

        return -1;
    }

    public static bool bJaVenceu(Quadrado[,] campo)
    {
        int numeroDeBandeirasCertas = 0;
        int numeroDeBombas = 0;
        for (int x = 0; x < campo.GetLength(0); x++)
        {
            for (int y = 0; y < campo.GetLength(1); y++)
            {
                if (campo[x, y].Bomba)
                { 
                    numeroDeBombas++;
                }
                if (campo[x, y].Bandeira)
                {
                    if (campo[x, y].Bomba)
                    {
                        numeroDeBandeirasCertas++;
                    }
                }
            }
        }
        if (numeroDeBandeirasCertas == numeroDeBombas)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public static void EscreverEstiloAviso(string entrada)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.BackgroundColor = ConsoleColor.White;
        Console.Write(entrada);
        Console.ResetColor();
        Console.Write("\n");
    }
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var corOriginalFundo = Console.BackgroundColor;
        var corOriginalTexto = Console.ForegroundColor;

        bool inicioDoPrograma = true;
        bool partida = false;
        Quadrado[,]? CampoMinado = null;
        Console.WriteLine(titulo);
        while (ligado)
        {
            while (inicioDoPrograma)
            {
                printarLinhas(3);
                Console.WriteLine("Bem vindo!");
                Console.WriteLine("Qual tamanho você deseja para o campo?");
                string? entradaTamanhoCampo = Console.ReadLine();
                if (int.TryParse(entradaTamanhoCampo, out _) == false)
                {
                    EscreverEstiloAviso("Tamanho invalido! Escolha um valor numérico de no mínimo 3 e no máximo 30");
                    break;
                }
                int tamanhoCampo = Convert.ToInt32(entradaTamanhoCampo);
                if (tamanhoCampo < 3 || tamanhoCampo > 30)
                {
                    EscreverEstiloAviso("Tamanho inadequado! escolha um valor de no mínimo 3 e no máximo 30.");
                    break;
                }
                CampoMinado = criarCampoMinado(0, tamanhoCampo*2, tamanhoCampo);
                partida = true;
                inicioDoPrograma = false;
            }
            while (partida)
            {
                while(bJaVenceu(CampoMinado))
                {
                    printarLinhas(2);
                    Console.WriteLine("Parabéns! você ganhou.");
                    Console.WriteLine("Deseja começar novamente? (Sim[s] ou Não[n])");
                    string? escolha = Console.ReadLine();
                    if (escolha == "S")
                    {
                        inicioDoPrograma = true;
                        partida = false;
                    }
                    else if (escolha == "N")
                    {
                        inicioDoPrograma = false;
                        partida = false;
                    }
                    else
                    {
                        EscreverEstiloAviso("Entrada incorreta! responda com S ou N!");
                    }
                }
                mostrarCampo(CampoMinado);
                Console.WriteLine("Digite sua ação (interagir[i],bandeira[b]) acompanhada pelas cordenadas x e y, separadas por virgulas");
                printarLinhas(3);
                string? entradaRaw = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(entradaRaw))
                {
                    EscreverEstiloAviso("Entrada vazia. Formato: ação,x,y (ex: i,5,5)");
                    break;
                }

                var partes = entradaRaw.Split(',');
                if (partes.Length != 3)
                {
                    EscreverEstiloAviso("Formato inválido. Use: ação,x,y (ex: i,5,5)");
                    break;
                }
                string tipo = partes[0];
                int localX = localParaNumero(partes[1][0]) - 1;
                int localY = localParaNumero(partes[2][0]) - 1;
                if (localX < 0 || localX > CampoMinado.GetLength(0))
                {
                    EscreverEstiloAviso($"Valor de X incompatível! Escolha um valor entre 1 e {CampoMinado.GetLength(0)}");
                    break;
                }
                if (localY < 0 || localY > CampoMinado.GetLength(1))
                {
                    EscreverEstiloAviso($"Valor de Y incompatível! Escolha um valor entre 1 e {CampoMinado.GetLength(1)}");
                    break;
                }
                if (tipo.ToLower() == "i")
                {
                    interagir(CampoMinado,localX,localY);
                }
                else if(tipo.ToLower() == "b")
                {
                    ColocarBandeira(CampoMinado, localX, localY);
                }
                else
                {
                    EscreverEstiloAviso("Entrada inválida! o primeiro dígito deve ser i ou b.");
                }
                if (morto == true)
                {
                    inicioDoPrograma = true;
                    morte(CampoMinado);
                    partida = false;
                }
            }
        }
    }
}