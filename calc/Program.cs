namespace Calculator
{
    class Calculator
    {
        static void Main()
        {
            Calculator calculator = new Calculator();
        }
        string parsedString;
        char[] actionsLow = new char[2] { '+', '-' };
        char[] actionsHigh = new char[2] { '*', '/' };
        List<int> indices = new List<int>();
        public Calculator()
        {
            string expression;
            string[] expressionArr;

            Console.WriteLine("Введите выражение:");

            expression = newInput();
            expressionArr = expression.Split(' ');
            parsedString = String.Join("", expressionArr);

            scanExpression();

            for (int i = 0; i < indices.Count; i++)
            {
                if (!actionsHigh.Any(parsedString.Contains)) continue;
                if (parsedString.IndexOf(actionsHigh[0]).Equals(indices[i]))
                {
                    scanExpression();
                    int[] indicesArr = findRange(actionsHigh[0]);
                    string subExpression = parsedString.Substring(indicesArr[0], indicesArr[1] - indicesArr[0] + 1);
                    parsedString = parsedString.Replace(subExpression, multiplication(subExpression));
                }
                if (parsedString.IndexOf(actionsHigh[1]).Equals(indices[i]))
                {
                    scanExpression();
                    int[] indicesArr = findRange(actionsHigh[1]);
                    string subExpression = parsedString.Substring(indicesArr[0], indicesArr[1] - indicesArr[0] + 1);
                    parsedString = parsedString.Replace(subExpression, division(subExpression));
                }
            }

            for (int i = 0; i < indices.Count; i++)
            {
                if (actionsHigh.Any(parsedString.Contains)) continue;
                if (parsedString.IndexOf(actionsLow[0]).Equals(indices[i]))
                {
                    scanExpression();
                    int[] indicesArr = findRange(actionsLow[0]);
                    string subExpression = parsedString.Substring(indicesArr[0], indicesArr[1] - indicesArr[0] + 1);
                    parsedString = parsedString.Replace(subExpression, addition(subExpression));
                }
                if (parsedString.IndexOf(actionsLow[1]).Equals(indices[i]))
                {
                    scanExpression();
                    int[] indicesArr = findRange(actionsLow[1]);
                    string subExpression = parsedString.Substring(indicesArr[0], indicesArr[1] - indicesArr[0] + 1);
                    parsedString = parsedString.Replace(subExpression, substraction(subExpression));
                }
            }

            Console.WriteLine($"Результат: {parsedString}");
        }
        public int[] findRange(char action)
        {
            int parsedIndex = parsedString.IndexOf(action);
            int mainIndex = indices.IndexOf(parsedIndex);
            int postIndex;
            int preIndex;

            if (mainIndex == 0) preIndex = 0;
            else preIndex = indices[mainIndex - 1] + 1;

            if (mainIndex == indices.Count - 1) postIndex = parsedString.Length - 1;
            else postIndex = indices[mainIndex + 1] - 1;

            int[] indicesArr = new int[2] { preIndex, postIndex };
            return indicesArr;
        }
        public void scanExpression()
        {
            indices.Clear();
            char[] parsedCharsArr = parsedString.ToCharArray();
            for (int i = 0; i < parsedString.Length; i++)
            {
                if (
                    actionsHigh.Any(parsedCharsArr[i].Equals) |
                    actionsLow.Any(parsedCharsArr[i].Equals)
                )
                {
                    indices.Add(i);
                }
            }
        }
        public string newInput()
        {
            string? readInput;
            do readInput = Console.ReadLine(); while (readInput == null);
            return readInput;
        }
        public float convertFloat(string str)
        {
            bool tryConvert = float.TryParse(str, out float number);
            if (!tryConvert) Console.WriteLine("Неверный тип данных, ожидаю float");
            return number;
        }
        public string multiplication(string expression)
        {
            string[] numbers = expression.Split(actionsHigh[0]);
            float resultNumber = convertFloat(numbers[0]) * convertFloat(numbers[1]);
            return resultNumber.ToString();
        }
        public string division(string expression)
        {
            string[] numbers = expression.Split(actionsHigh[1]);
            float resultNumber = convertFloat(numbers[0]) / convertFloat(numbers[1]);
            return resultNumber.ToString();
        }
        public string addition(string expression)
        {
            string[] numbers = expression.Split(actionsLow[0]);
            float resultNumber = convertFloat(numbers[0]) + convertFloat(numbers[1]);
            return resultNumber.ToString();
        }
        public string substraction(string expression)
        {
            string[] numbers = expression.Split(actionsLow[1]);
            float resultNumber = convertFloat(numbers[0]) - convertFloat(numbers[1]);
            return resultNumber.ToString();
        }
    }
}
