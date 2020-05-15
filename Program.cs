using System;
using System.Collections.Generic;

namespace SortingAlgorithms
{
    class Program
    {
        private struct ListWithInfo{
            public int[] list;
            public long steps;
            public long passes;
            public long swaps;
            
            public ListWithInfo(int[] list,long steps,long passes,long swaps){
                this.list = list;
                this.steps = steps;                    
                this.passes = passes;
                this.swaps = swaps;
            }

        }

        private static void Main(string[] args)
        {
            var unsortedList = GenerateRandomIntegers(5,0,1000); 
            
            PrintList(unsortedList);
            //Console.WriteLine("< Unsorted");

            var list2 = CocktailShakerSort(unsortedList);
            var listInfo1 = BubbleSort(unsortedList);

            
            System.Console.WriteLine("\tBubble Sort No Optimization:\n"+CreateResultString(listInfo1));

            
            System.Console.WriteLine("\tCocktailShaker Sort No Optimization:\n"+CreateResultString(list2));

            
            System.Console.WriteLine("\nPress Enter to Continue");
            Console.ReadLine();
            Console.Clear();
        }

        #region Bubble Sort             //https://en.wikipedia.org/wiki/Bubble_sort

        static ListWithInfo BubbleSort(int[] list)  //The default Implementation of BubbleSort
        {
            var len = list.Length;     
            long steps = 0;            
            long passes = 0;
            long swaps = 0;

            for(int i = 0; i<len; i++)
            {
                for(int j = 0; j<len-1; j++)
                {
                    if(list[j]>list[j+1])
                    {
                        list = Swap(list,j,j+1);
                        swaps++;
                    }                    
                    steps++;
                }
                passes++;
            }
            return new ListWithInfo(list,steps,passes,swaps);
        } 
        
        static ListWithInfo BubbleSort_Optimization1(int[] list) 
        //Outer loop is based on bool swapped.  unless no swaps occur, the loop will continue, 
        //when everything is sorted the final pass will occur resulting in swapped = false and outerloop will terminate  
        {
            var len = list.Length;     
            long steps = 0;            
            long passes = 0;
            long swaps = 0;

            #region Algorithm code        
            
            var swapped = true;   
            while(swapped)
            {
                swapped = false;
                for(int j = 0; j<len-1; j++)
                {                       
                    if(list[j]>list[j+1])
                    {
                        var t = list[j];
                        list[j] = list[j+1];
                        list[j+1] = t;
                        swapped = true;            
                        swaps++;
                    }   
                    steps++;                                         
                }
                passes++;                       
            }
            #endregion  
            return new ListWithInfo(list,steps,passes,swaps);
        }         
        static ListWithInfo BubbleSort_Optimization2(int[] list)                 
        //The outer loops is based on newLength. which is reduced by the number of itterations based on the number of elements 
        //that have reached their final position. In particular, after every pass, all elements after the last swap are sorted, 
        //and do not need to be checked again. 
        {
            var len = list.Length;     
            long steps = 0;            
            long passes = 0;
            long swaps = 0;
            
            
            while(len>=1)
            {
                var newLength = 0;
                for(int j = 0; j<len-1; j++)
                {                       
                    if(list[j]>list[j+1])
                    {
                        var t = list[j];
                        list[j] = list[j+1];
                        list[j+1] = t;            
                        newLength = j;
                        swaps++;
                    }   
                    steps++;                                         
                }
                 passes++;   
                 len = newLength;                    
            }

            return new ListWithInfo(list,steps,passes,swaps);
        }     
        #endregion

        static ListWithInfo CocktailShakerSort(int[] list)          //https://en.wikipedia.org/wiki/Cocktail_shaker_sort
        {
            var len = list.Length;
            long swaps = 0;
            long steps = 0;
            long passes = 0;

            var swapped = true;
            
            while(swapped)
            {
                swapped = false;
                //forward direction 
                for (var j = 0; j < len - 1; j++)
                {
                    if(list[j]>list[j+1])
                    {
                        list = Swap(list,j,j+1);
                        swaps++;
                        swapped = true;
                    }
                    steps++;
                    //Debug code
                    ////PrintList(list,true);
                    ////Console.WriteLine(" < Forward"+i);
                }

                if (!swapped) {break;}
                
                //reverse direction
                for (var j = len-1; j >0; j--)
                {
                    if(list[j]<list[j-1])
                    {
                        list = Swap(list,j,j-1);
                        swaps++;
                    }
                    steps++;
                    
                    //Debug code
                    ////PrintList(list,true);
                    ////Console.WriteLine(" < Backward"+i);
                }
                passes++;
            }
            return new ListWithInfo(list,steps,passes,swaps);
        }
        static int[] GenerateRandomIntegers(int length,int minValue,int maxValue)
        {
            var list = new List<int>();

            for(int i = 0; i <= length; i++)                    
                list.Add(new Random().Next(minValue,maxValue));

            return list.ToArray();
        }
        static void PrintList(ListWithInfo listInfo)
        {
            foreach(var x in listInfo.list)
                System.Console.WriteLine(x);          
        }
        static void PrintList(int[] list)
        {
            foreach(var x in list)
                System.Console.WriteLine(x);          
        }
        static void PrintList(int[] list,bool horizontal)
        {
            Console.Write("[ ");
            foreach(var x in list)
                System.Console.Write(x+", ");          
            Console.Write("]");
        }
        static string CreateResultString(ListWithInfo listInfo)
        {
            return $"Steps Taken:{listInfo.steps}  Passes:{listInfo.passes}  Swaps: {listInfo.swaps}";          
        }
        static int[] Swap(int[] list,int indexA,int indexB)
        {
            var temp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = temp;

            return list;               
        }
    }
}
