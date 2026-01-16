using System;
using System.Linq;

class Exercicio9
{
    public class Jogador
    {
        public string Nome { get; set; }
        public int Nivel { get; set; }
        public double Credito { get; set; }

        public Jogador(string nome, int nivel, double credito)
        {
            Nome = nome;
            Nivel = nivel;
            Credito = credito;
        }

        public void Exibir()
        {
            Console.WriteLine($"Nome: {Nome} \nCrédito: {Credito}$\nNível: {Nivel}\n");
        }
    }

    public class LojaDeItens
    {
        public string Item { get; set; }
        public double Preco { get; set; }
        public int Nivel { get; set; }

        public LojaDeItens(string item, double preco, int nivel)
        {
            Item = item;
            Preco = preco;
            Nivel = nivel;
        }
    }

    static void Main()
    {
        // Cria o jogador
        Jogador jogador = new Jogador("Jogador", 3, 80);

        // Criação dos itens disponíveis na loja
        LojaDeItens[] itensLoja = {
            new LojaDeItens("Espada", 35, 1),
            new LojaDeItens("Arco", 25, 1),
            new LojaDeItens("Lança", 15, 1)
        };

        // Respostas positivas e negativas
        string[] respostasNegativas = { "N", "NÃO", "NAO", "NO", "NOT", "NEGATIVO" };
        string[] respostasPositivas = { "S", "SIM", "Y", "YES", "CLARO" };

        bool rodando = true;

        Console.WriteLine("=== Bem-vindo à Loja de Itens! ===\n");

        while (rodando)
        {
            jogador.Exibir();

            Console.WriteLine("Deseja comprar algo? (Y/N)");
            string comprar = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

            if (respostasPositivas.Contains(comprar))
            {
                Console.WriteLine("\nItens disponíveis:");
                foreach (var item in itensLoja)
                {
                    Console.WriteLine($"{item.Item}: {item.Preco}$ (Nível {item.Nivel})");
                }

                Console.WriteLine("\nDigite o item que deseja comprar:");
                string opcao = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

                // Procura o item pelo nome
                LojaDeItens escolhido = itensLoja.FirstOrDefault(i => i.Item.ToUpperInvariant() == opcao);

                if (escolhido != null)
                {
                    ComprarItem(escolhido, jogador);
                }
                else
                {
                    Console.WriteLine("- Item inválido -");
                }

                Console.WriteLine();
            }
            else if (respostasNegativas.Contains(comprar))
            {
                Console.WriteLine("\n- Volte sempre! -");
                rodando = false;
            }
            else
            {
                Console.WriteLine("\nComando inválido. Digite Y para sim ou N para não.\n");
            }
        }

        Console.WriteLine("\n=== Loja encerrada ===");
    }

    static bool ChecarSaldo(double valor, double credito)
    {
        return credito >= valor;
    }

    static void ReduzirCredito(Jogador jogador, double valor)
    {
        jogador.Credito -= valor;
    }

    static void ComprarItem(LojaDeItens item, Jogador jogador)
    {
        if (!ChecarSaldo(item.Preco, jogador.Credito))
        {
            Console.WriteLine($"- Saldo insuficiente para comprar {item.Item} ({item.Preco}$) -");
        }
        else if (jogador.Nivel < item.Nivel)
        {
            Console.WriteLine($"- Nível insuficiente para comprar {item.Item} (Necessário: {item.Nivel}) -");
        }
        else
        {
            ReduzirCredito(jogador, item.Preco);
            Console.WriteLine($"Compra realizada! Você adquiriu {item.Item}. Crédito restante: {jogador.Credito}$");
        }
    }
}