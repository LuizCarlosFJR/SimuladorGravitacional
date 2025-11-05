using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Universo
{
    public class Universo
    {   // Força -> medida em N
        // Massa -> medida em Kg
        // Distância -> medida em m
        // G = 6,67408 X 10E-11 

        private ObservableCollection<Corpos> listaCorp;
        private double G = 6.67408 * Math.Pow(10, -11.0);

        public Universo()
        {
            listaCorp = new ObservableCollection<Corpos>();
        }

        public Corpos getCorpo(int pos)
        {
            if ((pos >= 0) && (pos < listaCorp.Count()))
            {
                return listaCorp.ElementAt(pos);
            }
            else
            {
                return null;
            }
        }

        public ObservableCollection<Corpos> getCorpo()
        {
            return listaCorp;
        }

        public void setCorpo(Corpos cp, int pos){
            if(pos < listaCorp.Count())
            {
                listaCorp.ElementAt(pos).copCorp(cp);
            }
            else 
            {
                listaCorp.Add(cp);
            }
        }

        public int qtdCorp() {
            return listaCorp.Count();
        }

        public double dist(Corpos c1, Corpos c2)
        {
            double b, c;

            b = c1.getPosY() - c2.getPosY();
            c = c1.getPosX() - c2.getPosX();

            return Math.Sqrt(Math.Pow(b, 2) + Math.Pow(c, 2));
        }

        private void forcaG(Corpos c1, Corpos c2)
        {
            double hipotenusa = dist(c2, c1);
            if (hipotenusa == 0) return;

            double catetoAdjacenteC1 = c2.getPosY() - c1.getPosY();
            double catetoOpostoC1 = c2.getPosX() - c1.getPosX();

            double forca = G * ((c1.getMassa() * c2.getMassa()) / Math.Pow(hipotenusa, 2));
            double forcaY = catetoAdjacenteC1 * forca / hipotenusa;
            double forcaX = catetoOpostoC1 * forca / hipotenusa;

            lock (c1)
            {
                c1.setForcaX(c1.getForcaX() + forcaX);
                c1.setForcaY(c1.getForcaY() + forcaY);
            }

            lock (c2)
            {
                c2.setForcaX(c2.getForcaX() - forcaX);
                c2.setForcaY(c2.getForcaY() - forcaY);
            }
        }


        private bool colisao(Corpos c1, Corpos c2)
        {
            double Px;
            double Py;
            double d;
            bool teveColisao = false;

            if ((dist(c1, c2)) <= (c1.getRaio() + c2.getRaio()))
            {
                teveColisao = true;
                Px = (c1.getMassa() * c1.getVelX()) + (c2.getMassa() * c2.getVelX());
                Py = (c1.getMassa() * c1.getVelY()) + (c2.getMassa() * c2.getVelY());

                d = ((c1.getMassa() * c1.getDensidade() + c2.getMassa() * c2.getDensidade()) / 
                     (c1.getMassa() + c2.getMassa()));

                if (c1.getMassa() >= c2.getMassa())
                {
                    c1.setNome(c1.getNome() + c2.getNome());
                    c1.setMassa(c1.getMassa() + c2.getMassa());
                    c1.setDensidade(d);
                    
                    c1.setVelX(Px / c1.getMassa());
                    c1.setVelY(Py / c1.getMassa());

                    c2.setValido(false);
                }
                else
                {
                    c2.setNome(c2.getNome() + c1.getNome());
                    c2.setMassa(c2.getMassa() + c1.getMassa());
                    c2.setDensidade(d);

                    c2.setVelX(Px / c2.getMassa());
                    c2.setVelY(Py / c2.getMassa());

                    c1.setValido(false);

                }
            }
            return teveColisao;
        }

        public void carCorp(int numCorpos, int xIni, int xFim, int yIni, int yFim, int masIni, int masFim)
        {
            // 1. Limpa a lista de corpos existente, garantindo um novo universo a cada clique.
            listaCorp.Clear();

            Random rd = new Random();

            for (int i = 0; i < numCorpos; i++)
            {
                string nome = "cp" + i;
                // A massa está sendo gerada corretamente.
                int massa = rd.Next(masIni, masFim);

                // Gere posições e densidades aleatórias com base nos limites da UI.
                int posX = rd.Next(xIni, xFim);
                int posY = rd.Next(yIni, yFim);
                int densidade = rd.Next(1, 255);

                // 2. Gere velocidades aleatórias para tornar a simulação dinâmica.
                // O método NextDouble() retorna um número entre 0.0 e 1.0.
                // Multiplicamos por um valor (ex: 10) para definir um intervalo de velocidade.
                double velX = (rd.NextDouble() - 0.5) * 20; // Velocidade entre -10 e 10
                double velY = (rd.NextDouble() - 0.5) * 20; // Velocidade entre -10 e 10

                // Cria o novo corpo com os valores gerados aleatoriamente.
                // Note que o construtor Corpos deve aceitar todos esses parâmetros.
                listaCorp.Add(new Corpos(nome, massa, posX, posY, 0, velX, velY, 0, densidade));
            }
        }

        public void interCorp(int qtdSegundos)
        {
            // Passo 1: Zera as forças de todos os corpos para a nova iteração.
            zeraForc();

            // Passo 2: Calcula as forças gravitacionais entre todos os pares de corpos.
            // Isso precisa ser feito ANTES de atualizar posições e velocidades,
            // e antes de verificar colisões, usando as posições ATUAIS.
            Parallel.For(0, qtdCorp(), i =>
            {
                for (int j = i + 1; j < qtdCorp(); j++)
                {
                    forcaG(listaCorp[i], listaCorp[j]);
                }
            });

            // Passo 3: Atualiza a velocidade e a posição de cada corpo
            // com base nas forças calculadas.
            Parallel.For(0, qtdCorp(), i =>
            {
                calcVelPosCorpos(qtdSegundos, listaCorp[i]);
            });

            // Passo 4: Verifica e trata colisões APÓS as posições terem sido atualizadas.
            // O loop precisa ser sequencial para evitar problemas de concorrência
            // ao modificar a lista com a flag 'Valido'.
            for (int i = 0; i < qtdCorp() - 1; i++)
            {
                if (!listaCorp[i].getVal()) continue; // Se o corpo já foi desativado por uma colisão anterior, pular

                for (int j = i + 1; j < qtdCorp(); j++)
                {
                    if (!listaCorp[j].getVal()) continue; // Se o corpo já foi desativado, pular

                    colisao(listaCorp[i], listaCorp[j]);
                }
            }

            // Passo 5: Remove os corpos que foram marcados como inválidos devido a colisões.
            OrganizaUniverso();
        }
        // Termina por volta da linha 229


        public void copiaUniverso(Universo u)
        {
            listaCorp = new ObservableCollection<Corpos>();
            Corpos cp;
            for (int i = 0; i < u.qtdCorp(); i++)
            {
                cp = new Corpos(u.getCorpo(i).getNome(),
                               u.getCorpo(i).getMassa(),
                               u.getCorpo(i).getPosX(),
                               u.getCorpo(i).getPosY(),
                               u.getCorpo(i).getPosZ(),
                               u.getCorpo(i).getVelX(),
                               u.getCorpo(i).getVelY(),
                               u.getCorpo(i).getVelZ(),
                               u.getCorpo(i).getDensidade());
                this.setCorpo(cp, i);
            }
        }

        private void zeraForc()
        {
            Parallel.For(0, qtdCorp(), i =>
            {
                listaCorp[i].setForcaX(0);
                listaCorp[i].setForcaY(0);
                listaCorp[i].setForcaZ(0);
            });
        }

        private void calcVelPosCorpos(int qtdSegundos, Corpos c1)
        {
            double acelX;
            double acelY;

            acelX = c1.getForcaX() / c1.getMassa();
            acelY = c1.getForcaY() / c1.getMassa();

            c1.setPosX(c1.getPosX() + (c1.getVelX() * qtdSegundos) + (acelX * Math.Pow(qtdSegundos, 2) / 2));
            c1.setVelX(c1.getVelX() + (acelX * qtdSegundos));

            c1.setPosY(c1.getPosY() + (c1.getVelY() * qtdSegundos) + (acelY * Math.Pow(qtdSegundos, 2) / 2));
            c1.setVelY(c1.getVelY() + (acelY * qtdSegundos));

        }


        // Linha 314 do código completo fornecido anteriormente
        private void OrganizaUniverso()
        {
            // Filtra os corpos que são válidos e cria uma nova lista com eles.
            var corposValidos = listaCorp.Where(c => c.getVal()).ToList();

            // Limpa a ObservableCollection existente e a preenche com os corpos válidos.
            // Esta é uma maneira segura de remover itens em massa sem problemas de indexação.
            listaCorp.Clear();
            foreach (var corpo in corposValidos)
            {
                listaCorp.Add(corpo);
            }
        }
        // Termina por volta da linha 322
    }
}
