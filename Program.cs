using System;

namespace Greedy
{
    class Program
    {
        // Get the items ratio: value / weight
        // Sort the items by ratio in descending order
        // Get the sum of the items weight and value. 
            // If there's no room for the entire last item, last item final value = remaining space / last item initial value (final = remaining/initial)
        
        const int KNAP_CAPACITY = 60;
        const int NUM_ITEMS = 10;

        struct Item 
        {
            public double value;
            public double weight;
            public double ratio;
        }

        static double KnapSack(int[] weights, int[] values)
        {   
            Item[] items = new Item[NUM_ITEMS];
            for (int i = 0; i < NUM_ITEMS; ++i)
            {
                items[i].weight = weights[i];
                items[i].value = values[i];
                items[i].ratio = items[i].value / items[i].weight;
            }
            
            Array.Sort<Item>(items, (x,y) => y.ratio.CompareTo(x.ratio));

            double totalSackValue = 0;
            double totalWeight = 0;
            double remainingWeight = KNAP_CAPACITY;
            
            for (int i = 0; i < NUM_ITEMS; i++)
            {
                
                totalWeight += items[i].weight;
                remainingWeight -= items[i].weight;

                if (totalWeight == KNAP_CAPACITY)
                {
                    break;
                }
                else if (totalWeight > KNAP_CAPACITY)
                {
                    double coeff = Math.Abs(remainingWeight) / items[i].weight;
                    items[i].value *= coeff;
                    items[i].weight *= coeff;
                    remainingWeight += items[i].weight;
                    totalWeight += items[i].weight;
					break;
                }
                totalSackValue += items[i].value;
            }
            return totalSackValue;
        }

        static void Main(string[] args)
        {
            int min = 0;
            int max = 50;

            int[] values = new int[NUM_ITEMS];
            int[] weights = new int[NUM_ITEMS];

            Random randNum = new Random();
            for (int i = 0; i < NUM_ITEMS; i++)
            {
                values[i] = randNum.Next(min, max);
                weights[i] = randNum.Next(min, max);
            }

            double result = KnapSack(weights, values);

            Console.WriteLine(result);
        }
    }
}
