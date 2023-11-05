namespace SimplexMethod
{
    public class SimplexSolver
    {
        private SimplexProblem problema;

        public SimplexSolver(SimplexProblem problem)
        {
            this.problema = problem;
        }

        public void Solve()
        {
            int numVariables = problema.GetNumVariables();
            int numConstraints = problema.GetNumConstraints();

            float[,] tableau = InitializeTableau(problema);

            while (!IsOptimal(tableau))
            {
                int colunaPivo = SelectPivotColumn(tableau);
                int linhaPivo = SelectPivotRow(tableau, colunaPivo);

                if (linhaPivo == -1)
                {
                    Console.WriteLine("O problema é ilimitado.");
                    return;
                }

                UpdateTableau(tableau, linhaPivo, colunaPivo);
            }

            float valorOtimo = tableau[0, tableau.GetLength(1) - 1];

            Console.WriteLine("Solução ótima encontrada:");
            Console.WriteLine($"Valor ótimo de Z: {valorOtimo:F2}");

            for (int i = 1; i <= numVariables; i++)
            {
                Console.WriteLine($"X{i}: {tableau[i, tableau.GetLength(1) - 1]:F2}");
            }
        }

        private float[,] InitializeTableau(SimplexProblem problem)
        {
            int numVariables = problem.GetNumVariables();
            int numConstraints = problem.GetNumConstraints();

            float[] objectiveFunctionCoefficients = problem.GetObjectiveFunctionCoefficients();
            float[,] constraintCoefficients = problem.GetConstraintCoefficients();
            float[] constraintLimits = problem.GetConstraintLimits();

            int tableauWidth = numVariables + numConstraints + 2;
            int tableauHeight = numConstraints + 1;

            float[,] tableau = new float[tableauHeight, tableauWidth];

            // Preenche a primeira linha com os coeficientes da função objetivo e uma coluna extra para resultados
            for (int i = 1; i <= numVariables; i++)
            {
                tableau[0, i] = -objectiveFunctionCoefficients[i - 1];
            }

            tableau[0, tableauWidth - 1] = 0; // Valor Z inicial é 0

            for (int i = 1; i <= numConstraints; i++)
            {
                for (int j = 1; j <= numVariables; j++)
                {
                    tableau[i, j] = constraintCoefficients[i - 1, j - 1];
                }

                tableau[i, numVariables + i] = 1; // Variáveis de folga
                tableau[i, tableauWidth - 1] = constraintLimits[i - 1];
            }

            return tableau;
        }

        private bool IsOptimal(float[,] tableau)
        {
            int tableauWidth = tableau.GetLength(1);

            for (int i = 1; i < tableauWidth - 1; i++)
            {
                if (tableau[0, i] < 0)
                {
                    return false;
                }
            }

            return true;
        }

        private int SelectPivotColumn(float[,] tableau)
        {
            int tableauWidth = tableau.GetLength(1);
            int pivotColumn = -1;
            float minValue = float.MaxValue;

            for (int i = 1; i < tableauWidth - 1; i++)
            {
                if (tableau[0, i] < minValue)
                {
                    minValue = tableau[0, i];
                    pivotColumn = i;
                }
            }

            return pivotColumn;
        }

        private int SelectPivotRow(float[,] tableau, int pivotColumn)
        {
            int tableauHeight = tableau.GetLength(0);
            int pivotRow = -1;
            float minRatio = float.MaxValue;

            for (int i = 1; i < tableauHeight; i++)
            {
                if (tableau[i, pivotColumn] > 0)
                {
                    float ratio = tableau[i, tableau.GetLength(1) - 1] / tableau[i, pivotColumn];
                    if (ratio < minRatio)
                    {
                        minRatio = ratio;
                        pivotRow = i;
                    }
                }
            }

            return pivotRow;
        }

        private void UpdateTableau(float[,] tableau, int pivotRow, int pivotColumn)
        {
            int tableauHeight = tableau.GetLength(0);
            int tableauWidth = tableau.GetLength(1);

            float pivotElement = tableau[pivotRow, pivotColumn];

            // Atualiza a linha do pivô
            for (int j = 1; j < tableauWidth; j++)
            {
                tableau[pivotRow, j] /= pivotElement;
            }

            // Atualiza as outras linhas
            for (int i = 0; i < tableauHeight; i++)
            {
                if (i != pivotRow)
                {
                    float ratio = tableau[i, pivotColumn];
                    for (int j = 1; j < tableauWidth; j++)
                    {
                        tableau[i, j] -= ratio * tableau[pivotRow, j];
                    }
                }
            }
        }

    }
}
