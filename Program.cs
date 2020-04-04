using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraxcnCodingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = Convert.ToInt32(Console.ReadLine());

            string[] ingredientsFixed = { "Fat", "Fiber", "Carb" };
            string[,] stockIngredients = new string[num, 3];
            StringBuilder output = new StringBuilder();

            if (num < 3)
            {
                for (int i = 0; i < num; i++)
                {
                    output.Append(0);
                }
            }

            CalculateOutput(num, stockIngredients, ingredientsFixed, ref output);

            Console.WriteLine(output.ToString());
            Console.ReadLine();
        }

        private static void CalculateOutput(int num, string[,] stockIngredients, string[] ingredientsFixed,
            ref StringBuilder output)
        {
            for (int i = 0; i < num && num >= 3; i++)
            {
                string ingredientInput = Console.ReadLine();

                stockIngredients = StockIngredient(ingredientsFixed, ingredientInput, stockIngredients, i);

                IngredientRulesAreChecked(stockIngredients, ref output, i);
            }
        }

        private static void IngredientRulesAreChecked(string[,] stockIngredients,
            ref StringBuilder output, int i)
        {
            int minimumDaysToCollectIngredients = 3;
            int decisionForCooking = 0;
            int requiredIngredientToCook = 0;
            List<string> dish = new List<string>();
            bool minimumStockRequirement = false;

            if (i >= minimumDaysToCollectIngredients - 1)
            {
                for (int j = 0; j <= i && requiredIngredientToCook < 4; j++)
                {
                    if (stockIngredients[j, 2] == "Used")
                        continue;

                    requiredIngredientToCook = PrepareDish(stockIngredients, j, dish, requiredIngredientToCook, ref decisionForCooking, ref minimumStockRequirement);
                    if (requiredIngredientToCook == minimumDaysToCollectIngredients)
                    {
                        ModifyStockUsed(stockIngredients, i, dish);
                        decisionForCooking = 1;
                    }
                }
            }

            output.Append(decisionForCooking);
        }

        private static void ModifyStockUsed(string[,] stockIngredients, int i, List<string> dish)
        {
            for (int k = 0; k <= i; k++)
            {
                if (stockIngredients[k, 2] == "Used")
                    continue;

                foreach (var item in dish)
                {
                    if (item == stockIngredients[k, 1])
                    {
                        stockIngredients[k, 2] = "Used";
                        break;
                    }
                }
            }
        }

        private static int PrepareDish(string[,] stockIngredients, int j, List<string> dish,
            int requiredIngredientToCook, ref int decisionForCooking, ref bool minimumStockRequirement)
        {
            if (requiredIngredientToCook == 2 && !(dish.Contains(stockIngredients[j, 1]) || minimumStockRequirement))
            {
                return requiredIngredientToCook;
            }
            else if (dish.Contains(stockIngredients[j, 1]))
            {
                minimumStockRequirement = true;
            }

            dish.Add(stockIngredients[j, 1]);
            return ++requiredIngredientToCook;
        }

        private static string[,] StockIngredient(string[] ingredientsFixed, string ingredientInput,
            string[,] stockIngredients,
            int i)
        {
            foreach (var item in ingredientsFixed)
            {
                if (ingredientInput != null && ingredientInput.Contains(item))
                {
                    stockIngredients[i, 0] = ingredientInput;
                    stockIngredients[i, 1] = ingredientInput.Substring(0, item.Length);
                    stockIngredients[i, 2] = "UnUsed";
                }
            }

            return stockIngredients;
        }
    }
}
