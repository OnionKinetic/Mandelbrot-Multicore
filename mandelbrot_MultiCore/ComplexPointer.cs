namespace mandelbrot_MultiCore
{
    class ComplexPointer
    {
        public ComplexNumber Ul;
        public ComplexNumber Lr;
        public int PointerID;

        public ComplexPointer(ComplexNumber Ul, ComplexNumber Lr, int PointerID)
        {
            this.Ul = Ul;
            this.Lr = Lr;
            this.PointerID = PointerID;
        }

        public ComplexPointer()
        {
            this.Ul = new ComplexNumber();
            this.Lr = new ComplexNumber();
            this.PointerID = 0;
        }
    }
}
