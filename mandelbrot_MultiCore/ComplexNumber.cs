using System;

namespace mandelbrot_MultiCore
{
    class ComplexNumber
    {
        public double Real;
        public double Imag;

        public ComplexNumber()
        {
            Real = 0.0;
            Imag = 0.0;
        }

        public ComplexNumber(double Real, double Imaginary)
        {
            this.Real = Real;
            this.Imag = Imaginary;
        }

        public static ComplexNumber operator +(ComplexNumber c1, ComplexNumber c2)
        {
            ComplexNumber NewCN = new ComplexNumber();
            NewCN.Real = c1.Real + c2.Real;
            NewCN.Imag = c1.Imag + c2.Imag;
            return NewCN;
        }

        public static ComplexNumber operator -(ComplexNumber c1, ComplexNumber c2)
        {
            ComplexNumber NewCN = new ComplexNumber();
            NewCN.Real = c1.Real - c2.Real;
            NewCN.Imag = c1.Imag - c2.Imag;
            return NewCN;
        }

        public static ComplexNumber operator *(ComplexNumber c1, ComplexNumber c2)
        {
            ComplexNumber NewCN = new ComplexNumber();
            NewCN.Real = c1.Real * c2.Real - c1.Imag * c2.Imag;
            NewCN.Imag = c1.Real * c2.Imag + c1.Imag * c2.Real;
            return NewCN;
        }

        public static bool operator <(ComplexNumber c1, ComplexNumber c2)
        {
            return Math.Sqrt(c1.Real * c1.Real + c1.Imag * c1.Imag) < Math.Sqrt(c2.Real * c2.Real + c2.Imag * c2.Imag);
        }

        public static bool operator >(ComplexNumber c1, ComplexNumber c2)
        {
            return Math.Sqrt(c1.Real * c1.Real + c1.Imag * c1.Imag) > Math.Sqrt(c2.Real * c2.Real + c2.Imag * c2.Imag);
        }

        public override string ToString()
        {
            return string.Format("{0} + {1}i", this.Real, this.Imag);
        }

        public ComplexNumber Power(int pow)
        {
            ComplexNumber newCN = this;
            for (int i = 1; i < pow; i++)
            {
                newCN = newCN * this;
            }
            return newCN;
        }
    }
}
