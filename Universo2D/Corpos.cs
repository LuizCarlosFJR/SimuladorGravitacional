using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Universo
{
    public class Corpos
    {
        private bool Val;
        private String nome;
        private double massa;
        private double densidade;
        private double posX;
        private double posY;
        private double posZ;
        private double velX;
        private double velY;
        private double velZ;
        private double forcaX;
        private double forcaY;
        private double forcaZ;

        public Corpos()
        {
            this.Val = true;
            this.nome = "";
            this.massa = 0;
            this.densidade = 1;
            this.posX = 0;
            this.posY = 0;
            this.posZ = 0;
            this.velX = 0;
            this.velY = 0;
            this.velZ = 0;
            this.forcaX = 0;
            this.forcaY = 0;
            this.forcaZ = 0;
        }

        public Corpos(String n, double m, double pX, double pY, double pZ, double vX, double vY, double vZ, double d)
        {
            this.Val = true;
            this.nome = n;
            this.massa = m;
            this.posX = pX;
            this.posY = pY;
            this.posZ = pZ;
            this.velX = vX;
            this.velY = vY;
            this.velZ = vZ;
            this.forcaX = 0;
            this.forcaY = 0;
            this.forcaZ = 0;
            this.densidade = d;
        }
        public bool getVal() {
            return this.Val;
        }
        public String getNome()
        {
            return this.nome;
        }
        public double getMassa()
        {
            return this.massa;
        }
        public double getPosX()
        {
            return this.posX;
        }
        public double getPosY()
        {
            return this.posY;
        }
        public double getPosZ()
        {
            return this.posZ;
        }
        public double getVelX()
        {
            return this.velX;
        }
        public double getVelY()
        {
            return this.velY;
        }
        public double getVelZ()
        {
            return this.velZ;
        }
        public double getForcaX()
        {
            return this.forcaX;
        }
        public double getForcaY()
        {
            return this.forcaY;
        }
        public double getForcaZ()
        {
            return this.forcaZ;
        }
        public double getDensidade()
        {
            return this.densidade;
        }
        public double getRaio()
        {
            double raio = Math.Pow((3 * Math.PI * this.massa) / (4 * this.densidade), ((double)1 / 3));

            return raio / 5;
        }

        public void setValido(bool v) {
            this.Val = v;
        }
        public void setNome(string nome)
        {
            this.nome = nome;
        }
        public void setMassa(double m)
        {
            this.massa = m;
        }
        public void setPosX(double x)
        {
            this.posX = x;
        }
        public void setPosY(double y)
        {
            this.posY = y;
        }
        public void setPosZ(double z)
        {
            this.posZ = z;
        }
        public void setVelX(double x)
        {
            this.velX = x;
        }
        public void setVelY(double y)
        {
            this.velY = y;
        }
        public void setVelZ(double z)
        {
            this.velZ = z;
        }
        public void setForcaX(double forca)
        {
            this.forcaX = forca;
        }
        public void setForcaY(double forca)
        {
            this.forcaY = forca;
        }
        public void setForcaZ(double forca)
        {
            this.forcaZ = forca;
        }
        public void setDensidade(double dens) {
            this.densidade = dens;
        }

        public void copCorp(Corpos cp)
        {
            this.nome = cp.getNome();
            this.massa = cp.getMassa();
            this.posX = cp.getPosX();
            this.posY = cp.getPosY();
            this.posZ = cp.getPosZ();
            this.velX = cp.getVelX();
            this.velY = cp.getVelY();
            this.velZ = cp.getVelZ();
        }   

    }
}
