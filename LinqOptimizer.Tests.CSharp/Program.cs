﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LinqOptimizer.Core;
using LinqOptimizer.CSharp;


namespace LinqOptimizer.Tests
{
    class Program
    {

        public static void Main(string[] args)
        {
            Random random = new Random();
            var nums = Enumerable.Range(1, 1000).Select(_ => random.Next(1, 1000)).Select(x => x).ToArray();
            //var keys = nums.ToArray();

            var r1 = nums.AsQueryExpr().SelectMany(x => nums.Select(i => i * i+ nums.Select(l => l).Count())).Sum().Run();
            var r2 = nums.AsQueryExpr().Select(x => nums.Select(i => i * i).Count() + 1).Sum().Run();
            var r3 = nums.AsQueryExpr().Select(x => nums.Select(i => i * i).Count()).Sum().Run();
            var r4 = nums.AsQueryExpr().Select(x => nums.Select(i => i * Enumerable.Range(1, 10).Count()).Count()).Sum().Run();
            
            //var nums = new double[] { 0.0, -12.0, 19.0, -6.0, -8.285714286, -14.0, 6.0, 17.0, 7.0 };

            //var r = nums.AsParallelQueryExpr().Select(x => x * 2).Sum().Run();
            //var _r = nums.AsQueryExpr().Select(x => x * 2).Sum().Run();

            //var t = nums.Select(x => x * 2).Sum();
            //var _t = nums.AsParallel().Select(x => x * 2).Sum();

            ////Measure(() => Array.Sort(nums));
            ////Measure(() => nums.AsQueryExpr().OrderBy(x => x).Run());
            ////Measure(() => nums.AsParallelQueryExpr().OrderBy(x => x).Run());
        }

        static void Measure(Action action)
        {
            var watch = new Stopwatch();
            watch.Start();
            action();
            Console.WriteLine(watch.Elapsed);
        }
    }
}