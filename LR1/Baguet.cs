using System;
using System.Runtime.Serialization;

namespace BaguetFactory
{
    [Serializable]
    [DataContract]
    public class Baguet
    {
        [DataMember]
        public int Width;
        [DataMember]
        public int Height;
        [DataMember]
        public double Cost;
    }
    interface IMaterial
    {
        int Cost { get; }
        double NeededToCreate { get; }
    }
    class Wood : IMaterial
    {
        public int Cost { get; }
        public double NeededToCreate { get; }
        public Wood()
        {
            Cost = 100;
            NeededToCreate = 1.2;
        }
    }
    class MetalProfile : IMaterial
    {
        public int Cost { get; }
        public double NeededToCreate { get; }
        public MetalProfile()
        {
            Cost = 1000;
            NeededToCreate = 1.6;
        }
    }
    class PlasticProfile : IMaterial
    {
        public int Cost { get; }
        public double NeededToCreate { get; }
        public PlasticProfile()
        {
            Cost = 700;
            NeededToCreate = 1.4;
        }
    }
    class Dye : IMaterial
    {
        public int Cost { get; protected set; }
        public double NeededToCreate { get; protected set; }
        public Dye()
        {
            Cost = 450;
            NeededToCreate = 0.04;
        }
    }
    class Polish : Dye
    {
        public Polish()
        {
            Cost = 300;
            NeededToCreate = 0.07;
        }
    }
}