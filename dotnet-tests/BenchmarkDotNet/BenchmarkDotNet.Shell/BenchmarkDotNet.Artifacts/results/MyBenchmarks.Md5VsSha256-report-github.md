``` ini

BenchmarkDotNet=v0.12.1, OS=ubuntu 20.04
AMD Ryzen 7 3750H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.103
  [Host]     : .NET Core 5.0.3 (CoreCLR 5.0.321.7203, CoreFX 5.0.321.7203), X64 RyuJIT
  Job-YJCLHD : .NET Core 5.0.3 (CoreCLR 5.0.321.7203, CoreFX 5.0.321.7203), X64 RyuJIT


```
| Method |      Mean |     Error |    StdDev |
|------- |----------:|----------:|----------:|
| Sha256 |  5.565 μs | 0.0173 μs | 0.0153 μs |
|    Md5 | 12.934 μs | 0.0293 μs | 0.0229 μs |
