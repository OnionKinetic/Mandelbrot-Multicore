namespace mandelbrot_MultiCore
{
    public class ClusterHost
    {
        public string Name { get; internal set; }
        public string IP { get; internal set; }
        public int Cores { get; internal set; }

        public ClusterHost(string Name, string IP, int Cores)
        {
            this.Name = Name;
            this.IP = IP;
            this.Cores = Cores;
        }
    }
}
