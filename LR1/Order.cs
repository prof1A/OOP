using System;

namespace BaguetFactory
{
    class Order : ICloneable
    {
        //final cost of baguet
        public double Cost = 0;
        public delegate bool deleg(Type material, double Amount);
        deleg del;
        public Order()
        {
            //Storage st = new Storage();
            del = Storage.MaterialTakingFromDB;
        }
        public Order(deleg meth)
        {
            del = meth;
        }
        public Baguet MakeOrder(int Width, int Height, params Type[] materials)
        {
            IMaterial mt = new Wood();
            IMaterial[] arr = new IMaterial[materials.Length];

            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i].ToString() == "BaguetFactory.Wood")
                {
                    mt = new Wood();
                    if (del(typeof(Wood), (Width * Height * mt.NeededToCreate)))
                    {

                        Cost += Width * Height * mt.NeededToCreate * mt.Cost;
                        arr[i] = new Wood();
                        Storage.MaterialTakingPerWeek(Width * Height * mt.NeededToCreate);
                    }
                    else
                    {
                        Cost = -1;
                        break;
                    }
                }
                else if (materials[i].ToString() == "BaguetFactory.MetalProfile")
                {
                    mt = new MetalProfile();
                    if (del(typeof(MetalProfile), (Width * Height * mt.NeededToCreate)))
                    {

                        Cost += Width * Height * mt.NeededToCreate * mt.Cost;
                        arr[i] = new MetalProfile();
                        Storage.MaterialTakingPerWeek(Width * Height * mt.NeededToCreate);
                    }
                    else
                    {
                        Cost = -1;
                        break;
                    }
                }
                else if (materials[i].ToString() == "BaguetFactory.PlasticProfile")
                {
                    mt = new PlasticProfile();
                    if (del(typeof(PlasticProfile), (Width * Height * mt.NeededToCreate)))
                    {

                        Cost += Width * Height * mt.NeededToCreate * mt.Cost;
                        arr[i] = new PlasticProfile();
                        Storage.MaterialTakingPerWeek(Width * Height * mt.NeededToCreate);
                    }
                    else
                    {
                        Cost = -1;
                        break;
                    }
                }
                else if (materials[i].ToString() == "BaguetFactory.Dye")
                {
                    mt = new Dye();
                    if (del(typeof(Dye), (Width * Height * mt.NeededToCreate)))
                    {

                        Cost += Width * Height * mt.NeededToCreate * mt.Cost;
                        arr[i] = new Dye();
                        Storage.MaterialTakingPerWeek(Width * Height * mt.NeededToCreate);
                    }
                    else
                    {
                        Cost = -1;
                        break;
                    }
                }
                else if (materials[i].ToString() == "BaguetFactory.Polish")
                {
                    mt = new Polish();
                    if (del(typeof(Polish), (Width * Height * mt.NeededToCreate)))
                    {

                        Cost += Width * Height * mt.NeededToCreate * mt.Cost;
                        arr[i] = new Polish();
                        Storage.MaterialTakingPerWeek(Width * Height * mt.NeededToCreate);
                    }
                    else
                    {
                        Cost = -1;
                        break;
                    }
                }
            }
            return new Baguet
            {
                Width = Width,
                Height = Height,
                Cost = this.Cost
                //materials = arr
            };
        }
        public object Clone()
        {
            return new Order { Cost = this.Cost, del = this.del };
        }
    }
}