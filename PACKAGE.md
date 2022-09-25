# Niru

**Warning:** This is currently in-development vaporware.

**Niru** provides libraries and end-user tools for emulating foreign CPU
architectures and various other hardware components on any machine that .NET
runs on.

**Niru** dynamically translates blocks of guest architecture code to a generic
intermediate representation, optimizes the code, and then passes it off to an
execution module which either compiles the IR to host architecture code and runs
it, or interprets the IR directly if just-in-time compilation is not supported
on the host platform. As a result of this design, **Niru** can be easily ported
to new guest and host architectures.

This project offers the following packages:

* [niru](https://www.nuget.org/packages/niru): Provides the .NET global tool.
* [Vezel.Niru.Common](https://www.nuget.org/packages/Vezel.Niru.Common):
  Provides common functionality used by all Niru packages.
* [Vezel.Niru.Core](https://www.nuget.org/packages/Vezel.Niru.Core):
  Provides shared emulation functionality used by all guests and hosts.
* [Vezel.Niru.Guests.Riscv](https://www.nuget.org/packages/Vezel.Niru.Guests.Riscv):
  Provides emulation for RISC-V guests.
* [Vezel.Niru.Hosts.X64](https://www.nuget.org/packages/Vezel.Niru.Hosts.X64):
  Provides just-in-time compilation for x64 hosts.
* [Vezel.Niru.Hosts.Arm64](https://www.nuget.org/packages/Vezel.Niru.Hosts.Arm64):
  Provides just-in-time compilation for Arm64 hosts.
* [Vezel.Niru.Hosts.Cil](https://www.nuget.org/packages/Vezel.Niru.Hosts.Cil):
  Provides just-in-time compilation through the .NET runtime.
* [Vezel.Niru.Hosts.Interpreter](https://www.nuget.org/packages/Vezel.Niru.Hosts.Interpreter):
  Provides interpretation for hosts that disallow just-in-time compilation.

For more information, please visit the
[project home page](https://docs.vezel.dev/niru).
