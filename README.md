LinqOptimizer
=============
An automatic query optimizer for LINQ to Objects and PLINQ. 
LinqOptimizer compiles declarative LINQ queries into fast loop-based imperative code.
The compiled code has fewer virtual calls, better data locality and speedups of up to 15x.

Optimizations
-----------------------
* Lambda inlining
* Loop fusion
* Nested loop generation
* Specialized strategies and algorithms

The expression
```csharp
var query = (from num in nums.AsQueryExpr()
             where num % 2 == 0
             select num * num).Sum();
```
will compile to
```csharp
int sum = 0;
for (int index = 0; index < nums.Length; index++)
{
   int num = nums[index];
   if (num % 2 == 0)
      sum += num * num;
}
```
and for the parallel case
```csharp
var query = (from num in nums.AsParallelQueryExpr()
             where num % 2 == 0
             select num * num).Sum();
```
will compile to a reduce-combine style straregy
```csharp
Parallel.ReduceCombine(nums, 0, 
                          (acc, num) => { 
                                       if (num % 2 == 0)  
                                         return acc + num * num; 
                                       else
                                         return acc; 
                          }, (left, right) => left + right);
```

Future work
-----------
* Many missing operators
* New specialized operators 
* Even more optimizations
* GPU backend
* DistributedLinq (Combining LinqOptimizer with MBrace)

References
----------
LinqOptimizer draws heavy inspiration from 
* [Steno](http://research.microsoft.com/pubs/173946/paper-pldi.pdf)
* [Clojure - reducers](http://clojure.org/reducers)