namespace SimplexMethod
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Projeto Programação Linear - Simplex Padrão");
            Console.WriteLine("-------------------------------------");


            Console.WriteLine("\nPressione Enter para continuar...");
            Console.ReadLine();
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Quantas variáveis: ");
            int tamVar = int.Parse(Console.ReadLine());

            Console.Write("Quantas restrições: ");
            int tamRest = int.Parse(Console.ReadLine());
            Console.Clear();

            SimplexProblem problem = new SimplexProblem(tamVar, tamRest);

            problem.GetVariableValuesFromUser();
            problem.PrintObjectiveFunction();
            problem.GetConstraintValuesFromUser();
            problem.PrintConstraints();

            SimplexSolver solver = new SimplexSolver(problem);
            solver.Solve();

            Console.ReadLine();
        }
    }
}