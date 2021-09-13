``` ini

BenchmarkDotNet=v0.13.1, OS=ubuntu 20.04
AMD Ryzen 7 3750H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=5.0.400
  [Host]     : .NET 5.0.9 (5.0.921.35908), X64 RyuJIT DEBUG
  Job-MGVRSE : .NET 5.0.9 (5.0.921.35908), X64 RyuJIT


```
| Method |      Mean |     Error |    StdDev |
|------- |----------:|----------:|----------:|
| Sha256 |  5.582 μs | 0.0358 μs | 0.0279 μs |
|    Md5 | 13.661 μs | 0.2660 μs | 0.2846 μs |
