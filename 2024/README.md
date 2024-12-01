# Advent of Code 2024

Q: Why the language change? <br/>
A: Over the past year I've been doing a lot of work in dotnet as part of my job, and as I've learned dotnet and C# I've
come to really love the language.
It seems so much more intuitive than JS/TS, and it being compiled down to IL Code it's fast. Not as fast as Rust or C++,
but very, very close.

Q: What about the other languages? <br/>
A: 2023, which I didn't finish, was in TypeScript. It became difficult to work certain puzzles as TypeScript, while
strongly typed, is still JS and has some of the same issues.
With dotnet, I have access to things like LINQ, which is a godsend for some of these puzzles. That is my very
unprofessional opinion™️.

Q: How in the world do I run these? <br/>
A: Clone the repo, `cd` into the `2024/AoC2024` folder, and run
`dotnet run --project AoC2024\AoC2024.csproj -- 1 1 "C:\input.txt"`. You need dotnet 9.

Q: How does this even work? <br/>
A: I'm using `Spectre.Console` for the input. `Spectre.Console` is initialized with a default command
(`var app = new CommandApp<RunCommand>(registrar);`). When doing this, you don't have to pass a command name in.

The `Spectre.Console` `app` also takes in a TypeRegistrar, which is a special class implementing an Interface from
`Spectre.Console.Cli` which basically holds the IServiceCollection with all our dependencies. Internally Spectre calls
this
to create its own "registrar" of services to provide to commands.

Part of this DI container is our `SolutionResolver`. The `SolutionResolver` uses reflection to find the correct class
with an
`AdventSolutionAttribute` and the correct day and part parameters within the attribute. These classes _should_, if done
properly, implement `IAdventSolution`, which requires a `SolveAsync` method.

The `SolutionResolver` then calls the `SolutionFactory` to build the class with all necessary dependencies, then calls
that `SolveAsync` method via Reflection.

Is it stupidly complicated? Yes. Does it allow me to literally just add a class with that attribute and boom, it works?
Also yes. Doing it this way made it extremely easy to add new solutions as time goes on. 