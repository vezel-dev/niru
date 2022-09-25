# Home

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
