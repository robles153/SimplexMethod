namespace SimplexMethod
{
    public class SimplexProblem
    {

        private int numVariables;
        private int numConstraints;
        private float[] objectiveFunctionCoefficients;
        private float[,] constraintCoefficients;
        private float[] constraintLimits;


        public SimplexProblem(int numVariables, int numConstraints)
        {
            this.numVariables = numVariables;
            this.numConstraints = numConstraints;
            objectiveFunctionCoefficients = new float[numVariables];
            constraintCoefficients = new float[numConstraints, numVariables];
            constraintLimits = new float[numConstraints];
        }


        public void GetVariableValuesFromUser()
        {
            Console.WriteLine("Informe o valor de cada variável:");
            for (int i = 0; i < numVariables; i++)
            {
                Console.Write($"X{i + 1}: ");
                objectiveFunctionCoefficients[i] = float.Parse(Console.ReadLine());

                if (objectiveFunctionCoefficients[i] < 0)
                {
                    while (objectiveFunctionCoefficients[i] < 0)
                    {
                        Console.Write($"Informe um valor positivo de X{i + 1}: ");
                        objectiveFunctionCoefficients[i] = float.Parse(Console.ReadLine());
                    }
                }
            }
        }

        public void PrintObjectiveFunction()
        {
            Console.Write("FoMax(Z) = ");
            for (int i = 0; i < numVariables; i++)
            {
                Console.Write($"{objectiveFunctionCoefficients[i]}X{i + 1}");

                if (i + 1 < numVariables)
                {
                    Console.Write(" + ");
                }
            }

            Console.WriteLine();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Pressione Enter para continuar...");
            Console.ReadLine();
            Console.Clear();
        }

        public void GetConstraintValuesFromUser()
        {
            Console.WriteLine("Informe as restrições:");

            for (int i = 0; i < numConstraints; i++)
            {
                Console.WriteLine($"Restrição {i + 1}");
                Console.Write("Tipo de restrição (digite '+' ou '-'): ");
                string constraintType = Console.ReadLine();

                if (constraintType == "+")
                {
                    Console.Write("Quantidade de X1: ");
                    if (float.TryParse(Console.ReadLine(), out float x1))
                    {
                        constraintCoefficients[i, 0] = x1;
                    }
                    else
                    {
                        Console.WriteLine("Valor inválido para X1. Tente novamente.");
                        i--;
                        continue;
                    }

                    Console.Write("Quantidade de X2: ");
                    if (float.TryParse(Console.ReadLine(), out float x2))
                    {
                        constraintCoefficients[i, 1] = x2;
                    }
                    else
                    {
                        Console.WriteLine("Valor inválido para X2. Tente novamente.");
                        i--;
                        continue;
                    }
                }
                else if (constraintType == "-")
                {
                    Console.Write("Quantidade de X1: ");
                    if (float.TryParse(Console.ReadLine(), out float x1))
                    {
                        constraintCoefficients[i, 0] = x1;
                    }
                    else
                    {
                        Console.WriteLine("Valor inválido para X1. Tente novamente.");
                        i--;
                        continue;
                    }

                    Console.Write("Quantidade de X2: ");
                    if (float.TryParse(Console.ReadLine(), out float x2))
                    {
                        constraintCoefficients[i, 1] = -x2; // Coeficiente negativo para X2
                    }
                    else
                    {
                        Console.WriteLine("Valor inválido para X2. Tente novamente.");
                        i--;
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Tipo de restrição inválido. Use '+' ou '-'.");
                    i--;
                    continue;
                }

                Console.Write("Informe o limite do recurso ");
                if (float.TryParse(Console.ReadLine(), out float limit))
                {
                    constraintLimits[i] = limit;
                }
                else
                {
                    Console.WriteLine("Valor inválido para o limite. Tente novamente.");
                    i--;
                }
            }

            Console.Clear();
        }


        public void PrintConstraints()
        {
            Console.WriteLine($"FoMax(Z) = {ObjectiveFunctionToString()}");
            Console.WriteLine("Sujeito a");
            Console.WriteLine("---------------------------------------");

            for (int i = 0; i < numConstraints; i++)
            {
                string constraintType = constraintCoefficients[i, 1] >= 0 ? "+" : "-";
                float x2Coefficient = Math.Abs(constraintCoefficients[i, 1]); // Valor absoluto para X2

                Console.WriteLine($"R{i + 1}: {constraintCoefficients[i, 0]}X1 {constraintType} {x2Coefficient}X2 <= {constraintLimits[i]}");
            }

            Console.WriteLine();

            for (int i = 0; i < numVariables; i++)
            {
                Console.WriteLine($"X{i + 1} >= 0");
            }

            Console.WriteLine("\nPressione Enter para continuar...");
            Console.ReadLine();
            Console.Clear();
        }


        private string ObjectiveFunctionToString()
        {
            string foMax = "";
            for (int i = 0; i < numVariables; i++)
            {
                foMax += $"{objectiveFunctionCoefficients[i]}X{i + 1}";

                if (i + 1 < numVariables)
                {
                    foMax += " + ";
                }
            }
            return foMax;
        }

        private string ConstraintToString(int index)
        {
            string concat = "";
            for (int j = 0; j < numVariables; j++)
            {
                concat += $"{constraintCoefficients[index, j]}X{j + 1}";
                if (j + 1 == numVariables)
                {
                    concat += $" <= {constraintLimits[index]}";
                }
                else
                {
                    concat += " + ";
                }
            }
            return concat;
        }

        public int GetNumVariables()
        {
            return numVariables;
        }

        public int GetNumConstraints()
        {
            return numConstraints;
        }

        public float[] GetObjectiveFunctionCoefficients()
        {
            return objectiveFunctionCoefficients;
        }

        public float[,] GetConstraintCoefficients()
        {
            return constraintCoefficients;
        }

        public float[] GetConstraintLimits()
        {
            return constraintLimits;
        }
    }
}
