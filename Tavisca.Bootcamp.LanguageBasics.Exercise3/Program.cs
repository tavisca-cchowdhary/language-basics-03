using System;
using System.Collections.Generic;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            // Add your code here.
            int total = protein.Length;
            List <int> copy = new List <int> ();
            int [] meal = new int [dietPlans.Length];
            int [] cal = new int [total];
            int i,j;
            for(i=0;i<total;i++)
                cal[i] = 5*protein[i] + 5*carbs[i] + 9*fat[i];
            
            for(i=0;i<dietPlans.Length;i++)
            {
                copy.Clear();
                for(j=0;j<total;j++)
                    copy.Add(j);
                int dietlen=dietPlans[i].Length;
                foreach(var character in dietPlans[i])
                {
                    if(character=='c')
                        meal[i]=index(character,carbs,ref copy,dietlen);
                    else if(character=='C')
                        meal[i]=index(character,carbs,ref copy,dietlen);
                    else if(character=='p')
                        meal[i]=index(character,protein,ref copy,dietlen);
                    else if(character=='P')
                        meal[i]=index(character,protein,ref copy,dietlen);
                    else if(character=='t')
                        meal[i]=index(character,cal,ref copy,dietlen);
                    else if(character=='T')
                        meal[i]=index(character,cal,ref copy,dietlen);
                    else if(character=='f')
                        meal[i]=index(character,fat,ref copy,dietlen);
                    else if(character=='F')
                        meal[i]=index(character,fat,ref copy,dietlen);
                    else 
                        meal[i]=0;
                    if(meal[i] != -1)
                        break;
                    dietlen -=1;
                }
            }
            return meal;
            //throw new NotImplementedException();
        }

        private static int index(char letter,int []ingredient, ref List<int> copy,int len)
        {
            List<int> temp = new List<int>();
            int index=0,max,min;
            if(char.IsUpper(letter))
            {
                max = int.MinValue;
                foreach(var x in copy)
                {
                    if(ingredient[x]>max)
                    {
                        max = ingredient[x];
                        index = x;
                    }
                }

                foreach(var y in copy)
                {
                    if(ingredient[y] == max)
                        temp.Add(y);
                }
                copy = temp;
            }
            else
            {
                min = int.MaxValue;
                foreach(var x in copy)
                {
                    if(ingredient[x]<min)
                    {
                        min = ingredient[x];
                        index = x;
                    }
                }
                foreach(var y in copy)
                {
                    if(ingredient[y] == min)
                        temp.Add(y);
                }
                copy=temp;
            }
            if(copy.Count>1 && len != 1)
                return -1;
            return index;
        }
    }
}
