``` ini

BenchmarkDotNet=v0.12.1, OS=ubuntu 20.04
AMD Ryzen 7 3750H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.103
  [Host]     : .NET Core 5.0.3 (CoreCLR 5.0.321.7203, CoreFX 5.0.321.7203), X64 RyuJIT
  Job-HKFUMJ : .NET Core 5.0.3 (CoreCLR 5.0.321.7203, CoreFX 5.0.321.7203), X64 RyuJIT


```
|              Method |     Mean |     Error |    StdDev |
|-------------------- |---------:|----------:|----------:|
|               Parse | 4.330 μs | 0.0753 μs | 0.0704 μs |
|     ParseNewtonsoft | 9.742 μs | 0.1905 μs | 0.2732 μs |
|           Serialize | 1.859 μs | 0.0360 μs | 0.0319 μs |
| SerializeNewtonsoft | 4.396 μs | 0.0869 μs | 0.1522 μs |
