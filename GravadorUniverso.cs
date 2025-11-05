using System;
using System.IO;
using System.Linq;

namespace Universo
{
    public abstract class GravadorUniverso
    {
        // Método abstrato para gravar a configuração inicial do universo
        public abstract void GravarUniversoInicial(Universo universo, string caminho);

        // Método abstrato para gravar o estado do universo com iterações
        public abstract void GravarUniverso(Universo universo, string caminho, int numInterac, int numTempoInterac);

        // Método abstrato para carregar a configuração inicial do universo
        public abstract Universo CarregarUniversoInicial(string caminho);

        // Método abstrato para carregar uma simulação completa com iterações
        public abstract Universo CarregarSimulacao(string caminho, out int numInterac, out int numTempoInterac);
    }

    public class GravadorTexto : GravadorUniverso
    {
        public override void GravarUniversoInicial(Universo universo, string caminho)
        {
            using (StreamWriter sw = new StreamWriter(caminho))
            {
                sw.WriteLine(universo.qtdCorp()); // Quantidade de corpos
                for (int i = 0; i < universo.qtdCorp(); i++)
                {
                    Corpos cp = universo.getCorpo(i);
                    if (cp != null && cp.getVal())
                    {
                        // Formato: <Nome>;<massa>;<raio>;<PosX>;<PosY>;<VelX>;<VelY>
                        string linha = $"{cp.getNome()};{cp.getMassa()};{cp.getRaio()};{cp.getPosX()};{cp.getPosY()};{cp.getVelX()};{cp.getVelY()}";
                        sw.WriteLine(linha);
                    }
                }
            }
        }

        public override void GravarUniverso(Universo universo, string caminho, int numInterac, int numTempoInterac)
        {
            using (StreamWriter sw = new StreamWriter(caminho))
            {
                // Formato: <quantidade de corpos>;<quantidade de iterações>;<tempo entre iterações>
                sw.WriteLine($"{universo.qtdCorp()};{numInterac};{numTempoInterac}");
                for (int i = 0; i < universo.qtdCorp(); i++)
                {
                    Corpos cp = universo.getCorpo(i);
                    if (cp != null && cp.getVal())
                    {
                        // Formato: <Nome>;<massa>;<raio>;<PosX>;<PosY>;<VelX>;<VelY>
                        string linha = $"{cp.getNome()};{cp.getMassa()};{cp.getRaio()};{cp.getPosX()};{cp.getPosY()};{cp.getVelX()};{cp.getVelY()}";
                        sw.WriteLine(linha);
                    }
                }
            }
        }

        public override Universo CarregarUniversoInicial(string caminho)
        {
            Universo universo = new Universo();
            using (StreamReader sr = new StreamReader(caminho))
            {
                int numCorpos = int.Parse(sr.ReadLine());
                for (int i = 0; i < numCorpos; i++)
                {
                    string[] valores = sr.ReadLine().Split(';');
                    Corpos cp = new Corpos(
                        valores[0], // Nome
                        double.Parse(valores[1]), // Massa
                        double.Parse(valores[3]), // PosX
                        double.Parse(valores[4]), // PosY
                        0, // PosZ (não usado)
                        double.Parse(valores[5]), // VelX
                        double.Parse(valores[6]), // VelY
                        0, // VelZ (não usado)
                        double.Parse(valores[1]) / Math.Pow(double.Parse(valores[2]), 3) * (3 / (4 * Math.PI)) // Densidade estimada
                    );
                    universo.setCorpo(cp, i);
                }
            }
            return universo;
        }

        public override Universo CarregarSimulacao(string caminho, out int numInterac, out int numTempoInterac)
        {
            Universo universo = new Universo();
            using (StreamReader sr = new StreamReader(caminho))
            {
                string[] cabecalho = sr.ReadLine().Split(';');
                int numCorpos = int.Parse(cabecalho[0]);
                numInterac = int.Parse(cabecalho[1]);
                numTempoInterac = int.Parse(cabecalho[2]);
                for (int i = 0; i < numCorpos; i++)
                {
                    string[] valores = sr.ReadLine().Split(';');
                    Corpos cp = new Corpos(
                        valores[0], // Nome
                        double.Parse(valores[1]), // Massa
                        double.Parse(valores[3]), // PosX
                        double.Parse(valores[4]), // PosY
                        0, // PosZ (não usado)
                        double.Parse(valores[5]), // VelX
                        double.Parse(valores[6]), // VelY
                        0, // VelZ (não usado)
                        double.Parse(valores[1]) / Math.Pow(double.Parse(valores[2]), 3) * (3 / (4 * Math.PI)) // Densidade estimada
                    );
                    universo.setCorpo(cp, i);
                }
            }
            return universo;
        }
    }
}