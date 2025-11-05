using System;
using System.IO;

namespace Universo
{
    public class GravadorTexto
    {
        public void GravarDados(string nomeArquivo, string dados)
        {
            // O 'try...catch' garante que a aplicação não trave se houver um erro de escrita.
            try
            {
                // Usa 'StreamWriter' para escrever os dados no arquivo.
                // O 'using' garante que o arquivo seja fechado corretamente.
                using (StreamWriter sw = new StreamWriter(nomeArquivo))
                {
                    sw.WriteLine(dados);
                }
            }
            catch (Exception ex)
            {
                // Em caso de erro, você pode exibir uma mensagem ou registrar o problema.
                // Aqui, apenas imprimimos o erro no console para fins de depuração.
                Console.WriteLine("Erro ao gravar o arquivo: " + ex.Message);
            }
        }

        public string LerDados(string nomeArquivo)
        {
            // O 'try...catch' garante que a aplicação não trave se o arquivo não existir.
            try
            {
                // Usa 'StreamReader' para ler o conteúdo do arquivo.
                using (StreamReader sr = new StreamReader(nomeArquivo))
                {
                    // Lê todo o conteúdo do arquivo de uma vez e o retorna.
                    return sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                // Se houver um erro, como 'FileNotFoundException', a aplicação não irá falhar.
                Console.WriteLine("Erro ao ler o arquivo: " + ex.Message);
                return null; // Retorna null para indicar que a leitura falhou.
            }
        }
    }
}