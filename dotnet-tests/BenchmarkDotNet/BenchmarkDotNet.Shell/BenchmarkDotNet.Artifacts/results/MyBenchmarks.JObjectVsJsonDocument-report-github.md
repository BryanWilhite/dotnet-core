``` ini

BenchmarkDotNet=v0.12.1, OS=ubuntu 20.04
AMD Ryzen 7 3750H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.103
  [Host]     : .NET Core 5.0.3 (CoreCLR 5.0.321.7203, CoreFX 5.0.321.7203), X64 RyuJIT
  Job-BYEDBO : .NET Core 5.0.3 (CoreCLR 5.0.321.7203, CoreFX 5.0.321.7203), X64 RyuJIT


```
|              Method |     Mean |     Error |    StdDev |
|-------------------- |---------:|----------:|----------:|
|           Serialize | 1.915 μs | 0.0382 μs | 0.0484 μs |
| SerializeNewtonsoft | 4.247 μs | 0.0821 μs | 0.0843 μs |
