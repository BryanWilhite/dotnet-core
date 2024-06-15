```

BenchmarkDotNet v0.13.12, Ubuntu 22.04.4 LTS (Jammy Jellyfish)
AMD Ryzen 7 3750H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.301
  [Host]     : .NET 8.0.6 (8.0.624.26715), X64 RyuJIT AVX2 DEBUG
  Job-EAGYUW : .NET 8.0.6 (8.0.624.26715), X64 RyuJIT AVX2


```
| Method | Mean      | Error     | StdDev    |
|------- |----------:|----------:|----------:|
| Sha256 |  6.753 μs | 0.1318 μs | 0.2165 μs |
| Md5    | 15.611 μs | 0.3120 μs | 0.7534 μs |
